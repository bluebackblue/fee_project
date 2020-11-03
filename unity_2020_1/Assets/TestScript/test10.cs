

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
		private UnityEngine.GameObject[] cube;

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
		UnityEngine.RenderTexture render_texture_color;
		UnityEngine.RenderTexture render_texture_depth;

		/** ButtonId
		*/
		public enum ButtonId
		{
			Blur,
			Depth,
		}

		/** SliderId
		*/
		public enum SliderId
		{
			BlurBlendrate,
			DepthBlendrate,
		}

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

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.CAMERADEPTH_START = 10.0f;
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//ブラー。インスタンス作成。
			/*
			Fee.Blur.Config.LOG_ENABLE = true;
			Fee.Blur.Blur.CreateInstance();
			*/

			//深度。インスタンス作成。
			Fee.Depth.Depth.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Input.CreateInstance(true,false,true,false);

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			//Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//深度。カメラに設定。
			{
				UnityEngine.Camera t_main_camera = UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>();

				t_main_camera.clearFlags = UnityEngine.CameraClearFlags.Nothing;
				t_main_camera.depthTextureMode = UnityEngine.DepthTextureMode.Depth;
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
				this.cube = new UnityEngine.GameObject[2];
				for(int ii=0;ii<this.cube.Length;ii++){
					if(ii == 0){
						this.cube[ii] = UnityEngine.GameObject.Instantiate(this.prefablist.GetPrefab(Common.PrefabType.Test10_Cube),new UnityEngine.Vector3(1 + 1,0,10),UnityEngine.Quaternion.identity);
					}else{
						this.cube[ii] = UnityEngine.GameObject.Instantiate(this.prefablist.GetPrefab(Common.PrefabType.Test10_Cube),new UnityEngine.Vector3(1 + 4,0,10),UnityEngine.Quaternion.identity);
					}
					this.cube[ii].transform.localScale = new UnityEngine.Vector3(2,2,2);
				}
			}

			//スプライト。
			{
				int t_w = 200;
				int t_h = 200;
				int t_x = (Fee.Render2D.Config.VIRTUAL_W - t_w) / 2;
				int t_y = (Fee.Render2D.Config.VIRTUAL_H - t_h) / 2;

				int t_layerindex = 0;
				long t_drawpriority = t_layerindex * Fee.Render2D.Config.DRAWPRIORITY_STEP;
				this.sprite = Fee.Render2D.Sprite2D.Create(this.deleter,t_drawpriority);
				this.sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				this.sprite.SetRect(t_x,t_y,t_w,t_h);
				this.sprite.SetTexture(this.prefablist.GetTexture(Common.TextureType.Test10_Texture));
			}

			{
				int t_y = 100;

				int t_button_h = 25;
				int t_button_w = 120;

				//ブラー。
				/*
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
				*/

				t_y += 60;

				//デプス。
				{
					this.depth_button = this.prefablist.CreateButton(this.deleter,0);
					this.depth_button.SetOnButtonClick(this,ButtonId.Depth);
					this.depth_button.SetRect(100,t_y,t_button_w,t_button_h);
					this.depth_button.SetText("Depth : " + Fee.Depth.Depth.GetInstance().IsEnable().ToString());

					t_y += 40;

					this.depth_rate_blend_slider = this.prefablist.CreateSlider(this.deleter,0);
					this.depth_rate_blend_slider.SetOnSliderChangeValue(this,SliderId.DepthBlendrate);
					this.depth_rate_blend_slider.SetRect(100,t_y,200,10);
					this.depth_rate_blend_slider.SetButtonSize(20,25);
					this.depth_rate_blend_slider.SetValue(Fee.Depth.Depth.GetInstance().GetBlendRate());
				}
			}
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
			//キューブ。
			for(int ii=0;ii<this.cube.Length;ii++){
				if(ii == 0){
					this.cube[ii].transform.Rotate(UnityEngine.Vector3.up,0.1f);
				}else{
					this.cube[ii].transform.Rotate(UnityEngine.Vector3.up,-0.1f);
				}
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
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			switch(a_id){
			case ButtonId.Blur:
				{
					/*
					//ブラー。
					if(Fee.Blur.Blur.GetInstance().IsEnable() == true){
						Fee.Blur.Blur.GetInstance().SetEnable(false);
					}else{
						Fee.Blur.Blur.GetInstance().SetEnable(true);
					}
					this.blur_button.SetText("Blur " + Fee.Blur.Blur.GetInstance().IsEnable().ToString());
					*/
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
			case SliderId.BlurBlendrate:
				{
					/*
					Fee.Blur.Blur.GetInstance().SetBlendRate(a_value);
					*/
				}break;
			case SliderId.DepthBlendrate:
				{
					Fee.Depth.Depth.GetInstance().SetBlendRate(a_value);
				}break;

			}
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

