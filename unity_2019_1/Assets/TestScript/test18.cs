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
			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
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

			//フォント。
			Font t_font = Resources.Load<Font>(Data.Resources.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//frustum_culling
			this.frustum_culling = new Fee.Geometry.FrustumCulling();

			//main_camera
			this.main_camera = GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>();
			this.main_camera.depth = Fee.Render2D.Render2D.GetInstance().GetCameraAfterDepth(Fee.Render2D.Config.MAX_LAYER);
			this.main_camera.clearFlags = CameraClearFlags.Nothing;

			//キューブ。
			{
				GameObject t_prefab = Resources.Load<GameObject>(Data.Resources.PREFAB_CUBE);
				this.cube = GameObject.Instantiate(t_prefab,Vector3.zero,Quaternion.identity);
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

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
	}
}

