

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

		/** cube
		*/
		private UnityEngine.GameObject cube;

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

			//frustum_culling
			this.frustum_culling = new Fee.Geometry.FrustumCulling();

			//main_camera
			this.main_camera = UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>();
			this.main_camera.depth = Fee.Render2D.Render2D.GetInstance().GetCameraAfterDepth(Fee.Render2D.Config.MAX_LAYER);
			this.main_camera.clearFlags = UnityEngine.CameraClearFlags.Nothing;

			//キューブ。
			{
				this.cube = UnityEngine.GameObject.Instantiate(this.prefablist.GetPrefab(Common.PrefabType.Test18_Cube),UnityEngine.Vector3.zero,UnityEngine.Quaternion.identity);
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
				this.cube.transform.position = new UnityEngine.Vector3(t_x,-t_y,0);
			}
			
			//カリングテスト。
			if(this.frustum_culling.CullingTest(this.cube.transform.position) == true){
				this.cube.GetComponent<UnityEngine.MeshRenderer>().material.color = new UnityEngine.Color(1.0f,0.0f,0.0f,1.0f);
			}else{
				this.cube.GetComponent<UnityEngine.MeshRenderer>().material.color = new UnityEngine.Color(0.0f,0.0f,1.0f,1.0f);
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

