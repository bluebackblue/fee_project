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
	/** test21
	*/
	public class test21 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<int>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test21",
				"test21",

				@"
				オープンファイルダイアログ
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** button
		*/
		private Fee.Ui.Button button;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** Start
		*/
		private void Start()
		{
			//プラットフォーム。インスタンス作成。
			Fee.Platform.Platform.CreateInstance();

			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
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

			//button
			{
				int t_x = 50;
				int t_y = 200;
				int t_w = 90;
				int t_h = 60;

				//button
				this.button = new Fee.Ui.Button(this.deleter,1);
				this.button.SetOnButtonClick(this,0);
				this.button.SetTextureCornerSize(10);
				this.button.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.button.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.button.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.button.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
				this.button.SetText("開始");
				this.button.SetRect(t_x,t_y,t_w,t_h);
			}

			//text
			{
				int t_x = 50 + 150;
				int t_y = 200 + 20;

				this.text = Fee.Render2D.Text2D.Create(this.deleter,1);
				this.text.SetXY(t_x,t_y);
			}
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(int a_id)
		{
			Fee.Platform.Platform.GetInstance().OpenFileDialog();
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(this.is_focus,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			{
				string t_filename = Fee.Platform.Platform.GetInstance().GetOpenFileDialogResult();
				if(t_filename == null){
					t_filename = "null";
				}else if(t_filename == ""){
					t_filename = "space";
				}
				this.text.SetText(t_filename);
			}

			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_After();
		}

		/** Update
		*/
		private void Update()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_PreDraw();
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
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

