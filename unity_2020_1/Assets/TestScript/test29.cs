

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
	/** test29

		ミラー

	*/
	public class test29 : MainBase , Fee.Function.UnityOnPreRender_CallBackInterface<int>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test29",
				"test29",

				@"
				ミラー
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** box
		*/
		private UnityEngine.Transform box_transform;
		private float box_angle;

		/** mirror
		*/
		private Fee.Mirror.MirrorObject_MonoBehaviour mirror_1;
		private Fee.Mirror.MirrorObject_MonoBehaviour mirror_2;

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
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = false;
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

			//ミラー。インスタンス作成。
			Fee.Mirror.Mirror.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//ミラー。
			UnityEngine.GameObject t_mirror_1_gameobject;
			{
				t_mirror_1_gameobject = new UnityEngine.GameObject("mirror1");
				UnityEngine.MeshFilter t_mirror_meshfilter = t_mirror_1_gameobject.AddComponent<UnityEngine.MeshFilter>();
				UnityEngine.MeshRenderer t_mirror_meshrenderer = t_mirror_1_gameobject.AddComponent<UnityEngine.MeshRenderer>();

				System.Collections.Generic.List<UnityEngine.Vector3> t_mirror_vertex_list = new System.Collections.Generic.List<UnityEngine.Vector3>(Fee.Mesh.Plate.CAPACITY_VERTEX_LIST);
				System.Collections.Generic.List<int> t_mirror_index_list = new System.Collections.Generic.List<int>(Fee.Mesh.Plate.CAPACITY_INDEX_LIST);
				System.Collections.Generic.List<UnityEngine.Vector2> t_mirror_uv_list = new System.Collections.Generic.List<UnityEngine.Vector2>(Fee.Mesh.Plate.CAPACITY_UV_LIST);
				Fee.Mesh.Plate.CreateVertexList(t_mirror_vertex_list);
				Fee.Mesh.Plate.CreateIndexList(t_mirror_index_list);
				Fee.Mesh.Plate.CreateUvList(t_mirror_uv_list);
				
				t_mirror_meshfilter.mesh = Fee.Mesh.Plate.CreateMesh(t_mirror_vertex_list,t_mirror_index_list,t_mirror_uv_list);
				t_mirror_meshrenderer.material = null;

				UnityEngine.Transform t_mirror_transform = t_mirror_1_gameobject.GetComponent<UnityEngine.Transform>();
				t_mirror_transform.localScale = new UnityEngine.Vector3(100.0f,100.0f,100.0f);
				t_mirror_transform.position = new UnityEngine.Vector3(0.0f,0.0f,0.0f);
			}

			//ミラー。
			UnityEngine.GameObject t_mirror_2_gameobject;
			{
				t_mirror_2_gameobject = new UnityEngine.GameObject("mirror2");
				UnityEngine.MeshFilter t_mirror_meshfilter = t_mirror_2_gameobject.AddComponent<UnityEngine.MeshFilter>();
				UnityEngine.MeshRenderer t_mirror_meshrenderer = t_mirror_2_gameobject.AddComponent<UnityEngine.MeshRenderer>();

				System.Collections.Generic.List<UnityEngine.Vector3> t_mirror_vertex_list = new System.Collections.Generic.List<UnityEngine.Vector3>(Fee.Mesh.Plate.CAPACITY_VERTEX_LIST);
				System.Collections.Generic.List<int> t_mirror_index_list = new System.Collections.Generic.List<int>(Fee.Mesh.Plate.CAPACITY_INDEX_LIST);
				System.Collections.Generic.List<UnityEngine.Vector2> t_mirror_uv_list = new System.Collections.Generic.List<UnityEngine.Vector2>(Fee.Mesh.Plate.CAPACITY_UV_LIST);
				Fee.Mesh.Plate.CreateVertexList(t_mirror_vertex_list);
				Fee.Mesh.Plate.CreateIndexList(t_mirror_index_list);
				Fee.Mesh.Plate.CreateUvList(t_mirror_uv_list);
				
				t_mirror_meshfilter.mesh = Fee.Mesh.Plate.CreateMesh(t_mirror_vertex_list,t_mirror_index_list,t_mirror_uv_list);
				t_mirror_meshrenderer.material = null;

				UnityEngine.Transform t_mirror_transform = t_mirror_2_gameobject.GetComponent<UnityEngine.Transform>();
				t_mirror_transform.localScale = new UnityEngine.Vector3(10.0f,10.0f,10.0f);
				t_mirror_transform.position = new UnityEngine.Vector3(0.0f,5.0f,11.0f);
				t_mirror_transform.rotation = UnityEngine.Quaternion.AngleAxis(90.0f,UnityEngine.Vector3.left);
			}

			//箱。
			{
				UnityEngine.GameObject t_box_gameobject = new UnityEngine.GameObject("box");
				this.box_transform = t_box_gameobject.GetComponent<UnityEngine.Transform>();
				this.box_transform.position = new UnityEngine.Vector3(0.0f,0.0f,0.0f);
				this.box_transform.localScale = new UnityEngine.Vector3(1.0f,1.0f,1.0f);
				t_box_gameobject.layer = 0;

				UnityEngine.MeshFilter t_box_meshfilter = t_box_gameobject.AddComponent<UnityEngine.MeshFilter>();
				UnityEngine.MeshRenderer t_box_meshrenderer = t_box_gameobject.AddComponent<UnityEngine.MeshRenderer>();

				System.Collections.Generic.List<UnityEngine.Vector3> t_box_vertex_list = new System.Collections.Generic.List<UnityEngine.Vector3>(Fee.Mesh.Box.CAPACITY_VERTEX_LIST);
				System.Collections.Generic.List<int> t_box_index_list = new System.Collections.Generic.List<int>(Fee.Mesh.Box.CAPACITY_INDEX_LIST);
				Fee.Mesh.Box.CreateVertexList(t_box_vertex_list);
				Fee.Mesh.Box.CreateIndexList(t_box_index_list);
				t_box_meshfilter.mesh = Fee.Mesh.Box.CreateMesh(t_box_vertex_list,t_box_index_list);
				t_box_meshrenderer.material = new UnityEngine.Material(UnityEngine.Shader.Find("Fee/Shader/NormalTest"));

				this.box_angle = 0.0f;
			}

			
			//ミラーオブジェクトのOnWillRenderObjectでミラーを描画。
			{
				this.mirror_1 = Fee.Mirror.Mirror.GetInstance().CreateMirror(Fee.Mirror.RenderTextureSizeType.Size_1024,t_mirror_1_gameobject,UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>(),"Mirror Camera 1");
				this.mirror_1.mirror_camera.raw_camera.clearFlags = UnityEngine.CameraClearFlags.Skybox;
				this.mirror_1.enabled = true;
				this.mirror_1.gameObject.layer = 4;

				//ボックスを表示。ミラーオブジェクトは写さない。
				this.mirror_1.mirror_camera.raw_camera.cullingMask = 1;
			}

			//メインカメラのUnityOnPreRenderでミラーを描画。
			{
				this.mirror_2 = Fee.Mirror.Mirror.GetInstance().CreateMirror(Fee.Mirror.RenderTextureSizeType.Size_1024,t_mirror_2_gameobject,UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>(),"Mirror Camera 2");
				this.mirror_2.mirror_camera.raw_camera.clearFlags = UnityEngine.CameraClearFlags.Skybox;
				this.mirror_2.enabled = false;
				this.mirror_2.gameObject.layer = 4;

				//ボックスを表示。ミラーオブジェクトは写さない。
				this.mirror_2.mirror_camera.raw_camera.cullingMask = 1;
			}

			//ボックスとミラーを表示。
			{
				UnityEngine.GameObject.Find("Main Camera").AddComponent<Fee.Function.UnityOnPreRender_MonoBehaviour>().SetCallBack(this,0);
				UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().cullingMask = (1) | (1 << 4);
			}
		}

		/** [Fee.Graphic.UnityOnPreRender_CallBackInterface]UnityOnPreRender
		*/
		public void UnityOnPreRender(int a_id)
		{
			this.mirror_2.Render();
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
			this.box_angle += UnityEngine.Time.deltaTime;
			this.box_transform.position = new UnityEngine.Vector3(UnityEngine.Mathf.Cos(this.box_angle) * 5.0f,1.0f,UnityEngine.Mathf.Sin(this.box_angle) * 5.0f);
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

