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

		キー
		パッド
		マウス		

	*/
	public class test05 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test05",
				"test05",

				@"
				キー
				パッド
				マウス
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

			MousePosition_InputSystem_Mouse,
			MousePosition_InputSystem_Pointer,
			MousePosition_InputManager_InputMouse,

			MouseButton_InputSystem_Mouse,
			MouseButton_InputSystem_Pointer,
			MouseButton_InputManager_MouseButton,

			MouseWheel_InputSystem_Mouse,
			MouseWheel_InputManager_InputName,

			Key_InputSystem_KeyBoard,
			Key_InputManager_GetKey,

			PadDigitalButton_InputSystem_GamePad,
			PadDigitalButton_InputManager_InputName,

			PadStick_InputSystem_GamePad,
			PadStick_InputManager_InputName,

			PadTrigger_InputSystem_GamePad,
			PadTrigger_InputManager_InputName,

			PadMorotr_InputSystem_GamePad,
		}

		/** button_fix
		*/
		private Fee.Ui.Button button_fix;

		/** mouseposition
		*/
		private Fee.Ui.Button button_inputsystem_mouse_mouseposition;
		private Fee.Ui.Button button_inputsystem_pointer_mouseposition;
		private Fee.Ui.Button button_inputmanager_inputmouse_mouseposition;

		/** mousebutton
		*/
		private Fee.Ui.Button button_inputsystem_mouse_mousebutton;
		private Fee.Ui.Button button_inputsystem_pointer_mousebutton;
		private Fee.Ui.Button button_inputmanager_inputmouse_mousebutton;

		/** mousewheel
		*/
		private Fee.Ui.Button button_inputsystem_mouse_mousewheel;
		private Fee.Ui.Button button_inputmanager_inputname_mousewheel;

		/** key
		*/
		private Fee.Ui.Button button_inputsystem_keyboard_key;
		private Fee.Ui.Button button_inputmanager_getkey_key;

		/** paddigitalbutton
		*/
		private Fee.Ui.Button button_inputsystem_gamepad_paddigitalbutton;
		private Fee.Ui.Button button_inputmanager_inputname_paddigitalbutton;

		/** padstick
		*/
		private Fee.Ui.Button button_inputsystem_gamepad_padstick;
		private Fee.Ui.Button button_inputmanager_inputname_padstick;

		/** padtrigger
		*/
		private Fee.Ui.Button button_inputsystem_gamepad_padtrigger;
		private Fee.Ui.Button button_inputmanager_inputname_padtrigger;

		/** padmotor
		*/
		private Fee.Ui.Button button_inputsystem_gamepad_padmotor;

		/** マウス。
		*/
		private Fee.Render2D.Text2D mouse_text;

		/** マウス。
		*/
		private Fee.Render2D.Sprite2D mouse_sprite;

		/** キー。
		*/
		private Fee.Render2D.Text2D key_text;

		/** パッド。
		*/
		private Fee.Render2D.Text2D pad_text;

		/** バックアップ。ロールバックカウント。
		*/
		private int backup_rollbackcount;

		/** バックアップ。マウス位置。
		*/
		private bool backup_INPUTSYSTEM_MOUSE_MOUSEPOSITION = true;
		private bool backup_INPUTSYSTEM_POINTER_MOUSEPOSITION = false;
		private bool backup_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION = false;

		/** バックアップ。マウスボタン。
		*/
		private bool backup_INPUTSYSTEM_MOUSE_MOUSEBUTTON = true;
		private bool backup_INPUTSYSTEM_POINTER_MOUSEBUTTON = false;
		private bool backup_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON = false;

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
			Fee.Input.Mouse.CreateInstance();

			//キー。インスタンス作成。
			Fee.Input.Key.CreateInstance();

			//パッド。インスタンス作成。
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
			this.mouse_sprite = new Fee.Render2D.Sprite2D(this.deleter,t_drawpriority + 1);
			this.mouse_sprite.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.mouse_sprite.SetTexture(Texture2D.whiteTexture);
			this.mouse_sprite.SetRect(0,0,10,10);
			this.mouse_sprite.SetColor(1.0f,1.0f,1.0f,1.0f);

			//キー。
			this.key_text = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
			this.key_text.SetRect(400,50,0,0);
			this.key_text.SetFontSize(17);

			//パッド。
			this.pad_text = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
			this.pad_text.SetRect(570,50,0,0);
			this.pad_text.SetFontSize(17);

			//マウス。
			this.mouse_text = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
			this.mouse_text.SetRect(750,50,0,0);
			this.mouse_text.SetFontSize(17);


			/*
			//touch_list
			this.touch_list = Fee.Input.Touch.CreateTouchList<TouchView>();
			*/

			//button_fix
			{
				this.button_fix = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.Fex);
				this.button_fix.SetRect(100,1,100,40);
				this.button_fix.SetText("確定");
				this.button_fix.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			}

			int t_xx = 10;
			int t_yy = 50;
			int t_w = 350;
			int t_h = 25;
			int t_space_h = 3;
			int t_separate_h = 6;
			int t_fontsize = 14;

			//mouseposition
			{
				this.button_inputsystem_mouse_mouseposition = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MousePosition_InputSystem_Mouse);
				this.button_inputsystem_mouse_mouseposition.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_mouse_mouseposition.SetText("InputSystem.Mouse => MousePosition");
				this.button_inputsystem_mouse_mouseposition.SetFrontSize(t_fontsize);

				t_yy += t_h + t_space_h;

				this.button_inputsystem_pointer_mouseposition = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MousePosition_InputSystem_Pointer);
				this.button_inputsystem_pointer_mouseposition.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_pointer_mouseposition.SetText("InputSystem.Pointer => MousePosition");
				this.button_inputsystem_pointer_mouseposition.SetFrontSize(t_fontsize);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputmouse_mouseposition = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MousePosition_InputManager_InputMouse);
				this.button_inputmanager_inputmouse_mouseposition.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputmouse_mouseposition.SetText("InputManager.InputMouse => MousePosition");
				this.button_inputmanager_inputmouse_mouseposition.SetFrontSize(t_fontsize);
			}

			t_yy += t_h + t_separate_h;

			//mousebutton
			{
				this.button_inputsystem_mouse_mousebutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MouseButton_InputSystem_Mouse);
				this.button_inputsystem_mouse_mousebutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_mouse_mousebutton.SetText("InputSystem.Mouse => MouseButton");
				this.button_inputsystem_mouse_mousebutton.SetFrontSize(t_fontsize);

				t_yy += t_h + t_space_h;

				this.button_inputsystem_pointer_mousebutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MouseButton_InputSystem_Pointer);
				this.button_inputsystem_pointer_mousebutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_pointer_mousebutton.SetText("InputSystem.Pointer => MouseButton");
				this.button_inputsystem_pointer_mousebutton.SetFrontSize(t_fontsize);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputmouse_mousebutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MouseButton_InputManager_MouseButton);
				this.button_inputmanager_inputmouse_mousebutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputmouse_mousebutton.SetText("InputManager.InputMouse => MouseButton");
				this.button_inputmanager_inputmouse_mousebutton.SetFrontSize(t_fontsize);
			}

			t_yy += t_h + t_separate_h;

			//mousewheel
			{
				this.button_inputsystem_mouse_mousewheel = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MouseWheel_InputSystem_Mouse);
				this.button_inputsystem_mouse_mousewheel.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_mouse_mousewheel.SetText("InputSystem.Mouse => MouseWheel");
				this.button_inputsystem_mouse_mousewheel.SetFrontSize(t_fontsize);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputname_mousewheel = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.MouseWheel_InputManager_InputName);
				this.button_inputmanager_inputname_mousewheel.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputname_mousewheel.SetText("InputManager.InputName => MouseWheel");
				this.button_inputmanager_inputname_mousewheel.SetFrontSize(t_fontsize);
			}

			t_yy += t_h + t_separate_h;

			//key
			{
				this.button_inputsystem_keyboard_key = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.Key_InputSystem_KeyBoard);
				this.button_inputsystem_keyboard_key.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_keyboard_key.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
				this.button_inputsystem_keyboard_key.SetText("InputSystem.KeyBoard => Key");
				this.button_inputsystem_keyboard_key.SetFrontSize(t_fontsize);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_getkey_key = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.Key_InputManager_GetKey);
				this.button_inputmanager_getkey_key.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_getkey_key.SetText("InputManager.GetKey => Key");
				this.button_inputmanager_getkey_key.SetFrontSize(t_fontsize);
			}

			t_yy += t_h + t_separate_h;

			//PadDigitalButton
			{
				this.button_inputsystem_gamepad_paddigitalbutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.PadDigitalButton_InputSystem_GamePad);
				this.button_inputsystem_gamepad_paddigitalbutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_gamepad_paddigitalbutton.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
				this.button_inputsystem_gamepad_paddigitalbutton.SetText("InputSystem.GamePad => PadDigitalButton");
				this.button_inputsystem_gamepad_paddigitalbutton.SetFrontSize(t_fontsize);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputname_paddigitalbutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.PadDigitalButton_InputManager_InputName);
				this.button_inputmanager_inputname_paddigitalbutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputname_paddigitalbutton.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
				this.button_inputmanager_inputname_paddigitalbutton.SetText("InputManager.InputName => PadDigitalButton");
				this.button_inputmanager_inputname_paddigitalbutton.SetFrontSize(t_fontsize);
			}

			t_yy += t_h + t_separate_h;

			//PadStick
			{
				this.button_inputsystem_gamepad_padstick = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.PadStick_InputSystem_GamePad);
				this.button_inputsystem_gamepad_padstick.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_gamepad_padstick.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
				this.button_inputsystem_gamepad_padstick.SetText("InputSystem.GamePad => PadStick");
				this.button_inputsystem_gamepad_padstick.SetFrontSize(t_fontsize);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputname_padstick = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.PadStick_InputManager_InputName);
				this.button_inputmanager_inputname_padstick.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputname_padstick.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
				this.button_inputmanager_inputname_padstick.SetText("InputManager.InputName => PadStick");
				this.button_inputmanager_inputname_padstick.SetFrontSize(t_fontsize);
			}

			t_yy += t_h + t_separate_h;

			//PadTrigger
			{
				this.button_inputsystem_gamepad_padtrigger = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.PadTrigger_InputSystem_GamePad);
				this.button_inputsystem_gamepad_padtrigger.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_gamepad_padtrigger.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
				this.button_inputsystem_gamepad_padtrigger.SetText("InputSystem.GamePad => PadTrigger");
				this.button_inputsystem_gamepad_padtrigger.SetFrontSize(t_fontsize);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputname_padtrigger = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.PadTrigger_InputManager_InputName);
				this.button_inputmanager_inputname_padtrigger.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputname_padtrigger.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
				this.button_inputmanager_inputname_padtrigger.SetText("InputManager.InputName => PadTrigger");
				this.button_inputmanager_inputname_padtrigger.SetFrontSize(t_fontsize);
			}

			t_yy += t_h + t_separate_h;

			//PadMotor
			{
				this.button_inputsystem_gamepad_padmotor = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,(int)ClickId.PadMorotr_InputSystem_GamePad);
				this.button_inputsystem_gamepad_padmotor.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_gamepad_padmotor.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
				this.button_inputsystem_gamepad_padmotor.SetText("InputSystem.GamePad => PadMotor");
				this.button_inputsystem_gamepad_padmotor.SetFrontSize(t_fontsize);
			}

			this.UpdateButtonStatus();

			this.backup_rollbackcount = 0;
			this.SaveFlag();
		}

		/** SaveFlag
		*/
		public void SaveFlag()
		{
			//マウス位置。
			this.backup_INPUTSYSTEM_MOUSE_MOUSEPOSITION = Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION;
			this.backup_INPUTSYSTEM_POINTER_MOUSEPOSITION = Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION;
			this.backup_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION = Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION;

			//マウスボタン。
			this.backup_INPUTSYSTEM_MOUSE_MOUSEBUTTON = Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON;
			this.backup_INPUTSYSTEM_POINTER_MOUSEBUTTON = Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON;
			this.backup_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON = Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON;
		}

		/** LoadFlag
		*/
		public void LoadFlag()
		{
			//マウス位置。
			Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = this.backup_INPUTSYSTEM_MOUSE_MOUSEPOSITION;
			Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION = this.backup_INPUTSYSTEM_POINTER_MOUSEPOSITION;
			Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION = this.backup_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION;

			//マウスボタン。
			Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = this.backup_INPUTSYSTEM_MOUSE_MOUSEBUTTON;
			Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON = this.backup_INPUTSYSTEM_POINTER_MOUSEBUTTON;
			Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON = this.backup_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON;
		}

		/** [Button_Base]コールバック。クリック。
		*/
		public void CallBack_Click(int a_id)
		{
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
				}break;
			case ClickId.MouseWheel_InputManager_InputName:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL ^= true;
				}break;
			case ClickId.Key_InputSystem_KeyBoard:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_KEYBOARD_KEY ^= true;
				}break;
			case ClickId.Key_InputManager_GetKey:
				{
					Fee.Input.Config.USE_INPUTMANAGER_GETKEY_KEY ^= true;
				}break;
			case ClickId.PadDigitalButton_InputSystem_GamePad:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON ^= true;
				}break;
			case ClickId.PadDigitalButton_InputManager_InputName:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON ^= true;
				}break;
			case ClickId.PadStick_InputSystem_GamePad:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADSTICK ^= true;
				}break;
			case ClickId.PadStick_InputManager_InputName:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADSTICK ^= true;
				}break;
			case ClickId.PadTrigger_InputSystem_GamePad:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER ^= true;
				}break;
			case ClickId.PadTrigger_InputManager_InputName:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADTRIGGER ^= true;
				}break;
			case ClickId.PadMorotr_InputSystem_GamePad:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADMOTOR ^= true;
				}break;
			}

			this.UpdateButtonStatus();
		}

		/** UpdateButtonStatus
		*/
		public void UpdateButtonStatus()
		{
			this.button_inputsystem_mouse_mouseposition.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputsystem_pointer_mouseposition.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputmanager_inputmouse_mouseposition.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));

			this.button_inputsystem_mouse_mousebutton.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputsystem_pointer_mousebutton.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputmanager_inputmouse_mousebutton.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));

			this.button_inputsystem_mouse_mousewheel.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputmanager_inputname_mousewheel.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));

			this.button_inputsystem_keyboard_key.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_KEYBOARD_KEY ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputmanager_getkey_key.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_GETKEY_KEY ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));

			this.button_inputsystem_gamepad_paddigitalbutton.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputmanager_inputname_paddigitalbutton.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
		
			this.button_inputsystem_gamepad_padstick.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADSTICK ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputmanager_inputname_padstick.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADSTICK ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
		
			this.button_inputsystem_gamepad_padtrigger.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
			this.button_inputmanager_inputname_padtrigger.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADTRIGGER ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
		
			this.button_inputsystem_gamepad_padmotor.SetTexture(Resources.Load<Texture2D>(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADMOTOR ? Data.UI_TEXTURE_BUTTON_ACTIVE : Data.UI_TEXTURE_BUTTON));
		}

		/** コールバック。
		*/
		/*
		public void CallBack_OnTouch(Fee.Input.Touch_Phase a_touch_phase)
		{
			this.touchview_id++;
			this.touch_list.Add(new TouchView(this.touchview_id,this.deleter,a_touch_phase),a_touch_phase);
		}
		*/

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
			Fee.Input.Pad.GetInstance().Main(true);

			//モーター。
			{
				Fee.Input.Pad.GetInstance().moter_low.Request(Fee.Input.Pad.GetInstance().l_trigger_2.value);
				Fee.Input.Pad.GetInstance().moter_high.Request(Fee.Input.Pad.GetInstance().r_trigger_2.value);
			}

			//タッチ。
			/*
			Fee.Input.Touch.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());
			*/

			//タッチ。
			/*
			{
				Fee.Input.Touch.UpdateTouchList(this.touch_list);
			}
			*/

			if(this.backup_rollbackcount > 0){
				this.backup_rollbackcount--;
				if(this.backup_rollbackcount == 0){
					this.LoadFlag();
					this.UpdateButtonStatus();
				}
			}

			//確定ボタン。
			if(this.backup_rollbackcount == 0){
				this.button_fix.SetText("確定");
			}else{
				this.button_fix.SetText("確定(" + (this.backup_rollbackcount / 60).ToString() + ")");
			}

			//マウス。
			{
				string t_text = "mouse\n";

				t_text += "left   = " + Fee.Input.Mouse.GetInstance().left.on.ToString() + "\n";
				t_text += "right  = " + Fee.Input.Mouse.GetInstance().right.on.ToString() + "\n";
				t_text += "middle = " + Fee.Input.Mouse.GetInstance().middle.on.ToString() + "\n";

				t_text += "x = " + Fee.Input.Mouse.GetInstance().pos.x.ToString() + "\n";
				t_text += "y = " + Fee.Input.Mouse.GetInstance().pos.y.ToString() + "\n";
				t_text += "m = " + Fee.Input.Mouse.GetInstance().mouse_wheel.y.ToString() + "\n";

				this.mouse_text.SetText(t_text);

				this.mouse_sprite.SetXY(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);
			}

			//キー。
			{
				string t_text = "key\n";

				t_text += "left        = " + Fee.Input.Key.GetInstance().left.on.ToString() + "\n";
				t_text += "right       = " + Fee.Input.Key.GetInstance().right.on.ToString() + "\n";
				t_text += "up          = " + Fee.Input.Key.GetInstance().up.on.ToString() + "\n";
				t_text += "down        = " + Fee.Input.Key.GetInstance().down.on.ToString() + "\n";

				t_text += "enter       = " + Fee.Input.Key.GetInstance().enter.on.ToString() + "\n";
				t_text += "escape      = " + Fee.Input.Key.GetInstance().escape.on.ToString() + "\n";
				t_text += "sub1        = " + Fee.Input.Key.GetInstance().sub1.on.ToString() + "\n";
				t_text += "sub2        = " + Fee.Input.Key.GetInstance().sub2.on.ToString() + "\n";

				t_text += "left_menu   = " + Fee.Input.Key.GetInstance().left_menu.on.ToString() + "\n";
				t_text += "right_menu  = " + Fee.Input.Key.GetInstance().right_menu.on.ToString() + "\n";

				t_text += "l_trigger_1 = " + Fee.Input.Key.GetInstance().l_trigger_1.on.ToString() + "\n";
				t_text += "r_trigger_1 = " + Fee.Input.Key.GetInstance().r_trigger_1.on.ToString() + "\n";
				t_text += "l_trigger_2 = " + Fee.Input.Key.GetInstance().l_trigger_2.on.ToString() + "\n";
				t_text += "r_trigger_2 = " + Fee.Input.Key.GetInstance().r_trigger_2.on.ToString() + "\n";

				t_text += "l_left      = " + Fee.Input.Key.GetInstance().l_left.on.ToString() + "\n";
				t_text += "l_right     = " + Fee.Input.Key.GetInstance().l_right.on.ToString() + "\n";
				t_text += "l_up        = " + Fee.Input.Key.GetInstance().l_up.on.ToString() + "\n";
				t_text += "l_down      = " + Fee.Input.Key.GetInstance().l_down.on.ToString() + "\n";

				t_text += "r_left      = " + Fee.Input.Key.GetInstance().r_left.on.ToString() + "\n";
				t_text += "r_right     = " + Fee.Input.Key.GetInstance().r_right.on.ToString() + "\n";
				t_text += "r_up        = " + Fee.Input.Key.GetInstance().r_up.on.ToString() + "\n";
				t_text += "r_down      = " + Fee.Input.Key.GetInstance().r_down.on.ToString() + "\n";

				this.key_text.SetText(t_text);
			}

			//パッド。
			{
				string t_text = "";

				t_text += "left        = " + Fee.Input.Pad.GetInstance().left.on.ToString() + "\n";
				t_text += "right       = " + Fee.Input.Pad.GetInstance().right.on.ToString() + "\n";
				t_text += "up          = " + Fee.Input.Pad.GetInstance().up.on.ToString() + "\n";
				t_text += "down        = " + Fee.Input.Pad.GetInstance().down.on.ToString() + "\n";

				t_text += "enter       = " + Fee.Input.Pad.GetInstance().enter.on.ToString() + "\n";
				t_text += "escape      = " + Fee.Input.Pad.GetInstance().escape.on.ToString() + "\n";
				t_text += "sub1        = " + Fee.Input.Pad.GetInstance().sub1.on.ToString() + "\n";
				t_text += "sub2        = " + Fee.Input.Pad.GetInstance().sub2.on.ToString() + "\n";

				t_text += "left_menu   = " + Fee.Input.Pad.GetInstance().left_menu.on.ToString() + "\n";
				t_text += "right_menu  = " + Fee.Input.Pad.GetInstance().right_menu.on.ToString() + "\n";

				t_text += "l_trigger_1 = " + Fee.Input.Pad.GetInstance().l_trigger_1.on.ToString() + "\n";
				t_text += "r_trigger_1 = " + Fee.Input.Pad.GetInstance().r_trigger_1.on.ToString() + "\n";
				t_text += "l_trigger_2 = " + Fee.Input.Pad.GetInstance().l_trigger_2.on.ToString() + "\n";
				t_text += "r_trigger_2 = " + Fee.Input.Pad.GetInstance().r_trigger_2.on.ToString() + "\n";

				t_text += "l_trigger_2 = " + Fee.Input.Pad.GetInstance().l_trigger_2.value.ToString() + "\n";
				t_text += "r_trigger_2 = " + Fee.Input.Pad.GetInstance().r_trigger_2.value.ToString() + "\n";

				t_text += "left_stick   = " + Fee.Input.Pad.GetInstance().left_stick.x.ToString() + "\n";
				t_text += "left_stick   = " + Fee.Input.Pad.GetInstance().left_stick.y.ToString() + "\n";
				t_text += "right_stick  = " + Fee.Input.Pad.GetInstance().right_stick.x.ToString() + "\n";
				t_text += "right_stick  = " + Fee.Input.Pad.GetInstance().right_stick.y.ToString() + "\n";

				t_text += "left_stick_button  = " + Fee.Input.Pad.GetInstance().left_stick_button.on.ToString() + "\n";
				t_text += "right_stick_button = " + Fee.Input.Pad.GetInstance().right_stick_button.on.ToString() + "\n";

				this.pad_text.SetText(t_text);
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

