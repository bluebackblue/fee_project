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
	/** test03

		ウィンドウ

	*/
	public class test03 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<int>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test03",
				"test03",

				@"
				ウィンドウ
				"
			);
		}

		/** Window
		*/
		private class Window : Fee.Ui.OnWindow_CallBackInterface , Fee.Ui.OnButtonClick_CallBackInterface<int>
		{
			/** deleter
			*/
			private Fee.Deleter.Deleter deleter;

			/** window
			*/
			private Fee.Ui.Window window;

			/** title
			*/
			private Fee.Render2D.Text2D title;

			/** close_button
			*/
			private Fee.Ui.Button close_button;

			/** is_close
			*/
			bool is_close;

			/** constructor
			*/
			public Window(Common.PrefabList a_prefablist,string a_label,int a_index)
			{
				//新規作成の場合に設定する矩形。
				Fee.Geometry.Rect2D_R<int> t_new_rect = new Fee.Geometry.Rect2D_R<int>(100 + a_index * 30,100 + a_index * 30,300,300);

				//deleter
				this.deleter = new Fee.Deleter.Deleter();

				//window
				{
					this.window = Fee.Ui.Window.Create(this.deleter,this);
					this.window.RegistWindowResume(a_label,in t_new_rect);

					Color t_color = new Color(0.0f,0.0f,0.0f,0.5f);
					switch(a_index){
					case 0:
						{
							this.window.SetBgColor(in t_color);
						}break;
					case 1:
						{
							t_color.r = 0.5f;
							this.window.SetBgColor(in t_color);
						}break;
					case 2:
						{
							t_color.g = 0.5f;
							this.window.SetBgColor(in t_color);
						}break;
					case 3:
						{
							t_color.b = 0.5f;
							this.window.SetBgColor(in t_color);
						}break;
					case 4:
						{
							t_color.a = 0.5f;
							this.window.SetBgColor(in t_color);
							this.window.SetBgMaterialType(Fee.Render2D.Config.MaterialType.Alpha);
						}break;
					default:
						{
							t_color.r = 1.0f;
							this.window.SetBgColor(in t_color);
						}break;
					}
				}

				//drawpriority
				long t_drawpriority = 0;

				//title
				this.title = a_prefablist.CreateText(this.deleter,t_drawpriority);
				this.title.SetRect(0,0,0,0);
				this.title.SetText(a_label);

				//close_button
				this.close_button = a_prefablist.CreateButton(this.deleter,t_drawpriority);
				this.close_button.SetOnButtonClick(this,-1);
				this.close_button.SetText("x");
				this.close_button.SetWH(this.window.GetTitleBarH(),this.window.GetTitleBarH());

				//is_close
				this.is_close = false;

				//コールバック呼び出しがあるので最後に設定。
				this.window.SetRectFromWindowResumeItem();
			}

			/** 削除。
			*/
			public void Delete()
			{
				this.deleter.DeleteAll();
			}

			/** [Fee.Ui.OnWindow_CallBackInterface]レイヤーインデックス変更。
			*/
			public void OnWindowChangeLayerIndex(int a_layerindex)
			{
				long t_drawpriority = a_layerindex * Fee.Render2D.Config.DRAWPRIORITY_STEP;

				this.title.SetDrawPriority(t_drawpriority + this.window.GetTitleBarDrawPriorityOffset() + 1);
				this.close_button.SetDrawPriority(t_drawpriority + this.window.GetTitleBarDrawPriorityOffset() + 1);
			}

			/** [Fee.Ui.OnWindow_CallBackInterface]矩形変更。
			*/
			public void OnWindowChangeRect(in Fee.Geometry.Rect2D_R<int> a_rect)
			{
				this.title.SetXY(a_rect.x + this.window.GetTitleBarH() + 5,a_rect.y);
				this.close_button.SetXY(a_rect.x,a_rect.y);
			}

			/** [Fee.Ui.OnWindow_CallBackInterface]矩形変更。
			*/
			public void OnWindowChangeXY(int a_x,int a_y)
			{
				this.title.SetXY(a_x + this.window.GetTitleBarH() + 5,a_y);
				this.close_button.SetXY(a_x,a_y);
			}

			/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
			*/
			public void OnButtonClick(int a_id)
			{
				this.is_close = true;
			}

			/** IsClose
			*/
			public bool IsClose()
			{
				return this.is_close;
			}

			/** CloseRequest
			*/
			public void CloseRequest()
			{
				this.is_close = true;
			}
		
			/** 更新。
			*/
			public void Main()
			{
			}
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** button
		*/
		private Fee.Ui.Button button;

		/** view_flag
		*/
		private bool view_flag;

		/** window_list
		*/
		private Window[] window_list;

		/** Start
		*/
		private void Start()
		{
			//プレイヤーループシステム。インスタンス作成。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance();

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

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.LOG_ENABLE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Config.LOG_ENABLE = true;
			Fee.Input.Input.CreateInstance();

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

			//drawpriority
			long t_drawpriority = 0;

			//button
			this.button = this.prefablist.CreateButton(this.deleter,t_drawpriority);
			this.button.SetOnButtonClick(this,-1);
			this.button.SetRect(10,50,100,50);
			this.button.SetText("表示");

			//view_flag
			this.view_flag = true;

			//window_list
			this.window_list = new Window[5];
			for(int ii=0;ii<this.window_list.Length;ii++){
				this.window_list[ii] = new Window(this.prefablist,"window" + ii.ToString(),ii);
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//window_list
			if(this.window_list != null){
				for(int ii=0;ii<this.window_list.Length;ii++){
					if(this.window_list[ii] != null){
						this.window_list[ii].Main();
						if(this.window_list[ii].IsClose() == true){
							this.window_list[ii].Delete();
							this.window_list[ii] = null;
						}
					}
				}
			}

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Input.GetInstance().mouse.cursor.pos);

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

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(int a_id)
		{
			if(this.view_flag == true){
				this.view_flag = false;

				for(int ii=0;ii<this.window_list.Length;ii++){
					if(this.window_list[ii] != null){
						this.window_list[ii].CloseRequest();
					}
				}
			}else{
				this.view_flag = true;

				for(int ii=0;ii<this.window_list.Length;ii++){
					if(this.window_list[ii] == null){
						this.window_list[ii] = new Window(this.prefablist,"window" + ii.ToString(),ii);
					}
				}
			}
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

