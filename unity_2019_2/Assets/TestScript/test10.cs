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
	/** test10

		ブルーム
		ブラー
		デプス

	*/
	public class test10 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test10.ButtonId> , Fee.Ui.OnSliderChangeValue_CallBackInterface<test10.SliderId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test10",
				"test10",

				@"
				ブルーム
				ブラー
				デプス
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** sprite
		*/
		private Fee.Render2D.Sprite2D sprite;

		/** cube
		*/
		private GameObject[] cube;

		/** ブルーム。
		*/
		private Fee.Ui.Button bloom_button;
		private Fee.Ui.Slider bloom_threshold_slider;
		private Fee.Ui.Slider bloom_intensity_slider;

		/** ブラー。
		*/
		private Fee.Ui.Button blur_button;
		private Fee.Ui.Slider blur_rate_blend_slider;

		/** デプス。
		*/
		private Fee.Ui.Button depth_button;
		private Fee.Ui.Slider depth_rate_blend_slider;

		/** render_texture
		*/
		RenderTexture render_texture_color;
		RenderTexture render_texture_depth;

		/** ButtonId
		*/
		public enum ButtonId
		{
			Bloom,
			Blur,
			Depth,
		}

		/** SliderId
		*/
		public enum SliderId
		{
			Threshold,
			Intensity,
			BlurBlendrate,
			DepthBlendrate,
		}

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
			Fee.Render2D.Config.CAMERADEPTH_START = 10.0f;
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
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

			//プレハブリスト。
			{
				this.prefablist = new Common.PrefabList();
				this.prefablist.LoadFontList();
				this.prefablist.LoadTextureList();
			}

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont("FONT"));

			//プレハブリスト。
			Fee.Instantiate.PrefabList t_prefablist;
			{
				UnityEngine.GameObject t_prefab = UnityEngine.Resources.Load<UnityEngine.GameObject>("PrefabList");
				t_prefablist = new Fee.Instantiate.PrefabList(t_prefab.GetComponent<Fee.Instantiate.PrefabList_MonoBehaviour>());
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//深度。カメラに設定。
			{
				Camera t_main_camera = GameObject.Find("Main Camera").GetComponent<Camera>();

				t_main_camera.clearFlags = CameraClearFlags.Nothing;
				t_main_camera.depthTextureMode = DepthTextureMode.Depth;
				t_main_camera.depth = 20.0f;

				Fee.Depth.Depth.GetInstance().SetBlendRate(0.5f);

				#if(false)
				{
					this.render_texture_color = new RenderTexture(Screen.width,Screen.height,0,RenderTextureFormat.ARGB32);
					this.render_texture_color.Create();
					this.render_texture_depth = new RenderTexture(Screen.width,Screen.height,24,RenderTextureFormat.Depth);
					this.render_texture_depth.Create();
					t_main_camera.SetTargetBuffers(this.render_texture_color.colorBuffer,this.render_texture_depth.depthBuffer);
					Fee.Depth.Depth.GetInstance().SetDepthTexture(this.render_texture_depth);
				}
				#else
				{
					Fee.Depth.Depth.GetInstance().SetDepthTexture(null);
				}
				#endif
			}

			//キューブ。
			{
				this.cube = new GameObject[2];
				for(int ii=0;ii<this.cube.Length;ii++){
					if(ii == 0){
						this.cube[ii] = GameObject.Instantiate(t_prefablist.GetPrefab("TEST10_CUBE"),new Vector3(1 + 1,0,10),Quaternion.identity);
					}else{
						this.cube[ii] = GameObject.Instantiate(t_prefablist.GetPrefab("TEST10_CUBE"),new Vector3(1 + 4,0,10),Quaternion.identity);
					}
					this.cube[ii].transform.localScale = new Vector3(2,2,2);
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
				this.sprite = Fee.Render2D.Sprite2D.Create(this.deleter,t_drawpriority);
				this.sprite.SetTextureRect(in Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
				this.sprite.SetRect(t_x,t_y,t_w,t_h);
				this.sprite.SetTexture(this.prefablist.GetTexture("TEST10_TEXTURE"));
			}

			{
				int t_y = 100;

				int t_button_h = 25;
				int t_button_w = 120;

				//ブルーム。
				{
					this.bloom_button = this.prefablist.CreateButton(this.deleter,0);
					this.bloom_button.SetOnButtonClick(this,ButtonId.Bloom);
					this.bloom_button.SetRect(100,t_y,t_button_w,t_button_h);
					this.bloom_button.SetText("Bloom : " + Fee.Bloom.Bloom.GetInstance().IsEnable().ToString());

					t_y += 40;

					this.bloom_threshold_slider = this.prefablist.CreateSlider(this.deleter,0);
					this.bloom_threshold_slider.SetOnSliderChangeValue(this,SliderId.Threshold);
					this.bloom_threshold_slider.SetRect(100,t_y,200,10);
					this.bloom_threshold_slider.SetButtonSize(20,25);
					this.bloom_threshold_slider.SetValue(Fee.Bloom.Bloom.GetInstance().GetThreshold());

					t_y += 30;

					this.bloom_intensity_slider = this.prefablist.CreateSlider(this.deleter,0);
					this.bloom_intensity_slider.SetOnSliderChangeValue(this,SliderId.Intensity);
					this.bloom_intensity_slider.SetRect(100,t_y,200,10);
					this.bloom_intensity_slider.SetButtonSize(20,25);
					this.bloom_intensity_slider.SetValue(Fee.Bloom.Bloom.GetInstance().GetIntensity());
					this.bloom_intensity_slider.SetValueScale(5.0f);
				}

				t_y += 60;

				//ブラー。
				{
					this.blur_button = this.prefablist.CreateButton(this.deleter,0);
					this.blur_button.SetOnButtonClick(this,ButtonId.Blur);
					this.blur_button.SetRect(100,t_y,t_button_w,t_button_h);
					this.blur_button.SetText("Blur : " + Fee.Blur.Blur.GetInstance().IsEnable().ToString());

					t_y += 40;

					this.blur_rate_blend_slider = this.prefablist.CreateSlider(this.deleter,0);
					this.blur_rate_blend_slider.SetOnSliderChangeValue(this,SliderId.BlurBlendrate);
					this.blur_rate_blend_slider.SetRect(100,t_y,200,10);
					this.blur_rate_blend_slider.SetButtonSize(20,25);
					this.blur_rate_blend_slider.SetValue(Fee.Blur.Blur.GetInstance().GetBlendRate());
				}

				t_y += 60;

				//デプス。
				{
					this.depth_button = this.prefablist.CreateButton(this.deleter,0);
					this.depth_button.SetOnButtonClick(this,ButtonId.Depth);
					this.depth_button.SetRect(100,t_y,t_button_w,t_button_h);
					this.depth_button.SetText("Depth : " + Fee.Blur.Blur.GetInstance().IsEnable().ToString());

					t_y += 40;

					this.depth_rate_blend_slider = this.prefablist.CreateSlider(this.deleter,0);
					this.depth_rate_blend_slider.SetOnSliderChangeValue(this,SliderId.DepthBlendrate);
					this.depth_rate_blend_slider.SetRect(100,t_y,200,10);
					this.depth_rate_blend_slider.SetButtonSize(20,25);
					this.depth_rate_blend_slider.SetValue(Fee.Depth.Depth.GetInstance().GetBlendRate());
				}
			}
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

			//キューブ。
			for(int ii=0;ii<this.cube.Length;ii++){
				if(ii == 0){
					this.cube[ii].transform.Rotate(Vector3.up,0.1f);
				}else{
					this.cube[ii].transform.Rotate(Vector3.up,-0.1f);
				}
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

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			switch(a_id){
			case ButtonId.Bloom:
				{
					//ブルーム。
					if(Fee.Bloom.Bloom.GetInstance().IsEnable() == true){
						Fee.Bloom.Bloom.GetInstance().SetEnable(false);
					}else{
						Fee.Bloom.Bloom.GetInstance().SetEnable(true);
					}
					this.bloom_button.SetText("Bloom " + Fee.Bloom.Bloom.GetInstance().IsEnable().ToString());
				}break;
			case ButtonId.Blur:
				{
					//ブラー。
					if(Fee.Blur.Blur.GetInstance().IsEnable() == true){
						Fee.Blur.Blur.GetInstance().SetEnable(false);
					}else{
						Fee.Blur.Blur.GetInstance().SetEnable(true);
					}
					this.blur_button.SetText("Blur " + Fee.Blur.Blur.GetInstance().IsEnable().ToString());
				}break;
			case ButtonId.Depth:
				{
					//デプス。
					if(Fee.Depth.Depth.GetInstance().IsEnable() == true){
						Fee.Depth.Depth.GetInstance().SetEnable(false);
					}else{
						Fee.Depth.Depth.GetInstance().SetEnable(true);
					}
					this.depth_button.SetText("Depth " + Fee.Depth.Depth.GetInstance().IsEnable().ToString());
				}break;
			}
		}

		/** [Fee.Ui.OnSliderChangeValue_CallBackInterface]値変更。
		*/
		public void OnSliderChangeValue(SliderId a_id,float a_value)
		{
			switch(a_id){
			case SliderId.Threshold:
				{
					Fee.Bloom.Bloom.GetInstance().SetThreshold(a_value);
				}break;
			case SliderId.Intensity:
				{
					Fee.Bloom.Bloom.GetInstance().SetIntensity(a_value);
				}break;
			case SliderId.BlurBlendrate:
				{
					Fee.Blur.Blur.GetInstance().SetBlendRate(a_value);
				}break;
			case SliderId.DepthBlendrate:
				{
					Fee.Depth.Depth.GetInstance().SetBlendRate(a_value);
				}break;

			}
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

