

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief エクセル。パラメータリストアイテム。
*/


/** Fee.Excel
*/
namespace Fee.Excel
{
	/** ParamListItem
	*/
	public readonly struct ParamListItem
	{
		/** パラメータタイプ。
		*/
		public readonly ParamType param_type;

		/** パラメメータ名。
		*/
		public readonly string param_name;

		/** 位置。
		*/
		public readonly int pos_x;

		/** constructor
		*/
		public ParamListItem(ParamType a_param_type,string a_param_name,int a_pos_x)
		{
			//param_type
			this.param_type = a_param_type;

			//param_name
			this.param_name = a_param_name;

			//pos_x
			this.pos_x = a_pos_x;
		}
	}
}

