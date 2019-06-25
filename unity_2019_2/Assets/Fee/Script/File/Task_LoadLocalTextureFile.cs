

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。タスク。
*/


//Async block lacks await operator and will run synchronously.
#pragma warning disable 1998


/** Fee.File
*/
namespace Fee.File
{
	/** ロードローカル。テクスチャファイル。
	*/
	public class Task_LoadLocalTextureFile
	{
		/** ResultType
		*/
		public struct ResultType
		{
			public byte[] binary;
			public string errorstring;
		}

		/** TaskMain
		*/
		private static async System.Threading.Tasks.Task<ResultType> TaskMain(Fee.File.OnFileTask_CallBackInterface a_callback_interface,Path a_path,System.Threading.CancellationToken a_cancel)
		{
			ResultType t_ret;
			{
				t_ret.binary = null;
				t_ret.errorstring = null;
			}

			System.IO.FileStream t_filestream = null;

			try{
				//ファイルパス。
				System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_path.GetPath());

				//開く。
				t_filestream = t_fileinfo.OpenRead();

				//読み込み。
				if(t_filestream != null){
					t_ret.binary = new byte[t_filestream.Length];
					if(Config.USE_ASYNC == true){
						await t_filestream.ReadAsync(t_ret.binary,0,t_ret.binary.Length,a_cancel);
						await t_filestream.FlushAsync(a_cancel);
					}else{
						t_filestream.Read(t_ret.binary,0,t_ret.binary.Length);
					}
				}			
			}catch(System.Exception t_exception){
				t_ret.binary = null;
				t_ret.errorstring = "Task_LoadLocalTextureFile : " + t_exception.Message;
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			if(a_cancel.IsCancellationRequested == true){
				t_ret.binary = null;
				t_ret.errorstring = "Task_LoadLocalTextureFile : Cancel";

				a_cancel.ThrowIfCancellationRequested();
			}

			if(t_ret.binary == null){
				if(t_ret.errorstring == null){
					t_ret.errorstring = "Task_LoadLocalTextureFile : null";
				}
			}

			return t_ret;
		}

		/** 実行。
		*/
		public static Fee.TaskW.Task<ResultType> Run(Fee.File.OnFileTask_CallBackInterface a_callback_interface,Path a_path,Fee.TaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new Fee.TaskW.Task<ResultType>(() => {
				return Task_LoadLocalTextureFile.TaskMain(a_callback_interface,a_path,t_cancel_token);
			});
		}
	}
}

