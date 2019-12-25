

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
	/** EditorMenu
	*/
	class EditorMenu
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


		/** [メニュー]シーンリスト初期化。
		*/
		#if(UNITY_EDITOR)
		[UnityEditor.MenuItem("Fee/Test/Initialize/EditSceneList")]
		private static void MenuItem_EditSceneList()
		{
			//フォルダ内のファイルを列挙。
			System.Collections.Generic.List<string> t_file_list = Fee.EditorTool.Utility.GetFileNameList("TestScene/");

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

			Fee.EditorTool.Utility.WriteTextFile(Fee.File.Path.CreateAssetsPath("TestScript/System/SceneList.cs"),t_script,true);
		}
		#endif

		/** [メニュー]パッケージ。作成。
		*/
		#if(UNITY_EDITOR)
		[UnityEditor.MenuItem("Fee/Test/BuildThirdPartyPackage")]
		private static void MenuItem_BuildThirdPartyPackage()
		{
			//サブディレクトリの再帰探査。
			UnityEditor.ExportPackageOptions t_options = UnityEditor.ExportPackageOptions.Recurse;

			//非同期実行。
			t_options |= UnityEditor.ExportPackageOptions.Interactive;

			//出力。
			System.Collections.Generic.List<string> t_directoryname_list = Fee.EditorTool.Utility.GetDirectoryNameList("ThirdParty/");
			for(int ii=0;ii<t_directoryname_list.Count;ii++){
				string t_filename = t_directoryname_list[ii] + ".unitypackage";
				string t_path = "Assets/ThirdParty/" + t_directoryname_list[ii];

				if(Fee.EditorTool.Utility.IsExistFile(new Fee.File.Path(t_filename)) == false){
					UnityEditor.AssetDatabase.ExportPackage(t_path,t_filename,t_options);
				}else{
					UnityEngine.Debug.Log("Exist : " + t_filename);
				}
			}
		}
		#endif
	}
}

