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

		/** is_clip
		*/
		private bool is_clip;

		/** update_clip_rect
		*/
		private bool update_clip_rect;

		/** clipsprite
		*/
		private Fee.Ui.ClipSprite clipsprite;

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
		private Fee.Render2D.InputField2D inputfield;

		/** slider
		*/
		private Fee.Ui.Slider slider;

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
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.CreateInstance();

			//キ。インスタンス作成。
			Fee.Input.Key.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Ui.CreateInstance();

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//フォント。
			Font t_font = Resources.Load<Font>(Data.Resources.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//is_clip
			this.is_clip = true;

			//update_clip_rect
			this.update_clip_rect = true;

			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;

			//clipsprite
			{
				Texture2D t_texture = Resources.Load<Texture2D>(Data.Resources.TEXTURE_SKYIMAGE);

				this.clipsprite = new Fee.Ui.ClipSprite(this.deleter,t_drawpriority + 1);
				this.clipsprite.SetRect(in Fee.Render2D.Render2D.VIRTUAL_RECT_MAX);
				this.clipsprite.SetTexture(Texture2D.whiteTexture);
				this.clipsprite.SetTextureRect(in Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
				this.clipsprite.SetColor(0.7f,0.7f,0.7f,1.0f);
				this.clipsprite.SetTexture(t_texture);
			}

			//text
			{
				int t_x = Fee.Render2D.Render2D.VIRTUAL_W / 2;
				int t_y = 100;

				this.text = new Fee.Render2D.Text2D(this.deleter,t_drawpriority + 1);
				this.text.SetRect(t_x,t_y,0,0);
				this.text.SetText("ESC ENTER");
				this.text.SetCenter(true,true);
				this.text.SetColor(1.0f,0.0f,0.0f,1.0f);
				this.text.SetFontSize(27);
				this.text.SetShadow(true);
				this.text.SetOutLine(true);
			}

			//button
			{
				int t_w = 100;
				int t_h = 30;
				int t_x = (Fee.Render2D.Render2D.VIRTUAL_W - t_w) /2;
				int t_y = 300;


				this.button = new Fee.Ui.Button(this.deleter,t_drawpriority + 2);
				this.button.SetRect(t_x,t_y,t_w,t_h);
				this.button.SetTextureCornerSize(10);
				this.button.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.button.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.button.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.button.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
			}

			//checkbutton
			{
				int t_w = 50;
				int t_h = 50;
				int t_x = (Fee.Render2D.Render2D.VIRTUAL_W - t_w) /2;
				int t_y = 200;

				this.checkbutton = new Fee.Ui.CheckButton(this.deleter,t_drawpriority + 2);
				this.checkbutton.SetRect(t_x,t_y,t_w,t_h);
				this.checkbutton.SetBgNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_CHECKBUTTON));
				this.checkbutton.SetBgOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_CHECKBUTTON));
				this.checkbutton.SetBgLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_CHECKBUTTON));
				this.checkbutton.SetBgNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.checkbutton.SetBgOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.checkbutton.SetBgLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
				this.checkbutton.SetCheckNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_CHECKBUTTON));
				this.checkbutton.SetCheckLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_CHECKBUTTON));
				this.checkbutton.SetCheckNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.checkbutton.SetCheckNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			}

			//inputfield
			{
				int t_w = 100;
				int t_h = 100;
				int t_x = 100;
				int t_y = 100;

				this.inputfield = new Fee.Render2D.InputField2D(this.deleter,t_drawpriority + 2);
				this.inputfield.SetRect(t_x,t_y,t_w,t_h);
			}

			//slider
			{
				int t_w = 300;
				int t_h = 20;
				int t_x = (Fee.Render2D.Render2D.VIRTUAL_W - t_w) /2;
				int t_y = 350;

				this.slider = new Fee.Ui.Slider(this.deleter,t_drawpriority + 2);
				this.slider.SetRect(t_x,t_y,t_w,t_h);
				this.slider.SetButtonSize(0,30);
				this.slider.SetTextureCornerSize(10);
				this.slider.SetBgNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
				this.slider.SetBgLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
				this.slider.SetValueNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
				this.slider.SetValueLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
				this.slider.SetBgNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.slider.SetBgLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.slider.SetValueNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.slider.SetValueLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
				this.slider.SetButtonNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.slider.SetButtonLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.slider.SetButtonNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.slider.SetButtonLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
			}

			//is_clip
			{
				this.clipsprite.SetClip(this.is_clip);
				this.text.SetClip(this.is_clip);
				this.button.SetClip(this.is_clip);
				this.checkbutton.SetClip(this.is_clip);
				this.inputfield.SetClip(this.is_clip);
				this.slider.SetClip(this.is_clip);
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//キー。
			Fee.Input.Key.GetInstance().Main(true);

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			if(Fee.Input.Key.GetInstance().enter.down == true){
				this.is_clip = !this.is_clip;

				this.clipsprite.SetClip(this.is_clip);
				this.text.SetClip(this.is_clip);
				this.button.SetClip(this.is_clip);
				this.checkbutton.SetClip(this.is_clip);
				this.inputfield.SetClip(this.is_clip);
				this.slider.SetClip(this.is_clip);
			}

			if(Fee.Input.Key.GetInstance().escape.down == true){
				this.update_clip_rect = !this.update_clip_rect;
			}

			if(this.update_clip_rect == true){
				Fee.Geometry.Rect2D_R<int> t_cliprect;
				{
					t_cliprect.w = 200;
					t_cliprect.h = 200;
					t_cliprect.x = Fee.Input.Mouse.GetInstance().cursor.pos.x - t_cliprect.w / 2;
					t_cliprect.y = Fee.Input.Mouse.GetInstance().cursor.pos.y - t_cliprect.h / 2;
				}

				this.clipsprite.SetClipRect(in t_cliprect);
				this.text.SetClipRect(in t_cliprect);
				this.button.SetClipRect(in t_cliprect);
				this.checkbutton.SetClipRect(in t_cliprect);
				this.inputfield.SetClipRect(in t_cliprect);
				this.slider.SetClipRect(in t_cliprect);
			}
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

