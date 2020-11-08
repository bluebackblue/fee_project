

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
	*/
	public class test10 : MainBase , Fee.Ui.OnSliderChangeValue_CallBackInterface<test10.SliderId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test10",
				"test10",

				@"
				雲
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** cloud
		*/
		private Fee.Cloud.Material_VolumeCloud cloud_material_volumecloud;
		private float cloud_speed;
		private UnityEngine.Vector3 cloud_dir;

		/** SliderId
		*/
		public enum SliderId
		{
			Power,
			NoiseSacle,
			InvScale,
			Speed,
			Dir,
		}

		/** ＵＩ。
		*/
		private Fee.Ui.Slider ui_power_slider;
		private Fee.Ui.Slider ui_noisescale_slider;
		private Fee.Ui.Slider ui_invscale_slider;
		private Fee.Ui.Slider ui_speed_slider;
		private Fee.Ui.Slider ui_dir_slider;

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

			//maincamera
			{
				UnityEngine.GameObject t_meincamera_gameobject = UnityEngine.GameObject.Find("Main Camera");
				UnityEngine.Camera t_maincamera_camera = t_meincamera_gameobject.GetComponent<UnityEngine.Camera>();
				t_maincamera_camera.depth = 20.0f;
				t_maincamera_camera.clearFlags = UnityEngine.CameraClearFlags.Depth;
			}

			//cloud
			this.cloud_material_volumecloud = new Fee.Cloud.Material_VolumeCloud(new UnityEngine.Material(UnityEngine.Shader.Find(Fee.Cloud.Config.SHADER_NAME_VOLUMECLOUD)));
			this.cloud_speed = 0.1f;

			//box
			{
				UnityEngine.GameObject t_box_gameobject = new UnityEngine.GameObject("box");
				System.Collections.Generic.List<UnityEngine.Vector3> t_box_vertex_list = new System.Collections.Generic.List<UnityEngine.Vector3>();
				System.Collections.Generic.List<int> t_box_index_list = new System.Collections.Generic.List<int>();
				Fee.Mesh.Box.CreateVertexList(t_box_vertex_list);
				Fee.Mesh.Box.CreateIndexList(t_box_index_list);
				t_box_gameobject.AddComponent<UnityEngine.MeshFilter>().mesh = Fee.Mesh.Box.CreateMesh(t_box_vertex_list,t_box_index_list);
				t_box_gameobject.AddComponent<UnityEngine.MeshRenderer>().material = this.cloud_material_volumecloud.material;

				UnityEngine.Transform t_box_transform = t_box_gameobject.GetComponent<UnityEngine.Transform>();
				t_box_transform.localScale = new UnityEngine.Vector3(6.0f,6.0f,6.0f);
				t_box_transform.position = new UnityEngine.Vector3(0.0f,0.0f,10.0f);
			}

			//ＵＩ。
			{
				int t_y = 100;

				this.ui_power_slider = this.prefablist.CreateSlider(this.deleter,0);
				this.ui_power_slider.SetOnSliderChangeValue(this,SliderId.Power);
				this.ui_power_slider.SetRect(100,t_y,200,10);
				this.ui_power_slider.SetButtonSize(20,25);
				this.ui_power_slider.SetValueScale(10.0f);
				this.ui_power_slider.SetValue(this.cloud_material_volumecloud.GetPower());

				t_y += 30;

				this.ui_noisescale_slider = this.prefablist.CreateSlider(this.deleter,0);
				this.ui_noisescale_slider.SetOnSliderChangeValue(this,SliderId.NoiseSacle);
				this.ui_noisescale_slider.SetRect(100,t_y,200,10);
				this.ui_noisescale_slider.SetButtonSize(20,25);
				this.ui_noisescale_slider.SetValueScale(10.0f);
				this.ui_noisescale_slider.SetValue(this.cloud_material_volumecloud.GetNoiseScale());

				t_y += 30;

				this.ui_invscale_slider = this.prefablist.CreateSlider(this.deleter,0);
				this.ui_invscale_slider.SetOnSliderChangeValue(this,SliderId.InvScale);
				this.ui_invscale_slider.SetRect(100,t_y,200,10);
				this.ui_invscale_slider.SetButtonSize(20,25);
				this.ui_invscale_slider.SetValueScale(5.0f);
				this.ui_invscale_slider.SetValue(this.cloud_material_volumecloud.GetInvScale());

				t_y += 30;

				this.ui_speed_slider = this.prefablist.CreateSlider(this.deleter,0);
				this.ui_speed_slider.SetOnSliderChangeValue(this,SliderId.Speed);
				this.ui_speed_slider.SetRect(100,t_y,200,10);
				this.ui_speed_slider.SetButtonSize(20,25);
				this.ui_speed_slider.SetValueScale(0.3f);
				this.ui_speed_slider.SetValue(this.cloud_speed);

				t_y += 30;

				this.ui_dir_slider = this.prefablist.CreateSlider(this.deleter,0);
				this.ui_dir_slider.SetOnSliderChangeValue(this,SliderId.Dir);
				this.ui_dir_slider.SetRect(100,t_y,200,10);
				this.ui_dir_slider.SetButtonSize(20,25);
				this.ui_dir_slider.SetValueScale(6.28f);
				this.ui_dir_slider.SetValue(0.0f);
			}
		}

		/** [Fee.Ui.OnSliderChangeValue_CallBackInterface]値変更。
		*/
		public void OnSliderChangeValue(SliderId a_id,float a_value)
		{
			switch(a_id){
			case SliderId.Power:
				{
					this.cloud_material_volumecloud.SetPower(a_value);
				}break;
			case SliderId.NoiseSacle:
				{
					this.cloud_material_volumecloud.SetNoiseScale(a_value);
				}break;
			case SliderId.InvScale:
				{
					this.cloud_material_volumecloud.SetInvScale(a_value);
				}break;
			case SliderId.Speed:
				{
					this.cloud_speed = a_value;
				}break;
			case SliderId.Dir:
				{
					this.cloud_dir = new UnityEngine.Vector3(UnityEngine.Mathf.Cos(a_value),UnityEngine.Mathf.Sin(a_value),0.0f);
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
			this.cloud_material_volumecloud.SetNoiseOffset(this.cloud_material_volumecloud.GetNoiseOffset() + this.cloud_dir * this.cloud_speed);
		}

		/** Update
		*/
		private void Update()
		{
			this.cloud_material_volumecloud.Apply();
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

