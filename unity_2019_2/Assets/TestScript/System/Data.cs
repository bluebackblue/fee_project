
/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。
*/


/** TestScript
*/
namespace TestScript
{
	/** Data
	*/
	namespace Data
	{
		/** Resources
		*/
		public static class Resources
		{
			/** UI_TEXTURE_BUTTON
			*/
			public const string UI_TEXTURE_BUTTON = "Common/Texture/ui_button";

			/** UI_TEXTURE_BUTTON_ACTIVE
			*/
			public const string UI_TEXTURE_BUTTON_ACTIVE = "Common/Texture/ui_button_active";

			/** UI_TEXTURE_SLIDER
			*/
			public const string UI_TEXTURE_SLIDER = "Common/Texture/ui_slider";

			/** UI_TEXTURE_CHECKBUTTON
			*/
			public const string UI_TEXTURE_CHECKBUTTON = "Common/Texture/ui_checkbutton";

			/** TEXTURE_SKYIMAGE
			*/
			public const string TEXTURE_SKYIMAGE = "Common/Texture/skyimage";

			/** FONT
			*/
			public const string FONT = "Common/Font/mplus-1p-medium";

			/** PREFAB_CUBE
			*/
			public const string PREFAB_CUBE = "Common/Prefab/cube";





			/** TEST04_TEXTURE
			*/
			public const string TEST04_TEXTURE = "Test04/texture";

			/** TEST04_TEXT
			*/
			public const string TEST04_TEXT = "Test04/text";

			/** TEST07_KEY_PRIVATE
			*/
			public const string TEST07_KEY_PRIVATE = "Test07/private_key";

			/** TEST07_KEY_PUBLIC
			*/
			public const string TEST07_KEY_PUBLIC = "Test07/public_key";

			/** TEST07_KEY_PRIVATE_ASSETSPATH
			*/
			public const string TEST07_KEY_PRIVATE_ASSETSPATH = "Resources/Test07/private_key.json";

			/** TEST07_KEY_PUBLIC_ASSETSPATH
			*/
			public const string TEST07_KEY_PUBLIC_ASSETSPATH = "Resources/Test07/public_key.json";

			/** TEST11_PREFAB_BGM
			*/
			public const string TEST11_PREFAB_BGM = "Test11/bgm";

			/** TEST11_PREFAB_SE
			*/
			public const string TEST11_PREFAB_SE = "Test11/se";

		}

		/** StreamingAssets
		*/
		public static class StreamingAssets
		{
			/** TEST04_TEXT
			*/
			public const string TEST04_TEXT = "Test04/text.txt";

			/** TEST04_BINARY
			*/
			public const string TEST04_BINARY = "Test04/binary";

			/** TEST04_TEXTURE
			*/
			public const string TEST04_TEXTURE = "Test04/texture.png";

			/** TEST04_SOUNDPOOL_SE
			*/
			public const string TEST04_SOUNDPOOL_SE = "Test04/SoundPool/se.txt";

			/** TEST11_SOUNDPOOL_SE
			*/
			public const string TEST11_SOUNDPOOL_SE = "Test11/SoundPool/se.txt";

		}

		/** Url
		*/
		public static class Url
		{
			/** SOUNDPOOL_SE
			*/
			public const string SOUNDPOOL_SE = "https://bbbproject.sakura.ne.jp/www/project_webgl/SoundPool/se.txt";




			/** TESTDATA_TEXT
			*/
			public const string TESTDATA_TEXT = "https://bbbproject.sakura.ne.jp/www/project_webgl/TestData/text.txt";

			/** TESTDATA_BINARY
			*/
			public const string TESTDATA_BINARY = "https://bbbproject.sakura.ne.jp/www/project_webgl/TestData/binary";

			/** TESTDATA_TEXTURE
			*/
			public const string TESTDATA_TEXTURE = "https://bbbproject.sakura.ne.jp/www/project_webgl/TestData/texture.png";
		}

		/** Local
		*/
		public static class Local
		{
			/** SOUNDPOOL_SE
			*/
			public const string SOUNDPOOL_SE = "se.txt";




			/** TEST04_TEXT
			*/
			public const string TEST04_TEXT = "text.txt";

			/** TEST04_BINARY
			*/
			public const string TEST04_BINARY = "binary";

			/** TEST04_TEXTURE
			*/
			public const string TEST04_TEXTURE = "texture.png";
		}

		/** Assets
		*/
		#if(UNITY_EDITOR)
		public static class Assets
		{
			/** EXCEL
			*/
			public const string EXCEL = "Editor/Excel/excel.xlsx";
		}
		#endif
	}
}

