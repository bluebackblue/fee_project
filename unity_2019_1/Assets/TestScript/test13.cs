using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** TestScript
*/
namespace TestScript
{
	/** test13

		タッチ

	*/
	public class test13 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test13",
				"test13",

				@"
				タッチ
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** 背景。
		*/
		private Fee.Render2D.Sprite2D bg;

		/** ClickId
		*/
		private enum ClickId
		{
			Touch_InputSystem_TouchScreen,
			Touch_InputManager_InputTouch,
			Touch_InputManager_InputMouse,
		}

		/** touch
		*/
		private Fee.Ui.Button button_inputsystem_touchscreen_touch;
		private Fee.Ui.Button button_inputmanager_inputtouch_touch;
		private Fee.Ui.Button button_inputmanager_inputmouse_touch;

		/** タッチ。
		*/
		private Fee.Render2D.Text2D touch_text;

		/** タッチビューＩＤ。
		*/
		private int touchview_id;

		/** タッチビュー。
		*/
		class TouchView : Fee.Input.Touch_Phase_Key_Base
		{
			public int id;
			public Fee.Render2D.Sprite2D sprite;
			public Fee.Render2D.Text2D text;
			public Fee.Input.Touch_Phase touch_phase;
			public Fee.Deleter.Deleter deleter;

			/** constructor
			*/
			public TouchView(int a_id,Fee.Deleter.Deleter a_deleter,Fee.Input.Touch_Phase a_touch_phase)
			{
				this.id = a_id;
				this.deleter = a_deleter;
				this.touch_phase = a_touch_phase;

				this.sprite = new Fee.Render2D.Sprite2D(this.deleter,1);
				{
					int t_size = 100;

					this.sprite.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
					this.sprite.SetTexture(Texture2D.whiteTexture);
					this.sprite.SetColor(Random.value,Random.value,Random.value,1.0f);
					this.sprite.SetRect(this.touch_phase.value_x-t_size/2,this.touch_phase.value_y-t_size/2,t_size,t_size);
				}

				this.text = new Fee.Render2D.Text2D(this.deleter,1);
				{
					this.text.SetRect(this.touch_phase.value_x,this.touch_phase.value_y,0,0);
				}
			}

			/** [Touch_Phase_Key_Base]更新。
			*/
			public void OnUpdate()
			{
				int t_size = 100;
				this.sprite.SetRect(this.touch_phase.value_x-t_size/2,this.touch_phase.value_y-t_size/2,t_size,t_size);
				this.text.SetRect(this.touch_phase.value_x,this.touch_phase.value_y - 100,0,0);

				string t_text = "";
				t_text += "id = " + this.id.ToString() + " ";
				t_text += this.touch_phase.phasetype.ToString().Substring(0,1) + " ";
				t_text += "rawid = " + this.touch_phase.raw_id.ToString() + " ";

				this.text.SetText(t_text);
			}

			/** [Touch_Phase_Key_Base]削除。
			*/
			public void OnRemove()
			{
				this.deleter.UnRegister(this.sprite);
				this.deleter.UnRegister(this.text);

				this.sprite.Delete();
				this.text.Delete();

				this.sprite = null;
				this.text = null;
			}
		};

		/** touchview_list
		*/
		private System.Collections.Generic.Dictionary<TouchView,Fee.Input.Touch_Phase> touchview_list;

		/** Start
		*/
		private void Start()
		{
			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			//Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			//Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.CreateInstance();

			//タッチ。
			Fee.Input.Touch.CreateInstance();
			Fee.Input.Touch.GetInstance().SetCallBack(CallBack_OnTouch);

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

			//背景。
			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;
			this.bg = new Fee.Render2D.Sprite2D(this.deleter,t_drawpriority);
			this.bg.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.bg.SetTexture(Texture2D.whiteTexture);
			this.bg.SetRect(ref Fee.Render2D.Render2D.VIRTUAL_RECT_MAX);
			this.bg.SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);
			this.bg.SetColor(0.0f,0.0f,0.0f,1.0f);

			//キー。
			this.touch_text = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
			this.touch_text.SetRect(750,50,300,0);
			this.touch_text.SetFontSize(17);
			
			//touchview_list
			this.touchview_list = Fee.Input.Touch.CreateTouchList<TouchView>();

			int t_xx = 10;
			int t_yy = 50;
			int t_w = 350;
			int t_h = 25;
			int t_space_h = 3;
			int t_separate_h = 6;
			int t_fontsize = 14;

			//mouseposition
			{
				this.button_inputsystem_touchscreen_touch = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.Touch_InputSystem_TouchScreen);
				this.button_inputsystem_touchscreen_touch.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_touchscreen_touch.SetText("InputSystem.TouchScreen => Touch");
				this.button_inputsystem_touchscreen_touch.SetFontSize(t_fontsize);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputtouch_touch = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.Touch_InputManager_InputTouch);
				this.button_inputmanager_inputtouch_touch.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputtouch_touch.SetText("InputManager.InputTouch => Touch");
				this.button_inputmanager_inputtouch_touch.SetFontSize(t_fontsize);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputmouse_touch = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.Touch_InputManager_InputMouse);
				this.button_inputmanager_inputmouse_touch.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputmouse_touch.SetText("InputManager.InputMouse => Touch");
				this.button_inputmanager_inputmouse_touch.SetFontSize(t_fontsize);
			}

			t_yy += t_h + t_separate_h;

			this.UpdateButtonStatus();
		}

		/** [Button_Base]コールバック。クリック。
		*/
		public void CallBack_Click(int a_id)
		{
			switch((ClickId)a_id)
			{
			case ClickId.Touch_InputSystem_TouchScreen:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH ^= true;
				}break;
			case ClickId.Touch_InputManager_InputTouch:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTTOUCH_TOUCH ^= true;
				}break;
			case ClickId.Touch_InputManager_InputMouse:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_TOUCH ^= true;
				}break;
			}

			this.UpdateButtonStatus();
		}

		/** UpdateButtonStatus
		*/
		public void UpdateButtonStatus()
		{
			this.button_inputsystem_touchscreen_touch.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH ? Data.Resources.UI_TEXTURE_BUTTON_ACTIVE : Data.Resources.UI_TEXTURE_BUTTON));
			this.button_inputmanager_inputtouch_touch.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_INPUTTOUCH_TOUCH ? Data.Resources.UI_TEXTURE_BUTTON_ACTIVE : Data.Resources.UI_TEXTURE_BUTTON));
			this.button_inputmanager_inputmouse_touch.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_TOUCH ? Data.Resources.UI_TEXTURE_BUTTON_ACTIVE : Data.Resources.UI_TEXTURE_BUTTON));
		}

		/** コールバック。
		*/
		public void CallBack_OnTouch(Fee.Input.Touch_Phase a_touch_phase)
		{
			this.touchview_id++;
			this.touchview_list.Add(new TouchView(this.touchview_id,this.deleter,a_touch_phase),a_touch_phase);
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//タッチ。
			Fee.Input.Touch.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());

			//タッチ。
			Fee.Input.Touch.UpdateTouchList(this.touchview_list);
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

