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

		/** prefablist
		*/
		private Common.PrefabList prefablist;

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

		/** ライン。
		*/
		private Fee.Ui.Line line;

		/** スプライト。
		*/
		private Fee.Render2D.Sprite2D start;

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

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.LOG_ENABLE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Config.LOG_ENABLE = true;
			Fee.Input.Input.CreateInstance(true,false,true,false);

			//イベントプレート。
			Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//レイヤインデックス。
			int t_layerindex = 0;

			//描画プライオリティ。
			long t_drawpriority = Fee.Render2D.Config.DRAWPRIORITY_STEP * t_layerindex;

			{
				//スプライト。
				this.sprite = Fee.Render2D.Sprite2D.Create(this.deleter,t_drawpriority);
				this.sprite.SetTexture(Texture2D.whiteTexture);
				this.sprite.SetTextureRect(0,0,Fee.Render2D.Config.TEXTURE_W,Fee.Render2D.Config.TEXTURE_H);
				this.sprite.SetRect(0,0,Fee.Render2D.Config.VIRTUAL_W,Fee.Render2D.Config.VIRTUAL_H);
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
		
					this.inputfield = this.prefablist.CreateInputField(this.deleter,t_drawpriority);
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

					this.text = this.prefablist.CreateText(this.deleter,t_drawpriority);
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

				this.status = this.prefablist.CreateText(this.deleter,t_drawpriority);
				this.status.SetRect(10,100,0,0);
				this.status.SetFontSize(15);
				this.status.SetText(t_text);
			}

			{
				int t_xx = 150;
				int t_yy = 10;

				//ログボタン。
				this.button_log = this.prefablist.CreateButton(this.deleter,t_drawpriority + 1);
				this.button_log.SetOnButtonClick(this,ButtonId.Log);
				this.button_log.SetRect(t_xx,t_yy,80,50);
				this.button_log.SetText("Log");

				t_xx += 100;

				//ログエラーボタン。
				this.button_logerror = this.prefablist.CreateButton(this.deleter,t_drawpriority + 1);
				this.button_logerror.SetOnButtonClick(this,ButtonId.LogError);
				this.button_logerror.SetRect(t_xx,t_yy,80,50);
				this.button_logerror.SetText("LogError");

				t_xx += 100;

				//アサートボタン。
				this.button_assert = this.prefablist.CreateButton(this.deleter,t_drawpriority + 1);
				this.button_assert.SetOnButtonClick(this,ButtonId.Assert);
				this.button_assert.SetRect(t_xx,t_yy,80,50);
				this.button_assert.SetText("Assert");
			}

			{
				//line
				this.line = Fee.Ui.Line.Create(this.deleter,t_drawpriority + 1);
				this.line.SetSize(30);

				//start
				this.start = Fee.Render2D.Sprite2D.Create(this.deleter,t_drawpriority + 2);
				this.start.SetTexture(UnityEngine.Texture2D.whiteTexture);
				this.start.SetRect(0,0,0,0);
				this.start.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				this.start.SetColor(1.0f,0.0f,0.0f,1.0f);
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

		/** RowUpdate
		*/
		private void RowUpdate()
		{
			int t_start_x = Fee.Render2D.Config.VIRTUAL_W / 2;
			int t_start_y = Fee.Render2D.Config.VIRTUAL_H / 2;

			this.line.SetRect(new Fee.Geometry.Rect2D_A<int>(t_start_x,t_start_y,Fee.Input.Input.GetInstance().mouse.cursor.pos.x,Fee.Input.Input.GetInstance().mouse.cursor.pos.y));
			this.start.SetRect(t_start_x - 2,t_start_y - 2,4,4);
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

