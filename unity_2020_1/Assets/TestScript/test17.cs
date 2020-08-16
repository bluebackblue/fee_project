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
	/** test17

		スクロール

	*/
	public class test17 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test17.ButtonId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test17",
				"test17",

				@"
				スクロール
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** ScrollItem
		*/
		public class ScrollItem : Fee.Ui.ScrollItem_Base  , Fee.Ui.OnButtonClick_CallBackInterface<int> /*, Fee.Deleter.OnDelete_CallBackInterface*/
		{
			/** CallBack
			*/
			public delegate void CallBack_ScrollItem(ScrollItem a_item);

			/** deleter
			*/
			private Fee.Deleter.Deleter deleter;

			/** create_id
			*/
			private int create_id;

			/** scroll_type
			*/
			private Fee.Ui.Scroll_Type scroll_type;

			/** コールバック。
			*/
			private CallBack_ScrollItem callback;

			/** sprite
			*/
			private Fee.Ui.Sprite_Clip sprite;

			/** text
			*/
			private Fee.Render2D.Text2D text;

			/** button
			*/
			private Fee.Ui.Button button;

			/** 矩形。取得。
			*/
			public static int GetW()
			{
				return 100;
			}

			/** 矩形。取得。
			*/
			public static int GetH()
			{
				return 30;
			}

			/** constructor
			*/
			public ScrollItem(Common.PrefabList a_prefablist,Fee.Deleter.Deleter a_deleter,int a_create_id,CallBack_ScrollItem a_callback,Fee.Ui.Scroll_Type a_scroll_type)
			{
				//deleter
				this.deleter = new Fee.Deleter.Deleter();

				//create_id
				this.create_id = a_create_id;

				//scroll_type
				this.scroll_type = a_scroll_type;

				//callback
				this.callback = a_callback;

				//drawpriority
				long t_drawpriority = 1;

				//sprite
				this.sprite = a_prefablist.CreateClipSprite(this.deleter,t_drawpriority);
				this.sprite.SetTexture(Texture2D.whiteTexture);
				this.sprite.SetClipRect(0,0,0,0);
				this.sprite.SetColor(Random.value,Random.value,Random.value,1.0f);
				this.sprite.SetClip(true);
				this.sprite.SetVisible(false);

				//text
				this.text = a_prefablist.CreateText(this.deleter,t_drawpriority);
				this.text.SetRect(0,0,0,0);
				this.text.SetClipRect(0,0,0,0);
				this.text.SetText(this.create_id.ToString());
				this.text.SetClip(true);
				this.text.SetVisible(false);

				//button
				this.button = a_prefablist.CreateButton(this.deleter,t_drawpriority + 1);
				this.button.SetOnButtonClick(this,-1);
				this.button.SetRect(0,0,20,20);
				this.button.SetClipRect(0,0,0,0);
				this.button.SetText("o");
				this.button.SetClip(true);
				this.button.SetVisible(false);
				this.button.SetDragCancelFlag(true);

				//削除管理。
				if(a_deleter != null){
					a_deleter.Regist(this);
				}
			}

			/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
			*/
			public override void OnDelete()
			{
				this.deleter.DeleteAll();
			}

			/** [Fee.Ui.ScrollItem_Base]矩形変更。
			*/
			public override void OnChangeRectX(int a_x)
			{
				this.sprite.SetX(a_x);
				this.text.SetX(a_x);

				if(this.scroll_type == Fee.Ui.Scroll_Type.Vertical){
					//縦。
					this.button.SetX(a_x + ScrollItem.GetW() - this.button.GetW() - 15);
				}else{
					this.button.SetX(a_x + ScrollItem.GetW() - this.button.GetW() - 5);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]矩形変更。
			*/
			public override void OnChangeRectY(int a_y)
			{
				this.sprite.SetY(a_y);
				this.text.SetY(a_y);
				this.button.SetY(a_y + 5);
			}

			/** [Fee.Ui.ScrollItem_Base]矩形変更。
			*/
			public override void OnChangeParentRectWH(int a_parent_w,int a_parent_h)
			{
				if(this.scroll_type == Fee.Ui.Scroll_Type.Vertical){
					this.sprite.SetWH(a_parent_w - 10,a_parent_h);
				}else{
					this.sprite.SetWH(a_parent_w,a_parent_h - 10);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]クリップ矩形変更。
			*/
			public override void OnChangeParentClipRect(in Fee.Geometry.Rect2D_R<int> a_rect)
			{
				this.sprite.SetClipRect(in a_rect);
				this.text.SetClipRect(in a_rect);
				this.button.SetClipRect(in a_rect);
			}

			/** [Fee.Ui.ScrollItem_Base]描画プライオリティ変更。
			*/
			public override void OnChangeParentDrawPriority(long a_drawpriority)
			{
				this.sprite.SetDrawPriority(a_drawpriority + 1);
				this.text.SetDrawPriority(a_drawpriority + 1);
				this.button.SetDrawPriority(a_drawpriority + 2);
			}

			/** [Fee.Ui.ScrollItem_Base]表示内。
			*/
			public override  void OnViewIn()
			{
				this.sprite.SetVisible(true);
				this.text.SetVisible(true);
				this.button.SetVisible(true);
			}

			/** [Fee.Ui.ScrollItem_Base]表示外。
			*/
			public override void OnViewOut()
			{
				this.sprite.SetVisible(false);
				this.text.SetVisible(false);
				this.button.SetVisible(false);
			}

			/** 作成ＩＤ。取得。
			*/
			public int GetCreateID()
			{
				return this.create_id;
			}

			/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
			*/
			public void OnButtonClick(int a_id)
			{
				this.callback(this);
			}
		}

		/** scrollview
		*/
		private Fee.Ui.Scroll<ScrollItem> v_scrollview;
		private Fee.Ui.Scroll<ScrollItem> h_scrollview;
		private int v_scrollview_create_id;
		private int h_scrollview_create_id;

		/** status_text
		*/
		private Fee.Render2D.Text2D status_text;

		/** button
		*/
		private Fee.Ui.Button button_push;
		private Fee.Ui.Button button_pop;

		private Fee.Ui.Button button_insert_top;
		private Fee.Ui.Button button_remove_top;

		private Fee.Ui.Button button_insert_top_5;
		private Fee.Ui.Button button_remove_top_5;

		private Fee.Ui.Button button_insert_last_5;
		private Fee.Ui.Button button_remove_last_5;

		private Fee.Ui.Button button_up;
		private Fee.Ui.Button button_down;

		private Fee.Ui.Button button_sort_a;
		private Fee.Ui.Button button_sort_b;

		private Fee.Ui.Button button_swap;

		/** ButtonId
		*/
		public enum ButtonId
		{
			AddLast,
			RemoveLast,
			AddFirst,
			RemoveFirst,
			Insert5,
			Remove5,
			InsertLast5,
			RemoveLast5,
			MoveToFirst,
			MoveToLast,
			SortA,
			SortB,
			Swap,
		}

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

			//２Ｄ描画。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Input.CreateInstance(true,false,true,false);

			//イベントテンプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Ui.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//v_scrollview
			this.v_scrollview = Fee.Ui.Scroll<ScrollItem>.Create(this.deleter,0,Fee.Ui.Scroll_Type.Vertical,ScrollItem.GetH());
			this.v_scrollview.SetRect(200,100,100,400);
			this.v_scrollview_create_id = 0;

			//h_scrollview
			this.h_scrollview = Fee.Ui.Scroll<ScrollItem>.Create(this.deleter,0,Fee.Ui.Scroll_Type.Horizontal,ScrollItem.GetW());
			this.h_scrollview.SetRect(450,100,400,40);
			this.h_scrollview_create_id = 0;

			//status_text
			this.status_text = this.prefablist.CreateText(this.deleter,0);
			this.status_text.SetText("");
			this.status_text.SetRect(200,10,0,0);

			int t_y_index = 0;

			//button_push
			this.button_push = this.prefablist.CreateButton(this.deleter,0);
			this.button_push.SetOnButtonClick(this,ButtonId.AddLast);
			this.button_push.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_push.SetText("最後尾追加");

			t_y_index++;

			//button_pop
			this.button_pop = this.prefablist.CreateButton(this.deleter,0);
			this.button_pop.SetOnButtonClick(this,ButtonId.RemoveLast);
			this.button_pop.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_pop.SetText("最後尾削除");

			t_y_index++;

			//button_insert_top
			this.button_insert_top = this.prefablist.CreateButton(this.deleter,0);
			this.button_insert_top.SetOnButtonClick(this,ButtonId.AddFirst);
			this.button_insert_top.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_insert_top.SetText("先頭追加");

			t_y_index++;

			//button_remove_top
			this.button_remove_top = this.prefablist.CreateButton(this.deleter,0);
			this.button_remove_top.SetOnButtonClick(this,ButtonId.RemoveFirst);
			this.button_remove_top.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_remove_top.SetText("先頭削除");

			t_y_index++;

			//button_insert_top_5
			this.button_insert_top_5 = this.prefablist.CreateButton(this.deleter,0);
			this.button_insert_top_5.SetOnButtonClick(this,ButtonId.Insert5);
			this.button_insert_top_5.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_insert_top_5.SetText("挿入(５番目)");

			t_y_index++;

			//button_remove_top_5
			this.button_remove_top_5 = this.prefablist.CreateButton(this.deleter,0);
			this.button_remove_top_5.SetOnButtonClick(this,ButtonId.Remove5);
			this.button_remove_top_5.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_remove_top_5.SetText("削除(５番目)");

			t_y_index++;

			//button_insert_last_5
			this.button_insert_last_5 = this.prefablist.CreateButton(this.deleter,0);
			this.button_insert_last_5.SetOnButtonClick(this,ButtonId.InsertLast5);
			this.button_insert_last_5.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_insert_last_5.SetText("挿入(後５)");

			t_y_index++;

			//button_remove_last_5
			this.button_remove_last_5 = this.prefablist.CreateButton(this.deleter,0);
			this.button_remove_last_5.SetOnButtonClick(this,ButtonId.RemoveLast5);
			this.button_remove_last_5.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_remove_last_5.SetText("削除(後５)");

			t_y_index++;

			//button_up
			this.button_up = this.prefablist.CreateButton(this.deleter,0);
			this.button_up.SetOnButtonClick(this,ButtonId.MoveToFirst);
			this.button_up.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_up.SetText("前方に移動");

			t_y_index++;

			//button_down
			this.button_down = this.prefablist.CreateButton(this.deleter,0);
			this.button_down.SetOnButtonClick(this,ButtonId.MoveToLast);
			this.button_down.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_down.SetText("後方に移動");

			t_y_index++;

			//button_sort
			this.button_sort_a = this.prefablist.CreateButton(this.deleter,0);
			this.button_sort_a.SetOnButtonClick(this,ButtonId.SortA);
			this.button_sort_a.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_sort_a.SetText("ソート");

			t_y_index++;

			//button_sort
			this.button_sort_b = this.prefablist.CreateButton(this.deleter,0);
			this.button_sort_b.SetOnButtonClick(this,ButtonId.SortB);
			this.button_sort_b.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_sort_b.SetText("ソート");

			t_y_index++;

			//button_swap
			this.button_swap = this.prefablist.CreateButton(this.deleter,0);
			this.button_swap.SetOnButtonClick(this,ButtonId.Swap);
			this.button_swap.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_swap.SetText("SWAP(20,25)");
		}

		/** CallBack_ScrollItem_V
		*/
		private void CallBack_ScrollItem_V(ScrollItem a_item)
		{
			this.status_text.SetText("Vertical :" + a_item.GetCreateID().ToString());

			int t_index = this.v_scrollview.FindIndex(a_item);

			this.v_scrollview.Swap(t_index,t_index+1);
		}

		/** CallBack_ScrollItem_H
		*/
		private void CallBack_ScrollItem_H(ScrollItem a_item)
		{
			this.status_text.SetText("Horizontal : " + a_item.GetCreateID().ToString());

			int t_index = this.h_scrollview.FindIndex(a_item);

			this.h_scrollview.Swap(t_index,t_index+1);
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			switch(a_id){
			case ButtonId.MoveToFirst:
				{
					//上に移動。
					this.v_scrollview.SetDragScrollSpeed(this.v_scrollview.GetDragScrollSpeed() - 15.0f);
					this.h_scrollview.SetDragScrollSpeed(this.h_scrollview.GetDragScrollSpeed() - 15.0f);
				}break;
			case ButtonId.MoveToLast:
				{
					//下に移動。
					this.v_scrollview.SetDragScrollSpeed(this.v_scrollview.GetDragScrollSpeed() + 15.0f);
					this.h_scrollview.SetDragScrollSpeed(this.h_scrollview.GetDragScrollSpeed() + 15.0f);
				}break;
			case ButtonId.SortA:
				{
					//ソート。
					this.v_scrollview.Sort((ScrollItem a_test,ScrollItem a_target) => {return a_test.GetCreateID() - a_target.GetCreateID();});
					this.h_scrollview.Sort((ScrollItem a_test,ScrollItem a_target) => {return a_test.GetCreateID() - a_target.GetCreateID();});
				}break;
			case ButtonId.SortB:
				{
					//ソート。
					this.v_scrollview.Sort((ScrollItem a_test,ScrollItem a_target) => {return a_target.GetCreateID() - a_test.GetCreateID();});
					this.h_scrollview.Sort((ScrollItem a_test,ScrollItem a_target) => {return a_target.GetCreateID() - a_test.GetCreateID();});
				}break;
			case ButtonId.Swap:
				{
					//入れ替え。
					this.v_scrollview.Swap(20,25);
					this.h_scrollview.Swap(20,25);
				}break;
			case ButtonId.AddLast:
				{
					//最後尾追加。

					{
						this.v_scrollview_create_id++;
						this.v_scrollview.PushItem(new ScrollItem(this.prefablist,this.deleter,this.v_scrollview_create_id,CallBack_ScrollItem_V,Fee.Ui.Scroll_Type.Vertical));
					}
					{
						this.h_scrollview_create_id++;
						this.h_scrollview.PushItem(new ScrollItem(this.prefablist,this.deleter,this.h_scrollview_create_id,CallBack_ScrollItem_H,Fee.Ui.Scroll_Type.Horizontal));
					}
				}break;
			case ButtonId.RemoveLast:
				{
					//最後尾削除。

					{
						ScrollItem t_item = this.v_scrollview.PopItem(false);
						if(t_item != null){
							this.deleter.UnRegist(t_item);
							t_item.OnDelete();
							t_item = null;
						}
					}
					{
						ScrollItem t_item = this.h_scrollview.PopItem(false);
						if(t_item != null){
							this.deleter.UnRegist(t_item);
							t_item.OnDelete();
							t_item = null;
						}
					}
				}break;
			case ButtonId.AddFirst:
				{
					//先頭追加。

					{
						int t_index = 0;
						this.v_scrollview_create_id++;
						this.v_scrollview.AddItem(new ScrollItem(this.prefablist,this.deleter,this.v_scrollview_create_id,CallBack_ScrollItem_V,Fee.Ui.Scroll_Type.Vertical),t_index);
					}
					{
						int t_index = 0;
						this.h_scrollview_create_id++;
						this.h_scrollview.AddItem(new ScrollItem(this.prefablist,this.deleter,this.h_scrollview_create_id,CallBack_ScrollItem_H,Fee.Ui.Scroll_Type.Horizontal),t_index);
					}
				}break;
			case ButtonId.RemoveFirst:
				{
					//先頭削除。

					{
						int t_index = 0;
						ScrollItem t_item = this.v_scrollview.RemoveItem(t_index,false);
						if(t_item != null){
							this.deleter.UnRegist(t_item);
							t_item.OnDelete();
							t_item = null;
						}
					}
					{
						int t_index = 0;
						ScrollItem t_item = this.h_scrollview.RemoveItem(t_index,false);
						if(t_item != null){
							this.deleter.UnRegist(t_item);
							t_item.OnDelete();
							t_item = null;
						}
					}
				}break;
			case ButtonId.Insert5:
				{
					//追加。

					{
						int t_index = 4;
						this.v_scrollview_create_id++;
						this.v_scrollview.AddItem(new ScrollItem(this.prefablist,this.deleter,this.v_scrollview_create_id,CallBack_ScrollItem_V,Fee.Ui.Scroll_Type.Vertical),t_index);
					}
					{
						int t_index = 4;
						this.h_scrollview_create_id++;
						this.h_scrollview.AddItem(new ScrollItem(this.prefablist,this.deleter,this.h_scrollview_create_id,CallBack_ScrollItem_H,Fee.Ui.Scroll_Type.Horizontal),t_index);
					}
				}break;
			case ButtonId.Remove5:
				{
					//削除。

					{
						int t_index = 4;
						ScrollItem t_item = this.v_scrollview.RemoveItem(t_index,false);
						if(t_item != null){
							this.deleter.UnRegist(t_item);
							t_item.OnDelete();
							t_item = null;
						}
					}
					{
						int t_index = 4;
						ScrollItem t_item = this.h_scrollview.RemoveItem(t_index,false);
						if(t_item != null){
							this.deleter.UnRegist(t_item);
							t_item.OnDelete();
							t_item = null;
						}
					}
				}break;
			case ButtonId.InsertLast5:
				{
					//追加。

					{
						int t_index = this.v_scrollview.GetListCount() - 5;
						this.v_scrollview_create_id++;
						this.v_scrollview.AddItem(new ScrollItem(this.prefablist,this.deleter,this.v_scrollview_create_id,CallBack_ScrollItem_V,Fee.Ui.Scroll_Type.Vertical),t_index);
					}
					{
						int t_index = this.h_scrollview.GetListCount() - 5;
						this.h_scrollview_create_id++;
						this.h_scrollview.AddItem(new ScrollItem(this.prefablist,this.deleter,this.h_scrollview_create_id,CallBack_ScrollItem_H,Fee.Ui.Scroll_Type.Horizontal),t_index);
					}
				}break;
			case ButtonId.RemoveLast5:
				{
					//削除。

					{
						int t_index = this.v_scrollview.GetListCount() - 6;
						ScrollItem t_item = this.v_scrollview.RemoveItem(t_index,false);
						if(t_item != null){
							this.deleter.UnRegist(t_item);
							t_item.OnDelete();
							t_item = null;
						}
					}
					{
						int t_index = this.h_scrollview.GetListCount() - 6;
						ScrollItem t_item = this.h_scrollview.RemoveItem(t_index,false);
						if(t_item != null){
							this.deleter.UnRegist(t_item);
							t_item.OnDelete();
							t_item = null;
						}
					}
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
		}

		/** Update
		*/
		private void Update()
		{
			//ドラッグスクロールアップデート。
			this.v_scrollview.DragScrollUpdate(0.98f,UnityEngine.Time.deltaTime);
			this.h_scrollview.DragScrollUpdate(0.98f,UnityEngine.Time.deltaTime);
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

