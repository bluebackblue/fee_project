

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
	/** test24

		タイム

	*/
	public class test24 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test24",
				"test24",

				@"
				タイム
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** time
		*/
		private float time_update;
		private float time_fixedupdate;
		private float time_rowupdate;

		/** count
		*/
		private int count_update;
		private int count_fixedupdate;
		private int count_rowupdate;

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

			//関数呼び出し。インスタンス作成。
			Fee.Function.Config.ROWUPDATE_DELTA = 1 / 20.0f;
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

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//タイム。インスタンス作成。
			Fee.Time.Time.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//text
			this.text = Fee.Render2D.Text2D.Create(this.deleter,0);
			this.text.SetRect(100,100,0,0);

			//time
			this.time_update = 0.0f;
			this.time_fixedupdate = 0.0f;
			this.time_rowupdate = 0.0f;

			//count
			this.count_update = 0;
			this.count_fixedupdate = 0;
			this.count_rowupdate = 0;

			//fixedDeltaTime
			UnityEngine.Time.fixedDeltaTime = 1.0f / 60;
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
			this.time_rowupdate += Fee.Function.Config.ROWUPDATE_DELTA * Fee.Time.Time.GetInstance().GetTimeScale();
			this.count_rowupdate++;
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			this.time_fixedupdate += UnityEngine.Time.fixedDeltaTime;
			this.count_fixedupdate++;
		}

		/** Update
		*/
		private void Update()
		{
			this.time_update += UnityEngine.Time.deltaTime;
			this.count_update++;

			this.text.SetText(
				"deltatime = " + UnityEngine.Time.deltaTime.ToString() + "\n" +

				"time_update = " + this.time_update.ToString()  + "\n" +
				"time_fixedupdate = " + this.time_fixedupdate.ToString()  + "\n" +
				"time_rowupdate = " + this.time_rowupdate.ToString()  + "\n" +

				"count_update = " + this.count_update.ToString()  + "\n" +
				"count_fixedupdate = " + this.count_fixedupdate.ToString()  + "\n" +
				"count_rowupdate = " + this.count_rowupdate.ToString()  + "\n"
			);
			
			if(Fee.Input.Input.GetInstance().mouse.right.down == true){
				float t_timescale = Fee.Time.Time.GetInstance().GetTimeScale();

				if(t_timescale > 0.0f){
					//停止。
					UnityEngine.Debug.Log("Stop : " + t_timescale.ToString());
					Fee.Time.Time.GetInstance().SetNextFrameTimeScale(0.0f);
				}else{
					//再生。
					UnityEngine.Debug.Log("Play : " + t_timescale.ToString());
					Fee.Time.Time.GetInstance().SetNextFrameTimeScale(1.0f);
				}
			}

			if(Fee.Input.Input.GetInstance().mouse.left.down == true){
				//ステップ。
				Fee.Time.Time.GetInstance().StepOneFrame(1.0f);
			}
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
			Fee.Time.Time.GetInstance().SetNextFrameTimeScale(1.0f);

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

