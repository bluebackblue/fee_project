

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
			public const string TEST12_ASSETBUNDLE = "Editor/temp/create_from_excel_dummyassetbundle_test.assetbundle.json";
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
			/** CERTIFICATE
			*/
			public const string CERTIFICATE_NAME = "blueback";
			public const string CERTIFICATE_PATTERN = "^https\\:\\/\\/blueback\\.myqnapcloud\\.com\\:8081\\/.*$";
			public const string CERTIFICATE = "3082010A0282010100D2E5F503BA1164987405F51CE8755815B154880EC5DD42B2F7B83139AC7F4E40A0CE36240BF091AB417D8E43E5F55B94877EA378E648084F225E65E19789FD506E86304AF5FB9F674974E6CE581321837F3A8EF1CB42A2E9F0538A9C2BA83CD8A8214BC767EA3CD8EAE1A058D091D86740E3C96C0709175042D0E93ED05921CBF274BC5148F0235CF242CE9E8A2A607445CE23A9BBE3B24FA2829DCE7D79456658AD1D7D9829AF2F6DA960898BE4638CB1D1689F035C2EE2E942407344A341ACE7B120A982E355FCDAF69CF1ACB8F7F8E3B78B5F68319AD1A3FDF9F81746D2293E7F15032AFE4BDA2A154F0AF30546E91A0892AF1E37ABC5BA642DD84DE676790203010001";

			/** TEST04_TEXT
			*/
			public const string TEST04_TEXT = "https://blueback.myqnapcloud.com:8081/project_fee/StreamingAssets/Test04/text.txt";

			/** TEST04_BINARY
			*/
			public const string TEST04_BINARY = "https://blueback.myqnapcloud.com:8081/project_fee/StreamingAssets/Test04/binary.dat";

			/** TEST04_TEXTURE
			*/
			public const string TEST04_TEXTURE = "https://blueback.myqnapcloud.com:8081/project_fee/StreamingAssets/Test04/texture.png";

			/** TEST12_ASSETBUNDLE
			*/
			#if(UNITY_WEBGL)
			public const string TEST12_ASSETBUNDLE = "https://blueback.myqnapcloud.com:8081/project_fee/StreamingAssets/AssetBundle_WebGL/test.assetbundle";
			#elif(UNITY_ANDROID)
			public const string TEST12_ASSETBUNDLE = "https://blueback.myqnapcloud.com:8081/project_fee/StreamingAssets/AssetBundle_Android/test.assetbundle";
			#else
			public const string TEST12_ASSETBUNDLE = "https://blueback.myqnapcloud.com:8081/project_fee/StreamingAssets/AssetBundle_StandaloneWindows/test.assetbundle";
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

