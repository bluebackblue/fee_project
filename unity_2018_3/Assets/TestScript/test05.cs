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


/** test05

	マウス
	ジョイスティック
	キー

*/
public class test05 : main_base
{
	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** 背景。
	*/
	private Fee.Render2D.Sprite2D sprite_bg;

	/** テキスト。
	*/
	private Fee.Render2D.Text2D text_mouse;

	/** テキスト。
	*/
	private Fee.Render2D.Text2D text_key;

	/** テキスト。
	*/
	private Fee.Render2D.Text2D text_pad_1;
	private Fee.Render2D.Text2D text_pad_2;

	/** mouse_sprite
	*/
	private Fee.Render2D.Sprite2D mouse_sprite;

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

			/*
			t_text += "pressure = " + this.touch_phase.pressure.ToString() + " ";
			t_text += "radius = " + this.touch_phase.radius.ToString() + " ";
			t_text += "altitude = " + this.touch_phase.angle_altitude.ToString() + " ";
			t_text += "azimuth = " + this.touch_phase.angle_azimuth.ToString() + " ";
			*/

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

	/** touch_list
	*/
	private System.Collections.Generic.Dictionary<TouchView,Fee.Input.Touch_Phase> touch_list;

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		Fee.TaskW.TaskW.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		//Fee.PerformanceCounter.Config.LOG_ENABLE = true;
		//Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。
		Fee.Render2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		Fee.Input.Mouse.CreateInstance();

		//キー。インスタンス作成。
		Fee.Input.Key.CreateInstance();

		//パッド。インスタンス作成。
		Fee.Input.Config.USE_INPUTMANAGER_GAMEPAD = false;
		Fee.Input.Config.USE_INPUTSYSTEM_GAMEPAD = true;
		Fee.Input.Pad.CreateInstance();

		//タッチ。
		Fee.Input.Touch.CreateInstance();
		Fee.Input.Touch.GetInstance().SetCallBack(CallBack_OnTouch);

