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


/** test10

	ブルーム
	ブラー

*/
public class test10 : main_base
{
	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** sprite
	*/
	private Fee.Render2D.Sprite2D sprite;

	/** ブルーム。
	*/
	private Fee.Ui.Button bloom_button;
	private Fee.Ui.Slider bloom_threshold_slider;
	private Fee.Ui.Slider bloom_intensity_slider;

	/** ブラー。
	*/
	private Fee.Ui.Button blur_button;
	private Fee.Ui.Slider blur_rate_blend_slider;

	/** depthchange_button
	*/
	private Fee.Ui.Button depthchange_button;

	/** デプス。
	*/
	private Fee.Ui.Button depth_button;
	private Fee.Ui.Slider depth_rate_blend_slider;

	/** render_texture
	*/
	RenderTexture render_texture_color;
	RenderTexture render_texture_depth;

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
		Fee.Render2D.Config.CAMERADEPTH_START = 10.0f;
		Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
		Fee.Render2D.Render2D.CreateInstance();

		//ブラー。インスタンス作成。
		Fee.Blur.Blur.LOG_ENABLE = true;
		Fee.Blur.Blur.CreateInstance();

		//ブルーム。インスタンス作成。
		Fee.Bloom.Bloom.CreateInstance();

		//深度。インスタンス作成。
		Fee.Depth.Depth.CreateInstance();

		//マウス。インスタンス作成。
		Fee.Input.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		Fee.EventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		//Fee.Ui.Config.LOG_ENABLE = true;
		Fee.Ui.Ui.CreateInstance();

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//深度。カメラに設定。
		{
			Camera t_main_camera = GameObject.Find("Main Camera").GetComponent<Camera>();

			t_main_camera.clearFlags = CameraClearFlags.Nothing;
			t_main_camera.depthTextureMode = DepthTextureMode.Depth;
			t_main_camera.depth = 20.0f;

			Fee.Depth.Depth.GetInstance().SetBlendRate(0.5f);

			if(false){
				this.render_texture_color = new RenderTexture(Screen.width,Screen.height,0,RenderTextureFormat.ARGB32);
				this.render_texture_color.Create();
				this.render_texture_depth = new RenderTexture(Screen.width,Screen.height,24,RenderTextureFormat.Depth);
				this.render_texture_depth.Create();
				t_main_camera.SetTargetBuffers(this.render_texture_color.colorBuffer,this.render_texture_depth.depthBuffer);
				Fee.Depth.Depth.GetInstance().SetDepthTexture(this.render_texture_depth);
			}else{
				Fee.Depth.Depth.GetInstance().SetDepthTexture(null);
			}
		}

		//スプライト。
		{
			int t_w = 200;
			int t_h = 200;
			int t_x = (Fee.Render2D.Render2D.VIRTUAL_W - t_w) / 2;
			int t_y = (Fee.Render2D.Render2D.VIRTUAL_H - t_h) / 2;

			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;
			this.sprite = new Fee.Render2D.Sprite2D(this.deleter,t_drawpriority);
			this.sprite.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite.SetRect(t_x,t_y,t_w,t_h);
			this.sprite.SetTexture(Resources.Load<Texture2D>("IMGP8657"));
		}

		{
			int t_y = 100;

			int t_button_h = 25;
			int t_button_w = 120;

			//優先度。
			{
				this.depthchange_button = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click_Enable,999);
				this.depthchange_button.SetTexture(Resources.Load<Texture2D>("button"));
				this.depthchange_button.SetRect(100,t_y,t_button_w,t_button_h);
				this.depthchange_button.SetText("DepthChange");
			}

			t_y += 40;

			//ブルーム。
			{
				this.bloom_button = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click_Enable,0);
				this.bloom_button.SetTexture(Resources.Load<Texture2D>("button"));
				this.bloom_button.SetRect(100,t_y,t_button_w,t_button_h);
				this.bloom_button.SetText("Bloom" + Fee.Bloom.Bloom.GetInstance().IsEnable().ToString());

				t_y += 40;

				this.bloom_threshold_slider = new Fee.Ui.Slider(this.deleter,0,this.CallBack_Change_Slider,100);
				this.bloom_threshold_slider.SetTexture(Resources.Load<Texture2D>("slider"));
				this.bloom_threshold_slider.SetButtonTexture(Resources.Load<Texture2D>("button"));
				this.bloom_threshold_slider.SetRect(100,t_y,200,10);
				this.bloom_threshold_slider.SetTextureCornerSize(3);
				this.bloom_threshold_slider.SetButtonTextureCornerSize(3);
				this.bloom_threshold_slider.SetButtonSize(20,25);
				this.bloom_threshold_slider.SetValue(Fee.Bloom.Bloom.GetInstance().GetThreshold());

				t_y += 30;

