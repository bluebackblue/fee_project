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

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** Item
		*/
		private class Item : Fee.Ui.OnButtonClick_CallBackInterface<Item.ButtonID>
		{
			/** task
			*/
			public Fee.TaskW.Task<int> task;

			/** canceltoken
			*/
			public Fee.TaskW.CancelToken canceltoken;

			/** button_start
			*/
			public Fee.Ui.Button button_start;

			/** button_end
			*/
			public Fee.Ui.Button button_end;

			/** text
			*/
			public Fee.Render2D.Text2D text;

			/** resultvalue
			*/
			public int resultvalue;

			/** ButtonID
			*/
			public enum ButtonID
			{
				/** Start
				*/
				Start,

				/** End
				*/
				End,
			}

			/** constructor
			*/
			public Item(Common.PrefabList a_prefablist,Fee.Deleter.Deleter a_deleter,int a_index)
			{
				//task
				this.task = null;

				//canceltoken
				this.canceltoken = new Fee.TaskW.CancelToken();

				{
					int t_x = 100;
					int t_y = 100 + a_index * 100;
					int t_w = 90;
					int t_h = 60;

					//button_start
					this.button_start = a_prefablist.CreateButton(a_deleter,1);
					this.button_start.SetOnButtonClick(this,ButtonID.Start);
					this.button_start.SetText("開始");
					this.button_start.SetRect(t_x,t_y,t_w,t_h);
				}

				{
					int t_x = 100 + 100;
					int t_y = 100 + a_index * 100;
					int t_w = 90;
					int t_h = 60;

					//button_end
					this.button_end = a_prefablist.CreateButton(a_deleter,1);
					this.button_end.SetOnButtonClick(this,ButtonID.End);
					this.button_end.SetText("停止");
					this.button_end.SetRect(t_x,t_y,t_w,t_h);
				}

				{
					int t_x = 100 + 200;
					int t_y = 100 + a_index * 100 + 20;

					//text
					this.text = a_prefablist.CreateText(a_deleter,1);
					this.text.SetRect(t_x,t_y,0,0);
					this.text.SetText("");
				}
			}

			/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
			*/
			public void OnButtonClick(ButtonID a_id)
			{
				switch(a_id){
				case ButtonID.End:
					{
						//キャンセルを通知。
						this.canceltoken.Cancel();
					}break;
				case ButtonID.Start:
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
								Fee.TaskW.Task.Sleep(200);

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
					}break;
				}
			}

			/** 更新。
			*/
			public void Update()
			{
				this.text.SetText(this.resultvalue.ToString());
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

		/** list
		*/
		private Item[] list;

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

			this.list = new Item[3];
			for(int ii=0;ii<this.list.Length;ii++){
				this.list[ii] = new Item(this.prefablist,this.deleter,ii);
			}
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
			//list
			for(int ii=0;ii<this.list.Length;ii++){
				this.list[ii].Update();
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

