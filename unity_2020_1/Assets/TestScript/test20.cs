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
			public Item(int a_index,UnityEngine.Texture2D a_texture,Fee.Deleter.Deleter a_deleter)
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
					this.button_start = new Fee.Ui.Button(a_deleter,1);
					this.button_start.SetOnButtonClick(this,ButtonID.Start);
					this.button_start.SetTextureCornerSize(10);
					this.button_start.SetNormalTexture(a_texture);
					this.button_start.SetOnTexture(a_texture);
					this.button_start.SetDownTexture(a_texture);
					this.button_start.SetLockTexture(a_texture);
					this.button_start.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
					this.button_start.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
					this.button_start.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
					this.button_start.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
					this.button_start.SetText("開始");
					this.button_start.SetRect(t_x,t_y,t_w,t_h);
				}

				{
					int t_x = 100 + 100;
					int t_y = 100 + a_index * 100;
					int t_w = 90;
					int t_h = 60;

					//button_end
					this.button_end = new Fee.Ui.Button(a_deleter,1);
					this.button_end.SetOnButtonClick(this,ButtonID.End);
					this.button_end.SetTextureCornerSize(10);
					this.button_end.SetNormalTexture(a_texture);
					this.button_end.SetOnTexture(a_texture);
					this.button_end.SetDownTexture(a_texture);
					this.button_end.SetLockTexture(a_texture);
					this.button_end.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
					this.button_end.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
					this.button_end.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
					this.button_end.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
					this.button_end.SetText("停止");
					this.button_end.SetRect(t_x,t_y,t_w,t_h);
				}

				{
					int t_x = 100 + 200;
					int t_y = 100 + a_index * 100 + 20;

					//text
					this.text = Fee.Render2D.Text2D.Create(a_deleter,1);
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
								System.Threading.Thread.Sleep(200);

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
			{
				this.prefablist = new Common.PrefabList();
				this.prefablist.LoadFontList();
				this.prefablist.LoadTextureList();
			}

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont("FONT"));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,this.prefablist.GetTexture("UI_BUTTON"),(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			this.list = new Item[3];
			for(int ii=0;ii<this.list.Length;ii++){
				this.list[ii] = new Item(ii,this.prefablist.GetTexture("UI_BUTTON"),this.deleter);
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

			//list
			for(int ii=0;ii<this.list.Length;ii++){
				this.list[ii].Update();
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

