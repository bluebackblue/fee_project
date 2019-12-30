

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
	/** ロードＵＲＬ。バイナリファイル。
	*/
	public class Coroutine_LoadUrlBinaryFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** バイナリファイル。
			*/
			public byte[] binary_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** レスポンスヘッダー。
			*/
			public System.Collections.Generic.Dictionary<string,string> responseheader;

			/** constructor
			*/
			public ResultType()
			{
				//binary_file
				this.binary_file = null;

				//errorstring
				this.errorstring = null;

				//responseheader
				this.responseheader = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CreateWebRequestInstance
		*/
		private static UnityEngine.Networking.UnityWebRequest CreateWebRequestInstance(Fee.File.Path a_path,UnityEngine.WWWForm a_post_data,UnityEngine.Networking.CertificateHandler a_certificate_handler)
		{
			UnityEngine.Networking.UnityWebRequest t_webrequest = null;

			if(a_post_data != null){
				t_webrequest = UnityEngine.Networking.UnityWebRequest.Post(a_path.GetPath(),a_post_data);
			}else{
				t_webrequest = UnityEngine.Networking.UnityWebRequest.Get(a_path.GetPath());
			}

			if(a_certificate_handler != null){
				t_webrequest.certificateHandler = a_certificate_handler;
			}

			return t_webrequest;
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path,UnityEngine.WWWForm a_post_data,UnityEngine.Networking.CertificateHandler a_certificate_handler)
		{
			//result
			this.result = new ResultType();

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = CreateWebRequestInstance(a_path,a_post_data,a_certificate_handler)){

				#if(UNITY_5)
				UnityEngine.AsyncOperation t_webrequest_async = null;
				#else
				UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;
				#endif

				if(t_webrequest != null){

					#if(UNITY_5)
					t_webrequest_async = t_webrequest.Send();
					#else
					t_webrequest_async = t_webrequest.SendWebRequest();
					#endif

					if(t_webrequest_async == null){
						this.result.errorstring = "Coroutine_LoadUrlBinaryFile : webrequest_async == null";
						yield break;
					}
				}else{
					this.result.errorstring = "Coroutine_LoadUrlBinaryFile : webrequest == null";
					yield break;
				}

				do{
					//エラーチェック。

					#if(UNITY_5)
					if(t_webrequest.isError == true){
						//エラー終了。
					}else if(t_webrequest.isDone == true){
						//正常終了。
						break;
					}
					#else
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.result.errorstring = "Coroutine_LoadUrlBinaryFile : webrequest : " + t_webrequest.error;
						yield break;
					}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
						//正常終了。
						break;
					}
					#endif

					//キャンセル。
					if(a_callback_interface != null){
						float t_progress = (t_webrequest.uploadProgress + t_webrequest.downloadProgress) / 2;
						if(a_callback_interface.OnFileCoroutine(t_progress) == false){
							t_webrequest.Abort();
						}
					}

					yield return null;
				}while(true);

				if(t_webrequest_async != null){
					yield return t_webrequest_async;
				}

				//コンバート。
				byte[] t_result = null;
				try{
					//レスポンスヘッダー。
					this.result.responseheader = t_webrequest.GetResponseHeaders();

					if(t_webrequest.downloadHandler != null){
						t_result = t_webrequest.downloadHandler.data;
					}else{
						this.result.errorstring = "Coroutine_LoadUrlBinaryFile : downloadHandler == null";
						yield break;
					}
				}catch(System.Exception t_exception){
					this.result.errorstring = "Coroutine_LoadUrlBinaryFile : " + t_exception.Message;
					yield break;
				}

				//成功。
				if(t_result != null){
					this.result.binary_file = t_result;
					yield break;
				}

				//失敗。
				this.result.errorstring = "Coroutine_LoadUrlBinaryFile : null";
				yield break;
			}
		}
	}
}
