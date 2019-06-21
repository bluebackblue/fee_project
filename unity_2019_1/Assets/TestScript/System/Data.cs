
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

			/** PREFAB_BGM
			*/
			public const string PREFAB_BGM = "Common/Prefab/bgm";

			/** PREFAB_SE
			*/
			public const string PREFAB_SE = "Common/Prefab/se";

			/** KEY_PRIVATE
			*/
			public const string KEY_PRIVATE = "Common/Key/private_key";

			/** KEY_PUBLIC
			*/
			public const string KEY_PUBLIC = "Common/Key/public_key";

			/** KEY_PRIVATE_ASSETSPATH
			*/
			public const string KEY_PRIVATE_ASSETSPATH = "Resources/Common/Key/private_key.json";

			/** KEY_PUBLIC_ASSETSPATH
			*/
			public const string KEY_PUBLIC_ASSETSPATH = "Resources/Common/Key/public_key.json";



			/** TEST04_TEXTURE
			*/
			public const string TEST04_TEXTURE = "Test04/texture";

			/** TEST04_TEXT
			*/
			public const string TEST04_TEXT = "Test04/text";

		}

		/** StreamingAssets
		*/
		public static class StreamingAssets
		{
			/** SOUNDPOOL_SE
			*/
			public const string SOUNDPOOL_SE = "Common/SoundPool/se.txt";

			/** BGM
			*/
			/*
			public const string BGM_ANDROID = "Common/AssetBundle/Android/bgm";
			public const string BGM_WEBGL = "Common/AssetBundle/WebGL/bgm";
			public const string BGM_STANDALONEWINDOWS = "Common/AssetBundle/StandaloneWindows/bgm";
			*/

			/** SE
			*/
			/*
			public const string SE_ANDROID = "Common/AssetBundle/Android/se";
			public const string SE_WEBGL = "Common/AssetBundle/WebGL/se";
			public const string SE_STANDALONEWINDOWS = "Common/AssetBundle/StandaloneWindows/se";
			*/

			/** TEST04_TEXT
			*/
			public const string TEST04_TEXT = "Test04/text.txt";

			/** TEST04_BINARY
			*/
			public const string TEST04_BINARY = "Test04/binary";

			/** TEST04_TEXTURE
			*/
			public const string TEST04_TEXTURE = "Test04/texture.png";
		}

		/** Url
		*/
		public static class Url
		{
			/** SOUNDPOOL_SE
			*/
			public const string SOUNDPOOL_SE = "https://bbbproject.sakura.ne.jp/www/project_webgl/SoundPool/se.txt";




			/** TEST04_TEXT
			*/
			public const string TEST04_TEXT = "https://bbbproject.sakura.ne.jp/www/project_webgl/Test04/text.txt";

			/** TEST04_BINARY
			*/
			public const string TEST04_BINARY = "https://bbbproject.sakura.ne.jp/www/project_webgl/Test04/binary";

			/** TEST04_TEXTURE
			*/
			public const string TEST04_TEXTURE = "https://bbbproject.sakura.ne.jp/www/project_webgl/Test04/texture.png";
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

