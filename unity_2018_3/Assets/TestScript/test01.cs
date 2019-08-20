using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	/** test01

		スプライト
		テキスト
		入力フィールド
		ボタン
		パス

	*/
	public class test01 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test01.ButtonId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test01",
				"test01",

				@"
				スプライト
				テキスト
				入力フィールド
				ボタン
				パス
				"
			);
		}
	
		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** スプライト。
		*/
		private Fee.Render2D.Sprite2D sprite;

		/** テキスト。
		*/
		private Fee.Render2D.Text2D text;

		/** 入力フィールド。
		*/
		private Fee.Render2D.InputField2D inputfield;

		/** ステータス。
		*/
		private Fee.Render2D.Text2D status;

		/** ボタン。
		*/
		private Fee.Ui.Button button_log;
		private Fee.Ui.Button button_logerror;
		private Fee.Ui.Button button_assert;

		/** ButtonId
		*/
		public enum ButtonId
		{
			Log,
			LogError,
			Assert,
		}

		/** Start
		*/
		private void Start()
		{
			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.LOG_ENABLE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Config.LOG_ENABLE = true;
			Fee.Input.Mouse.CreateInstance();

			//イベントプレート。
			Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//フォント。
			Font t_font = Resources.Load<Font>(Data.Resources.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//レイヤインデックス。
			int t_layerindex = 0;

			//描画プライオリティ。
			long t_drawpriority = Fee.Render2D.Render2D.DRAWPRIORITY_STEP * t_layerindex;

			{
				//スプライト。
				this.sprite = new Fee.Render2D.Sprite2D(this.deleter,t_drawpriority);
				this.sprite.SetTexture(Texture2D.whiteTexture);
				this.sprite.SetTextureRect(0,0,Fee.Render2D.Render2D.TEXTURE_W,Fee.Render2D.Render2D.TEXTURE_H);
				this.sprite.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
				this.sprite.SetColor(0.0f,0.5f,0.0f,1.0f);
				this.sprite.SetMaterialType(Fee.Render2D.Config.MaterialType.Simple);
			}

			{
				//入力フィールド。
				{
					int t_w = 100;
					int t_h = 100;
					int t_x = 600;
					int t_y = 10;
		
					this.inputfield = new Fee.Render2D.InputField2D(this.deleter,t_drawpriority);
					this.inputfield.SetRect(t_x,t_y,t_w,t_h);
					this.inputfield.SetText("defaultテキスト");
					this.inputfield.SetMultiLine(true);
					this.inputfield.SetImageColor(1.0f,0.6f,0.6f,1.0f);
					this.inputfield.SetTextColor(0.0f,0.0f,0.0f,0.5f);
				}

				//テキスト。
				{
					int t_x = 600 + 150;
					int t_y = 10;

					this.text = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
					this.text.SetRect(t_x,t_y,0,0);
					this.text.SetText("abcあいうえおxyz");
					this.text.SetColor(0.0f,0.0f,0.0f,1.0f);
					this.text.SetOutLineColor(1.0f,1.0f,1.0f,0.3f);
					this.text.SetOutLine(true);
					this.text.SetShadow(true);
				}
			}

			{
				string t_text = "";

				t_text += "UnityVersion = "		+ Application.unityVersion + "\n";
				t_text += "Screen = "			+ Screen.width.ToString() + " x " + Screen.height.ToString() + "\n";
				t_text += "Data = "				+ Application.dataPath + "\n";
				t_text += "Persistent Data = "	+ Application.persistentDataPath + "\n";
				t_text += "Streaming Assets = "	+ Application.streamingAssetsPath + "\n";
				t_text += "Temporary Cache = "	+ Application.temporaryCachePath + "\n";

				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					t_text += "USE_DEF_FEE_INPUTSYSTEM = TRUE\n";
					#else
					t_text += "USE_DEF_FEE_INPUTSYSTEM = FALSE\n";
					#endif
				}
				{
					#if(USE_DEF_FEE_UTF8JSON)
					t_text += "USE_DEF_FEE_UTF8JSON = TRUE\n";
					#else
					t_text += "USE_DEF_FEE_UTF8JSON = FALSE\n";
					#endif
				}
				{
					#if(USE_DEF_FEE_NPOI)
					t_text += "USE_DEF_FEE_NPOI = TRUE\n";
					#else
					t_text += "USE_DEF_FEE_NPOI = FALSE\n";
					#endif
				}
				{
					#if(USE_DEF_FEE_EXCELDATAREADER)
					t_text += "USE_DEF_FEE_EXCELDATAREADER = TRUE\n";
					#else
					t_text += "USE_DEF_FEE_EXCELDATAREADER = FALSE\n";
					#endif
				}
				{
					#if(USE_DEF_FEE_PUN)
					t_text += "USE_DEF_FEE_PUN = TRUE\n";
					#else
					t_text += "USE_DEF_FEE_PUN = FALSE\n";
					#endif
				}
				{
					#if(USE_DEF_FEE_UNIVRM)
					t_text += "USE_DEF_FEE_UNIVRM = TRUE\n";
					#else
					t_text += "USE_DEF_FEE_UNIVRM = FALSE\n";
					#endif
				}
				{
					#if(USE_DEF_FEE_SIMPLEANIMATION)
					t_text += "USE_DEF_FEE_SIMPLEANIMATION = TRUE\n";
					#else
					t_text += "USE_DEF_FEE_SIMPLEANIMATION = FALSE\n";
					#endif
				}
				{
					#if(USE_DEF_FEE_TEMP)
					t_text += "USE_DEF_FEE_TEMP = TRUE\n";
					#else
					t_text += "USE_DEF_FEE_TEMP = FALSE\n";
					#endif
				}
				{
					#if(USE_DEF_FEE_EDITORMENU)
					t_text += "USE_DEF_FEE_EDITORMENU = TRUE\n";
					#else
					t_text += "USE_DEF_FEE_EDITORMENU = FALSE\n";
					#endif
				}
				{
					#if(USE_DEF_FEE_DEBUGTOOL)
					t_text += "USE_DEF_FEE_DEBUGTOOL = TRUE\n";
					#else
					t_text += "USE_DEF_FEE_DEBUGTOOL = FALSE\n";
					#endif
				}

				this.status = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
				this.status.SetRect(10,100,0,0);
				this.status.SetFontSize(15);
				this.status.SetText(t_text);
			}

			{
				int t_xx = 150;
				int t_yy = 10;

				this.button_log = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_log.SetOnButtonClick(this,ButtonId.Log);
				this.button_log.SetRect(t_xx,t_yy,80,50);
				this.button_log.SetText("Log");
				this.button_log.SetTextureCornerSize(10);
				this.button_log.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_log.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_log.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_log.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_log.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.button_log.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.button_log.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.button_log.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

				t_xx += 100;

				this.button_logerror = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_logerror.SetOnButtonClick(this,ButtonId.LogError);
				this.button_logerror.SetRect(t_xx,t_yy,80,50);
				this.button_logerror.SetText("LogError");
				this.button_logerror.SetTextureCornerSize(10);
				this.button_logerror.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_logerror.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_logerror.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_logerror.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_logerror.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.button_logerror.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.button_logerror.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.button_logerror.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

				t_xx += 100;

				this.button_assert = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_assert.SetOnButtonClick(this,ButtonId.Assert);
				this.button_assert.SetRect(t_xx,t_yy,80,50);
				this.button_assert.SetText("Assert");
				this.button_assert.SetTextureCornerSize(10);
				this.button_assert.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_assert.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_assert.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_assert.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button_assert.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.button_assert.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.button_assert.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.button_assert.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			}
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			switch(a_id){
			case ButtonId.Log:
				{
					Fee.Render2D.Tool.Log("CallBack_Click","Log");
				}break;
			case ButtonId.LogError:
				{
					Fee.Render2D.Tool.LogError("CallBack_Click","LogError");
				}break;
			case ButtonId.Assert:
				{
					Fee.Render2D.Tool.Assert(false);
				}break;
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
			return true;
		}

		/** OnDestroy
		*/
		private void OnDestroy()
		{
			this.deleter.DeleteAll();
		}
	}
}

