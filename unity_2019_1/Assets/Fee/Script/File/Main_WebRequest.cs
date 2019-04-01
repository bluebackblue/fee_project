

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。ＷＷＷ。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** Main_WebRequest
	*/
	public class Main_WebRequest : OnCoroutine_CallBack
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ダウンロード。バイナリファイル。
			*/
			DownLoadBinaryFile,

			/** ダウンロード。テキストファイル。
			*/
			DownLoadTextFile,

			/** ダウンロード。テクスチャファイル。
			*/
			DownLoadTextureFile,

			/** ダウンロード。アセットバンドル。
			*/
			DownLoadAssetBundle,

			/** ロードストリーミングアセット。バイナリファイル。
			*/
			LoadStreamingAssetsBinaryFile,
		};

		/** ResultType
		*/
		public enum ResultType
		{
			/** 未定義。
			*/
			None,

			/** エラー。
			*/
			Error,

			/** セーブ完了。
			*/
			//SaveEnd,

			/** バイナリー。
			*/
			Binary,

			/** テキスト。
			*/
			Text,

			/** テクスチャ。
			*/
			Texture,

			/** アセットバンドル。
			*/
			AssetBundle,
		};

		/** is_busy
		*/
		private bool is_busy;

		/** キャンセル。チェック。
		*/
		private bool is_cancel;

		/** シャットダウン。チェック。
		*/
		private bool is_shutdown;

		/** request_type
		*/
		private RequestType request_type;

		/** request_progress_mode
		*/
		private ProgressMode request_progress_mode;

		/** request_post_data
		*/
		private UnityEngine.WWWForm request_post_data;

		/** request_path
		*/
		private Path request_path;

		/** request_assetbundle_id
		*/
		private long request_assetbundle_id;

		/** request_data_version
		*/
		private uint request_data_version;

		/** request_data_crc
		*/
		private uint request_data_crc;

		/** result_progress 
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_binary
		*/
		private byte[] result_binary;

		/** result_text
		*/
		private string result_text;

		/** result_texture
		*/
		private UnityEngine.Texture2D result_texture;

		/** result_assetbundle
		*/
		private UnityEngine.AssetBundle result_assetbundle;

		/** result_responseheader
		*/
		private System.Collections.Generic.Dictionary<string,string> result_responseheader;

		/** constructor
		*/
		public Main_WebRequest()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_type = RequestType.None;
			this.request_progress_mode = ProgressMode.None;
			this.request_post_data = null;
			this.request_path = null;
			this.request_assetbundle_id = 0;
			this.request_data_version = 0;
			this.request_data_crc = 0;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_binary = null;
			this.result_text = null;
			this.result_texture = null;
			this.result_assetbundle = null;
			this.result_responseheader = null;
		}
		
		/** 削除。
		*/
		public void Delete()
		{
			this.is_shutdown = true;
		}

		/** キャンセル。
		*/
		public void Cancel()
		{
			this.is_cancel = true;
		}

		/** 完了。
		*/
		public void Fix()
		{
			this.is_busy = false;
		}

		/** GetResultProgress
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** GetResultErrorString
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** GetResultType
		*/
		public ResultType GetResultType()
		{
			return this.result_type;
		}

		/** GetResultBinary
		*/
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}

		/** GetResultText
		*/
		public string GetResultText()
		{
			return this.result_text;
		}

		/** GetResultTexture
		*/
		public UnityEngine.Texture2D GetResultTexture()
		{
			return this.result_texture;
		}

		/** GetResultAssetBundle
		*/
		public UnityEngine.AssetBundle GetResultAssetBundle()
		{
			return this.result_assetbundle;
		}

		/** GetResultResponseHeader
		*/
		public System.Collections.Generic.Dictionary<string,string> GetResultResponseHeader()
		{
			return this.result_responseheader;
		}

		/** [Fee.File.OnCoroutine_CallBack]コルーチンからのコールバック。

		戻り値 == false : キャンセル。

		*/
		public bool OnCoroutine(float a_progress)
		{
			if((this.is_cancel == true)||(this.is_shutdown == true)){
				return false;
			}

			this.result_progress = a_progress;
			return true;
		}

		/** リクエスト。ダウンロード。バイナリファイル。
		*/
		public bool RequestDownLoadBinaryFile(Path a_path,UnityEngine.WWWForm a_post_data,ProgressMode a_progress_mode)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_type = RequestType.DownLoadBinaryFile;
				this.request_progress_mode = a_progress_mode;
				this.request_post_data = a_post_data;
				this.request_path = a_path;
				this.request_assetbundle_id = 0;
				this.request_data_version = 0;
				this.request_data_crc = 0;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;
				this.result_responseheader = null;

				Function.Function.StartCoroutine(this.DoDownLoadBinaryFile());
				return true;
			}

			return false;
		}

		/** 実行。ダウンロード。バイナリファイル。
		*/
		private System.Collections.IEnumerator DoDownLoadBinaryFile()
		{
			Tool.Assert(this.request_type == RequestType.DownLoadBinaryFile);

			Coroutine_DownLoadBinaryFile t_coroutine = new Coroutine_DownLoadBinaryFile();
			yield return t_coroutine.CoroutineMain(this,this.request_path,this.request_post_data,this.request_progress_mode);

			if(t_coroutine.result.binary != null){
				this.result_progress = 1.0f;
				this.result_binary = t_coroutine.result.binary;
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.Binary;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ダウンロード。テキストファイル。
		*/
		public bool RequestDownLoadTextFile(Path a_path,UnityEngine.WWWForm a_post_data,ProgressMode a_progress_mode)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_type = RequestType.DownLoadTextFile;
				this.request_progress_mode = a_progress_mode;
				this.request_post_data = a_post_data;
				this.request_path = a_path;
				this.request_assetbundle_id = 0;
				this.request_data_version = 0;
				this.request_data_crc = 0;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				Function.Function.StartCoroutine(this.DoDownLoadTextFile());
				return true;
			}

			return false;
		}

		/** 実行。ダウンロード。テキストファイル。
		*/
		private System.Collections.IEnumerator DoDownLoadTextFile()
		{
			Tool.Assert(this.request_type == RequestType.DownLoadTextFile);

			Coroutine_DownLoadTextFile t_coroutine = new Coroutine_DownLoadTextFile();
			yield return t_coroutine.CoroutineMain(this,this.request_path,this.request_post_data,this.request_progress_mode);

			if(t_coroutine.result.text != null){
				this.result_progress = 1.0f;
				this.result_text = t_coroutine.result.text;
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.Text;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ダウンロード。テクスチャーファイル。
		*/
		public bool RequestDownLoadTextureFile(Path a_path,UnityEngine.WWWForm a_post_data,ProgressMode a_progress_mode)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_type = RequestType.DownLoadTextureFile;
				this.request_progress_mode = a_progress_mode;
				this.request_post_data = a_post_data;
				this.request_path = a_path;
				this.request_assetbundle_id = 0;
				this.request_data_version = 0;
				this.request_data_crc = 0;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				Function.Function.StartCoroutine(this.DoDownLoadTextureFile());
				return true;
			}

			return false;
		}

		/** 実行。ダウンロード。テクスチャーファイル。
		*/
		private System.Collections.IEnumerator DoDownLoadTextureFile()
		{
			Tool.Assert(this.request_type == RequestType.DownLoadTextureFile);

			Coroutine_DownLoadTextureFile t_coroutine = new Coroutine_DownLoadTextureFile();
			yield return t_coroutine.CoroutineMain(this,this.request_path);

			if(t_coroutine.result.texture != null){
				this.result_progress = 1.0f;
				this.result_texture = t_coroutine.result.texture;
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.Texture;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ダウンロード。アセットバンドル。
		*/
		public bool RequestDownLoadAssetBundle(Path a_path,UnityEngine.WWWForm a_post_data,ProgressMode a_progress_mode,long a_assetbundle_id,uint a_data_version)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_type = RequestType.DownLoadAssetBundle;
				this.request_progress_mode = a_progress_mode;
				this.request_post_data = a_post_data;
				this.request_path = a_path;
				this.request_assetbundle_id = a_assetbundle_id;
				this.request_data_version = a_data_version;
				this.request_data_crc = 0;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				Function.Function.StartCoroutine(this.DoDownLoadAssetBundle());
				return true;
			}

			return false;
		}

		/** 実行。ダウンロード。アセットバンドル。
		*/
		private System.Collections.IEnumerator DoDownLoadAssetBundle()
		{
			Tool.Assert(this.request_type == RequestType.DownLoadAssetBundle);

			Coroutine_DownLoadAssetBundle t_coroutine = new Coroutine_DownLoadAssetBundle();
			yield return t_coroutine.CoroutineMain(this,this.request_path,request_assetbundle_id,this.request_data_version,this.request_data_crc);

			if(t_coroutine.result.assetbundle != null){
				this.result_progress = 1.0f;
				this.result_assetbundle = t_coroutine.result.assetbundle;
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.AssetBundle;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードストリーミングアセット。バイナリファイル。
		*/
		public bool RequestLoadStreamingAssetsBinaryFile(Path a_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_type = RequestType.LoadStreamingAssetsBinaryFile;
				this.request_progress_mode = ProgressMode.None;
				this.request_post_data = null;
				this.request_path = a_path;
				this.request_assetbundle_id = 0;
				this.request_data_version = 0;
				this.request_data_crc = 0;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				Function.Function.StartCoroutine(this.DoLoadStreamingAssetsBinaryFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードストリーミングアセット。バイナリファイル。
		*/
		private System.Collections.IEnumerator DoLoadStreamingAssetsBinaryFile()
		{
			Tool.Assert(this.request_type == RequestType.DownLoadBinaryFile);

			//request_pathは相対パス。
			Fee.File.Path t_path = new Path(UnityEngine.Application.streamingAssetsPath + "/",this.request_path.GetPath());

			Coroutine_DownLoadBinaryFile t_coroutine = new Coroutine_DownLoadBinaryFile();
			yield return t_coroutine.CoroutineMain(this,t_path,null,ProgressMode.DownLoad);

			if(t_coroutine.result.binary != null){
				this.result_progress = 1.0f;
				this.result_binary = t_coroutine.result.binary;
				this.result_responseheader = t_coroutine.result.responseheader;	//TODO
				this.result_type = ResultType.Binary;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}
	}
}
