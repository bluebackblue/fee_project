﻿

/**
*/
namespace Editor
{
	/** コンバート。
	*/
	public class Convert
	{
		/** 鍵の作成。
		*/
		[UnityEditor.MenuItem("Fee/Test/Convert/CreatePublicKeyPrivateKey")]
		private static void MenuItem_Convert_CreatePublicKeyPrivateKey()
		{
			Fee.EditorTool.Crypt.CreatePublicKeyPrivateKey(
				Fee.File.Path.CreateAssetsPath("Editor/data/public_key.json"),
				Fee.File.Path.CreateAssetsPath("Editor/data/private_key.json")
			);
		}

		/** エクセルをコンバート。
		*/
		#if(UNITY_EDITOR)
		[UnityEditor.MenuItem("Fee/Test/Convert/ConvertFromExcel")]
		private static void MenuItem_Convert_ConvertFromExcel()
		{
			//エクセルからＪＳＯＮシートを作成。
			Fee.Excel.ExcelToJsonSheet t_excel_to_jsonsheet = new Fee.Excel.ExcelToJsonSheet();
			if(t_excel_to_jsonsheet.Convert(Fee.File.Path.CreateAssetsPath("Editor/data/excel.xlsx")) == true){
				Fee.JsonItem.JsonItem t_jsonsheet = t_excel_to_jsonsheet.GetJsonSheet();
				if(t_jsonsheet != null){
					//コンバート。
					if(Fee.JsonSheet.JsonSheet.ConvertFromJsonSheet(t_jsonsheet) == false){
						UnityEngine.Debug.LogError("faild");
					}
				}else{
					UnityEngine.Debug.LogError("faild");
				}
			}else{
				UnityEngine.Debug.LogError("faild");
			}
		}
		#endif

		/** プレハブ作成。
		*/
		#if(UNITY_EDITOR)
		[UnityEditor.MenuItem("Fee/Test/Convert/CreatePrefab")]
		private static void MenuItem_Convert_CreatePrefab()
		{
			//フォントリスト。TODO:エクセルからの作成。
			{
				Fee.Instantiate.FontList_Tool.ResourceItem[] t_font_list = new Fee.Instantiate.FontList_Tool.ResourceItem[]{

					//font
					new Fee.Instantiate.FontList_Tool.ResourceItem("FONT",						new Fee.File.Path("ThirdParty/Open/M+ FONTS/Font/mplus-1p-medium.ttf")),
				};
				Fee.Instantiate.FontList_Tool.Create(new Fee.File.Path("Resources/FontList.prefab"),t_font_list);
			}

			//テクスチャーリスト。TODO:エクセルからの作成。
			{
				Fee.Instantiate.TextureList_Tool.ResourceItem[] t_texture_list = new Fee.Instantiate.TextureList_Tool.ResourceItem[]{
					new Fee.Instantiate.TextureList_Tool.ResourceItem("UI_BUTTON",				new Fee.File.Path("Editor/data/ui_button.png")),
					new Fee.Instantiate.TextureList_Tool.ResourceItem("UI_BUTTON_ACTIVE",		new Fee.File.Path("Editor/data/ui_button_active.png")),
					new Fee.Instantiate.TextureList_Tool.ResourceItem("UI_CHECKBUTTON",			new Fee.File.Path("Editor/data/ui_checkbutton.png")),
					new Fee.Instantiate.TextureList_Tool.ResourceItem("UI_SLIDER",				new Fee.File.Path("Editor/data/ui_slider.png")),

					//ブルーム。
					new Fee.Instantiate.TextureList_Tool.ResourceItem("TEST10_TEXTURE",			new Fee.File.Path("Editor/data/skyimage.jpg")),

					//クリップ。
					new Fee.Instantiate.TextureList_Tool.ResourceItem("TEST15_TEXTURE",			new Fee.File.Path("Editor/data/skyimage.jpg")),
				};
				Fee.Instantiate.TextureList_Tool.Create(new Fee.File.Path("Resources/TextureList.prefab"),t_texture_list);
			}

			//プレハブリスト。TODO:エクセルからの作成。
			{
				Fee.Instantiate.PrefabList_Tool.ResourceItem[] t_prefab_list = new Fee.Instantiate.PrefabList_Tool.ResourceItem[]{

					//ＢＧＭ。
					new Fee.Instantiate.PrefabList_Tool.ResourceItem("TEST11_BGM",				new Fee.File.Path("Editor/data/create_from_excel_bgm.prefab")),	
					
					//ＳＥ。
					new Fee.Instantiate.PrefabList_Tool.ResourceItem("TEST11_SE",				new Fee.File.Path("Editor/data/create_from_excel_se.prefab")),				

					//デプス。
					new Fee.Instantiate.PrefabList_Tool.ResourceItem("TEST10_CUBE",				new Fee.File.Path("Editor/data/cube.prefab")),				

					//通信。
					new Fee.Instantiate.PrefabList_Tool.ResourceItem("TEST16_CUBE",				new Fee.File.Path("Editor/data/cube.prefab")),		

					//視錐台カリング。
					new Fee.Instantiate.PrefabList_Tool.ResourceItem("TEST18_CUBE",				new Fee.File.Path("Editor/data/cube.prefab")),
				};
				Fee.Instantiate.PrefabList_Tool.Create(new Fee.File.Path("Resources/PrefabList.prefab"),t_prefab_list);
			}

			//テキストアセットリスト。TODO:エクセルからの作成。
			{
				Fee.Instantiate.TextAssetList_Tool.ResourceItem[] t_textasset_list = new Fee.Instantiate.TextAssetList_Tool.ResourceItem[]{

					new Fee.Instantiate.TextAssetList_Tool.ResourceItem("TEST07_PUBLIC_KEY",	new Fee.File.Path("Editor/data/public_key.json")),				
					new Fee.Instantiate.TextAssetList_Tool.ResourceItem("TEST07_PRIVATE_KEY",	new Fee.File.Path("Editor/data/private_key.json")),
					

					new Fee.Instantiate.TextAssetList_Tool.ResourceItem("TEST12_DATA_DEBUG",	new Fee.File.Path("Editor/data/create_from_excel_data_debug.json")),				
					new Fee.Instantiate.TextAssetList_Tool.ResourceItem("TEST12_DATA_RELEASE",	new Fee.File.Path("Editor/data/create_from_excel_data_release.json")),
				};
				Fee.Instantiate.TextAssetList_Tool.Create(new Fee.File.Path("Resources/TextAssetList.prefab"),t_textasset_list);
			}
		}
		#endif
	}
}

