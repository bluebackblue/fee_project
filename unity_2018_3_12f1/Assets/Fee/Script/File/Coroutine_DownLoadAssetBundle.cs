

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。コルーチン。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** ダウンロード。アセットバンドル。
	*/
	public class Coroutine_DownLoadAssetBundle
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** アセットバンドル
			*/
			public UnityEngine.AssetBundle assetbundle;

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
				this.assetbundle = null;
				this.errorstring = null;
				this.responseheader = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CreateWebRequestInstance
		*/
		private static UnityEngine.Networking.UnityWebRequest CreateWebRequestInstance(Fee.File.Path a_path,UnityEngine.WWWForm a_post_data,long a_assetbundle_id,uint a_data_version,uint a_data_crc)
		{
			//TODO:a_post_data

			return UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(a_path.GetPath(),a_data_version,a_data_crc);
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,Fee.File.Path a_path,UnityEngine.WWWForm a_post_data,long a_assetbundle_id,uint a_data_version,uint a_data_crc)
		{
			//result
			this.result = new ResultType();

			//キャッシュからの読み込み。
			if(Fee.File.File.GetInstance() != null){
				UnityEngine.AssetBundle t_assetbundle = Fee.File.File.GetInstance().GetAssetBundleList().GetAssetBundle(a_assetbundle_id);
				if(t_assetbundle != null){
					//成功。
					this.result.assetbundle = t_assetbundle;
					yield break;
				}
			}

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = CreateWebRequestInstance(a_path,a_post_data,a_assetbundle_id,a_data_version,a_data_crc)){
				UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;
				if(t_webrequest != null){
					t_webrequest_async = t_webrequest.SendWebRequest();
					if(t_webrequest_async == null){
						this.result.errorstring = "Coroutine_DownLoadAssetBundle : webrequest_async == null";
						yield break;
					}
				}else{
					this.result.errorstring =  "Coroutine_DownLoadAssetBundle : webrequest == null";
					yield break;
				}

				while(true){
					//エラーチェック。
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.result.errorstring = "Coroutine_DownLoadAssetBundle : " + t_webrequest.error + " : " + a_path.GetPath();
						yield break;
					}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
						//正常終了。
						break;
					}

					//キャンセル。
					if(a_instance != null){
						if(a_instance.OnCoroutine(t_webrequest.uploadProgress,t_webrequest.downloadProgress) == false){
							t_webrequest.Abort();
						}
					}

					yield return null;
				}

				if(t_webrequest_async != null){
					yield return t_webrequest_async;
				}

				//コンバート。
				UnityEngine.AssetBundle t_result = null;
				try{
					//レスポンスヘッダー。
					this.result.responseheader = t_webrequest.GetResponseHeaders();

					t_result = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(t_webrequest);
				}catch(System.Exception t_exception){
					this.result.errorstring = "Coroutine_DownLoadAssetBundle : " + t_exception.Message;
					yield break;
				}

				//成功。
				if(t_result != null){

					//キャッシュに登録。
					if(Fee.File.File.GetInstance() != null){
						Fee.File.File.GetInstance().GetAssetBundleList().Regist(a_assetbundle_id,t_result);
					}

					this.result.assetbundle = t_result;
					yield break;
				}

				//失敗。
				this.result.errorstring = "Coroutine_DownLoadAssetBundle : null";
				yield break;
			}
		}
	}
}
