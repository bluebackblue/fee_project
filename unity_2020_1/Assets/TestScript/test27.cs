

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
	/** test27
	*/
	public class test27 : MainBase , Fee.Function.UnityOnRenderImage_CallBackInterface<int> , Fee.Ui.OnButtonClick_CallBackInterface<test27.ButtonId> , Fee.Ui.OnSliderChangeValue_CallBackInterface<test27.SliderId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test27",
				"test27",

				@"
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

		/** マテリアル。
		*/
		private Fee.Depth.Material_DepthTexture depth_material_depthtexture;

		/** レンダーテクスチャー。
		*/
		private UnityEngine.RenderTexture depth_rendertexture_color;
		private UnityEngine.RenderTexture depth_rendertexture_depth;

		/** maincamera
		*/
		private UnityEngine.Camera maincamera_camera;

		/** 箱。
		*/
		private UnityEngine.Material box_material;
		private UnityEngine.Transform box_transform;

		/** ＵＩ。
		*/
		private Fee.Ui.Button ui_button;
		private Fee.Ui.Slider ui_blendrate_slider;

		/** postcamera
		*/
		private Fee.Function.UnityOnRenderImage_MonoBehaviour postcamera_onrenderimage;

		/** ButtonId
		*/
		public enum ButtonId
		{
			Depth,
		}

		/** SliderId
		*/
		public enum SliderId
		{
			BlendRate,
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

			//インスタンス作成。
			Fee.Instantiate.Instantiate.CreateInstance();

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

			//デプス。
			{
				this.depth_material_depthtexture = new Fee.Depth.Material_DepthTexture(new UnityEngine.Material(UnityEngine.Shader.Find(Fee.Depth.Config.SHADER_NAME_DEPTHTEXTURE)));
				this.depth_rendertexture_color = new UnityEngine.RenderTexture(UnityEngine.Screen.width,UnityEngine.Screen.height,0,UnityEngine.RenderTextureFormat.ARGB32);
				this.depth_rendertexture_color.Create();
				this.depth_rendertexture_depth = new UnityEngine.RenderTexture(UnityEngine.Screen.width,UnityEngine.Screen.height,24,UnityEngine.RenderTextureFormat.Depth);
				this.depth_rendertexture_depth.Create();

				this.depth_material_depthtexture.SetDepthTexture(this.depth_rendertexture_depth);
			}

			//箱。
			{
				UnityEngine.GameObject t_box_gameobject = new UnityEngine.GameObject("box");
				this.box_transform = t_box_gameobject.GetComponent<UnityEngine.Transform>();
				this.box_transform.position = new UnityEngine.Vector3(0.0f,-1.0f,0.0f);
				this.box_transform.localScale = new UnityEngine.Vector3(6.0f,0.1f,6.0f);

				UnityEngine.MeshFilter t_box_meshfilter = t_box_gameobject.AddComponent<UnityEngine.MeshFilter>();
				UnityEngine.MeshRenderer t_box_meshrenderer = t_box_gameobject.AddComponent<UnityEngine.MeshRenderer>();

				System.Collections.Generic.List<UnityEngine.Vector3> t_box_vertex_list = new System.Collections.Generic.List<UnityEngine.Vector3>(Fee.Mesh.Box.CAPACITY_VERTEX_LIST);
				System.Collections.Generic.List<int> t_box_index_list = new System.Collections.Generic.List<int>(Fee.Mesh.Box.CAPACITY_INDEX_LIST);
				Fee.Mesh.Box.CreateVertexList(t_box_vertex_list);
				Fee.Mesh.Box.CreateIndexList(t_box_index_list);
				t_box_meshfilter.mesh = Fee.Mesh.Box.CreateMesh(t_box_vertex_list,t_box_index_list);
				
				this.box_material = new UnityEngine.Material(UnityEngine.Shader.Find("Fee/Shader/Color_CbZleon"));
				this.box_material.SetColor("_Color",new UnityEngine.Color(0.6f,0.5f,0.4f,1.0f));

				t_box_meshrenderer.material = this.box_material;
			}

			//ポストカメラ。
			{
				UnityEngine.GameObject t_postcamera_gameobject = new UnityEngine.GameObject("Post Camera");
				UnityEngine.Camera t_postcamera_camera = t_postcamera_gameobject.AddComponent<UnityEngine.Camera>();
				this.postcamera_onrenderimage = t_postcamera_gameobject.AddComponent<Fee.Function.UnityOnRenderImage_MonoBehaviour>();
				this.postcamera_onrenderimage.SetCallBack(this,0);

				//最後。描画しない。クリアしない。
				t_postcamera_camera.Reset();
				t_postcamera_camera.depth = 30.0f;
				t_postcamera_camera.clearFlags = UnityEngine.CameraClearFlags.Nothing;
				t_postcamera_camera.cullingMask = 0;
			}

			//カメラ。
			{
				UnityEngine.GameObject t_camera_gameobject = UnityEngine.GameObject.Find("Main Camera");
				UnityEngine.Transform t_camera_transform = t_camera_gameobject.GetComponent<UnityEngine.Transform>();
				this.maincamera_camera = t_camera_gameobject.GetComponent<UnityEngine.Camera>();
				t_camera_transform.position = new UnityEngine.Vector3(0.0f,0.0f,-5.0f);
				t_camera_transform.rotation = UnityEngine.Quaternion.LookRotation((this.box_transform.position - t_camera_transform.position).normalized,UnityEngine.Vector3.up);
				
				//最初。全部描画。デプスクリア。
				this.maincamera_camera.depth = 0.0f;
				this.maincamera_camera.cullingMask = -1;
				this.maincamera_camera.clearFlags = UnityEngine.CameraClearFlags.Depth;
				this.maincamera_camera.nearClipPlane = 1.0f;
				this.maincamera_camera.farClipPlane = 6.0f;

				this.maincamera_camera.SetTargetBuffers(this.depth_rendertexture_color.colorBuffer,this.depth_rendertexture_depth.depthBuffer);
			}

			//ＵＩ。
			{
				int t_y = 100;

				int t_button_h = 25;
				int t_button_w = 120;

				this.ui_button = this.prefablist.CreateButton(this.deleter,0);
				this.ui_button.SetOnButtonClick(this,ButtonId.Depth);
				this.ui_button.SetRect(100,t_y,t_button_w,t_button_h);
				this.ui_button.SetText("Depth : " + this.postcamera_onrenderimage.enabled.ToString());

				t_y += 40;

				this.ui_blendrate_slider = this.prefablist.CreateSlider(this.deleter,0);
				this.ui_blendrate_slider.SetOnSliderChangeValue(this,SliderId.BlendRate);
				this.ui_blendrate_slider.SetRect(100,t_y,200,10);
				this.ui_blendrate_slider.SetButtonSize(20,25);
				this.ui_blendrate_slider.SetValue(this.depth_material_depthtexture.GetBlendRate());
			}
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			switch(a_id){
			case ButtonId.Depth:
				{
					//デプス。

					if(this.postcamera_onrenderimage.enabled == true){
						this.postcamera_onrenderimage.enabled = false;
					}else{
						this.postcamera_onrenderimage.enabled = true;
					}

					this.ui_button.SetText("Depth : " + this.postcamera_onrenderimage.enabled.ToString());

				}break;
			}
		}

		/** [Fee.Ui.OnSliderChangeValue_CallBackInterface]値変更。
		*/
		public void OnSliderChangeValue(SliderId a_id,float a_value)
		{
			switch(a_id){
			case SliderId.BlendRate:
				{
					this.depth_material_depthtexture.SetBlendRate(a_value);
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
			this.depth_material_depthtexture.SetNear(this.maincamera_camera.nearClipPlane);
			this.depth_material_depthtexture.SetFar(this.maincamera_camera.farClipPlane);
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
		}

		/** [Fee.Graphic.UnityOnRenderImage_CallBackInterface]UnityOnRenderImage
		*/
		#pragma warning disable 0168
		public void UnityOnRenderImage(int a_id,UnityEngine.RenderTexture a_source,UnityEngine.RenderTexture a_dest)
		{
			try{
				this.depth_material_depthtexture.Apply();

				UnityEngine.Graphics.Blit(a_source,a_dest,this.depth_material_depthtexture.material);
			}catch(System.Exception t_exception){
				#if(UNITY_EDITOR)
				Fee.EditorTool.Tool.EditorLogError(t_exception.Message);
				#endif
			}
		}
		#pragma warning restore

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

