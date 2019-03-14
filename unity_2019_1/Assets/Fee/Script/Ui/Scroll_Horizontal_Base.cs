

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。スクロール。横スクロール。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Scroll_Horizontal_Base
	*/
	public abstract class Scroll_Horizontal_Base<ITEM> : Scroll_List_Base<ITEM>
		where ITEM : ScrollItem_Base
	{
		/** constructor
		*/
		public Scroll_Horizontal_Base(Fee.Deleter.Deleter a_deleter,long a_drawpriority,int a_item_length)
			:
			base(a_deleter,a_drawpriority,a_item_length)
		{
		}

		/** [Scroll_List_Base]アイテムの位置更新。スクロール方向の座標。
		*/
		protected override void UpdateItemPos(ITEM a_item,int a_index)
		{
			int t_x = this.rect.x + a_index * this.item_length - this.view_position;
			a_item.SetX(t_x);
		}

		/** [Scroll_List_Base]アイテムの位置更新。スクロール方向ではない座標。
		*/
		protected override void UpdateItemOtherPos(ITEM a_item,int a_index)
		{
			a_item.SetY(this.rect.y);
		}

		/** [Scroll_List_Base]表示幅更新。
		*/
		protected override void UpdateViewLength()
		{
			this.view_length = this.rect.w;
		}
	}
}

