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


/** test08

	ディレクトリ探査

*/
public class test08 : main_base
{
	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** リストアイテム。
	*/
	private class Scroll_Item : Fee.Ui.ScrollItem_Base
	{
		/** text
		*/
		public Fee.Render2D.Text2D text;

		/** GetItemLength
		*/
		public static int GetItemLength()
		{
			return 30;
		}

		/** constructor
		*/
		public Scroll_Item(Fee.Deleter.Deleter a_deleter,string a_text)
		{
			this.text = new Fee.Render2D.Text2D(a_deleter,1);
			this.text.SetRect(0,0,0,0);
			this.text.SetText(a_text);
			this.text.SetClip(true);
		}

		/** [Fee.Ui.ScrollItem_Base]矩形。設定。
		*/
		public override void SetY(int a_y)
		{
			this.text.SetY(a_y);
		}

		/** [Fee.Ui.ScrollItem_Base]矩形。設定。
		*/
		public override void SetX(int a_x)
		{
			this.text.SetX(a_x);
		}

		/** [Fee.Ui.ScrollItem_Base]クリップ矩形。設定。
		*/
		public override void SetClipRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			this.text.SetClipRect(ref a_rect);
		}

		/** [Fee.Ui.ScrollItem_Base]表示内。
		*/
		public override void OnViewIn()
		{
			this.text.SetVisible(true);
		}

		/** [Fee.Ui.ScrollItem_Base]表示外。
		*/
		public override void OnViewOut()
		{
			this.text.SetVisible(false);
		}
	}

	/** root
	*/
	private Fee.Render2D.Text2D root_text;
	private Fee.Ui.Scroll<Scroll_Item> root_scroll;

	/** fee
	*/
	private Fee.Render2D.Text2D fee_text;
	private Fee.Ui.Scroll<Scroll_Item> fee_scroll;

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		Fee.TaskW.TaskW.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		Fee.Render2D.Render2D.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		Fee.PerformanceCounter.Config.LOG_ENABLE = true;
		Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

		//マウス。インスタンス作成。
		Fee.Input.Config.LOG_ENABLE = true;
		Fee.Input.Mouse.CreateInstance();

		//イベントプレート。
		Fee.EventPlate.Config.LOG_ENABLE = true;
		Fee.EventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		Fee.Ui.Config.LOG_ENABLE = true;
		Fee.Ui.Ui.CreateInstance();

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		{
			this.root_text = new Fee.Render2D.Text2D(this.deleter,0);
			this.root_text.SetRect(10,100,0,0);
			this.root_scroll = new Fee.Ui.Scroll<Scroll_Item>(this.deleter,0,Fee.Ui.ScrollType.Vertical,Scroll_Item.GetItemLength());
			this.root_scroll.SetRect(this.root_text.GetX(),this.root_text.GetY() + 30,150,250);

			this.fee_text = new Fee.Render2D.Text2D(this.deleter,0);
			this.fee_text.SetRect(10 + 500,100,0,0);
			this.fee_scroll = new Fee.Ui.Scroll<Scroll_Item>(this.deleter,0,Fee.Ui.ScrollType.Vertical,Scroll_Item.GetItemLength());
			this.fee_scroll.SetRect(this.fee_text.GetX(),this.fee_text.GetY() + 30,150,250);
		}

		Fee.Directory.Item t_item_root = Fee.Directory.Directory.GetDirectoryItem(Application.dataPath);

		//ルート。
		{
			this.root_text.SetText(t_item_root.GetRoot().GetFullPath());
		
			List<Fee.Directory.Item> t_directory_list = t_item_root.GetDirectoryList();
			for(int ii=0;ii<t_directory_list.Count;ii++){
				this.root_scroll.AddItem(new Scroll_Item(this.deleter,t_directory_list[ii].GetName()),this.root_scroll.GetListCount());
			}
		}

		//ルート => Fee
		{
			Fee.Directory.Item t_item_root_fee = t_item_root.FindDirectory("Fee");

			this.fee_text.SetText(t_item_root_fee.GetName());
		
			List<Fee.Directory.Item> t_directory_list = t_item_root_fee.GetDirectoryList();
			for(int ii=0;ii<t_directory_list.Count;ii++){
				this.fee_scroll.AddItem(new Scroll_Item(this.deleter,t_directory_list[ii].GetName()),this.fee_scroll.GetListCount());
			}
			List<Fee.Directory.Item> t_file_list = t_item_root_fee.GetFileList();
			for(int ii=0;ii<t_file_list.Count;ii++){
				this.fee_scroll.AddItem(new Scroll_Item(this.deleter,t_file_list[ii].GetName()),this.fee_scroll.GetListCount());
			}
		}
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		Fee.Input.Mouse.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());

		//イベントプレート。
		Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

		//ＵＩ。
		Fee.Ui.Ui.GetInstance().Main();

		//ドラッグスクロールアップデート。
		this.root_scroll.DragScrollUpdate(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y,Fee.Input.Mouse.GetInstance().left.on);
		this.fee_scroll.DragScrollUpdate(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y,Fee.Input.Mouse.GetInstance().left.on);
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

