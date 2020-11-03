

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
	/** test25
	*/
	public class test25 : MainBase , Fee.Function.UnityOnRenderImage_CallBackInterface<int> , Fee.Ui.OnButtonClick_CallBackInterface<test25.ButtonId> , Fee.Ui.OnSliderChangeValue_CallBackInterface<test25.SliderId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test25",
				"test25",

				@"
				ブルーム
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** ブルーム。
		*/
		private UnityEngine.Material bloom_downsampling;
		private UnityEngine.Material bloom_upsampling;
		private Fee.Bloom.Material_FirstDownSampling bloom_firstdownsampling;
		private Fee.Bloom.Material_LastAddUpSampling bloom_lastupsampling;
		private UnityEngine.RenderTexture[] bloom_rendertexture;

		/** 輝度抽出閾値。
		*/
		private float bloom_threshold;

		/** 加算強度。
		*/
		private float bloom_intensity;

		/** 箱。
		*/
		private UnityEngine.Material box_material;
		public UnityEngine.Color box_color;
		private UnityEngine.Transform box_transform;

		/** ＵＩ。
		*/
		private Fee.Ui.Button ui_button;
		private Fee.Ui.Slider ui_threshold_slider;
		private Fee.Ui.Slider ui_intensity_slider;

		/** camera
		*/
		private Fee.Function.UnityOnRenderImage_MonoBehaviour camera_onrenderimage;

		/** ButtonId
		*/
		public enum ButtonId
		{
			Bloom,
		}

		/** SliderId
		*/
		public enum SliderId
		{
			Threshold,
			Intensity,
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
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.LOG_ENABLE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Config.LOG_ENABLE = true;
			Fee.Input.Input.CreateInstance(true,false,true,false);

			//イベントプレート。
			Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//ブルーム。
			{
				this.bloom_threshold = Fee.Bloom.Config.DEFAULT_THRESHOLD;
				this.bloom_intensity =  Fee.Bloom.Config.DEFAULT_INTENSITY;

				this.bloom_downsampling = new UnityEngine.Material(UnityEngine.Shader.Find(Fee.Bloom.Config.SHADER_NAME_DOWNSAMPLING));
				this.bloom_upsampling = new UnityEngine.Material(UnityEngine.Shader.Find(Fee.Bloom.Config.SHADER_NAME_ADDUPSAMPLING));
				this.bloom_firstdownsampling = new Fee.Bloom.Material_FirstDownSampling(new UnityEngine.Material(UnityEngine.Shader.Find(Fee.Bloom.Config.SHADER_NAME_FIRSTDOWNSAMPLING)));
				this.bloom_lastupsampling = new Fee.Bloom.Material_LastAddUpSampling(new UnityEngine.Material(UnityEngine.Shader.Find(Fee.Bloom.Config.SHADER_NAME_LASTADDUPSAMPLING)));
				this.bloom_rendertexture = new UnityEngine.RenderTexture[3];
			}

			//箱。
			{
				this.box_color = new UnityEngine.Color(0.6f,0.5f,0.4f,1.0f);

				UnityEngine.GameObject t_box_gameobject = new UnityEngine.GameObject("box");
				this.box_transform = t_box_gameobject.GetComponent<UnityEngine.Transform>();
				this.box_transform.position = new UnityEngine.Vector3(0.0f,0.0f,0.0f);

				UnityEngine.MeshFilter t_box_meshfilter = t_box_gameobject.AddComponent<UnityEngine.MeshFilter>();
				UnityEngine.MeshRenderer t_box_meshrenderer = t_box_gameobject.AddComponent<UnityEngine.MeshRenderer>();

				System.Collections.Generic.List<UnityEngine.Vector3> t_box_vertex_list = new System.Collections.Generic.List<UnityEngine.Vector3>(Fee.Mesh.Box.CAPACITY_VERTEX_LIST);
				System.Collections.Generic.List<int> t_box_index_list = new System.Collections.Generic.List<int>(Fee.Mesh.Box.CAPACITY_INDEX_LIST);
				Fee.Mesh.Box.CreateVertexList(t_box_vertex_list);
				Fee.Mesh.Box.CreateIndexList(t_box_index_list);
				t_box_meshfilter.mesh = Fee.Mesh.Box.CreateMesh(t_box_vertex_list,t_box_index_list);
				
				this.box_material = new UnityEngine.Material(UnityEngine.Shader.Find("Fee/Shader/Color_CfZleon"));
				this.box_material.SetColor("_Color",this.box_color);

				t_box_meshrenderer.material = this.box_material;
				t_box_meshrenderer.sharedMaterial = this.box_material;
			}

			//カメラ。
			{
				UnityEngine.GameObject t_camera_gameobject = UnityEngine.GameObject.Find("Main Camera");
				UnityEngine.Transform t_camera_transform = t_camera_gameobject.GetComponent<UnityEngine.Transform>();
				UnityEngine.Camera t_camera_camera = t_camera_gameobject.GetComponent<UnityEngine.Camera>();
				
				//３Ｄカメラ。設定。
				t_camera_transform.position = new UnityEngine.Vector3(0.0f,0.0f,-5.0f);
				t_camera_transform.rotation = UnityEngine.Quaternion.LookRotation((this.box_transform.position - t_camera_transform.position).normalized,UnityEngine.Vector3.up);
				t_camera_camera.depth = 20.0f;
				t_camera_camera.clearFlags = UnityEngine.CameraClearFlags.Nothing;

				//UnityOnRenderImage
				this.camera_onrenderimage = t_camera_gameobject.AddComponent<Fee.Function.UnityOnRenderImage_MonoBehaviour>();
				this.camera_onrenderimage.SetCallBack(this,0);
			}

			//ＵＩ。
			{
				int t_y = 100;

				int t_button_h = 25;
				int t_button_w = 120;

				this.ui_button = this.prefablist.CreateButton(this.deleter,0);
				this.ui_button.SetOnButtonClick(this,ButtonId.Bloom);
				this.ui_button.SetRect(100,t_y,t_button_w,t_button_h);
				this.ui_button.SetText("Bloom : " + this.camera_onrenderimage.enabled.ToString());

				t_y += 40;

				this.ui_threshold_slider = this.prefablist.CreateSlider(this.deleter,0);
				this.ui_threshold_slider.SetOnSliderChangeValue(this,SliderId.Threshold);
				this.ui_threshold_slider.SetRect(100,t_y,200,10);
				this.ui_threshold_slider.SetButtonSize(20,25);
				this.ui_threshold_slider.SetValue(this.bloom_threshold);

				t_y += 30;

				this.ui_intensity_slider = this.prefablist.CreateSlider(this.deleter,0);
				this.ui_intensity_slider.SetOnSliderChangeValue(this,SliderId.Intensity);
				this.ui_intensity_slider.SetRect(100,t_y,200,10);
				this.ui_intensity_slider.SetButtonSize(20,25);
				this.ui_intensity_slider.SetValue(this.bloom_intensity);
				this.ui_intensity_slider.SetValueScale(5.0f);
			}
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			switch(a_id){
			case ButtonId.Bloom:
				{
					//ブルーム。

					if(this.camera_onrenderimage.enabled == true){
						this.camera_onrenderimage.enabled = false;
					}else{
						this.camera_onrenderimage.enabled = true;
					}

					this.ui_button.SetText("Bloom : " + this.camera_onrenderimage.enabled.ToString());

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
					this.bloom_threshold = a_value;
				}break;
			case SliderId.Intensity:
				{
					this.bloom_intensity = a_value;
				}break;
			}
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			this.box_transform.rotation = UnityEngine.Quaternion.AngleAxis(0.1f,UnityEngine.Vector3.up) * this.box_transform.rotation;
		}

		/** Update
		*/
		private void Update()
		{
			this.box_material.SetColor("_Color",this.box_color);
			this.bloom_firstdownsampling.SetThreshold(this.bloom_threshold);
			this.bloom_lastupsampling.SetIntensity(this.bloom_intensity);
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
		}

		/** [Fee.Graphic.UnityOnRenderImage_CallBackInterface]UnityOnRenderImage
		*/
		public void UnityOnRenderImage(int a_id,UnityEngine.RenderTexture a_source,UnityEngine.RenderTexture a_dest)
		{
			//レンダリングテクスチャ作成。
			{
				int t_width = a_source.width;
				int t_height = a_source.height;
				for(int ii=0;ii<this.bloom_rendertexture.Length;ii++){
					t_width /= 2;
					t_height /= 2;
					this.bloom_rendertexture[ii] = UnityEngine.RenderTexture.GetTemporary(t_width,t_height,0,a_source.format,UnityEngine.RenderTextureReadWrite.Default);
				}
			}

			try{
				//マテリアル。更新。
				this.bloom_lastupsampling.SetOriginalTexture(a_source);
				this.bloom_firstdownsampling.Apply();
				this.bloom_lastupsampling.Apply();

				//ダウンサンプリング。
				for(int ii=0;ii<this.bloom_rendertexture.Length;ii++) {
					if(ii==0){
						UnityEngine.RenderTexture t_to = this.bloom_rendertexture[ii];
						UnityEngine.RenderTexture t_from = a_source;

						//初回ダウンサンプリング（輝度抽出）。
						UnityEngine.Graphics.Blit(t_from,t_to,this.bloom_firstdownsampling.material);
					}else{
						UnityEngine.RenderTexture t_to = this.bloom_rendertexture[ii];
						UnityEngine.RenderTexture t_from = this.bloom_rendertexture[ii - 1];

						//ダウンサンプリング。
						UnityEngine.Graphics.Blit(t_from,t_to,this.bloom_downsampling);
					}
				}

				//アップサンプリング。
				for(int ii=0;ii<(this.bloom_rendertexture.Length - 1);ii++){
					UnityEngine.RenderTexture t_to = this.bloom_rendertexture[this.bloom_rendertexture.Length - ii - 2];
					UnityEngine.RenderTexture t_from = this.bloom_rendertexture[this.bloom_rendertexture.Length - ii - 1];

					//アップサンプリング。
					this.bloom_rendertexture[this.bloom_rendertexture.Length - ii - 2].MarkRestoreExpected();
					UnityEngine.Graphics.Blit(t_from,t_to,this.bloom_upsampling);
				}

				//最終アップサンプリング（加算）。
				UnityEngine.Graphics.Blit(this.bloom_rendertexture[0],a_dest,this.bloom_lastupsampling.material);
			}catch(System.Exception t_exception){
				Fee.EditorTool.Tool.EditorLogError(t_exception.Message);
			}

			//レンダーテクスチャ解放。
			for(int ii=0;ii<this.bloom_rendertexture.Length;ii++){
				if(this.bloom_rendertexture[ii] != null){
					UnityEngine.RenderTexture.ReleaseTemporary(this.bloom_rendertexture[ii]);
					this.bloom_rendertexture[ii] = null;
				}
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

