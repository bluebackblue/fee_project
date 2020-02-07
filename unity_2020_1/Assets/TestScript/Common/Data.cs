

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。
*/


/** TestScript
*/
namespace TestScript.Common
{
	/** Data
	*/
	namespace Data
	{
		public static class Assets
		{
			/** TEST12_ASSETBUNDLE
			*/
			public const string TEST12_ASSETBUNDLE = "Editor/data/create_from_excel_dummyassetbundle_test.assetbundle.json";
		}

		/** Resources
		*/
		public static class Resources
		{
			/** TEST04_TEXTURE
			*/
			public const string TEST04_TEXTURE = "Test04/texture";

			/** TEST04_TEXT
			*/
			public const string TEST04_TEXT = "Test04/text";

			/** TEST14_DATA
			*/
			public const string TEST14_DATA = "Test14/datalist";
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
			public const string TEST04_BINARY = "Test04/binary.dat";

			/** TEST04_TEXTURE
			*/
			public const string TEST04_TEXTURE = "Test04/texture.png";

			/** TEST11_SOUNDPOOL_SE
			*/
			public const string TEST11_SOUNDPOOL_SE = "Test11/SoundPool/se.txt";

			/** TEST12_ASSETBUNDLE
			*/
			#if(UNITY_WEBGL)
			public const string TEST12_ASSETBUNDLE = "AssetBundle_WebGL/test.assetbundle";
			#elif(UNITY_ANDROID)
			public const string TEST12_ASSETBUNDLE = "AssetBundle_Android/test.assetbundle";
			#else
			public const string TEST12_ASSETBUNDLE = "AssetBundle_StandaloneWindows/test.assetbundle";
			#endif
		}

		/** Url
		*/
		public static class Url
		{
			/** TEST04_TEXT
			*/
			public const string TEST04_TEXT = "https://blueback.ddns.net:8081/project_fee/StreamingAssets/Test04/text.txt";

			/** TEST04_BINARY
			*/
			public const string TEST04_BINARY = "https://blueback.ddns.net:8081/project_fee/StreamingAssets/Test04/binary.dat";

			/** TEST04_TEXTURE
			*/
			public const string TEST04_TEXTURE = "https://blueback.ddns.net:8081/project_fee/StreamingAssets/Test04/texture.png";

			/** TEST12_ASSETBUNDLE
			*/
			#if(UNITY_WEBGL)
			public const string TEST12_ASSETBUNDLE = "https://blueback.ddns.net:8081/project_fee/StreamingAssets/AssetBundle_WebGL/test.assetbundle";
			#elif(UNITY_ANDROID)
			public const string TEST12_ASSETBUNDLE = "https://blueback.ddns.net:8081/project_fee/StreamingAssets/AssetBundle_Android/test.assetbundle";
			#else
			public const string TEST12_ASSETBUNDLE = "https://blueback.ddns.net:8081/project_fee/StreamingAssets/AssetBundle_StandaloneWindows/test.assetbundle";
			#endif
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
			public const string TEST04_BINARY = "binary.dat";

			/** TEST04_TEXTURE
			*/
			public const string TEST04_TEXTURE = "texture.png";
		}
	}
}

