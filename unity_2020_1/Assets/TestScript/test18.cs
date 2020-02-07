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
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.LOG_ENABLE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Config.LOG_ENABLE = true;
			Fee.Input.Mouse.CreateInstance();

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
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//frustum_culling
			this.frustum_culling = new Fee.Geometry.FrustumCulling();

			//main_camera
			this.main_camera = GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>();
			this.main_camera.depth = Fee.Render2D.Render2D.GetInstance().GetCameraAfterDepth(Fee.Render2D.Config.MAX_LAYER);
			this.main_camera.clearFlags = CameraClearFlags.Nothing;

			//キューブ。
			{
				this.cube = GameObject.Instantiate(this.prefablist.GetPrefab(Common.PrefabType.Test18_Cube),Vector3.zero,Quaternion.identity);
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

			//プレーンリスト更新。
			this.frustum_culling.UpdatePlaneList(this.main_camera);

			//キューブの位置。
			{
				float t_x = (Fee.Input.Mouse.GetInstance().cursor.pos.x - Fee.Render2D.Config.VIRTUAL_W  / 2) / 50.0f;
				float t_y = (Fee.Input.Mouse.GetInstance().cursor.pos.y - Fee.Render2D.Config.VIRTUAL_H  / 2) / 50.0f;
				this.cube.transform.position = new Vector3(t_x,-t_y,0);
			}
			
			//カリングテスト。
			if(this.frustum_culling.CullingTest(this.cube.transform.position) == true){
				this.cube.GetComponent<MeshRenderer>().material.color = new Color(1.0f,0.0f,0.0f,1.0f);
			}else{
				this.cube.GetComponent<MeshRenderer>().material.color = new Color(0.0f,0.0f,1.0f,1.0f);
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

