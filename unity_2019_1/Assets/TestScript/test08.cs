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
	/** test08

		ディレクトリ探査

	*/
	public class test08 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test08",
				"test08",

				@"
				ディレクトリ探査
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;
		private Fee.Deleter.Deleter deleter_scrollitem;

		/** リストアイテム。
		*/
		private class Scroll_Item : Fee.Ui.ScrollItem_Base , Fee.Ui.OnButtonClick_CallBackInterface<int>
		{
			/** button
			*/
			public Fee.Ui.Button button;

			/** text
			*/
			public Fee.Render2D.Text2D text;

			/** name
			*/
			public string name;

			/** CallBackType_Select
			*/
			public delegate void CallBackType_Select(string a_name);
			public CallBackType_Select callback;
			public string callback_path;

			/** GetItemLength
			*/
			public static int GetItemLength()
			{
				return 40;
			}

			/** constructor
			*/
			public Scroll_Item(Fee.Deleter.Deleter a_deleter,string a_name,CallBackType_Select a_callback,string a_callback_path)
			{
				//button
				if(a_callback != null){
					this.button = new Fee.Ui.Button(a_deleter,1);
					this.button.SetOnButtonClick(this,-1);
					this.button.SetClip(true);
					this.button.SetTextureCornerSize(10);
					this.button.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
					this.button.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
					this.button.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
					this.button.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);
				}else{
					this.button = null;
				}

				//text
				this.text = new Fee.Render2D.Text2D(a_deleter,2);
				this.text.SetRect(0,0,0,0);
				this.text.SetText(a_name);
				this.text.SetClip(true);

				//name
				this.name = a_name;

				//callback
				this.callback = a_callback;

				//callback_path
				this.callback_path = a_callback_path;
			}

			/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
			*/
			public void OnButtonClick(int a_id)
			{
				if(this.callback != null){
					this.callback(this.callback_path);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]矩形。設定。
			*/
			public override void SetY(int a_y)
			{
				this.text.SetY(a_y + 10);
				if(this.button != null){
					this.button.SetY(a_y);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]矩形。設定。
			*/
			public override void SetX(int a_x)
			{
				this.text.SetX(20 + a_x);
				if(this.button != null){
					this.button.SetX(a_x);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]矩形。設定。
			*/
			public override void SetWH(int a_w,int a_h)
			{
				if(this.button != null){
					this.button.SetWH(a_w - 10,a_h);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]クリップ矩形。設定。
			*/
			public override void SetClipRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
			{
				this.text.SetClipRect(ref a_rect);
				if(this.button != null){
					this.button.SetClipRect(ref a_rect);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]表示内。
			*/
			public override void OnViewIn()
			{
				this.text.SetVisible(true);
				if(this.button != null){
					this.button.SetVisible(true);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]表示外。
			*/
			public override void OnViewOut()
			{
				this.text.SetVisible(false);
				if(this.button != null){
					this.button.SetVisible(false);
				}
			}
		}

		/** scroll
		*/
		private Fee.Render2D.Text2D text;
		private Fee.Ui.Scroll<Scroll_Item> scroll;
		private System.Collections.Generic.List<string> prev_list;
		private string now_path;

		/** Start
		*/
		private void Start()
		{
			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

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
			this.deleter_scrollitem = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			this.text = new Fee.Render2D.Text2D(this.deleter,0);
			this.text.SetRect(100,100,0,0);
			
			this.scroll = new Fee.Ui.Scroll<Scroll_Item>(this.deleter,0,Fee.Ui.ScrollType.Vertical,Scroll_Item.GetItemLength());
			this.scroll.SetRect(this.text.GetX(),this.text.GetY() + 30,550,250);

			this.prev_list = new List<string>();
			this.now_path = Fee.File.Path.CreateLocalPath().GetPath();

			//Listup
			this.Listup(this.now_path);
		}

		/** リストアップ。
		*/
		private void Listup(string a_path)
		{
			Fee.Directory.Item t_item_root = Fee.Directory.Directory.GetDirectoryItem(a_path);

			//text
			this.text.SetText(t_item_root.GetRoot().GetFullPath());
		
			//removeall
			this.scroll.RemoveAllItem();
			this.deleter_scrollitem.DeleteAll();

			if(this.prev_list.Count > 0){
				this.scroll.AddItem(new Scroll_Item(this.deleter_scrollitem,"..",this.CallBackType_Select,".."),this.scroll.GetListCount());
			}

			//directory
			List<Fee.Directory.Item> t_directory_list = t_item_root.GetDirectoryList();
			for(int ii=0;ii<t_directory_list.Count;ii++){
				string t_path = t_item_root.GetRoot().GetFullPath() + t_directory_list[ii].GetName();
				this.scroll.AddItem(new Scroll_Item(this.deleter_scrollitem,t_directory_list[ii].GetName(),this.CallBackType_Select,t_path),this.scroll.GetListCount());
			}

			//file
			List<Fee.Directory.Item> t_file_list = t_item_root.GetFileList();
			for(int ii=0;ii<t_file_list.Count;ii++){
				this.scroll.AddItem(new Scroll_Item(this.deleter_scrollitem,t_file_list[ii].GetName(),null,null),this.scroll.GetListCount());
			}
		}

		/** 選択。
		*/
		private void CallBackType_Select(string a_path)
		{
			if(a_path == ".."){
				//戻る。
				this.now_path = this.prev_list[this.prev_list.Count - 1];
				this.prev_list.RemoveAt(this.prev_list.Count - 1);
			}else{
				//進む。
				this.prev_list.Add(this.now_path);
				this.now_path = a_path;
			}

			this.Listup(this.now_path);
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//ドラッグスクロールアップデート。
			this.scroll.DragScrollUpdate();
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
			this.deleter_scrollitem.DeleteAll();
		}
	}
}

