

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。コルーチン。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** セーブローカル。バイナリファイル。
	*/
	public class Coroutine_SaveLocalBinaryFile : Fee.File.OnFileTask_CallBackInterface
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** セーブ完了。
			*/
			public bool saveend;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//saveend
				this.saveend = false;

				//errorstring
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** taskprogress
		*/
		public float taskprogress;

		/** [Fee.File.OnFileTask_CallBackInterface]タスク実行中。
		*/
		public void OnFileTask(float a_progress)
		{
			this.taskprogress = a_progress;
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path,byte[] a_binary)
		{
			//result
			this.result = new ResultType();

			//taskprogress_
			this.taskprogress = 0.0f;

			//キャンセルトークン。
			Fee.TaskW.CancelToken t_cancel_token = new Fee.TaskW.CancelToken();

			//result
			Task_SaveLocalBinaryFile.ResultType t_result;

			//タスク起動。
			using(Fee.TaskW.Task<Task_SaveLocalBinaryFile.ResultType> t_task = Task_SaveLocalBinaryFile.Run(this,a_path,a_binary,t_cancel_token)){

				//終了待ち。
				do{
					//キャンセル。
					if(a_callback_interface != null){
						if(a_callback_interface.OnFileCoroutine(this.taskprogress) == false){
							t_cancel_token.Cancel();
						}
					}
					yield return null;
				}while(t_task.IsEnd() == false);

				//結果。
				t_result = t_task.GetResult();

				//成功。
				if(t_task.IsSuccess() == true){
					if(t_result.saveend == true){
						this.result.saveend = true;
						yield break;
					}
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "Coroutine_SaveLocalBinaryFile : null";
				yield break;
			}
		}
	}
}

