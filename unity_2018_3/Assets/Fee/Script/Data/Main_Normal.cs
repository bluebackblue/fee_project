

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。普通。
*/


/** Fee.Data
*/
namespace Fee.Data
{
	/** Main_Normal
	*/
	public class Main_Normal : OnCoroutine_CallBack
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ノーマル
			*/
			Normal,
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
			SaveEnd,

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

		/** request_listitem
		*/
		private ListItem request_listitem;

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
		/*
		private byte[] result_binary;
		*/

		/** result_text
		*/
		/*
		private string result_text;
		*/

		/** result_texture
		*/
		/*
		private UnityEngine.Texture2D result_texture;
		*/

		/** constructor
		*/
		public Main_Normal()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_type = RequestType.None;
			this.request_listitem = null;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			//this.result_binary = null;
			//this.result_text = null;
			//this.result_texture = null;
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
		/*
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}
		*/

		/** GetResultText
		*/
		/*
		public string GetResultText()
		{
			return this.result_text;
		}
		*/

		/** GetResultTexture
		*/
		/*
		public UnityEngine.Texture2D GetResultTexture()
		{
			return this.result_texture;
		}
		*/

		/** [Fee.Data.OnCoroutine_CallBack]コルーチンからのコールバック。

			return == false : キャンセル。

		*/
		public bool OnCoroutine(float a_progress)
		{
			if((this.is_cancel == true)||(this.is_shutdown == true)){
				return false;
			}

			this.result_progress = a_progress;
			return true;
		}

		/** リクエスト。ノーマル。
		*/
		public bool RequestNormal(ListItem a_listitem)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				//this.result_binary = null;
				//this.result_text = null;
				//this.result_texture = null;

				//request
				this.request_type = RequestType.Normal;
				this.request_listitem = a_listitem;

				Function.Function.StartCoroutine(this.DoNormal());
				return true;
			}

			return false;
		}

		/** 実行。ノーマル。
		*/
		private System.Collections.IEnumerator DoNormal()
		{
			Tool.Assert(this.request_type == RequestType.Normal);

			Coroutine_Normal t_coroutine = new Coroutine_Normal();
			yield return t_coroutine.CoroutineMain(this,this.request_listitem);

			/*
			if(t_coroutine.result.binary_file != null){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_binary = t_coroutine.result.binary_file;
				this.result_type = ResultType.Binary;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
			*/

			yield break;
		}
	}
}

