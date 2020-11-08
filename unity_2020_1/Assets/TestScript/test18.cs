

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
	/** test18

		視錐台カリング

	*/
	public class test18 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test18",
				"test18",

				@"
				視錐台カリング
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** frustum_culling
		*/
		private Fee.Geometry.FrustumCulling frustum_culling;

		/** main_camera
		*/
		private UnityEngine.Camera main_camera;

		/** box
		*/
		private UnityEngine.Transform box_transform;
		private UnityEngine.Material box_material;

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

			//frustum_culling
			this.frustum_culling = new Fee.Geometry.FrustumCulling();

			//main_camera
			this.main_camera = UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>();
			this.main_camera.depth = Fee.Render2D.Render2D.GetInstance().GetCameraAfterDepth(Fee.Render2D.Config.MAX_LAYER);
			this.main_camera.clearFlags = UnityEngine.CameraClearFlags.Nothing;

			//箱。
			{
				UnityEngine.GameObject t_box_gameobject = new UnityEngine.GameObject("box");
				this.box_transform = t_box_gameobject.GetComponent<UnityEngine.Transform>();
				this.box_transform.position = new UnityEngine.Vector3(0.0f,0.0f,0.0f);
				this.box_transform.localScale = new UnityEngine.Vector3(3.0f,3.0f,3.0f);

				UnityEngine.MeshFilter t_box_meshfilter = t_box_gameobject.AddComponent<UnityEngine.MeshFilter>();
				UnityEngine.MeshRenderer t_box_meshrenderer = t_box_gameobject.AddComponent<UnityEngine.MeshRenderer>();

				System.Collections.Generic.List<UnityEngine.Vector3> t_box_vertex_list = new System.Collections.Generic.List<UnityEngine.Vector3>(Fee.Mesh.Box.CAPACITY_VERTEX_LIST);
				System.Collections.Generic.List<int> t_box_index_list = new System.Collections.Generic.List<int>(Fee.Mesh.Box.CAPACITY_INDEX_LIST);
				Fee.Mesh.Box.CreateVertexList(t_box_vertex_list);
				Fee.Mesh.Box.CreateIndexList(t_box_index_list);
				t_box_meshfilter.mesh = Fee.Mesh.Box.CreateMesh(t_box_vertex_list,t_box_index_list);
				
				this.box_material = new UnityEngine.Material(UnityEngine.Shader.Find("Fee/Shader/Color_CbZleon"));
				t_box_meshrenderer.material = this.box_material;
			}
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
			//プレーンリスト更新。
			this.frustum_culling.UpdatePlaneList(this.main_camera);

			//キューブの位置。
			{
				float t_x = (Fee.Input.Input.GetInstance().mouse.cursor.pos.x - Fee.Render2D.Config.VIRTUAL_W  / 2) / 50.0f;
				float t_y = (Fee.Input.Input.GetInstance().mouse.cursor.pos.y - Fee.Render2D.Config.VIRTUAL_H  / 2) / 50.0f;
				this.box_transform.position = new UnityEngine.Vector3(t_x,-t_y,0);
			}
			
			//カリングテスト。
			if(this.frustum_culling.CullingTest(this.box_transform.position) == true){
				this.box_material.color = new UnityEngine.Color(1.0f,0.0f,0.0f,1.0f);
			}else{
				this.box_material.color = new UnityEngine.Color(0.0f,0.0f,1.0f,1.0f);
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

