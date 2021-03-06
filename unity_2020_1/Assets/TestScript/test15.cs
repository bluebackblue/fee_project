

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
	/** test15

		クリップ

	*/
	public class test15 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test15",
				"test15",

				@"
				クリップ
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** is_clip
		*/
		private bool is_clip;

		/** update_clip_rect
		*/
		private bool update_clip_rect;

		/** sprite
		*/
		private Fee.Ui.Sprite_Clip sprite;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** button
		*/
		private Fee.Ui.Button button;

		/** checkbutton
		*/
		private Fee.Ui.CheckButton checkbutton;

		/** inputfield
		*/
		private Fee.Ui.Input inputfield;

		/** slider
		*/
		private Fee.Ui.Slider slider;

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
			Fee.Input.Input.GetInstance().key.Regist(Fee.Input.Status_Key_Type.Enter);
			Fee.Input.Input.GetInstance().key.Regist(Fee.Input.Status_Key_Type.Esc);

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Ui.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//is_clip
			this.is_clip = true;

			//update_clip_rect
			this.update_clip_rect = true;

			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Config.DRAWPRIORITY_STEP;

			//sprite
			{
				this.sprite = this.prefablist.CreateClipSprite(this.deleter,t_drawpriority + 1);
				this.sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
				this.sprite.SetColor(0.7f,0.7f,0.7f,1.0f);
				this.sprite.SetTexture(this.prefablist.GetTexture(Common.TextureType.Test15_Texture));
			}

			//text
			{
				int t_x = Fee.Render2D.Config.VIRTUAL_W / 2;
				int t_y = 100;

				this.text = this.prefablist.CreateText(this.deleter,t_drawpriority + 1);
				this.text.SetRect(t_x,t_y,0,0);
				this.text.SetText("ESC ENTER");
				this.text.SetAlignmentType(Fee.Render2D.Text2D_HorizontalAlignmentType.Center,Fee.Render2D.Text2D_VerticalAlignmentType.Middle);
				this.text.SetColor(1.0f,0.0f,0.0f,1.0f);
				this.text.SetFontSize(27);
				this.text.SetShadow(true);
				this.text.SetOutLine(true);
			}

			//button
			{
				int t_w = 100;
				int t_h = 30;
				int t_x = (Fee.Render2D.Config.VIRTUAL_W - t_w) /2;
				int t_y = 300;

				this.button = this.prefablist.CreateButton(this.deleter,t_drawpriority + 2);
				this.button.SetRect(t_x,t_y,t_w,t_h);
				this.button.SetText("TEXT");
			}

			//checkbutton
			{
				int t_w = 50;
				int t_h = 50;
				int t_x = (Fee.Render2D.Config.VIRTUAL_W - t_w) /2;
				int t_y = 200;

				this.checkbutton = this.prefablist.CreateCheckButton(this.deleter,t_drawpriority + 2);
				this.checkbutton.SetRect(t_x,t_y,t_w,t_h);
				this.checkbutton.SetText("チェック");
			}

			//inputfield
			{
				int t_w = 100;
				int t_h = 100;
				int t_x = 100;
				int t_y = 100;

				this.inputfield = this.prefablist.CreateInputField(this.deleter,t_drawpriority + 2);
				this.inputfield.SetRect(t_x,t_y,t_w,t_h);
				this.inputfield.SetTextColor(1.0f,0.0f,0.0f,1.0f);
			}

			//slider
			{
				int t_w = 300;
				int t_h = 20;
				int t_x = (Fee.Render2D.Config.VIRTUAL_W - t_w) /2;
				int t_y = 350;

				this.slider = this.prefablist.CreateSlider(this.deleter,t_drawpriority + 2);
				this.slider.SetRect(t_x,t_y,t_w,t_h);
				this.slider.SetValue(0.0f);
				this.slider.SetButtonSize(10,40);
			}

			//is_clip
			{
				this.sprite.SetClip(this.is_clip);
				this.text.SetClip(this.is_clip);
				this.button.SetClip(this.is_clip);
				this.checkbutton.SetClip(this.is_clip);
				this.inputfield.SetClip(this.is_clip);
				this.slider.SetClip(this.is_clip);
			}
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
			if(this.update_clip_rect == true){
				Fee.Geometry.Rect2D_R<int> t_cliprect;
				{
					t_cliprect.w = 200;
					t_cliprect.h = 200;
					t_cliprect.x = Fee.Input.Input.GetInstance().mouse.cursor.pos.x - t_cliprect.w / 2;
					t_cliprect.y = Fee.Input.Input.GetInstance().mouse.cursor.pos.y - t_cliprect.h / 2;
				}

				this.sprite.SetClipRect(in t_cliprect);
				this.text.SetClipRect(in t_cliprect);
				this.button.SetClipRect(in t_cliprect);
				this.checkbutton.SetClipRect(in t_cliprect);
				this.inputfield.SetClipRect(in t_cliprect);
				this.slider.SetClipRect(in t_cliprect);
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
			if(Fee.Input.Input.GetInstance().key.GetKey(Fee.Input.Status_Key_Type.Enter).digital.down == true){
				this.is_clip = !this.is_clip;

				this.sprite.SetClip(this.is_clip);
				this.text.SetClip(this.is_clip);
				this.button.SetClip(this.is_clip);
				this.checkbutton.SetClip(this.is_clip);
				this.inputfield.SetClip(this.is_clip);
				this.slider.SetClip(this.is_clip);
			}

			if(Fee.Input.Input.GetInstance().key.GetKey(Fee.Input.Status_Key_Type.Esc).digital.down == true){
				this.update_clip_rect = !this.update_clip_rect;
			}
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

