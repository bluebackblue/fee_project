using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。コンフィグ。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** Config
	*/
	public class Config
	{
		/** ログ。
		*/
		public static bool LOG_ENABLE = false;

		/** アサート。
		*/
		public static bool ASSERT_ENABLE = true;

		/** USE_ASYNC
		*/
		#if(UNITY_WEBGL)
		public static bool USE_ASYNC = false;
		#else
		public static bool USE_ASYNC = true;
		#endif

		/** サウンドプールダウンロード前にデータバージョンのチェックを行う。
		*/
		public static bool SOUNDPOOL_CHECK_DATAVERSION = true;
	}
}

