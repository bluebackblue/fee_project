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
	/** test05

		パッド
		タッチ

	*/
	public class test13 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test05",
				"test05",

				@"
				パッド
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
			Fex,


		}

		/** paddigitalbutton
		*/
		private Fee.Ui.Button button_inputsystem_gamepad_paddigitalbutton;
		private Fee.Ui.Button button_inputmanager_inputname_paddigitalbutton;

		/** paddigitalbutton
		*/
		private Fee.Ui.Button button_inputsystem_gamepad_padstick;
		private Fee.Ui.Button button_inputmanager_inputname_padstick;

		/** paddigitalbutton
		*/
		private Fee.Ui.Button button_inputsystem_gamepad_padtrigger;
		private Fee.Ui.Button button_inputmanager_inputname_padtrigger;

		/** paddigitalbutton
		*/
		private Fee.Ui.Button button_inputsystem_gamepad_padmotor;

		/** パッド。
		*/
		private Fee.Render2D.Text2D pad_text;

		/** タッチ。
		*/
		private Fee.Render2D.Text2D touch_text;


		/** タッチビューＩＤ。
		*/
		/*
		private int touchview_id;
		*/

		/** タッチビュー。
		*/
		#if(false)
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
		#endif

		/** touch_list
		*/
		/*
		private System.Collections.Generic.Dictionary<TouchView,Fee.Input.Touch_Phase> touch_list;
		*/

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
			{
				//マウス位置。
				Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = true;
				Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION = false;
				Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION = false;

				//マウスボタン。
				Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = true;
				Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON = false;
				Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON = false;

				//マウスホイール。
				Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL = true;
				Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL = false;
			}
			Fee.Input.Mouse.CreateInstance();

			//キー。インスタンス作成。
			{
				//キー。
				Fee.Input.Config.USE_INPUTSYSTEM_KEYBOARD_KEY = true;
				Fee.Input.Config.USE_INPUTMANAGER_GETKEY_KEY = false;
			}
			Fee.Input.Key.CreateInstance();

			//パッド。インスタンス作成。
			{
				//パッドデジタルボタン。
				Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON = true;
				Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON = true;

				//パッドスティック。
				Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADSTICK = true;
				Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADSTICK = false;

				//パッドトリガー。
				Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER = true;
				Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADTRIGGER = false;

				//パッドモーター。
				Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADMOTOR = true;
			}
			Fee.Input.Pad.CreateInstance();

			/*
			//タッチ。
			{
				Fee.Input.Config.USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH = true;
				Fee.Input.Config.USE_INPUTMANAGER_INPUTTOUCH_TOUCH = false;
				Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_TOUCH = false;
			}
			Fee.Input.Touch.CreateInstance();
			Fee.Input.Touch.GetInstance().SetCallBack(CallBack_OnTouch);
			*/

			//イベントプレート。
			Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

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

			//マウス。
			this.pad_text = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
			this.pad_text.SetRect(400,50,0,0);
			this.pad_text.SetFontSize(17);

			//キー。
			this.touch_text = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
			this.touch_text.SetRect(600,50,0,0);
			this.touch_text.SetFontSize(17);
			

			/*
			//テキスト。
			this.text_key = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
			this.text_key.SetRect(10,100 + 50 * 1,0,0);
			this.text_key.SetFontSize(20);

			//テキスト。
			this.text_pad_1 = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
			this.text_pad_1.SetRect(10,100 + 50 * 2,0,0);
			this.text_pad_1.SetFontSize(20);

			//テキスト。
			this.text_pad_2 = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
			this.text_pad_2.SetRect(10,100 + 50 * 3,0,0);
			this.text_pad_2.SetFontSize(20);


			//touch_list
			this.touch_list = Fee.Input.Touch.CreateTouchList<TouchView>();
			*/

			/*
			int t_xx = 10;
			int t_yy = 50;
			int t_w = 350;
			int t_h = 30;

			//mouseposition
			{
				this.button_inputsystem_mouse_mouseposition = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MousePosition_InputSystem_Mouse);
				this.button_inputsystem_mouse_mouseposition.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_mouse_mouseposition.SetText("InputSystem.Mouse => MousePosition");

				t_yy += 40;

				this.button_inputsystem_pointer_mouseposition = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MousePosition_InputSystem_Pointer);
				this.button_inputsystem_pointer_mouseposition.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_pointer_mouseposition.SetText("InputSystem.Pointer => MousePosition");

				t_yy += 40;

				this.button_inputmanager_inputmouse_mouseposition = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MousePosition_InputManager_InputMouse);
				this.button_inputmanager_inputmouse_mouseposition.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputmouse_mouseposition.SetText("InputManager.InputMouse => MousePosition");
			}

			t_yy += 60;

			//mousebutton
			{
				this.button_inputsystem_mouse_mousebutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MouseButton_InputSystem_Mouse);
				this.button_inputsystem_mouse_mousebutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_mouse_mousebutton.SetText("InputSystem.Mouse => MouseButton");

				t_yy += 40;

				this.button_inputsystem_pointer_mousebutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MouseButton_InputSystem_Pointer);
				this.button_inputsystem_pointer_mousebutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_pointer_mousebutton.SetText("InputSystem.Pointer => MouseButton");

				t_yy += 40;

				this.button_inputmanager_inputmouse_mousebutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MouseButton_InputManager_MouseButton);
				this.button_inputmanager_inputmouse_mousebutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputmouse_mousebutton.SetText("InputManager.InputMouse => MouseButton");
			}

			t_yy += 60;

			//mousewheel
			{
				this.button_inputsystem_mouse_mousewheel = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MouseWheel_InputSystem_Mouse);
				this.button_inputsystem_mouse_mousewheel.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_mouse_mousewheel.SetText("InputSystem.Mouse => MouseWheel");

				t_yy += 40;

				this.button_inputmanager_inputname_mousewheel = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MouseWheel_InputManager_InputName);
				this.button_inputmanager_inputname_mousewheel.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputname_mousewheel.SetText("InputManager.InputName => MouseWheel");

				t_yy += 40;
			}

			t_yy += 60;

			//key
			{
				this.button_inputsystem_keyboard_key = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.Key_InputSystem_KeyBoard);
				this.button_inputsystem_keyboard_key.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_keyboard_key.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
				this.button_inputsystem_keyboard_key.SetText("InputSystem.KeyBoard => Key");

				t_yy += 40;

				this.button_inputmanager_getkey_key = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.Key_InputManager_GetKey);
				this.button_inputmanager_getkey_key.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_getkey_key.SetText("InputManager.GetKey => Key");
			}
			*/

			this.UpdateButtonStatus();
		}

		/** [Button_Base]コールバック。クリック。
		*/
		public void CallBack_Click(int a_id)
		{
			/*
			switch((ClickId)a_id)
			{
			case ClickId.Fex:
				{
					this.SaveFlag();
					this.backup_rollbackcount = 0;
				}break;
			case ClickId.MousePosition_InputSystem_Mouse:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ClickId.MousePosition_InputSystem_Pointer:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ClickId.MousePosition_InputManager_InputMouse:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ClickId.MouseButton_InputSystem_Mouse:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ClickId.MouseButton_InputSystem_Pointer:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ClickId.MouseButton_InputManager_MouseButton:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ClickId.MouseWheel_InputSystem_Mouse:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ClickId.MouseWheel_InputManager_InputName:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ClickId.Key_InputSystem_KeyBoard:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_KEYBOARD_KEY ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ClickId.Key_InputManager_GetKey:
				{
					Fee.Input.Config.USE_INPUTMANAGER_GETKEY_KEY ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			}
			*/

			this.UpdateButtonStatus();
		}

		/** UpdateButtonStatus
		*/
		public void UpdateButtonStatus()
		{
			/*
			this.button_inputsystem_mouse_mouseposition.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputsystem_pointer_mouseposition.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputmanager_inputmouse_mouseposition.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));

			this.button_inputsystem_mouse_mousebutton.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputsystem_pointer_mousebutton.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputmanager_inputmouse_mousebutton.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));

			this.button_inputsystem_mouse_mousewheel.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputmanager_inputname_mousewheel.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));

			this.button_inputsystem_keyboard_key.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_KEYBOARD_KEY ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputmanager_getkey_key.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_GETKEY_KEY ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));;
			*/
		}

		/** コールバック。
		*/
		public void CallBack_OnTouch(Fee.Input.Touch_Phase a_touch_phase)
		{
			/*
			this.touchview_id++;
			this.touch_list.Add(new TouchView(this.touchview_id,this.deleter,a_touch_phase),a_touch_phase);
			*/
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
			Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//パッド。
			/*
			Fee.Input.Pad.GetInstance().Main(true);
			*/

			//タッチ。
			/*
			Fee.Input.Touch.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());
			*/

			//モーター。
			/*
			{
				Fee.Input.Pad.GetInstance().moter_low.Request(Fee.Input.Pad.GetInstance().l_trigger_2.value);
				Fee.Input.Pad.GetInstance().moter_high.Request(Fee.Input.Pad.GetInstance().r_trigger_2.value);
			}
			*/

			//タッチ。
			/*
			{
				Fee.Input.Touch.UpdateTouchList(this.touch_list);
			}
			*/

			//マウス。
			/*
			{
				string t_text = "";

				t_text += "left   [" + (Fee.Input.Mouse.GetInstance().left.on ? "o" : " ") + "]\n";
				t_text += "right  [" + (Fee.Input.Mouse.GetInstance().right.on ? "o" : " ") + "]\n";
				t_text += "middle [" + (Fee.Input.Mouse.GetInstance().middle.on ? "o" : " ") + "]\n";

				t_text += "x = " + Fee.Input.Mouse.GetInstance().pos.x.ToString() + "\n";
				t_text += "y = " + Fee.Input.Mouse.GetInstance().pos.y.ToString() + "\n";
				t_text += "m = " + Fee.Input.Mouse.GetInstance().mouse_wheel.y.ToString() + "\n";

				this.mouse_text.SetText(t_text);

				this.mouse_sprite.SetXY(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);
			}
			*/

			//キー。
			/*
			{
				string t_text = "";

				t_text += "left       [" + (Fee.Input.Key.GetInstance().left.on ? "o" : " ") + "]\n";
				t_text += "right      [" + (Fee.Input.Key.GetInstance().right.on ? "o" : " ") + "]\n";
				t_text += "up         [" + (Fee.Input.Key.GetInstance().up.on ? "o" : " ") + "]\n";
				t_text += "down       [" + (Fee.Input.Key.GetInstance().down.on ? "o" : " ") + "]\n";

				t_text += "l_left     [" + (Fee.Input.Key.GetInstance().l_left.on ? "o" : " ") + "]\n";
				t_text += "l_right    [" + (Fee.Input.Key.GetInstance().l_right.on ? "o" : " ") + "]\n";
				t_text += "l_up       [" + (Fee.Input.Key.GetInstance().l_up.on ? "o" : " ") + "]\n";
				t_text += "l_down     [" + (Fee.Input.Key.GetInstance().l_down.on ? "o" : " ") + "]\n";

				t_text += "r_left     [" + (Fee.Input.Key.GetInstance().r_left.on ? "o" : " ") + "]\n";
				t_text += "r_right    [" + (Fee.Input.Key.GetInstance().r_right.on ? "o" : " ") + "]\n";
				t_text += "r_up       [" + (Fee.Input.Key.GetInstance().r_up.on ? "o" : " ") + "]\n";
				t_text += "r_down     [" + (Fee.Input.Key.GetInstance().r_down.on ? "o" : " ") + "]\n";

				t_text += "enter      [" + (Fee.Input.Key.GetInstance().enter.on ? "o" : " ") + "]\n";
				t_text += "escape     [" + (Fee.Input.Key.GetInstance().escape.on ? "o" : " ") + "]\n";
				t_text += "sub1       [" + (Fee.Input.Key.GetInstance().sub1.on ? "o" : " ") + "]\n";
				t_text += "sub2       [" + (Fee.Input.Key.GetInstance().sub2.on ? "o" : " ") + "]\n";

				t_text += "left_menu  [" + (Fee.Input.Key.GetInstance().left_menu.on ? "o" : " ") + "]\n";
				t_text += "right_menu [" + (Fee.Input.Key.GetInstance().right_menu.on ? "o" : " ") + "]\n";

				t_text += "l_trigger_1 [" + (Fee.Input.Key.GetInstance().l_trigger_1.on ? "o" : " ") + "]\n";
				t_text += "r_trigger_1 [" + (Fee.Input.Key.GetInstance().r_trigger_1.on ? "o" : " ") + "]\n";
				t_text += "l_trigger_2 [" + (Fee.Input.Key.GetInstance().l_trigger_2.on ? "o" : " ") + "]\n";
				t_text += "r_trigger_2 [" + (Fee.Input.Key.GetInstance().r_trigger_2.on ? "o" : " ") + "]\n";

				this.key_text.SetText(t_text);
			}
			*/

			//パッド。
			/*
			{
				string t_text = "";

				if(Fee.Input.Pad.GetInstance().enter.on == true){
					t_text += "enter[o]";
				}else{
					t_text += "enter[ ]";
				}

				if(Fee.Input.Pad.GetInstance().escape.on == true){
					t_text += "escape[o]";
				}else{
					t_text += "escape[ ]";
				}

				if(Fee.Input.Pad.GetInstance().sub1.on == true){
					t_text += "sub1[o]";
				}else{
					t_text += "sub1[ ]";
				}

				if(Fee.Input.Pad.GetInstance().sub2.on == true){
					t_text += "sub2[o]";
				}else{
					t_text += "sub2[ ]";
				}

				t_text += " ";

				if(Fee.Input.Pad.GetInstance().left.on == true){
					t_text += "l[o]";
				}else{
					t_text += "l[ ]";
				}

				if(Fee.Input.Pad.GetInstance().right.on == true){
					t_text += "r[o]";
				}else{
					t_text += "r[ ]";
				}

				if(Fee.Input.Pad.GetInstance().up.on == true){
					t_text += "u[o]";
				}else{
					t_text += "u[ ]";
				}

				if(Fee.Input.Pad.GetInstance().down.on == true){
					t_text += "d[o]";
				}else{
					t_text += "d[ ]";
				}

				t_text += " ";

				if(Fee.Input.Pad.GetInstance().left_menu.on == true){
					t_text += "left_menu[o]";
				}else{
					t_text += "left_menu[ ]";
				}

				if(Fee.Input.Pad.GetInstance().right_menu.on == true){
					t_text += "right_menu[o]";
				}else{
					t_text += "right_menu[ ]";
				}

				this.text_pad_1.SetText(t_text);
			}
			*/

			/*
			{
				string t_text = "";

				if(Fee.Input.Pad.GetInstance().left_stick_button.on == true){
					t_text += "l_stick[o]";
				}else{
					t_text += "l_stick[ ]";
				}

				if(Fee.Input.Pad.GetInstance().right_stick_button.on == true){
					t_text += "r_stick[o]";
				}else{
					t_text += "r_stick[ ]";
				}

				if(Fee.Input.Pad.GetInstance().l_trigger_1.on == true){
					t_text += "l_trigger1[o]";
				}else{
					t_text += "l_trigger1[ ]";
				}

				if(Fee.Input.Pad.GetInstance().r_trigger_1.on == true){
					t_text += "r_trigger1[o]";
				}else{
					t_text += "r_trigger1[ ]";
				}

				if(Fee.Input.Pad.GetInstance().l_trigger_2.on == true){
					t_text += "l_trigger2[o]";
				}else{
					t_text += "l_trigger2[ ]";
				}

				if(Fee.Input.Pad.GetInstance().r_trigger_2.on == true){
					t_text += "r_trigger2[o]";
				}else{
					t_text += "r_trigger2[ ]";
				}

				t_text += "\n";

				t_text += "l stick = " + ((int)(Fee.Input.Pad.GetInstance().left_stick.x * 100)).ToString() + " " + ((int)(Fee.Input.Pad.GetInstance().left_stick.y * 100)).ToString() + "\n";

				t_text += "r stick = " + ((int)(Fee.Input.Pad.GetInstance().right_stick.x * 100)).ToString() + " " + ((int)(Fee.Input.Pad.GetInstance().right_stick.y * 100)).ToString() + "\n";

				t_text += "trigger2 = "+ ((int)(Fee.Input.Pad.GetInstance().l_trigger_2.value * 100)).ToString() + " " + ((int)(Fee.Input.Pad.GetInstance().r_trigger_2.value * 100)).ToString() + "\n";

				this.text_pad_2.SetText(t_text);
			}
			*/
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