				this.bloom_intensity_slider = new Fee.Ui.Slider(this.deleter,0,this.CallBack_Change_Slider,101);
				this.bloom_intensity_slider.SetTexture(Resources.Load<Texture2D>("slider"));
				this.bloom_intensity_slider.SetButtonTexture(Resources.Load<Texture2D>("button"));
				this.bloom_intensity_slider.SetRect(100,t_y,200,10);
				this.bloom_intensity_slider.SetTextureCornerSize(3);
				this.bloom_intensity_slider.SetButtonTextureCornerSize(3);
				this.bloom_intensity_slider.SetButtonSize(20,25);
				this.bloom_intensity_slider.SetValue(Fee.Bloom.Bloom.GetInstance().GetIntensity());
				this.bloom_intensity_slider.SetValueScale(5.0f);
			}

			t_y += 60;

			//ブラー。
			{
				this.blur_button = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click_Enable,1);
				this.blur_button.SetTexture(Resources.Load<Texture2D>("button"));
				this.blur_button.SetRect(100,t_y,t_button_w,t_button_h);
				this.blur_button.SetText("Blur" + Fee.Blur.Blur.GetInstance().IsEnable().ToString());

				t_y += 40;

				this.blur_rate_blend_slider = new Fee.Ui.Slider(this.deleter,0,this.CallBack_Change_Slider,200);
				this.blur_rate_blend_slider.SetTexture(Resources.Load<Texture2D>("slider"));
				this.blur_rate_blend_slider.SetButtonTexture(Resources.Load<Texture2D>("button"));
				this.blur_rate_blend_slider.SetRect(100,t_y,200,10);
				this.blur_rate_blend_slider.SetTextureCornerSize(3);
				this.blur_rate_blend_slider.SetButtonTextureCornerSize(3);
				this.blur_rate_blend_slider.SetButtonSize(20,25);
				this.blur_rate_blend_slider.SetValue(Fee.Blur.Blur.GetInstance().GetBlendRate());
			}

			t_y += 60;

			//デプス。
			{
				this.depth_button = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click_Enable,2);
				this.depth_button.SetTexture(Resources.Load<Texture2D>("button"));
				this.depth_button.SetRect(100,t_y,t_button_w,t_button_h);
				this.depth_button.SetText("Depth" + Fee.Blur.Blur.GetInstance().IsEnable().ToString());

				t_y += 40;

				this.depth_rate_blend_slider = new Fee.Ui.Slider(this.deleter,0,this.CallBack_Change_Slider,300);
				this.depth_rate_blend_slider.SetTexture(Resources.Load<Texture2D>("slider"));
				this.depth_rate_blend_slider.SetButtonTexture(Resources.Load<Texture2D>("button"));
				this.depth_rate_blend_slider.SetRect(100,t_y,200,10);
				this.depth_rate_blend_slider.SetTextureCornerSize(3);
				this.depth_rate_blend_slider.SetButtonTextureCornerSize(3);
				this.depth_rate_blend_slider.SetButtonSize(20,25);
				this.depth_rate_blend_slider.SetValue(Fee.Depth.Depth.GetInstance().GetBlendRate());
			}
		}
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		Fee.Input.Mouse.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());

		//イベントプレート。
		Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

		//ＵＩ。
		Fee.Ui.Ui.GetInstance().Main();
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

	/** [Button_Base]コールバック。クリック。
	*/
	public void CallBack_Click_Enable(int a_id)
	{
		if(a_id == 999){
			if(Fee.Bloom.Bloom.GetInstance().GetCameraDepth() > Fee.Blur.Blur.GetInstance().GetCameraDepth()){
				Fee.Bloom.Bloom.GetInstance().SetCameraDepth(800.1f);
				Fee.Blur.Blur.GetInstance().SetCameraDepth(800.2f);
			}else{
				Fee.Bloom.Bloom.GetInstance().SetCameraDepth(800.2f);
				Fee.Blur.Blur.GetInstance().SetCameraDepth(800.1f);
			}
		}else if(a_id == 0){
			//ブルーム。
			if(Fee.Bloom.Bloom.GetInstance().IsEnable() == true){
				Fee.Bloom.Bloom.GetInstance().SetEnable(false);
			}else{
				Fee.Bloom.Bloom.GetInstance().SetEnable(true);
			}
			this.bloom_button.SetText("Bloom " + Fee.Bloom.Bloom.GetInstance().IsEnable().ToString());
		}else if(a_id == 1){
			//ブラー。
			if(Fee.Blur.Blur.GetInstance().IsEnable() == true){
				Fee.Blur.Blur.GetInstance().SetEnable(false);
			}else{
				Fee.Blur.Blur.GetInstance().SetEnable(true);
			}
			this.blur_button.SetText("Blur " + Fee.Blur.Blur.GetInstance().IsEnable().ToString());
		}else{
			//デプス。
			if(Fee.Depth.Depth.GetInstance().IsEnable() == true){
				Fee.Depth.Depth.GetInstance().SetEnable(false);
			}else{
				Fee.Depth.Depth.GetInstance().SetEnable(true);
			}
			this.depth_button.SetText("Depth " + Fee.Depth.Depth.GetInstance().IsEnable().ToString());
		}
	}

	/** [Slider_Base]コールバック。変更。
	*/
	public void CallBack_Change_Slider(int a_id,float a_value)
	{
		if(a_id == 100){
			//threshold
			Fee.Bloom.Bloom.GetInstance().SetThreshold(a_value);
		}else if(a_id == 101){
			//intensity
			Fee.Bloom.Bloom.GetInstance().SetIntensity(a_value);
		}else if(a_id == 200){
			//blendrate
			Fee.Blur.Blur.GetInstance().SetBlendRate(a_value);
		}else if(a_id == 300){
			//depth
			Fee.Depth.Depth.GetInstance().SetBlendRate(a_value);
		}
	}
}

