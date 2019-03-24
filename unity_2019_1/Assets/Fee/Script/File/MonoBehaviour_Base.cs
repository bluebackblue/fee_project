

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** MonoBehaviour_Base
	*/
	public abstract class MonoBehaviour_Base : UnityEngine.MonoBehaviour
	{
		/** Mode
		*/
		protected enum Mode
		{
			/** リクエスト待ち。
			*/
			WaitRequest,

			/** 開始。
			*/
			Start,

			/** 実行中。
			*/
			Do,

			/** エラー終了。
			*/
			Do_Error,

			/** 正常終了。
			*/
			Do_Success,

			/** 完了。
			*/
			Fix,
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

			/** サウンドプール。
			*/
			SoundPool,
		};

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected abstract void OnInitialize();

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected abstract System.Collections.IEnumerator OnStart();

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		protected abstract System.Collections.IEnumerator OnDo();

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected abstract System.Collections.IEnumerator OnDoError();

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected abstract System.Collections.IEnumerator OnDoSuccess();

		/** mode
		*/
		[UnityEngine.SerializeField]
		private Mode mode;

		/** cancel_flag
		*/
		[UnityEngine.SerializeField]
		private bool cancel_flag;

		/** delete_flag
		*/
		[UnityEngine.SerializeField]
		private bool delete_flag;

		/** result_progress 
		*/
		[UnityEngine.SerializeField]
		private float result_progress;

		/** result_errorstring
		*/
		[UnityEngine.SerializeField]
		private string result_errorstring;

		/** result_type
		*/
		[UnityEngine.SerializeField]
		private ResultType result_type;

		/** result_binary
		*/
		[UnityEngine.SerializeField]
		private byte[] result_binary;

		/** result_text
		*/
		[UnityEngine.SerializeField]
		private string result_text;

		/** result_texture
		*/
		[UnityEngine.SerializeField]
		private UnityEngine.Texture2D result_texture;

		/** result_assetbundle
		*/
		[UnityEngine.SerializeField]
		private UnityEngine.AssetBundle result_assetbundle;

		/** result_soundpool
		*/
		[UnityEngine.SerializeField]
		private Fee.Audio.Pack_SoundPool result_soundpool;

		/** result_responseheader
		*/
		[UnityEngine.SerializeField]
		private System.Collections.Generic.Dictionary<string,string> result_responseheader;

		/** 結果フラグリセット。
		*/
		protected void ResetResultFlag()
		{
			this.cancel_flag = false;

			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;

			this.result_binary = null;
			this.result_text = null;
			this.result_texture = null;
			this.result_assetbundle = null;
			this.result_soundpool = null;

			this.result_responseheader = null;
		}

		/** キャンセル。設定。
		*/
		public void Cancel()
		{
			this.cancel_flag = true;
		}

		/** キャンセル。取得。
		*/
		public bool IsCancel()
		{
			return this.cancel_flag;
		}

		/** プログレス。取得。
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** プログレス。設定。
		*/
		public void SetResultProgress(float a_progress)
		{
			this.result_progress = a_progress;
		}

		/** エラー文字。取得。
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** 結果タイプ。取得。
		*/
		public ResultType GetResultType()
		{
			return this.result_type;
		}

		/** リクエスト待ち開始。
		*/
		public void WaitRequest()
		{
			if(this.mode == Mode.Fix){
				this.mode = Mode.WaitRequest;
			}else{
				Tool.Assert(false);
			}
		}

		/** リクエスト待ち。
		*/
		protected bool IsWaitRequest()
		{
			if(this.mode == Mode.WaitRequest){
				return true;
			}
			return false;
		}

		/** 開始。
		*/
		protected void SetModeStart()
		{
			this.mode = Mode.Start;
		}

		/** 実行。
		*/
		protected void SetModeDo()
		{
			this.mode = Mode.Do;
		}

		/** 正常終了。
		*/
		protected void SetModeDoSuccess()
		{
			this.mode = Mode.Do_Success;
		}

		/** エラー終了。
		*/
		protected void SetModeDoError()
		{
			this.mode = Mode.Do_Error;
		}

		/** 完了。
		*/
		protected void SetModeFix()
		{
			this.mode = Mode.Fix;
		}

		/** 完了チェック。
		*/
		public bool IsFix()
		{
			if(this.mode == Mode.Fix){
				return true;
			}
			return false;
		}

		/** 削除リクエスト。設定。
		*/
		public void DeleteRequest()
		{
			this.delete_flag = true;
		}

		/** 削除リクエスト。取得。
		*/
		public bool IsDeleteRequest()
		{
			return this.delete_flag;
		}

		/** 結果。設定。
		*/
		public void SetResultErrorString(string a_error_string)
		{
			this.result_type = ResultType.Error;
			this.result_errorstring = a_error_string;
		}

		/** 結果。設定。
		*/
		public void SetResultSaveEnd()
		{
			this.result_type = ResultType.SaveEnd;
		}

		/** 結果。設定。
		*/
		public void SetResultBinary(byte[] a_binary)
		{
			this.result_type = ResultType.Binary;
			this.result_binary = a_binary;
		}

		/** 結果。取得。
		*/
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}

		/** 結果。設定。
		*/
		public void SetResultText(string a_text)
		{
			this.result_type = ResultType.Text;
			this.result_text = a_text;
		}

		/** 結果。取得。
		*/
		public string GetResultText()
		{
			return this.result_text;
		}

		/** 結果。設定。
		*/
		public void SetResultTexture(UnityEngine.Texture2D a_texture)
		{
			this.result_type = ResultType.Texture;
			this.result_texture = a_texture;
		}

		/** 結果。取得。
		*/
		public UnityEngine.Texture2D GetResultTexture()
		{
			return this.result_texture;
		}

		/** 結果。設定。
		*/
		public void SetResultAssetBundle(UnityEngine.AssetBundle a_assetbundle)
		{
			this.result_type = ResultType.AssetBundle;
			this.result_assetbundle = a_assetbundle;
		}

		/** 結果。取得。
		*/
		public UnityEngine.AssetBundle GetResultAssetBundle()
		{
			return this.result_assetbundle;
		}

		/** 結果。設定。
		*/
		public void SetResultSoundPool(Fee.Audio.Pack_SoundPool a_soundpool)
		{
			this.result_type = ResultType.SoundPool;
			this.result_soundpool = a_soundpool;
		}

		/** 結果。設定。
		*/
		public void SetResultResponseHeader(System.Collections.Generic.Dictionary<string,string> a_responseheader)
		{
			this.result_responseheader = a_responseheader;
		}

		/** 結果。取得。
		*/
		public Fee.Audio.Pack_SoundPool GetResultSoundPool()
		{
			return this.result_soundpool;
		}

		/** レスポンスヘッダー。取得。
		*/
		public System.Collections.Generic.Dictionary<string,string> GetResponseHeader()
		{
			return this.result_responseheader;
		}

		/** Awake
		*/
		private void Awake()
		{
			this.mode = Mode.WaitRequest;
			this.cancel_flag = false;
			this.delete_flag = false;
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;

			this.result_binary = null;
			this.result_text = null;
			this.result_texture = null;
			this.result_assetbundle = null;
			this.result_soundpool = null;

			this.result_responseheader = null;

			this.OnInitialize();
		}

		/** Start
		*/
		private System.Collections.IEnumerator Start()
		{
			bool t_loop = true;
			while(t_loop){
				switch(this.mode){
				case Mode.WaitRequest:
					{
						yield return null;
						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				case Mode.Fix:
					{
						yield return null;
						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				case Mode.Start:
					{
						yield return this.OnStart();
					}break;
				case Mode.Do:
					{
						yield return  this.OnDo();
					}break;
				case Mode.Do_Error:
					{
						yield return this.OnDoError();
					}break;
				case Mode.Do_Success:
					{
						yield return this.OnDoSuccess();
					}break;
				}
			}

			Tool.Log(this.gameObject.name,"GameObject.Destroy");
			UnityEngine.GameObject.Destroy(this.gameObject);
			yield break;
		}
	}
}

