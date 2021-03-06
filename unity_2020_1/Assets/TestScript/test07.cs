

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief テスト。
*/


/** TestScript
*/
namespace TestScript
{
	/** test07

		公開鍵暗号
		証明書
		共通鍵暗号

	*/
	public class test07 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test07.ButtonId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test07",
				"test07",

				@"
				公開鍵暗号
				証明書
				共通鍵暗号
				"
			);
		}

		/** Step
		*/
		private enum Step
		{
			None,

			EncryptPublicKey_Start,
			EncryptPublicKey_Do,
			DecryptPrivateKey_Start,
			DecryptPrivateKey_Do,

			EncryptPass_Start,
			EncryptPass_Do,
			DecryptPass_Start,
			DecryptPass_Do,

			CreateSignature_Start,
			CreateSignature_Do,
			VerifySignature_Start,
			VerifySignature_Do,
		};

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** step
		*/
		private Step step;

		/** button_key
		*/
		private Fee.Ui.Button button_key;

		/** button_pass
		*/
		private Fee.Ui.Button button_pass;

		/** button_signature
		*/
		private Fee.Ui.Button button_signature;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** crypt_item
		*/
		private Fee.Crypt.Item crypt_item;

		/** private_key
		*/
		private string private_key;

		/** public_key
		*/
		private string public_key;

		/** pass
		*/
		private string pass;

		/** salt
		*/
		private string salt;

		/** plane_binary
		*/
		private byte[] plane_binary;

		/** encrypt_binary
		*/
		private byte[] encrypt_binary;

		/** signature_binary
		*/
		private byte[] signature_binary;

		/** ButtonId
		*/
		public enum ButtonId
		{
			/** Key
			*/
			Key,

			/** Pass
			*/
			Pass,

			/** Signature
			*/
			Signature,
		}

		/** Start
		*/
		private void Start()
		{
			//プレイヤーループシステム。インスタンス作成。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance(null);
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(UnityEngine.PlayerLoop.PreUpdate.SendMouseEvents));

			//プラットフォーム。インスタンス作成。
			Fee.Platform.Platform.CreateInstance();

			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.CreateInstance();
			Fee.Function.Function.GetInstance().SetMonoBehaviour(this);
			Fee.Function.Function.GetInstance().SetRowUpdate(this.RowUpdate);

			//インスタンス作成。
			Fee.Instantiate.Instantiate.CreateInstance();

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Input.CreateInstance(true,false,true,false);

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			//Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//暗号。インスタンス作成。
			Fee.Crypt.Config.LOG_ENABLE = true;
			Fee.Crypt.Crypt.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//step
			this.step = Step.None;

			//button_key
			this.button_key = this.prefablist.CreateButton(this.deleter,0);
			this.button_key.SetOnButtonClick(this,ButtonId.Key);
			this.button_key.SetRect(100 + 200 * 0,100,150,50);
			this.button_key.SetText("公開鍵");

			//button_pass
			this.button_pass = this.prefablist.CreateButton(this.deleter,0);
			this.button_pass.SetOnButtonClick(this,ButtonId.Pass);
			this.button_pass.SetRect(100 + 200 * 1,100,150,50);
			this.button_pass.SetText("共通鍵");

			//button_signature
			this.button_signature = this.prefablist.CreateButton(this.deleter,0);
			this.button_signature.SetOnButtonClick(this,ButtonId.Signature);
			this.button_signature.SetRect(100 + 200 * 2,100,150,50);
			this.button_signature.SetText("証明書");

			//text
			this.text = this.prefablist.CreateText(this.deleter,0);
			this.text.SetRect(100,50,0,0);
			this.text.SetText("---");

			//crypt_item
			this.crypt_item = null;

			//key
			this.private_key = null;
			this.public_key = null;

			//pass
			this.pass = null;
			this.salt = null;

			//binary
			this.plane_binary = null;
			this.encrypt_binary = null;
			this.signature_binary = null;
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			if(this.step == Step.None){

				switch(a_id){
				case ButtonId.Key:
					{
						//public
						Fee.JsonItem.JsonItem t_item_public = new Fee.JsonItem.JsonItem(this.prefablist.GetTextAsset(Common.TextAssetType.Test07_PublicKey).text);
						this.public_key = null;
						if(t_item_public != null){
							if(t_item_public.IsAssociativeArray() == true){
								if(t_item_public.IsExistItem("public",Fee.JsonItem.ValueType.StringData) == true){
									this.public_key = t_item_public.GetItem("public").GetStringData();
								}
							}
						}

						//private
						Fee.JsonItem.JsonItem t_item_private = new Fee.JsonItem.JsonItem(this.prefablist.GetTextAsset(Common.TextAssetType.Test07_PrivateKey).text);
						this.private_key = null;
						if(t_item_private != null){
							if(t_item_private.IsAssociativeArray() == true){
								if(t_item_private.IsExistItem("private",Fee.JsonItem.ValueType.StringData) == true){
									this.private_key = t_item_private.GetItem("private").GetStringData();
								}
							}
						}

						this.step = Step.EncryptPublicKey_Start;
					}break;
				case ButtonId.Pass:
					{
						this.pass = "0123456789";
						this.salt = "zxcvasdf";

						this.step = Step.EncryptPass_Start;
					}break;
				case ButtonId.Signature:
					{
						//public
						Fee.JsonItem.JsonItem t_item_public = new Fee.JsonItem.JsonItem(this.prefablist.GetTextAsset(Common.TextAssetType.Test07_PublicKey).text);
						this.public_key = null;
						if(t_item_public != null){
							if(t_item_public.IsAssociativeArray() == true){
								if(t_item_public.IsExistItem("public",Fee.JsonItem.ValueType.StringData) == true){
									this.public_key = t_item_public.GetItem("public").GetStringData();
								}
							}
						}

						//private
						Fee.JsonItem.JsonItem t_item_private = new Fee.JsonItem.JsonItem(this.prefablist.GetTextAsset(Common.TextAssetType.Test07_PrivateKey).text);
						this.private_key = null;
						if(t_item_private != null){
							if(t_item_private.IsAssociativeArray() == true){
								if(t_item_private.IsExistItem("private",Fee.JsonItem.ValueType.StringData) == true){
									this.private_key = t_item_private.GetItem("private").GetStringData();
								}
							}
						}

						this.step = Step.CreateSignature_Start;
					}break;
				}
			}
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
			//ステップ。
			switch(this.step){
			case Step.EncryptPublicKey_Start:
				{
					this.plane_binary = new byte[15];
					for(int ii=0;ii<this.plane_binary.Length;ii++){
						this.plane_binary[ii] = (byte)(ii % 256);
					}

					//暗号化開始。
					this.crypt_item = Fee.Crypt.Crypt.GetInstance().RequestEncryptPublicKey(this.plane_binary,this.public_key);

					this.step = Step.EncryptPublicKey_Do;
				}break;
			case Step.EncryptPublicKey_Do:
				{
					if(this.crypt_item.IsBusy() == true){
						//暗号化中。
						this.text.SetText(this.step.ToString());
					}else{
						if(this.crypt_item.GetResultType() == Fee.Crypt.Item.ResultType.Binary){
							//成功。
							byte[] t_binary = this.crypt_item.GetResultBinary();
							this.text.SetText(this.step.ToString() + " : Success");

							{
								string t_log = "";
								for(int ii=0;ii<t_binary.Length;ii++){
									t_log += t_binary[ii].ToString() + " ";
								}
								UnityEngine.Debug.Log(t_log);
							}

							this.encrypt_binary = t_binary;
							this.crypt_item = null;
							this.step = Step.DecryptPrivateKey_Start;
						}else{
							//失敗。
							this.encrypt_binary = null;
							this.text.SetText(this.step.ToString() + " : Failed");

							this.crypt_item = null;
							this.step = Step.None;
						}
					}
				}break;
			case Step.DecryptPrivateKey_Start:
				{
					//複合化開始。
					this.crypt_item = Fee.Crypt.Crypt.GetInstance().RequestDecryptPrivateKey(this.encrypt_binary,this.private_key);

					this.step = Step.DecryptPrivateKey_Do;
				}break;
			case Step.DecryptPrivateKey_Do:
				{
					if(this.crypt_item.IsBusy() == true){
						//複合化中。
						this.text.SetText(this.step.ToString());
					}else{
						if(this.crypt_item.GetResultType() == Fee.Crypt.Item.ResultType.Binary){
							//成功。
							byte[] t_binary = this.crypt_item.GetResultBinary();
							this.text.SetText(this.step.ToString() + " : Success");

							{
								string t_log = "";
								for(int ii=0;ii<t_binary.Length;ii++){
									t_log += t_binary[ii].ToString() + " ";
								}
								UnityEngine.Debug.Log(t_log);
							}

							this.crypt_item = null;
							this.step = Step.None;
						}else{
							//失敗。
							this.encrypt_binary = null;
							this.text.SetText(this.step.ToString() + " : Failed");

							this.crypt_item = null;
							this.step = Step.None;
						}
					}
				}break;
			case Step.EncryptPass_Start:
				{
					this.plane_binary = new byte[15];
					for(int ii=0;ii<this.plane_binary.Length;ii++){
						this.plane_binary[ii] = (byte)(ii % 256);
					}

					//暗号化開始。
					this.crypt_item = Fee.Crypt.Crypt.GetInstance().RequestEncryptPass(this.plane_binary,0,this.plane_binary.Length,this.pass,this.salt);

					this.step = Step.EncryptPass_Do;
				}break;
			case Step.EncryptPass_Do:
				{
					if(this.crypt_item.IsBusy() == true){
						//暗号化中。
						this.text.SetText(this.step.ToString());
					}else{
						if(this.crypt_item.GetResultType() == Fee.Crypt.Item.ResultType.Binary){
							//成功。
							byte[] t_binary = this.crypt_item.GetResultBinary();
							this.text.SetText(this.step.ToString() + " : Success");

							{
								UnityEngine.Debug.Log("Encrypt = " + System.BitConverter.ToString(t_binary));
							}

							this.encrypt_binary = t_binary;
							this.crypt_item = null;
							this.step = Step.DecryptPass_Start;
						}else{
							//失敗。
							this.encrypt_binary = null;
							this.text.SetText(this.step.ToString() + " : Failed");

							this.crypt_item = null;
							this.step = Step.None;
						}
					}
				}break;
			case Step.DecryptPass_Start:
				{
					//複合化開始。
					this.crypt_item = Fee.Crypt.Crypt.GetInstance().RequestDecryptPass(this.encrypt_binary,0,this.encrypt_binary.Length,this.pass,this.salt);

					this.step = Step.DecryptPass_Do;
				}break;
			case Step.DecryptPass_Do:
				{
					if(this.crypt_item.IsBusy() == true){
						//複合化中。
						this.text.SetText(this.step.ToString());
					}else{
						if(this.crypt_item.GetResultType() == Fee.Crypt.Item.ResultType.Binary){
							//成功。
							byte[] t_binary = this.crypt_item.GetResultBinary();
							this.text.SetText(this.step.ToString() + " : Success");

							{
								string t_log = "";
								for(int ii=0;ii<t_binary.Length;ii++){
									t_log += t_binary[ii].ToString() + " ";
								}
								UnityEngine.Debug.Log(t_log);
							}

							this.encrypt_binary = t_binary;
							this.crypt_item = null;
							this.step = Step.None;
						}else{
							//失敗。
							this.encrypt_binary = null;
							this.text.SetText(this.step.ToString() + " : Failed");

							this.crypt_item = null;
							this.step = Step.None;
						}
					}
				}break;
			case Step.CreateSignature_Start:
				{
					this.plane_binary = new byte[15];
					for(int ii=0;ii<this.plane_binary.Length;ii++){
						this.plane_binary[this.plane_binary.Length - ii - 1] = (byte)(ii % 256);
					}

					//証明書作成開始。
					this.crypt_item = Fee.Crypt.Crypt.GetInstance().RequestCreateSignaturePrivateKey(this.plane_binary,this.private_key);

					this.step = Step.CreateSignature_Do;
				}break;
			case Step.CreateSignature_Do:
				{
					if(this.crypt_item.IsBusy() == true){
						//作成中。
						this.text.SetText(this.step.ToString());
					}else{
						if(this.crypt_item.GetResultType() == Fee.Crypt.Item.ResultType.Binary){
							//成功。
							byte[] t_binary = this.crypt_item.GetResultBinary();
							this.text.SetText(this.step.ToString() + " : Success");

							{
								string t_log = "";
								for(int ii=0;ii<t_binary.Length;ii++){
									t_log += t_binary[ii].ToString() + " ";
								}
								UnityEngine.Debug.Log(t_log);
							}

							this.signature_binary = t_binary;
							this.crypt_item = null;
							this.step = Step.VerifySignature_Start;
						}else{
							//失敗。
							this.signature_binary = null;
							this.text.SetText(this.step.ToString() + " : Failed");

							this.crypt_item = null;
							this.step = Step.None;
						}
					}
				}break;
			case Step.VerifySignature_Start:
				{
					//証明書検証開始。
					this.crypt_item = Fee.Crypt.Crypt.GetInstance().RequestVerifySignaturePublicKey(this.plane_binary,this.signature_binary,this.public_key);

					this.step = Step.VerifySignature_Do;
				}break;
			case Step.VerifySignature_Do:
				{
					if(this.crypt_item.IsBusy() == true){
						//検証中。
						this.text.SetText(this.step.ToString());
					}else{
						if(this.crypt_item.GetResultType() == Fee.Crypt.Item.ResultType.VerifySuccess){
							//成功。
							this.text.SetText(this.step.ToString() + " : Success");

							this.crypt_item = null;
							this.step = Step.None;
						}else{
							//失敗。
							this.text.SetText(this.step.ToString() + " : Failed");

							this.crypt_item = null;
							this.step = Step.None;
						}
					}
				}break;
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
		}

		/** Update
		*/
		private void Update()
		{
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
		}

		/** 強制終了。
		*/
		public override void Shutdown()
		{
			Fee.Function.Function.GetInstance().UnSetRowUpdate(this.RowUpdate);
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
			Fee.Function.Function.GetInstance().UnSetRowUpdate(this.RowUpdate);
			return true;
		}

		/** 削除。
		*/
		public override void Destroy()
		{
			//削除。
			this.deleter.DeleteAll();

			//ライブラリ停止。
			DeleteLibInstance.DeleteAll();
		}
	}
}

