

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
	/** ロードローカル。テクスチャファイル。
	*/
	public class Coroutine_LoadLocalTextureFile : Fee.File.OnFileTask_CallBackInterface
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** テクスチャファイル。
			*/
			public UnityEngine.Texture2D texture_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//texture_file
				this.texture_file = null;

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
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path)
		{
			//result
			this.result = new ResultType();

			//taskprogress_
			this.taskprogress = 0.0f;

			//キャンセルトークン。
			Fee.TaskW.CancelToken t_cancel_token = new Fee.TaskW.CancelToken();

			//result
			Task_LoadLocalTextureFile.ResultType t_result;

			//タスク起動。
			using(Fee.TaskW.Task<Task_LoadLocalTextureFile.ResultType> t_task = Task_LoadLocalTextureFile.Run(this,a_path,t_cancel_token)){

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
					if(t_result.binary != null){

						//コンバート。
						UnityEngine.Texture2D t_result_texture = null;

						try{
							t_result_texture = BinaryToTexture2D.Convert(t_result.binary);
						}catch(System.Exception t_exception){
							this.result.errorstring = "Coroutine_LoadLocalTextureFile : " + t_exception.Message;
							yield break;
						}

						if(t_result_texture != null){
							this.result.texture_file = t_result_texture;
							yield break;
						}else{
							this.result.errorstring = "Coroutine_LoadLocalTextureFile : result_texture == null";
							yield break;
						}
					}
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "Coroutine_LoadLocalTextureFile : null";
				yield break;
			}
		}
	}
}
