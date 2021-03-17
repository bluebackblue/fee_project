

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。スクロールアイテム。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** ScrollItem_Base
	*/
	public abstract class ScrollItem_Base : Fee.Deleter.OnDelete_CallBackInterface
	{
		/** capture_viewout
		*/
		private bool capture_viewout;

		/** constructor
		*/
		public ScrollItem_Base()
		{
		}

		/** キャプチャアイテム表示範囲外移動フラグ。設定。
		*/
		public void SetCaptureViewOutFlag(bool a_flag)
		{
			this.capture_viewout = a_flag;
		}

		/** キャプチャアイテム表示範囲外移動フラグ。取得。
		*/
		public bool GetCaptureViewOutFlag()
		{
			return this.capture_viewout;
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public abstract void OnDelete();

		/** [Fee.Ui.ScrollItem_Base]矩形変更。
		*/
		public abstract void OnChangeRectX(int a_x);

		/** [Fee.Ui.ScrollItem_Base]矩形変更。
		*/
		public abstract void OnChangeRectY(int a_y);

		/** [Fee.Ui.ScrollItem_Base]矩形変更。

			a_w : parent_w / item_length
			a_h : parent_h / item_length

		*/
		public abstract void OnChangeParentRectWH(int a_w,int a_h);

		/** [Fee.Ui.ScrollItem_Base]クリップ矩形変更。
		*/
		public abstract void OnChangeParentClipRect(in Fee.Geometry.Rect2D_R<int> a_parent_rect);

		/** [Fee.Ui.ScrollItem_Base]描画プライオリティ変更。
		*/
		public abstract void OnChangeParentDrawPriority(long a_parent_drawpriority);

		/** [Fee.Ui.ScrollItem_Base]表示内。
		*/
		public abstract void OnViewIn();

		/** [Fee.Ui.ScrollItem_Base]表示外。
		*/
		public abstract void OnViewOut();
	}
}

