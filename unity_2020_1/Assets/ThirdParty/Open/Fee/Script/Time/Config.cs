

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief タイム。コンフィグ。
*/


/** Fee.Time
*/
namespace Fee.Time
{
	/** Config
	*/
	public class Config
	{
		/** ログ。
		*/
		public static bool LOG_ENABLE = false;

		/** ログエラー。
		*/
		public static bool LOGERROR_ENABLE = true;

		/** アサート。
		*/
		public static bool ASSERT_ENABLE = true;

		/** デバッグリスロー。
		*/
		public static bool DEBUGRETHROW_ENABLE = false;
		
		/** プレイヤーループ。追加先
		*/
		public static System.Type PLAYERLOOP_TARGETTYPE = typeof(UnityEngine.PlayerLoop.Initialization);
		
		/** プレイヤーループ。追加方法。
		*/
		public static Fee.PlayerLoopSystem.AddType PLAYERLOOP_ADDTYPE = Fee.PlayerLoopSystem.AddType.AddLast;
	}
}