		//イベントプレート。
		Fee.EventPlate.Config.LOG_ENABLE = true;
		Fee.EventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		Fee.Ui.Config.LOG_ENABLE = true;
		Fee.Ui.Ui.CreateInstance();

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//背景。
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;
		this.sprite_bg = new Fee.Render2D.Sprite2D(this.deleter,t_drawpriority);
		this.sprite_bg.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
		this.sprite_bg.SetTexture(Texture2D.whiteTexture);
		this.sprite_bg.SetRect(ref Fee.Render2D.Render2D.VIRTUAL_RECT_MAX);
		this.sprite_bg.SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);
		this.sprite_bg.SetColor(0.0f,0.0f,0.0f,1.0f);

		//テキスト。
		this.text_mouse = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
		this.text_mouse.SetRect(10,100 + 50 * 0,0,0);
		this.text_mouse.SetFontSize(17);

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

		//スプライト。
		this.mouse_sprite = new Fee.Render2D.Sprite2D(this.deleter,t_drawpriority + 1);
		this.mouse_sprite.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
		this.mouse_sprite.SetTexture(Texture2D.whiteTexture);
		this.mouse_sprite.SetRect(0,0,10,10);
		this.mouse_sprite.SetColor(1.0f,1.0f,1.0f,1.0f);

		//touch_list
		this.touch_list = Fee.Input.Touch.CreateTouchList<TouchView>();
	}

	/** コールバック。
	*/
	public void CallBack_OnTouch(Fee.Input.Touch_Phase a_touch_phase)
	{
		this.touchview_id++;
		this.touch_list.Add(new TouchView(this.touchview_id,this.deleter,a_touch_phase),a_touch_phase);
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		Fee.Input.Mouse.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());

		//キー。
		Fee.Input.Key.GetInstance().Main();

		//パッド。
		Fee.Input.Pad.GetInstance().Main();

		//タッチ。
		Fee.Input.Touch.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());

		//イベントプレート。
		Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

		//ＵＩ。
		Fee.Ui.Ui.GetInstance().Main();

		//モーター。
		{
			Fee.Input.Pad.GetInstance().moter_low.Request(Fee.Input.Pad.GetInstance().left_trigger2_button.value);
			Fee.Input.Pad.GetInstance().moter_high.Request(Fee.Input.Pad.GetInstance().right_trigger2_button.value);
		}

		//タッチ。
		{
			Fee.Input.Touch.UpdateTouchList(this.touch_list);
		}

		//マウス位置。
		{
			string t_text = "";

			if(Fee.Input.Mouse.GetInstance().left.on == true){
				t_text += "l[o]";
			}else{
				t_text += "l[ ]";
			}

			if(Fee.Input.Mouse.GetInstance().right.on == true){
				t_text += "r[o]";
			}else{
				t_text += "r[ ]";
			}

			if(Fee.Input.Mouse.GetInstance().middle.on == true){
				t_text += "m[o]";
			}else{
				t_text += "m[ ]";
			}

			t_text += "x = " + Fee.Input.Mouse.GetInstance().pos.x.ToString() + " ";
			t_text += "y = " + Fee.Input.Mouse.GetInstance().pos.y.ToString() + " ";
			t_text += "m = " + Fee.Input.Mouse.GetInstance().mouse_wheel.y.ToString() + " ";

			this.text_mouse.SetText(t_text);

			this.mouse_sprite.SetXY(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);
		}

		//キー。
		{
			string t_text = "";

			if(Fee.Input.Key.GetInstance().enter.on == true){
				t_text += "enter[o]";
			}else{
				t_text += "enter[ ]";
			}

			if(Fee.Input.Key.GetInstance().escape.on == true){
				t_text += "escape[o]";
			}else{
				t_text += "escape[ ]";
			}

			if(Fee.Input.Key.GetInstance().sub1.on == true){
				t_text += "sub1[o]";
			}else{
				t_text += "sub1[ ]";
			}

			if(Fee.Input.Key.GetInstance().sub2.on == true){
				t_text += "sub2[o]";
			}else{
				t_text += "sub2[ ]";
			}

			t_text += " ";

			if(Fee.Input.Key.GetInstance().l_left.on == true){
				t_text += "l[o]";
			}else{
				t_text += "l[ ]";
			}

			if(Fee.Input.Key.GetInstance().l_right.on == true){
				t_text += "r[o]";
			}else{
				t_text += "r[ ]";
			}

			if(Fee.Input.Key.GetInstance().l_up.on == true){
				t_text += "u[o]";
			}else{
				t_text += "u[ ]";
			}

			if(Fee.Input.Key.GetInstance().l_down.on == true){
				t_text += "d[o]";
			}else{
				t_text += "d[ ]";
			}

			t_text += " ";

			if(Fee.Input.Key.GetInstance().r_left.on == true){
				t_text += "l[o]";
			}else{
				t_text += "l[ ]";
			}

			if(Fee.Input.Key.GetInstance().r_right.on == true){
				t_text += "r[o]";
			}else{
				t_text += "r[ ]";
			}

			if(Fee.Input.Key.GetInstance().r_up.on == true){
				t_text += "u[o]";
			}else{
				t_text += "u[ ]";
			}

			if(Fee.Input.Key.GetInstance().r_down.on == true){
				t_text += "d[o]";
			}else{
				t_text += "d[ ]";
			}

			t_text += " ";

			if(Fee.Input.Key.GetInstance().left_menu.on == true){
				t_text += "left_menu[o]";
			}else{
				t_text += "left_menu[ ]";
			}

			if(Fee.Input.Key.GetInstance().right_menu.on == true){
				t_text += "right_menu[o]";
			}else{
				t_text += "right_menu[ ]";
			}

			this.text_key.SetText(t_text);
		}

		//パッド。
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

			if(Fee.Input.Pad.GetInstance().left_trigger1_button.on == true){
				t_text += "l_trigger1[o]";
			}else{
				t_text += "l_trigger1[ ]";
			}

			if(Fee.Input.Pad.GetInstance().right_trigger1_button.on == true){
				t_text += "r_trigger1[o]";
			}else{
				t_text += "r_trigger1[ ]";
			}

			if(Fee.Input.Pad.GetInstance().left_trigger2_button.on == true){
				t_text += "l_trigger2[o]";
			}else{
				t_text += "l_trigger2[ ]";
			}

			if(Fee.Input.Pad.GetInstance().right_trigger2_button.on == true){
				t_text += "r_trigger2[o]";
			}else{
				t_text += "r_trigger2[ ]";
			}

			t_text += "\n";

			t_text += "l stick = " + ((int)(Fee.Input.Pad.GetInstance().left_stick.x * 100)).ToString() + " " + ((int)(Fee.Input.Pad.GetInstance().left_stick.y * 100)).ToString() + "\n";

			t_text += "r stick = " + ((int)(Fee.Input.Pad.GetInstance().right_stick.x * 100)).ToString() + " " + ((int)(Fee.Input.Pad.GetInstance().right_stick.y * 100)).ToString() + "\n";

			t_text += "trigger2 = "+ ((int)(Fee.Input.Pad.GetInstance().left_trigger2_button.value * 100)).ToString() + " " + ((int)(Fee.Input.Pad.GetInstance().right_trigger2_button.value * 100)).ToString() + "\n";

			this.text_pad_2.SetText(t_text);
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

