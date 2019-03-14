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
	private NDeleter.Deleter deleter;

	/** 背景。
	*/
	private NRender2D.Sprite2D sprite_bg;

	/** テキスト。
	*/
	private NRender2D.Text2D text_mouse;

	/** テキスト。
	*/
	private NRender2D.Text2D text_key;

	/** テキスト。
	*/
	private NRender2D.Text2D text_pad_1;
	private NRender2D.Text2D text_pad_2;

	/** mouse_sprite
	*/
	private NRender2D.Sprite2D mouse_sprite;

	/** タッチビューＩＤ。
	*/
	private int touchview_id;

	/** タッチビュー。
	*/
	class TouchView : NInput.Touch_Phase_Key_Base
	{
		public int id;
		public NRender2D.Sprite2D sprite;
		public NRender2D.Text2D text;
		public NInput.Touch_Phase touch_phase;
		public NDeleter.Deleter deleter;

		/** constructor
		*/
		public TouchView(int a_id,NDeleter.Deleter a_deleter,NInput.Touch_Phase a_touch_phase)
		{
			this.id = a_id;
			this.deleter = a_deleter;
			this.touch_phase = a_touch_phase;

			this.sprite = new NRender2D.Sprite2D(this.deleter,null,1);
			{
				int t_size = 100;

				this.sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
				this.sprite.SetTexture(Texture2D.whiteTexture);
				this.sprite.SetColor(Random.value,Random.value,Random.value,1.0f);
				this.sprite.SetRect(this.touch_phase.value_x-t_size/2,this.touch_phase.value_y-t_size/2,t_size,t_size);
			}

			this.text = new NRender2D.Text2D(this.deleter,null,1);
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
	private Dictionary<TouchView,NInput.Touch_Phase> touch_list;

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		NTaskW.TaskW.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		//NPerformanceCounter.Config.LOG_ENABLE = true;
		//NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//キー。インスタンス作成。
		NInput.Key.CreateInstance();

		//パッド。インスタンス作成。
		NInput.Pad.CreateInstance();

		//タッチ。
		NInput.Touch.CreateInstance();
		NInput.Touch.GetInstance().SetCallBack(CallBack_OnTouch);

		//イベントプレート。
		NEventPlate.Config.LOG_ENABLE = true;
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Config.LOG_ENABLE = true;
		NUi.Ui.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(NRender2D.Render2D.MAX_LAYER - 1) * NRender2D.Render2D.DRAWPRIORITY_STEP);

		//背景。
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;
		this.sprite_bg = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority);
		this.sprite_bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
		this.sprite_bg.SetTexture(Texture2D.whiteTexture);
		this.sprite_bg.SetRect(ref NRender2D.Render2D.VIRTUAL_RECT_MAX);
		this.sprite_bg.SetMaterialType(NRender2D.Config.MaterialType.Alpha);
		this.sprite_bg.SetColor(0.0f,0.0f,0.0f,1.0f);

		//テキスト。
		this.text_mouse = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text_mouse.SetRect(10,100 + 50 * 0,0,0);
		this.text_mouse.SetFontSize(17);

		//テキスト。
		this.text_key = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text_key.SetRect(10,100 + 50 * 1,0,0);
		this.text_key.SetFontSize(20);

		//テキスト。
		this.text_pad_1 = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text_pad_1.SetRect(10,100 + 50 * 2,0,0);
		this.text_pad_1.SetFontSize(20);

		//テキスト。
		this.text_pad_2 = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text_pad_2.SetRect(10,100 + 50 * 3,0,0);
		this.text_pad_2.SetFontSize(20);

		//スプライト。
		this.mouse_sprite = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority + 1);
		this.mouse_sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
		this.mouse_sprite.SetTexture(Texture2D.whiteTexture);
		this.mouse_sprite.SetRect(0,0,10,10);
		this.mouse_sprite.SetColor(1.0f,1.0f,1.0f,1.0f);

		//touch_list
		this.touch_list = NInput.Touch.CreateTouchList<TouchView>();
	}

	/** コールバック。
	*/
	public void CallBack_OnTouch(NInput.Touch_Phase a_touch_phase)
	{
		this.touchview_id++;
		this.touch_list.Add(new TouchView(this.touchview_id,this.deleter,a_touch_phase),a_touch_phase);
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//キー。
		NInput.Key.GetInstance().Main();

		//パッド。
		NInput.Pad.GetInstance().Main();

		//タッチ。
		NInput.Touch.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//モーター。
		{
			NInput.Pad.GetInstance().moter_low.Request(NInput.Pad.GetInstance().left_trigger2_button.value);
			NInput.Pad.GetInstance().moter_high.Request(NInput.Pad.GetInstance().right_trigger2_button.value);
		}

		//タッチ。
		{
			NInput.Touch.UpdateTouchList(this.touch_list);
		}

		//マウス位置。
		{
			string t_text = "";

			if(NInput.Mouse.GetInstance().left.on == true){
				t_text += "l[o]";
			}else{
				t_text += "l[ ]";
			}

			if(NInput.Mouse.GetInstance().right.on == true){
				t_text += "r[o]";
			}else{
				t_text += "r[ ]";
			}

			if(NInput.Mouse.GetInstance().middle.on == true){
				t_text += "m[o]";
			}else{
				t_text += "m[ ]";
			}

			t_text += "x = " + NInput.Mouse.GetInstance().pos.x.ToString() + " ";
			t_text += "y = " + NInput.Mouse.GetInstance().pos.y.ToString() + " ";
			t_text += "m = " + NInput.Mouse.GetInstance().mouse_wheel.y.ToString() + " ";

			this.text_mouse.SetText(t_text);

			this.mouse_sprite.SetXY(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);
		}

		//キー。
		{
			string t_text = "";

			if(NInput.Key.GetInstance().enter.on == true){
				t_text += "enter[o]";
			}else{
				t_text += "enter[ ]";
			}

			if(NInput.Key.GetInstance().escape.on == true){
				t_text += "escape[o]";
			}else{
				t_text += "escape[ ]";
			}

			if(NInput.Key.GetInstance().sub1.on == true){
				t_text += "sub1[o]";
			}else{
				t_text += "sub1[ ]";
			}

			if(NInput.Key.GetInstance().sub2.on == true){
				t_text += "sub2[o]";
			}else{
				t_text += "sub2[ ]";
			}

			t_text += " ";

			if(NInput.Key.GetInstance().left.on == true){
				t_text += "l[o]";
			}else{
				t_text += "l[ ]";
			}

			if(NInput.Key.GetInstance().right.on == true){
				t_text += "r[o]";
			}else{
				t_text += "r[ ]";
			}

			if(NInput.Key.GetInstance().up.on == true){
				t_text += "u[o]";
			}else{
				t_text += "u[ ]";
			}

			if(NInput.Key.GetInstance().down.on == true){
				t_text += "d[o]";
			}else{
				t_text += "d[ ]";
			}

			t_text += " ";

			if(NInput.Key.GetInstance().left_menu.on == true){
				t_text += "left_menu[o]";
			}else{
				t_text += "left_menu[ ]";
			}

			if(NInput.Key.GetInstance().right_menu.on == true){
				t_text += "right_menu[o]";
			}else{
				t_text += "right_menu[ ]";
			}

			this.text_key.SetText(t_text);
		}

		//パッド。
		{
			string t_text = "";

			if(NInput.Pad.GetInstance().enter.on == true){
				t_text += "enter[o]";
			}else{
				t_text += "enter[ ]";
			}

			if(NInput.Pad.GetInstance().escape.on == true){
				t_text += "escape[o]";
			}else{
				t_text += "escape[ ]";
			}

			if(NInput.Pad.GetInstance().sub1.on == true){
				t_text += "sub1[o]";
			}else{
				t_text += "sub1[ ]";
			}

			if(NInput.Pad.GetInstance().sub2.on == true){
				t_text += "sub2[o]";
			}else{
				t_text += "sub2[ ]";
			}

			t_text += " ";

			if(NInput.Pad.GetInstance().left.on == true){
				t_text += "l[o]";
			}else{
				t_text += "l[ ]";
			}

			if(NInput.Pad.GetInstance().right.on == true){
				t_text += "r[o]";
			}else{
				t_text += "r[ ]";
			}

			if(NInput.Pad.GetInstance().up.on == true){
				t_text += "u[o]";
			}else{
				t_text += "u[ ]";
			}

			if(NInput.Pad.GetInstance().down.on == true){
				t_text += "d[o]";
			}else{
				t_text += "d[ ]";
			}

			t_text += " ";

			if(NInput.Pad.GetInstance().left_menu.on == true){
				t_text += "left_menu[o]";
			}else{
				t_text += "left_menu[ ]";
			}

			if(NInput.Pad.GetInstance().right_menu.on == true){
				t_text += "right_menu[o]";
			}else{
				t_text += "right_menu[ ]";
			}

			this.text_pad_1.SetText(t_text);
		}

		{
			string t_text = "";

			if(NInput.Pad.GetInstance().left_stick_button.on == true){
				t_text += "l_stick[o]";
			}else{
				t_text += "l_stick[ ]";
			}

			if(NInput.Pad.GetInstance().right_stick_button.on == true){
				t_text += "r_stick[o]";
			}else{
				t_text += "r_stick[ ]";
			}

			if(NInput.Pad.GetInstance().left_trigger1_button.on == true){
				t_text += "l_trigger1[o]";
			}else{
				t_text += "l_trigger1[ ]";
			}

			if(NInput.Pad.GetInstance().right_trigger1_button.on == true){
				t_text += "r_trigger1[o]";
			}else{
				t_text += "r_trigger1[ ]";
			}

			if(NInput.Pad.GetInstance().left_trigger2_button.on == true){
				t_text += "l_trigger2[o]";
			}else{
				t_text += "l_trigger2[ ]";
			}

			if(NInput.Pad.GetInstance().right_trigger2_button.on == true){
				t_text += "r_trigger2[o]";
			}else{
				t_text += "r_trigger2[ ]";
			}

			t_text += "\n";

			t_text += "l stick = " + ((int)(NInput.Pad.GetInstance().left_stick.x * 100)).ToString() + " " + ((int)(NInput.Pad.GetInstance().left_stick.y * 100)).ToString() + "\n";

			t_text += "r stick = " + ((int)(NInput.Pad.GetInstance().right_stick.x * 100)).ToString() + " " + ((int)(NInput.Pad.GetInstance().right_stick.y * 100)).ToString() + "\n";

			t_text += "trigger2 = "+ ((int)(NInput.Pad.GetInstance().left_trigger2_button.value * 100)).ToString() + " " + ((int)(NInput.Pad.GetInstance().right_trigger2_button.value * 100)).ToString() + "\n";

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

