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
	public class test17 : MainBase
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

		/** ScrollItem
		*/
		public class ScrollItem : Fee.Ui.ScrollItem_Base , Fee.Deleter.DeleteItem_Base
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

			/** コールバック。
			*/
			private CallBack_ScrollItem callback;

			/** sprite
			*/
			private Fee.Ui.ClipSprite sprite;

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
			public ScrollItem(Fee.Deleter.Deleter a_deleter,int a_create_id,CallBack_ScrollItem a_callback)
			{
				//deleter
				this.deleter = new Fee.Deleter.Deleter();

				//create_id
				this.create_id = a_create_id;

				//callback
				this.callback = a_callback;

				//drawpriority
				long t_drawpriority = 1;

				//sprite
				this.sprite = new Fee.Ui.ClipSprite(this.deleter,t_drawpriority);
				this.sprite.SetRect(0,0,ScrollItem.GetW(),ScrollItem.GetH());
				this.sprite.SetTexture(Texture2D.whiteTexture);
				this.sprite.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
				this.sprite.SetClipRect(0,0,0,0);
				this.sprite.SetColor(Random.value,Random.value,Random.value,1.0f);
				this.sprite.SetClip(true);
				this.sprite.SetVisible(false);

				//text
				this.text = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
				this.text.SetRect(0,0,0,0);
				this.text.SetClipRect(0,0,0,0);
				this.text.SetText(this.create_id.ToString());
				this.text.SetClip(true);
				this.text.SetVisible(false);

				//button
				this.button = new Fee.Ui.Button(this.deleter,t_drawpriority + 1,this.CallBack_Click,-1);
				this.button.SetRect(0,0,20,20);
				this.button.SetClipRect(0,0,0,0);
				this.button.SetText("o");
				this.button.SetClip(true);
				this.button.SetVisible(false);
				this.button.SetTextureCornerSize(10);
				this.button.SetDragCancelFlag(true);
				this.button.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.button.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.button.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.button.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

				//削除管理。
				if(a_deleter != null){
					a_deleter.Register(this);
				}
			}

			/** 削除。
			*/
			public void Delete()
			{
				this.deleter.DeleteAll();
			}

			/** [ScrollItem_Base]矩形。設定。
			*/
			public override void SetX(int a_x)
			{
				this.sprite.SetX(a_x);
				this.text.SetX(a_x);
				this.button.SetX(a_x + ScrollItem.GetW() - this.button.GetW() - 5);
			}

			/** [ScrollItem_Base]矩形。設定。
			*/
			public override void SetY(int a_y)
			{
				this.sprite.SetY(a_y);
				this.text.SetY(a_y);
				this.button.SetY(a_y + 5);
			}

			/** [Fee.Ui.ScrollItem_Base]矩形。設定。
			*/
			public override void SetWH(int a_w,int a_h)
			{
			}

			/** [ScrollItem_Base]クリップ矩形。設定。
			*/
			public override void SetClipRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
			{
				this.sprite.SetClipRect(ref a_rect);
				this.text.SetClipRect(ref a_rect);
				this.button.SetClipRect(ref a_rect);
			}

			/** [ScrollItem_Base]表示内。
			*/
			public override  void OnViewIn()
			{
				this.sprite.SetVisible(true);
				this.text.SetVisible(true);
				this.button.SetVisible(true);
			}

			/** [ScrollItem_Base]表示外。
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

			/** [Button_Base]コールバック。クリック。
			*/
			private void CallBack_Click(int a_id)
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

			//２Ｄ描画。
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.CreateInstance();

			//キー。インスタンス作成。
			Fee.Input.Key.CreateInstance();

			//パッド。インスタンス作成。
			Fee.Input.Pad.CreateInstance();

			//イベントテンプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
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

			//v_scrollview
			this.v_scrollview = new Fee.Ui.Scroll<ScrollItem>(this.deleter,0,Fee.Ui.ScrollType.Vertical,ScrollItem.GetH());
			this.v_scrollview.SetRect(200,100,200,400);
			this.v_scrollview_create_id = 0;

			//h_scrollview
			this.h_scrollview = new Fee.Ui.Scroll<ScrollItem>(this.deleter,0,Fee.Ui.ScrollType.Horizontal,ScrollItem.GetW());
			this.h_scrollview.SetRect(450,100,400,200);
			this.h_scrollview_create_id = 0;

			//status_text
			this.status_text = new Fee.Render2D.Text2D(this.deleter,0);
			this.status_text.SetText("");
			this.status_text.SetRect(200,10,0,0);

			int t_y_index = 0;

			//button_push
			this.button_push = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,9000);
			this.button_push.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_push.SetText("最後尾追加");
			this.button_push.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_push.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_push.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_push.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_push.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_push.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_push.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_push.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_y_index++;

			//button_pop
			this.button_pop = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,9001);
			this.button_pop.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_pop.SetText("最後尾削除");
			this.button_pop.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_pop.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_pop.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_pop.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_pop.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_pop.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_pop.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_pop.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);


			t_y_index++;

			//button_insert_top
			this.button_insert_top = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,9002);
			this.button_insert_top.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_insert_top.SetText("先頭追加");
			this.button_insert_top.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_top.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_top.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_top.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_top.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_insert_top.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_insert_top.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_insert_top.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_y_index++;

			//button_remove_top
			this.button_remove_top = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,9003);
			this.button_remove_top.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_remove_top.SetText("先頭削除");
			this.button_remove_top.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_top.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_top.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_top.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_top.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_remove_top.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_remove_top.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_remove_top.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);


			t_y_index++;

			//button_insert_top_5
			this.button_insert_top_5 = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,9004);
			this.button_insert_top_5.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_insert_top_5.SetText("挿入(５番目)");
			this.button_insert_top_5.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_top_5.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_top_5.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_top_5.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_top_5.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_insert_top_5.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_insert_top_5.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_insert_top_5.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);


			t_y_index++;

			//button_remove_top_5
			this.button_remove_top_5 = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,9005);
			this.button_remove_top_5.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_remove_top_5.SetText("削除(５番目)");
			this.button_remove_top_5.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_top_5.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_top_5.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_top_5.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_top_5.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_remove_top_5.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_remove_top_5.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_remove_top_5.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);


			t_y_index++;

			//button_insert_last_5
			this.button_insert_last_5 = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,9006);
			this.button_insert_last_5.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_insert_last_5.SetText("挿入(後５)");
			this.button_insert_last_5.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_last_5.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_last_5.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_last_5.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_insert_last_5.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_insert_last_5.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_insert_last_5.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_insert_last_5.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);


			t_y_index++;

			//button_remove_last_5
			this.button_remove_last_5 = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,9007);
			this.button_remove_last_5.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_remove_last_5.SetText("削除(後５)");
			this.button_remove_last_5.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_last_5.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_last_5.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_last_5.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_remove_last_5.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_remove_last_5.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_remove_last_5.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_remove_last_5.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);


			t_y_index++;

			//button_up
			this.button_up = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,1000);
			this.button_up.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_up.SetText("前方に移動");
			this.button_up.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_up.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_up.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_up.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_up.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_up.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_up.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_up.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_y_index++;

			//button_down
			this.button_down = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,1001);
			this.button_down.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_down.SetText("後方に移動");
			this.button_down.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_down.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_down.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_down.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_down.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_down.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_down.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_down.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_y_index++;

			//button_sort
			this.button_sort_a = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,2000);
			this.button_sort_a.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_sort_a.SetText("ソート");
			this.button_sort_a.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_sort_a.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_sort_a.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_sort_a.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_sort_a.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_sort_a.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_sort_a.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_sort_a.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_y_index++;

			//button_sort
			this.button_sort_b = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,2001);
			this.button_sort_b.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_sort_b.SetText("ソート");
			this.button_sort_b.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_sort_b.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_sort_b.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_sort_b.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_sort_b.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_sort_b.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_sort_b.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_sort_b.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_y_index++;

			//button_swap
			this.button_swap = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click,3000);
			this.button_swap.SetRect(10,100 + 30 * t_y_index,100,30);
			this.button_swap.SetText("SWAP(20,25)");
			this.button_swap.SetNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_swap.SetOnTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_swap.SetDownTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_swap.SetLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_swap.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_swap.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_swap.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_swap.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);
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

		/** [Button_Base]コールバック。クリック。
		*/
		private void CallBack_Click(int a_id)
		{
			switch(a_id){
			case 1000:
				{
					//上に移動。
					this.v_scrollview.SetDragScrollSpeed(this.v_scrollview.GetDragScrollSpeed() - 15.0f);
					this.h_scrollview.SetDragScrollSpeed(this.h_scrollview.GetDragScrollSpeed() - 15.0f);
				}break;
			case 1001:
				{
					//下に移動。
					this.v_scrollview.SetDragScrollSpeed(this.v_scrollview.GetDragScrollSpeed() + 15.0f);
					this.h_scrollview.SetDragScrollSpeed(this.h_scrollview.GetDragScrollSpeed() + 15.0f);
				}break;
			case 2000:
				{
					//ソート。
					this.v_scrollview.Sort((ScrollItem a_test,ScrollItem a_target) => {return a_test.GetCreateID() - a_target.GetCreateID();});
					this.h_scrollview.Sort((ScrollItem a_test,ScrollItem a_target) => {return a_test.GetCreateID() - a_target.GetCreateID();});
				}break;
			case 2001:
				{
					//ソート。
					this.v_scrollview.Sort((ScrollItem a_test,ScrollItem a_target) => {return a_target.GetCreateID() - a_test.GetCreateID();});
					this.h_scrollview.Sort((ScrollItem a_test,ScrollItem a_target) => {return a_target.GetCreateID() - a_test.GetCreateID();});
				}break;
			case 3000:
				{
					//入れ替え。
					this.v_scrollview.Swap(20,25);
					this.h_scrollview.Swap(20,25);
				}break;
			case 9000:
				{
					//最後尾追加。

					{
						this.v_scrollview_create_id++;
						this.v_scrollview.PushItem(new ScrollItem(this.deleter,this.v_scrollview_create_id,CallBack_ScrollItem_V));
					}
					{
						this.h_scrollview_create_id++;
						this.h_scrollview.PushItem(new ScrollItem(this.deleter,this.h_scrollview_create_id,CallBack_ScrollItem_H));
					}
				}break;
			case 9001:
				{
					//最後尾削除。

					{
						ScrollItem t_item = this.v_scrollview.PopItem();
						if(t_item != null){
							this.deleter.UnRegister(t_item);
							t_item.Delete();
							t_item = null;
						}
					}
					{
						ScrollItem t_item = this.h_scrollview.PopItem();
						if(t_item != null){
							this.deleter.UnRegister(t_item);
							t_item.Delete();
							t_item = null;
						}
					}
				}break;
			case 9002:
				{
					//先頭追加。

					{
						int t_index = 0;
						this.v_scrollview_create_id++;
						this.v_scrollview.AddItem(new ScrollItem(this.deleter,this.v_scrollview_create_id,CallBack_ScrollItem_V),t_index);
					}
					{
						int t_index = 0;
						this.h_scrollview_create_id++;
						this.h_scrollview.AddItem(new ScrollItem(this.deleter,this.h_scrollview_create_id,CallBack_ScrollItem_H),t_index);
					}
				}break;
			case 9003:
				{
					//先頭削除。

					{
						int t_index = 0;
						ScrollItem t_item = this.v_scrollview.RemoveItem(t_index);
						if(t_item != null){
							this.deleter.UnRegister(t_item);
							t_item.Delete();
							t_item = null;
						}
					}
					{
						int t_index = 0;
						ScrollItem t_item = this.h_scrollview.RemoveItem(t_index);
						if(t_item != null){
							this.deleter.UnRegister(t_item);
							t_item.Delete();
							t_item = null;
						}
					}
				}break;
			case 9004:
				{
					//追加。

					{
						int t_index = 4;
						this.v_scrollview_create_id++;
						this.v_scrollview.AddItem(new ScrollItem(this.deleter,this.v_scrollview_create_id,CallBack_ScrollItem_V),t_index);
					}
					{
						int t_index = 4;
						this.h_scrollview_create_id++;
						this.h_scrollview.AddItem(new ScrollItem(this.deleter,this.h_scrollview_create_id,CallBack_ScrollItem_H),t_index);
					}
				}break;
			case 9005:
				{
					//削除。

					{
						int t_index = 4;
						ScrollItem t_item = this.v_scrollview.RemoveItem(t_index);
						if(t_item != null){
							this.deleter.UnRegister(t_item);
							t_item.Delete();
							t_item = null;
						}
					}
					{
						int t_index = 4;
						ScrollItem t_item = this.h_scrollview.RemoveItem(t_index);
						if(t_item != null){
							this.deleter.UnRegister(t_item);
							t_item.Delete();
							t_item = null;
						}
					}
				}break;
			case 9006:
				{
					//追加。

					{
						int t_index = this.v_scrollview.GetListCount() - 5;
						this.v_scrollview_create_id++;
						this.v_scrollview.AddItem(new ScrollItem(this.deleter,this.v_scrollview_create_id,CallBack_ScrollItem_V),t_index);
					}
					{
						int t_index = this.h_scrollview.GetListCount() - 5;
						this.h_scrollview_create_id++;
						this.h_scrollview.AddItem(new ScrollItem(this.deleter,this.h_scrollview_create_id,CallBack_ScrollItem_H),t_index);
					}
				}break;
			case 9007:
				{
					//削除。

					{
						int t_index = this.v_scrollview.GetListCount() - 6;
						ScrollItem t_item = this.v_scrollview.RemoveItem(t_index);
						if(t_item != null){
							this.deleter.UnRegister(t_item);
							t_item.Delete();
							t_item = null;
						}
					}
					{
						int t_index = this.h_scrollview.GetListCount() - 6;
						ScrollItem t_item = this.h_scrollview.RemoveItem(t_index);
						if(t_item != null){
							this.deleter.UnRegister(t_item);
							t_item.Delete();
							t_item = null;
						}
					}
				}break;
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//キー。
			Fee.Input.Key.GetInstance().Main(true);

			//パッド。
			Fee.Input.Pad.GetInstance().Main(true);

			//イベントテンプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//ドラッグスクロールアップデート。
			this.v_scrollview.DragScrollUpdate();
			this.h_scrollview.DragScrollUpdate();
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

