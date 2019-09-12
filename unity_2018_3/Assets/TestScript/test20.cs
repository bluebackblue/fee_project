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
	/** test20

		タスク

	*/
	public class test20 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test20.ButtonID>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test20",
				"test20",

				@"
				タスク
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** button
		*/
		private Fee.Ui.Button[] button;
		private Fee.TaskW.Task<int>[] task;
		private Fee.TaskW.CancelToken[]  canceltoken;

		/** ButtonID
		*/
		public enum ButtonID
		{
			StartA,
			StartB,
			StartC,
		}

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

			//フォント。
			Font t_font = Resources.Load<Font>(Data.Resources.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			{
				this.button = new Fee.Ui.Button[3];
				this.task = new Fee.TaskW.Task<int>[3];
				this.canceltoken = new Fee.TaskW.CancelToken[3];

				for(int ii=0;ii<this.button.Length;ii++){
					int t_x = 100;
					int t_y = 100 + ii * 100;
					int t_w = 100;
					int t_h = 60;

					//button
					this.button[ii] = new Fee.Ui.Button(this.deleter,1);
					this.button[ii].SetOnButtonClick(this,ButtonID.StartA + ii);
					this.button[ii].SetTextureCornerSize(10);
					this.button[ii].SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button[ii].SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button[ii].SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button[ii].SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button[ii].SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
					this.button[ii].SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
					this.button[ii].SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
					this.button[ii].SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
					this.button[ii].SetText("開始");
					this.button[ii].SetRect(t_x,t_y,t_w,t_h);

					//task
					this.task[ii] = null;

					//canceltoken
					this.canceltoken[ii] = new Fee.TaskW.CancelToken();
				}
			}
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonID a_id)
		{
			switch(a_id){
			case ButtonID.StartA:
			case ButtonID.StartB:
			case ButtonID.StartC:
				{
					int t_index = a_id - ButtonID.StartA;

					//終了。
					if(this.task[t_index] != null){
						//キャンセルを通知。
						this.canceltoken[t_index].Cancel();
						this.task[t_index].Wait();
						this.task[t_index].Dispose();
						this.task[t_index] = null;
					}

					//キャンセルのリセット。
					this.canceltoken[t_index].Reset();

					//開始。
					this.task[t_index] = new Fee.TaskW.Task<int>(() => {
						
						for(int ii=0;ii<100;ii++){

							//Sleep
							System.Threading.Thread.Sleep(1000);

							//ログ表示。
							UnityEngine.Debug.Log(t_index.ToString());

							//キャンセル。
							if(this.canceltoken[t_index].IsCancellationRequested() == true){
								this.canceltoken[t_index].ThrowIfCancellationRequested();
								return t_index;
							}
						}

						return t_index;
					});
				}break;
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

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

		/** OnDestroy
		*/
		private void OnDestroy()
		{
			for(int ii=0;ii<this.task.Length;ii++){
				//終了。
				if(this.task[ii] != null){
					//キャンセルを通知。
					this.canceltoken[ii].Cancel();
					this.task[ii].Wait();
					this.task[ii].Dispose();
					this.task[ii] = null;
				}
			}

			this.deleter.DeleteAll();
		}
	}
}

