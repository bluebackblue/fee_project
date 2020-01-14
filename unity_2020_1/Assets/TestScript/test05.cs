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
	/** test05

		キー
		パッド
		マウス		

	*/
	public class test05 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test05.ButtonId>
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

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** 背景。
		*/
		private Fee.Render2D.Sprite2D bg;

		/** ButtonId
		*/
		public enum ButtonId
		{
			Fix,

			PadIndex,
			PadType,

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
		
		/** pad_type
		*/
		private Fee.Ui.Button pad_type_button;
		private Fee.Input.Pad_InputManagerItemName.PadType pad_type;

		/** pad_index
		*/
		private Fee.Ui.Button pad_index_button;
		private int pad_index;

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

		/** Start
		*/
		private void Start()
		{
			//プラットフォーム。インスタンス作成。
			Fee.Platform.Platform.CreateInstance();

			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			//Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			//Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.CreateInstance();

			//キー。インスタンス作成。
			Fee.Input.Key.CreateInstance();

			//パッド。インスタンス作成。
			Fee.Input.Pad.CreateInstance();

			//イベントプレート。
			Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//プレハブリスト。
			{
				this.prefablist = new Common.PrefabList();
				this.prefablist.LoadFontList();
				this.prefablist.LoadTextureList();
			}

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont("FONT"));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,this.prefablist.GetTexture("UI_BUTTON"),(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//背景。
			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;
			this.bg = Fee.Render2D.Sprite2D.Create(this.deleter,t_drawpriority);
			this.bg.SetTextureRect(in Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.bg.SetTexture(Texture2D.whiteTexture);
			this.bg.SetRect(in Fee.Render2D.Render2D.VIRTUAL_RECT_MAX);
			this.bg.SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);
			this.bg.SetColor(0.0f,0.0f,0.0f,1.0f);


			//マウス。
			this.mouse_sprite = Fee.Render2D.Sprite2D.Create(this.deleter,t_drawpriority + 1);
			this.mouse_sprite.SetTextureRect(in Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.mouse_sprite.SetTexture(Texture2D.whiteTexture);
			this.mouse_sprite.SetRect(0,0,10,10);
			this.mouse_sprite.SetColor(1.0f,1.0f,1.0f,1.0f);

			//キー。
			this.key_text = Fee.Render2D.Text2D.Create(this.deleter,t_drawpriority);
			this.key_text.SetRect(400,10,300,0);
			this.key_text.SetFontSize(16);

			//パッド。
			this.pad_text = Fee.Render2D.Text2D.Create(this.deleter,t_drawpriority);
			this.pad_text.SetRect(570,10,300,0);
			this.pad_text.SetFontSize(16);

			//マウス。
			this.mouse_text = Fee.Render2D.Text2D.Create(this.deleter,t_drawpriority);
			this.mouse_text.SetRect(750,10,300,0);
			this.mouse_text.SetFontSize(16);

			//button_fix
			{
				this.button_fix = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_fix.SetOnButtonClick(this,ButtonId.Fix);
				this.button_fix.SetRect(100,1,100,40);
				this.button_fix.SetText("確定");
				this.button_fix.SetTextureCornerSize(10);
				this.button_fix.SetNormalTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.button_fix.SetOnTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.button_fix.SetDownTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.button_fix.SetLockTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.button_fix.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.button_fix.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.button_fix.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.button_fix.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
			}

			//pad_type
			{
				this.pad_type = Fee.Input.Pad_InputManagerItemName.PadType.Type_X;

				this.pad_type_button = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.pad_type_button.SetOnButtonClick(this,ButtonId.PadType);
				this.pad_type_button.SetRect(210,1,50,40);
				this.pad_type_button.SetText(this.pad_type.ToString());
				this.pad_type_button.SetTextureCornerSize(10);
				this.pad_type_button.SetNormalTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.pad_type_button.SetOnTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.pad_type_button.SetDownTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.pad_type_button.SetLockTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.pad_type_button.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.pad_type_button.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.pad_type_button.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.pad_type_button.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
			}

			//pad_index
			{
				this.pad_index = 0;

				this.pad_index_button = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.pad_index_button.SetOnButtonClick(this,ButtonId.PadIndex);
				this.pad_index_button.SetRect(260,1,50,40);
				this.pad_index_button.SetText(this.pad_index.ToString());
				this.pad_index_button.SetTextureCornerSize(10);
				this.pad_index_button.SetNormalTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.pad_index_button.SetOnTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.pad_index_button.SetDownTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.pad_index_button.SetLockTexture(this.prefablist.GetTexture("UI_BUTTON"));
				this.pad_index_button.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.pad_index_button.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.pad_index_button.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.pad_index_button.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
			}

			for(int ii=0;ii<Fee.Input.Pad.GetInstance().pad_status.Length;ii++){
				if(ii == this.pad_index){
					//Fee.Input.Pad.GetInstance().pad_status[ii].enable = true;
				}else{
					//Fee.Input.Pad.GetInstance().pad_status[ii].enable = false;
				}
				Fee.Input.Pad.GetInstance().pad_status[ii].pad_type = this.pad_type;
			}

			int t_xx = 10;
			int t_yy = 50;
			int t_w = 350;
			int t_h = 25;
			int t_space_h = 3;
			int t_separate_h = 6;
			int t_fontsize = 15;

			//mouseposition
			{
				this.button_inputsystem_mouse_mouseposition = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputsystem_mouse_mouseposition.SetOnButtonClick(this,ButtonId.MousePosition_InputSystem_Mouse);
				this.button_inputsystem_mouse_mouseposition.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_mouse_mouseposition.SetText("InputSystem.Mouse => MousePosition");
				this.button_inputsystem_mouse_mouseposition.SetFontSize(t_fontsize);
				this.button_inputsystem_mouse_mouseposition.SetTextureCornerSize(10);

				t_yy += t_h + t_space_h;

				this.button_inputsystem_pointer_mouseposition = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputsystem_pointer_mouseposition.SetOnButtonClick(this,ButtonId.MousePosition_InputSystem_Pointer);
				this.button_inputsystem_pointer_mouseposition.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_pointer_mouseposition.SetText("InputSystem.Pointer => MousePosition");
				this.button_inputsystem_pointer_mouseposition.SetFontSize(t_fontsize);
				this.button_inputsystem_pointer_mouseposition.SetTextureCornerSize(10);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputmouse_mouseposition = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputmanager_inputmouse_mouseposition.SetOnButtonClick(this,ButtonId.MousePosition_InputManager_InputMouse);
				this.button_inputmanager_inputmouse_mouseposition.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputmouse_mouseposition.SetText("InputManager.InputMouse => MousePosition");
				this.button_inputmanager_inputmouse_mouseposition.SetFontSize(t_fontsize);
				this.button_inputmanager_inputmouse_mouseposition.SetTextureCornerSize(10);
			}

			t_yy += t_h + t_separate_h;

			//mousebutton
			{
				this.button_inputsystem_mouse_mousebutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputsystem_mouse_mousebutton.SetOnButtonClick(this,ButtonId.MouseButton_InputSystem_Mouse);
				this.button_inputsystem_mouse_mousebutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_mouse_mousebutton.SetText("InputSystem.Mouse => MouseButton");
				this.button_inputsystem_mouse_mousebutton.SetFontSize(t_fontsize);
				this.button_inputsystem_mouse_mousebutton.SetTextureCornerSize(10);

				t_yy += t_h + t_space_h;

				this.button_inputsystem_pointer_mousebutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputsystem_pointer_mousebutton.SetOnButtonClick(this,ButtonId.MouseButton_InputSystem_Pointer);
				this.button_inputsystem_pointer_mousebutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_pointer_mousebutton.SetText("InputSystem.Pointer => MouseButton");
				this.button_inputsystem_pointer_mousebutton.SetFontSize(t_fontsize);
				this.button_inputsystem_pointer_mousebutton.SetTextureCornerSize(10);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputmouse_mousebutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputmanager_inputmouse_mousebutton.SetOnButtonClick(this,ButtonId.MouseButton_InputManager_MouseButton);
				this.button_inputmanager_inputmouse_mousebutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputmouse_mousebutton.SetText("InputManager.InputMouse => MouseButton");
				this.button_inputmanager_inputmouse_mousebutton.SetFontSize(t_fontsize);
				this.button_inputmanager_inputmouse_mousebutton.SetTextureCornerSize(10);
			}

			t_yy += t_h + t_separate_h;

			//mousewheel
			{
				this.button_inputsystem_mouse_mousewheel = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputsystem_mouse_mousewheel.SetOnButtonClick(this,ButtonId.MouseWheel_InputSystem_Mouse);
				this.button_inputsystem_mouse_mousewheel.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_mouse_mousewheel.SetText("InputSystem.Mouse => MouseWheel");
				this.button_inputsystem_mouse_mousewheel.SetFontSize(t_fontsize);
				this.button_inputsystem_mouse_mousewheel.SetTextureCornerSize(10);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputname_mousewheel = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputmanager_inputname_mousewheel.SetOnButtonClick(this,ButtonId.MouseWheel_InputManager_InputName);
				this.button_inputmanager_inputname_mousewheel.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputname_mousewheel.SetText("InputManager.InputName => MouseWheel");
				this.button_inputmanager_inputname_mousewheel.SetFontSize(t_fontsize);
				this.button_inputmanager_inputname_mousewheel.SetTextureCornerSize(10);
			}

			t_yy += t_h + t_separate_h;

			//key
			{
				this.button_inputsystem_keyboard_key = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputsystem_keyboard_key.SetOnButtonClick(this,ButtonId.Key_InputSystem_KeyBoard);
				this.button_inputsystem_keyboard_key.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_keyboard_key.SetText("InputSystem.KeyBoard => Key");
				this.button_inputsystem_keyboard_key.SetFontSize(t_fontsize);
				this.button_inputsystem_keyboard_key.SetTextureCornerSize(10);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_getkey_key = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputmanager_getkey_key.SetOnButtonClick(this,ButtonId.Key_InputManager_GetKey);
				this.button_inputmanager_getkey_key.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_getkey_key.SetText("InputManager.GetKey => Key");
				this.button_inputmanager_getkey_key.SetFontSize(t_fontsize);
				this.button_inputmanager_getkey_key.SetTextureCornerSize(10);
			}

			t_yy += t_h + t_separate_h;

			//PadDigitalButton
			{
				this.button_inputsystem_gamepad_paddigitalbutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputsystem_gamepad_paddigitalbutton.SetOnButtonClick(this,ButtonId.PadDigitalButton_InputSystem_GamePad);
				this.button_inputsystem_gamepad_paddigitalbutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_gamepad_paddigitalbutton.SetText("InputSystem.GamePad => PadDigitalButton");
				this.button_inputsystem_gamepad_paddigitalbutton.SetFontSize(t_fontsize);
				this.button_inputsystem_gamepad_paddigitalbutton.SetTextureCornerSize(10);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputname_paddigitalbutton = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputmanager_inputname_paddigitalbutton.SetOnButtonClick(this,ButtonId.PadDigitalButton_InputManager_InputName);
				this.button_inputmanager_inputname_paddigitalbutton.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputname_paddigitalbutton.SetText("InputManager.InputName => PadDigitalButton");
				this.button_inputmanager_inputname_paddigitalbutton.SetFontSize(t_fontsize);
				this.button_inputmanager_inputname_paddigitalbutton.SetTextureCornerSize(10);
			}

			t_yy += t_h + t_separate_h;

			//PadStick
			{
				this.button_inputsystem_gamepad_padstick = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputsystem_gamepad_padstick.SetOnButtonClick(this,ButtonId.PadStick_InputSystem_GamePad);
				this.button_inputsystem_gamepad_padstick.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_gamepad_padstick.SetText("InputSystem.GamePad => PadStick");
				this.button_inputsystem_gamepad_padstick.SetFontSize(t_fontsize);
				this.button_inputsystem_gamepad_padstick.SetTextureCornerSize(10);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputname_padstick = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputmanager_inputname_padstick.SetOnButtonClick(this,ButtonId.PadStick_InputManager_InputName);
				this.button_inputmanager_inputname_padstick.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputname_padstick.SetText("InputManager.InputName => PadStick");
				this.button_inputmanager_inputname_padstick.SetFontSize(t_fontsize);
				this.button_inputmanager_inputname_padstick.SetTextureCornerSize(10);
			}

			t_yy += t_h + t_separate_h;

			//PadTrigger
			{
				this.button_inputsystem_gamepad_padtrigger = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputsystem_gamepad_padtrigger.SetOnButtonClick(this,ButtonId.PadTrigger_InputSystem_GamePad);
				this.button_inputsystem_gamepad_padtrigger.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_gamepad_padtrigger.SetText("InputSystem.GamePad => PadTrigger");
				this.button_inputsystem_gamepad_padtrigger.SetFontSize(t_fontsize);
				this.button_inputsystem_gamepad_padtrigger.SetTextureCornerSize(10);

				t_yy += t_h + t_space_h;

				this.button_inputmanager_inputname_padtrigger = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputmanager_inputname_padtrigger.SetOnButtonClick(this,ButtonId.PadTrigger_InputManager_InputName);
				this.button_inputmanager_inputname_padtrigger.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputmanager_inputname_padtrigger.SetText("InputManager.InputName => PadTrigger");
				this.button_inputmanager_inputname_padtrigger.SetFontSize(t_fontsize);
				this.button_inputmanager_inputname_padtrigger.SetTextureCornerSize(10);
			}

			t_yy += t_h + t_separate_h;

			//PadMotor
			{
				this.button_inputsystem_gamepad_padmotor = new Fee.Ui.Button(this.deleter,t_drawpriority + 1);
				this.button_inputsystem_gamepad_padmotor.SetOnButtonClick(this,ButtonId.PadMorotr_InputSystem_GamePad);
				this.button_inputsystem_gamepad_padmotor.SetRect(t_xx,t_yy,t_w,t_h);
				this.button_inputsystem_gamepad_padmotor.SetText("InputSystem.GamePad => PadMotor");
				this.button_inputsystem_gamepad_padmotor.SetFontSize(t_fontsize);
				this.button_inputsystem_gamepad_padmotor.SetTextureCornerSize(10);
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

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			switch(a_id)
			{
			case ButtonId.PadType:
				{
					this.pad_type = (Fee.Input.Pad_InputManagerItemName.PadType)(((int)this.pad_type + 1) % (int)Fee.Input.Pad_InputManagerItemName.PadType.Max);
					this.pad_type_button.SetText(this.pad_type.ToString());

					for(int ii=0;ii<Fee.Input.Pad.GetInstance().pad_status.Length;ii++){
						if(ii == this.pad_index){
							//Fee.Input.Pad.GetInstance().pad_status[ii].enable = true;
						}else{
							//Fee.Input.Pad.GetInstance().pad_status[ii].enable = false;
						}
						Fee.Input.Pad.GetInstance().pad_status[ii].pad_type = this.pad_type;
					}
				}break;
			case ButtonId.PadIndex:
				{
					this.pad_index = (this.pad_index + 1) % (Fee.Input.Pad.GetInstance().pad_status.Length);
					this.pad_index_button.SetText(this.pad_index.ToString());

					for(int ii=0;ii<Fee.Input.Pad.GetInstance().pad_status.Length;ii++){
						if(ii == this.pad_index){
							//Fee.Input.Pad.GetInstance().pad_status[ii].enable = true;
						}else{
							//Fee.Input.Pad.GetInstance().pad_status[ii].enable = false;
						}
						Fee.Input.Pad.GetInstance().pad_status[ii].pad_type = this.pad_type;
					}
				}break;
			case ButtonId.Fix:
				{
					this.SaveFlag();
					this.backup_rollbackcount = 0;
				}break;
			case ButtonId.MousePosition_InputSystem_Mouse:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ButtonId.MousePosition_InputSystem_Pointer:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ButtonId.MousePosition_InputManager_InputMouse:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ButtonId.MouseButton_InputSystem_Mouse:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ButtonId.MouseButton_InputSystem_Pointer:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ButtonId.MouseButton_InputManager_MouseButton:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON ^= true;
					this.backup_rollbackcount = 60 * 15;
				}break;
			case ButtonId.MouseWheel_InputSystem_Mouse:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL ^= true;
				}break;
			case ButtonId.MouseWheel_InputManager_InputName:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL ^= true;
				}break;
			case ButtonId.Key_InputSystem_KeyBoard:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_KEYBOARD_KEY ^= true;
				}break;
			case ButtonId.Key_InputManager_GetKey:
				{
					Fee.Input.Config.USE_INPUTMANAGER_GETKEY_KEY ^= true;
				}break;
			case ButtonId.PadDigitalButton_InputSystem_GamePad:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON ^= true;
				}break;
			case ButtonId.PadDigitalButton_InputManager_InputName:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON ^= true;
				}break;
			case ButtonId.PadStick_InputSystem_GamePad:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADSTICK ^= true;
				}break;
			case ButtonId.PadStick_InputManager_InputName:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADSTICK ^= true;
				}break;
			case ButtonId.PadTrigger_InputSystem_GamePad:
				{
					Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER ^= true;
				}break;
			case ButtonId.PadTrigger_InputManager_InputName:
				{
					Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADTRIGGER ^= true;
				}break;
			case ButtonId.PadMorotr_InputSystem_GamePad:
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
			this.button_inputsystem_mouse_mouseposition.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mouseposition.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mouseposition.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mouseposition.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mouseposition.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputsystem_mouse_mouseposition.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputsystem_mouse_mouseposition.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputsystem_mouse_mouseposition.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputsystem_pointer_mouseposition.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_pointer_mouseposition.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_pointer_mouseposition.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_pointer_mouseposition.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_pointer_mouseposition.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputsystem_pointer_mouseposition.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputsystem_pointer_mouseposition.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputsystem_pointer_mouseposition.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputmanager_inputmouse_mouseposition.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputmouse_mouseposition.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputmouse_mouseposition.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputmouse_mouseposition.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputmouse_mouseposition.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputmanager_inputmouse_mouseposition.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputmanager_inputmouse_mouseposition.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputmanager_inputmouse_mouseposition.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputsystem_mouse_mousebutton.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mousebutton.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mousebutton.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mousebutton.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mousebutton.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputsystem_mouse_mousebutton.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputsystem_mouse_mousebutton.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputsystem_mouse_mousebutton.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputsystem_pointer_mousebutton.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_pointer_mousebutton.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_pointer_mousebutton.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_pointer_mousebutton.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_pointer_mousebutton.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputsystem_pointer_mousebutton.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputsystem_pointer_mousebutton.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputsystem_pointer_mousebutton.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputmanager_inputmouse_mousebutton.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputmouse_mousebutton.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputmouse_mousebutton.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputmouse_mousebutton.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputmouse_mousebutton.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputmanager_inputmouse_mousebutton.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputmanager_inputmouse_mousebutton.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputmanager_inputmouse_mousebutton.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputsystem_mouse_mousewheel.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mousewheel.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mousewheel.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mousewheel.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_mouse_mousewheel.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputsystem_mouse_mousewheel.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputsystem_mouse_mousewheel.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputsystem_mouse_mousewheel.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputmanager_inputname_mousewheel.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_mousewheel.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_mousewheel.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_mousewheel.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_mousewheel.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputmanager_inputname_mousewheel.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputmanager_inputname_mousewheel.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputmanager_inputname_mousewheel.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputsystem_keyboard_key.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_KEYBOARD_KEY ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_keyboard_key.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_KEYBOARD_KEY ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_keyboard_key.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_KEYBOARD_KEY ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_keyboard_key.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_KEYBOARD_KEY ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_keyboard_key.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputsystem_keyboard_key.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputsystem_keyboard_key.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputsystem_keyboard_key.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputmanager_getkey_key.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_GETKEY_KEY ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_getkey_key.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_GETKEY_KEY ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_getkey_key.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_GETKEY_KEY ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_getkey_key.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_GETKEY_KEY ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_getkey_key.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputmanager_getkey_key.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputmanager_getkey_key.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputmanager_getkey_key.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputsystem_gamepad_paddigitalbutton.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_paddigitalbutton.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_paddigitalbutton.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_paddigitalbutton.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_paddigitalbutton.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputsystem_gamepad_paddigitalbutton.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputsystem_gamepad_paddigitalbutton.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputsystem_gamepad_paddigitalbutton.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputmanager_inputname_paddigitalbutton.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_paddigitalbutton.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_paddigitalbutton.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_paddigitalbutton.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_paddigitalbutton.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputmanager_inputname_paddigitalbutton.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputmanager_inputname_paddigitalbutton.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputmanager_inputname_paddigitalbutton.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputsystem_gamepad_padstick.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADSTICK ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padstick.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADSTICK ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padstick.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADSTICK ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padstick.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADSTICK ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padstick.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputsystem_gamepad_padstick.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputsystem_gamepad_padstick.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputsystem_gamepad_padstick.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputmanager_inputname_padstick.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADSTICK ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_padstick.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADSTICK ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_padstick.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADSTICK ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_padstick.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADSTICK ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_padstick.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputmanager_inputname_padstick.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputmanager_inputname_padstick.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputmanager_inputname_padstick.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputsystem_gamepad_padtrigger.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padtrigger.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padtrigger.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padtrigger.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padtrigger.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputsystem_gamepad_padtrigger.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputsystem_gamepad_padtrigger.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputsystem_gamepad_padtrigger.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputmanager_inputname_padtrigger.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADTRIGGER ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_padtrigger.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADTRIGGER ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_padtrigger.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADTRIGGER ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_padtrigger.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTMANAGER_INPUTNAME_PADTRIGGER ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputmanager_inputname_padtrigger.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputmanager_inputname_padtrigger.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputmanager_inputname_padtrigger.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputmanager_inputname_padtrigger.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			this.button_inputsystem_gamepad_padmotor.SetNormalTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADMOTOR ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padmotor.SetOnTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADMOTOR ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padmotor.SetDownTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADMOTOR ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padmotor.SetLockTexture(this.prefablist.GetTexture(Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD_PADMOTOR ? "UI_BUTTON_ACTIVE" : "UI_BUTTON"));
			this.button_inputsystem_gamepad_padmotor.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_inputsystem_gamepad_padmotor.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_inputsystem_gamepad_padmotor.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_inputsystem_gamepad_padmotor.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(this.is_focus,Fee.Render2D.Render2D.GetInstance());

			//キー。
			Fee.Input.Key.GetInstance().Main(true);

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//パッド。
			Fee.Input.Pad.GetInstance().Main(true);

			//モーター。
			for(int ii=0;ii<Fee.Input.Pad.GetInstance().pad_status.Length;ii++){
				Fee.Input.Pad.GetInstance().pad_status[ii].moter_low.Request(Fee.Input.Pad.GetInstance().pad_status[ii].l_trigger_2.value);
				Fee.Input.Pad.GetInstance().pad_status[ii].moter_high.Request(Fee.Input.Pad.GetInstance().pad_status[ii].r_trigger_2.value);
			}

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

				t_text += "x = " + Fee.Input.Mouse.GetInstance().cursor.pos.x.ToString() + "\n";
				t_text += "y = " + Fee.Input.Mouse.GetInstance().cursor.pos.y.ToString() + "\n";
				t_text += "m = " + Fee.Input.Mouse.GetInstance().mouse_wheel.pos.y.ToString() + "\n";

				t_text += "name = " + Fee.Input.Mouse.GetInstance().cursor.devicename.ToLower() + "\n";

				this.mouse_text.SetText(t_text);

				this.mouse_sprite.SetXY(Fee.Input.Mouse.GetInstance().cursor.pos.x,Fee.Input.Mouse.GetInstance().cursor.pos.y);
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
				string t_text = "pad : " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].devicename + "\n";

				t_text += "left        = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].left.on.ToString() + "\n";
				t_text += "right       = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].right.on.ToString() + "\n";
				t_text += "up          = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].up.on.ToString() + "\n";
				t_text += "down        = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].down.on.ToString() + "\n";

				t_text += "enter       = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].enter.on.ToString() + "\n";
				t_text += "escape      = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].escape.on.ToString() + "\n";
				t_text += "sub1        = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].sub1.on.ToString() + "\n";
				t_text += "sub2        = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].sub2.on.ToString() + "\n";

				t_text += "left_menu   = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].left_menu.on.ToString() + "\n";
				t_text += "right_menu  = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].right_menu.on.ToString() + "\n";

				t_text += "l_trigger_1 = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].l_trigger_1.on.ToString() + "\n";
				t_text += "r_trigger_1 = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].r_trigger_1.on.ToString() + "\n";
				t_text += "l_trigger_2 = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].l_trigger_2.on.ToString() + "\n";
				t_text += "r_trigger_2 = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].r_trigger_2.on.ToString() + "\n";

				t_text += "l_trigger_2 = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].l_trigger_2.value.ToString() + "\n";
				t_text += "r_trigger_2 = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].r_trigger_2.value.ToString() + "\n";

				t_text += "l_stick_x   = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].l_stick.x.ToString() + "\n";
				t_text += "l_stick_y   = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].l_stick.y.ToString() + "\n";
				t_text += "r_stick_x   = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].r_stick.x.ToString() + "\n";
				t_text += "r_stick_y   = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].r_stick.y.ToString() + "\n";

				t_text += "l_stick_button = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].l_stick_button.on.ToString() + "\n";
				t_text += "r_stick_button = " + Fee.Input.Pad.GetInstance().pad_status[this.pad_index].r_stick_button.on.ToString() + "\n";

				this.pad_text.SetText(t_text);
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

