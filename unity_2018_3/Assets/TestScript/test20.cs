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
	public class test20 : MainBase
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

		/** Item
		*/
		private class Item : Fee.Ui.OnButtonClick_CallBackInterface<int>
		{
			public Fee.TaskW.Task<int> task;
			public Fee.TaskW.CancelToken canceltoken;
			public Fee.Ui.Button button;
			public Fee.Render2D.Text2D text;
			public int resultvalue;

			/** constructor
			*/
			public Item(int a_index,Fee.Deleter.Deleter a_deleter)
			{
				//task
				this.task = null;

				//canceltoken
				this.canceltoken = new Fee.TaskW.CancelToken();

				int t_x = 100;
				int t_y = 100 + a_index * 100;
				int t_w = 100;
				int t_h = 60;

				//button
				this.button = new Fee.Ui.Button(a_deleter,1);
				this.button.SetOnButtonClick(this,0);
				this.button.SetTextureCornerSize(10);
				this.button.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.button.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.button.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.button.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
				this.button.SetText("開始");
				this.button.SetRect(t_x,t_y,t_w,t_h);

				//text
				this.text = Fee.Render2D.Render2D.GetInstance().Text2D_PoolNew(a_deleter,1);
				this.text.SetRect(t_x + 100,t_y,0,0);
				this.text.SetText("");
			}

			/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
			*/
			public void OnButtonClick(int a_id)
			{
				//終了。
				if(this.task != null){
					//キャンセルを通知。
					this.canceltoken.Cancel();
					this.task.Wait();
					this.task.Dispose();
					this.task = null;
				}

				//キャンセルのリセット。
				this.canceltoken.Reset();

				//開始。
				this.task = new Fee.TaskW.Task<int>(() => {
						
					for(int ii=0;ii<100;ii++){

						//Sleep
						System.Threading.Thread.Sleep(1);

						//キャンセル。
						if(this.canceltoken.IsCancellationRequested() == true){
							this.canceltoken.ThrowIfCancellationRequested();
							return -1;
						}

						//resultvalue
						this.resultvalue++;
					}

					return 0;
				});
			}

			/** Dispose
			*/
			public void Dispose()
			{
				if(this.task != null){
					this.canceltoken.Cancel();
					this.task.Wait();
					this.task.Dispose();
					this.task = null;
				}
			}
		};

		private Item[] list;

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

			this.list = new Item[3];
			for(int ii=0;ii<this.list.Length;ii++){
				this.list[ii] = new Item(ii,this.deleter);
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
			//list
			for(int ii=0;ii<this.list.Length;ii++){
				this.list[ii].Dispose();
			}

			//削除。
			this.deleter.DeleteAll();

			//ライブラリ停止。
			DeleteLibInstance.DeleteAll();
		}
	}
}

