

/**
*/
namespace Editor
{
	/** コンバート。
	*/
	public class Convert
	{
		//スクリプト自動生成。
		private const string SCRIPT_START = @"

/** TestScript
*/
namespace TestScript
{
	/** 自動生成。
	*/
	public class SceneList
	{
		/** CreateStatusList
		*/
		public static TestStatus[] CreateStatusList()
		{
			return new TestStatus[]{
";

		//スクリプト自動生成。
		private const string SCRIPT_END = @"			};
		}
	}
}

";

		//スクリプト自動生成。
		private const string SCRIPT_ITEM = @"				<name>.CreateStatus(),
";


		/** シーンリスト再作成。
		*/
		#if(UNITY_EDITOR)
		[UnityEditor.MenuItem("TestScript/Tool/ReMakeEditSceneList")]
		private static void MenuItem_Tool_ReMakeEditSceneList()
		{
			//フォルダ内のファイルを列挙。
			System.Collections.Generic.List<string> t_file_list = Fee.EditorTool.Utility.CreateFileNameList(new Fee.File.Path("TestScene/"));

			//シーン追加。
			System.Collections.Generic.List<UnityEditor.EditorBuildSettingsScene> t_scene_list = new System.Collections.Generic.List<UnityEditor.EditorBuildSettingsScene>();

			//スクリプト自動生成。
			string t_script = "";

			//シーン追加。
			t_scene_list.Add(new UnityEditor.EditorBuildSettingsScene("Assets/TestScene/main.unity",true));

			for(int ii=0;ii<t_file_list.Count;ii++){
				if(t_file_list[ii] == "main.unity"){
				}else if(t_file_list[ii].EndsWith(".unity") == true){
					//シーン追加。
					t_scene_list.Add(new UnityEditor.EditorBuildSettingsScene("Assets/TestScene/" + t_file_list[ii],true));

					//スクリプト自動生成。
					t_script += SCRIPT_ITEM.Replace("<name>",t_file_list[ii].Substring(0,t_file_list[ii].Length - 6));
				}
			}

			//シーン追加。
			UnityEditor.EditorBuildSettings.scenes = t_scene_list.ToArray();

			//スクリプト自動生成。
			t_script =  SCRIPT_START + t_script +  SCRIPT_END;

			Fee.EditorTool.Utility.WriteTextFile(new Fee.File.Path("TestScript/System/SceneList.cs"),t_script);
			Fee.EditorTool.Utility.Refresh();
		}
		#endif

		/** 鍵の作成。
		*/
		[UnityEditor.MenuItem("TestScript/Tool/CreatePublicKeyPrivateKey")]
		private static void MenuItem_Tool_CreatePublicKeyPrivateKey()
		{
			Fee.EditorTool.Crypt.CreatePublicKeyPrivateKey(
				new Fee.File.Path("Editor/data/public_key.json"),
				new Fee.File.Path("Editor/data/private_key.json")
			);
		}

		/** 証明書の作成。
		*/
		[UnityEditor.MenuItem("TestScript/Tool/CreateCertificate")]
		private static void MenuItem_Tool_CreateCertificate()
		{
			Fee.File.CustomCertificateHandler t_certificate = new Fee.File.CustomCertificateHandler("");
			Fee.EditorTool.Utility.CreateWebRequest(new Fee.File.Path("https://blueback.myqnapcloud.com:8081/"),t_certificate);
			Fee.EditorTool.Utility.WriteTextFile(new Fee.File.Path("Editor/data/certificate.txt"),t_certificate.GetReceiveCertificateString());
			Fee.EditorTool.Utility.Refresh();
		}

		/** エクセルをコンバート。
		*/
		[UnityEditor.MenuItem("TestScript/Convert/ConvertFromExcel")]
		private static void MenuItem_Convert_ConvertFromExcel()
		{
			Fee.JsonSheet.ConvertParam t_convertparam = new Fee.JsonSheet.ConvertParam();
			{
				t_convertparam.Reset();
				t_convertparam.create_dummy_assetbundle = true;
			}

			//エクセルからＪＳＯＮシートを作成。
			Fee.Excel.ExcelToJsonSheet t_excel_to_jsonsheet = new Fee.Excel.ExcelToJsonSheet();
			if(t_excel_to_jsonsheet.Convert(new Fee.File.Path("Editor/data/excel.xlsx")) == true){
				Fee.JsonItem.JsonItem t_jsonsheet = t_excel_to_jsonsheet.GetJsonSheet();
				if(t_jsonsheet != null){
					//コンバート。
					if(Fee.JsonSheet.JsonSheet.ConvertFromJsonSheet(t_jsonsheet,t_convertparam) == false){
						UnityEngine.Debug.LogError("faild");
					}
				}else{
					UnityEngine.Debug.LogError("faild");
				}
			}else{
				UnityEngine.Debug.LogError("faild");
			}
		}

		/** エクセルをコンバート。
		*/
		[UnityEditor.MenuItem("TestScript/Convert/ConvertFromExcel_CreateAssetBundle")]
		private static void MenuItem_Convert_ConvertFromExcel_Create_AssetBundle()
		{
			Fee.JsonSheet.ConvertParam t_convertparam = new Fee.JsonSheet.ConvertParam();
			{
				t_convertparam.Reset();
				t_convertparam.create_assetbundle = true;
				t_convertparam.create_dummy_assetbundle = true;
			}

			//エクセルからＪＳＯＮシートを作成。
			Fee.Excel.ExcelToJsonSheet t_excel_to_jsonsheet = new Fee.Excel.ExcelToJsonSheet();
			if(t_excel_to_jsonsheet.Convert(new Fee.File.Path("Editor/data/excel.xlsx")) == true){
				Fee.JsonItem.JsonItem t_jsonsheet = t_excel_to_jsonsheet.GetJsonSheet();
				if(t_jsonsheet != null){
					//コンバート。
					if(Fee.JsonSheet.JsonSheet.ConvertFromJsonSheet(t_jsonsheet,t_convertparam) == false){
						UnityEngine.Debug.LogError("faild");
					}
				}else{
					UnityEngine.Debug.LogError("faild");
				}
			}else{
				UnityEngine.Debug.LogError("faild");
			}
		}
	}
}

