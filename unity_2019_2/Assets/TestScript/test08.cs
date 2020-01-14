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

		/** texturelist
		*/
		private Fee.Instantiate.TextureList texturelist;

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
			public Scroll_Item(Fee.Deleter.Deleter a_deleter,string a_name,UnityEngine.Texture2D a_texture,CallBackType_Select a_callback,string a_callback_path)
			{
				//button
				if(a_callback != null){
					this.button = new Fee.Ui.Button(a_deleter,1);
					this.button.SetOnButtonClick(this,-1);
					this.button.SetClip(true);
					this.button.SetTextureCornerSize(10);
					this.button.SetNormalTexture(a_texture);
					this.button.SetOnTexture(a_texture);
					this.button.SetDownTexture(a_texture);
					this.button.SetLockTexture(a_texture);
					this.button.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
					this.button.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
					this.button.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
					this.button.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
				}else{
					this.button = null;
				}

				//text
				this.text = Fee.Render2D.Text2D.Create(a_deleter,2);
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

			/** [Fee.Ui.ScrollItem_Base]矩形変更。
			*/
			public override void OnChangeParentRectX(int a_parent_x)
			{
				this.text.SetX(20 + a_parent_x);
				if(this.button != null){
					this.button.SetX(a_parent_x);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]矩形変更。
			*/
			public override void OnChangeParentRectY(int a_parent_y)
			{
				this.text.SetY(a_parent_y + 10);
				if(this.button != null){
					this.button.SetY(a_parent_y);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]矩形変更。
			*/
			public override void OnChangeParentRectWH(int a_parent_w,int a_parent_h)
			{
				if(this.button != null){
					this.button.SetWH(a_parent_w - 10,a_parent_h);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]クリップ矩形変更。
			*/
			public override void OnChangeParentClipRect(in Fee.Geometry.Rect2D_R<int> a_parent_rect)
			{
				this.text.SetClipRect(in a_parent_rect);
				if(this.button != null){
					this.button.SetClipRect(in a_parent_rect);
				}
			}

			/** [Fee.Ui.ScrollItem_Base]描画プライオリティ変更。
			*/
			public override void OnChangeParentDrawPriority(long a_parent_drawpriority)
			{
				this.text.SetDrawPriority(a_parent_drawpriority + 1);
				if(this.button != null){
					this.button.SetDrawPriority(a_parent_drawpriority + 1);
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
			//プラットフォーム。インスタンス作成。
			Fee.Platform.Platform.CreateInstance();

			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
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
			{
				UnityEngine.GameObject t_prefab = UnityEngine.Resources.Load<UnityEngine.GameObject>("FontList");
				Fee.Instantiate.FontList t_fontlist = new Fee.Instantiate.FontList(t_prefab.GetComponent<Fee.Instantiate.FontList_MonoBehaviour>());
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_fontlist.GetFont("FONT"));
			}

			//テクスチャーリスト。
			{
				UnityEngine.GameObject t_prefab = UnityEngine.Resources.Load<UnityEngine.GameObject>("TextureList");
				this.texturelist = new Fee.Instantiate.TextureList(t_prefab.GetComponent<Fee.Instantiate.TextureList_MonoBehaviour>());
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();
			this.deleter_scrollitem = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,this.texturelist.GetTexture("UI_BUTTON"),(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			this.text = Fee.Render2D.Text2D.Create(this.deleter,0);
			this.text.SetRect(100,100,0,0);
			
			this.scroll = new Fee.Ui.Scroll<Scroll_Item>(this.deleter,0,Fee.Ui.Scroll_Type.Vertical,Scroll_Item.GetItemLength());
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
				this.scroll.AddItem(new Scroll_Item(this.deleter_scrollitem,"..",this.texturelist.GetTexture("UI_BUTTON"),this.CallBackType_Select,".."),this.scroll.GetListCount());
			}

			//directory
			List<Fee.Directory.Item> t_directory_list = t_item_root.GetDirectoryList();
			for(int ii=0;ii<t_directory_list.Count;ii++){
				string t_path = t_item_root.GetRoot().GetFullPath() + t_directory_list[ii].GetName();
				this.scroll.AddItem(new Scroll_Item(this.deleter_scrollitem,t_directory_list[ii].GetName(),this.texturelist.GetTexture("UI_BUTTON"),this.CallBackType_Select,t_path),this.scroll.GetListCount());
			}

			//file
			List<Fee.Directory.Item> t_file_list = t_item_root.GetFileList();
			for(int ii=0;ii<t_file_list.Count;ii++){
				this.scroll.AddItem(new Scroll_Item(this.deleter_scrollitem,t_file_list[ii].GetName(),this.texturelist.GetTexture("UI_BUTTON"),null,null),this.scroll.GetListCount());
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
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(this.is_focus,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//ドラッグスクロールアップデート。
			this.scroll.DragScrollUpdate();

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
			//削除。
			this.deleter.DeleteAll();
			this.deleter_scrollitem.DeleteAll();

			//ライブラリ停止。
			DeleteLibInstance.DeleteAll();
		}
	}
}

