

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief オーディオ。コンフィグ。
*/


/** Fee.Audio
*/
namespace Fee.Audio
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

		/** デフォルトボリューム。マスター。
		*/
		public static float DEFAULT_VOLUME_MASTER = 0.7f;

		/** デフォルトボリューム。ＳＥ。
		*/
		public static float DEFAULT_VOLUME_SE = 0.7f;

		/** デフォルトボリューム。ＢＧＭ。
		*/
		public static float DEFAULT_VOLUME_BGM = 0.7f;

		/** クロスフェード速度。
		*/
		public static float BGM_CROSSFADE_SPEED = 0.02f;

		/** ＢＧＭ再生。フェードイン。
		*/
		public static bool BGM_PLAY_FADEIN = true;

		/** プレイヤーループ。追加先
		*/
		public static System.Type PLAYERLOOP_TARGETTYPE = typeof(UnityEngine.Experimental.PlayerLoop.PreLateUpdate);
		
		/** プレイヤーループ。追加方法。
		*/
		public static Fee.PlayerLoopSystem.AddType PLAYERLOOP_ADDTYPE = Fee.PlayerLoopSystem.AddType.AddLast;
	}
}

