
/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テストステータス。
*/


/** TestScript
*/
namespace TestScript
{
	/** TestStatus
	*/
	public class TestStatus
	{
		/** view
		*/
		public string view;

		/** scenename
		*/
		public string scenename;

		/** detailtext
		*/
		public string detailtext;

		/** constructor
		*/
		public TestStatus(string a_view,string a_scenename,string a_detailtext)
		{
			//view
			this.view = a_view;

			//scenename
			this.scenename = a_scenename;

			//detailtext
			this.detailtext = a_detailtext;
		}
	}
}

