using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** TestScript
*/
namespace TestScript
{
	/** test03

		ウィンドウ

	*/
	public class test03 : MainBase
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
		private class Window : Fee.Ui.OnWindowCallBack_Base
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
			public Window(string a_label,int a_index)
			{
				//新規作成の場合に設定する矩形。
				Fee.Render2D.Rect2D_R<int> t_new_rect = new Fee.Render2D.Rect2D_R<int>(100 + a_index * 30,100 + a_index * 30,300,300);

				//deleter
				this.deleter = new Fee.Deleter.Deleter();

				//window
				{
					this.window = new Fee.Ui.Window(this.deleter,this);
					this.window.RegisterWindowResume(a_label,ref t_new_rect);

					Color t_color = new Color(0.0f,0.0f,0.0f,0.5f);
					switch(a_index){
					case 0:
						{
							this.window.SetBgColor(ref t_color);
						}break;
					case 1:
						{
							t_color.r = 0.5f;
							this.window.SetBgColor(ref t_color);
						}break;
					case 2:
						{
							t_color.g = 0.5f;
							this.window.SetBgColor(ref t_color);
						}break;
					case 3:
						{
							t_color.b = 0.5f;
							this.window.SetBgColor(ref t_color);
						}break;
					case 4:
						{
							t_color.a = 0.5f;
							this.window.SetBgColor(ref t_color);
							this.window.SetBgMaterialType(Fee.Render2D.Config.MaterialType.Alpha);
						}break;
					default:
						{
							t_color.r = 1.0f;
							this.window.SetBgColor(ref t_color);
						}break;
					}
				}

				//drawpriority
				long t_drawpriority = 0;

				//title
				this.title = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
				this.title.SetRect(0,0,0,0);
				this.title.SetText(a_label);

				//close_button
				this.close_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click_Close,-1);
				this.close_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
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

			/** [Fee.Ui.OnWindowCallBack_Base]レイヤーインデックス変更。
			*/
			public void OnChangeLayerIndex(int a_layerindex)
			{
				long t_drawpriority = a_layerindex * Fee.Render2D.Config.DRAWPRIORITY_STEP;

				this.title.SetDrawPriority(t_drawpriority + this.window.GetTitleBarDrawPriorityOffset() + 1);
				this.close_button.SetDrawPriority(t_drawpriority + this.window.GetTitleBarDrawPriorityOffset() + 1);
			}

			/** [Fee.Ui.OnWindowCallBack_Base]矩形変更。
			*/
			public void OnChangeRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
			{
				this.title.SetXY(a_rect.x + this.window.GetTitleBarH() + 5,a_rect.y);
				this.close_button.SetXY(a_rect.x,a_rect.y);
			}

			/** [Fee.Ui.OnWindowCallBack_Base]矩形変更。
			*/
			public void OnChangeXY(int a_x,int a_y)
			{
				this.title.SetXY(a_x + this.window.GetTitleBarH() + 5,a_y);
				this.close_button.SetXY(a_x,a_y);
			}

			/** [Button_Base]コールバック。クリック。
			*/
			public void CallBack_Click_Close(int a_id)
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
			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.LOG_ENABLE = true;
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
			Font t_font = Resources.Load<Font>(Data.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//drawpriority
			long t_drawpriority = 0;

			//button
			this.button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,-1);
			this.button.SetRect(10,50,100,50);
			this.button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.button.SetText("表示");

			//view_flag
			this.view_flag = true;

			//window_list
			this.window_list = new Window[5];
			for(int ii=0;ii<this.window_list.Length;ii++){
				this.window_list[ii] = new Window("window" + ii.ToString(),ii);
			}

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
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

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();
		}

		/** [Button_Base]コールバック。クリック。
		*/
		public void CallBack_Click(int a_id)
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
						this.window_list[ii] = new Window("window" + ii.ToString(),ii);
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

		/** OnDestroy
		*/
		private void OnDestroy()
		{
			this.deleter.DeleteAll();
		}
	}
}

