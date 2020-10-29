

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
	public class test25 : MainBase , Fee.Function.UnityOnRenderImage_CallBackInterface<int>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test25",
				"test25",

				@"
				---
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
		private UnityEngine.Material material_bloom_downsampling;
		private UnityEngine.Material material_bloom_upsampling;
		private UnityEngine.RenderTexture[] bloom_rendertexture;
		private Fee.Bloom.Material_FirstDownSampling material_bloom_firstdownsampling;
		private Fee.Bloom.Material_LastAddUpSampling material_bloom_lastupsampling;

		/** 箱。
		*/
		private UnityEngine.Material box_material;

		/** 輝度抽出閾値。
		*/
		[UnityEngine.SerializeField,UnityEngine.Range(0.0f,1.0f)]
		private float threshold;

		/** 加算強度。
		*/
		[UnityEngine.SerializeField,UnityEngine.Range(0.0f,30.0f)]
		private float intensity;

		/** 色。
		*/
		public UnityEngine.Color color;

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

			//操作。
			{
				this.threshold = Fee.Bloom.Config.DEFAULT_THRESHOLD;
				this.intensity =  Fee.Bloom.Config.DEFAULT_INTENSITY;
				this.color = new UnityEngine.Color(0.6f,0.5f,0.4f,1.0f);
			}

			//ブルーム。
			this.material_bloom_downsampling = new UnityEngine.Material(UnityEngine.Shader.Find(Fee.Bloom.Config.SHADER_NAME_DOWNSAMPLING));
			this.material_bloom_upsampling = new UnityEngine.Material(UnityEngine.Shader.Find(Fee.Bloom.Config.SHADER_NAME_ADDUPSAMPLING));
			this.bloom_rendertexture = new UnityEngine.RenderTexture[3];
			this.material_bloom_firstdownsampling = new Fee.Bloom.Material_FirstDownSampling(new UnityEngine.Material(UnityEngine.Shader.Find(Fee.Bloom.Config.SHADER_NAME_FIRSTDOWNSAMPLING)));
			this.material_bloom_lastupsampling = new Fee.Bloom.Material_LastAddUpSampling(new UnityEngine.Material(UnityEngine.Shader.Find(Fee.Bloom.Config.SHADER_NAME_LASTADDUPSAMPLING)));

			//箱。
			UnityEngine.Transform t_box_transform = null;
			{
				UnityEngine.GameObject t_box_gameobject = new UnityEngine.GameObject("box");
				t_box_transform = t_box_gameobject.GetComponent<UnityEngine.Transform>();
				t_box_transform.position = new UnityEngine.Vector3(0.0f,0.0f,0.0f);

				UnityEngine.MeshFilter t_box_meshfilter = t_box_gameobject.AddComponent<UnityEngine.MeshFilter>();
				UnityEngine.MeshRenderer t_box_meshrenderer = t_box_gameobject.AddComponent<UnityEngine.MeshRenderer>();

				System.Collections.Generic.List<UnityEngine.Vector3> t_box_vertex_list = new System.Collections.Generic.List<UnityEngine.Vector3>(Fee.Mesh.Box.CAPACITY_VERTEX_LIST);
				System.Collections.Generic.List<int> t_box_index_list = new System.Collections.Generic.List<int>(Fee.Mesh.Box.CAPACITY_INDEX_LIST);
				Fee.Mesh.Box.CreateVertexList(t_box_vertex_list);
				Fee.Mesh.Box.CreateIndexList(t_box_index_list);
				t_box_meshfilter.mesh = Fee.Mesh.Box.CreateMesh(t_box_vertex_list,t_box_index_list);
				
				this.box_material = new UnityEngine.Material(UnityEngine.Shader.Find("Fee/Shader/Color_CfZelon"));
				this.box_material.SetColor("_Color",this.color);

				t_box_meshrenderer.material = this.box_material;
				t_box_meshrenderer.sharedMaterial = this.box_material;
			}

			//カメラ。
			{
				UnityEngine.GameObject t_camera_gameobject = UnityEngine.GameObject.Find("Main Camera");
				UnityEngine.Transform t_camera_transform = t_camera_gameobject.GetComponent<UnityEngine.Transform>();
				UnityEngine.Camera t_camera_camera = t_camera_gameobject.GetComponent<UnityEngine.Camera>();
					
				t_camera_transform.position = new UnityEngine.Vector3(0.0f,0.0f,-10.0f);
				t_camera_transform.rotation = UnityEngine.Quaternion.LookRotation((t_box_transform.position - t_camera_transform.position).normalized,UnityEngine.Vector3.up);
				t_camera_camera.depth = 20.0f;
				t_camera_camera.clearFlags = UnityEngine.CameraClearFlags.Nothing;

				Fee.Function.UnityOnRenderImage_MonoBehaviour t_callback = t_camera_gameobject.AddComponent<Fee.Function.UnityOnRenderImage_MonoBehaviour>();
				t_callback.SetCallBack(this,0);
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
		}

		/** Update
		*/
		private void Update()
		{
			this.box_material.SetColor("_Color",this.color);
			this.material_bloom_firstdownsampling.SetThreshold(this.threshold);
			this.material_bloom_lastupsampling.SetIntensity(this.intensity);
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
				this.material_bloom_lastupsampling.SetOriginalTexture(a_source);
				this.material_bloom_firstdownsampling.Apply();
				this.material_bloom_lastupsampling.Apply();

				//ダウンサンプリング。
				for(int ii=0;ii<this.bloom_rendertexture.Length;ii++) {
					if(ii==0){
						UnityEngine.RenderTexture t_to = this.bloom_rendertexture[ii];
						UnityEngine.RenderTexture t_from = a_source;

						//初回ダウンサンプリング（輝度抽出）。
						UnityEngine.Graphics.Blit(t_from,t_to,this.material_bloom_firstdownsampling.material);
					}else{
						UnityEngine.RenderTexture t_to = this.bloom_rendertexture[ii];
						UnityEngine.RenderTexture t_from = this.bloom_rendertexture[ii - 1];

						//ダウンサンプリング。
						UnityEngine.Graphics.Blit(t_from,t_to,this.material_bloom_downsampling);
					}
				}

				//アップサンプリング。
				for(int ii=0;ii<(this.bloom_rendertexture.Length - 1);ii++){
					UnityEngine.RenderTexture t_to = this.bloom_rendertexture[this.bloom_rendertexture.Length - ii - 2];
					UnityEngine.RenderTexture t_from = this.bloom_rendertexture[this.bloom_rendertexture.Length - ii - 1];

					//アップサンプリング。
					this.bloom_rendertexture[this.bloom_rendertexture.Length - ii - 2].MarkRestoreExpected();
					UnityEngine.Graphics.Blit(t_from,t_to,this.material_bloom_upsampling);
				}

				//最終アップサンプリング（加算）。
				UnityEngine.Graphics.Blit(this.bloom_rendertexture[0],a_dest,this.material_bloom_lastupsampling.material);
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

