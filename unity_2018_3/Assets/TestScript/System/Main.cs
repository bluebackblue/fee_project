
/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief メイン。
*/


/** TestScript
*/
namespace TestScript
{
	/** Main
	*/
	public class Main : UnityEngine.MonoBehaviour , Fee.Ui.OnButtonChangeOverFlag_CallBackInterface<int> , Fee.Ui.OnButtonClick_CallBackInterface<int>
	{
		/** scene_list
		*/
		private TestStatus[] scene_list;

		/** deleter
		*/
		private Fee.Deleter.Deleter deleter;

		/** button
		*/
		private Fee.Ui.Button[] button;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** アプリ起動時。
		*/
		[UnityEngine.RuntimeInitializeOnLoadMethod]
		private static void AppInitialize()
		{
		}

		/** ファイルアップロード。
		*/
		public void FileSelected(string a_url)
		{
			if(this.text != null){
				this.text.SetText(a_url);
			}
		}

		/** Start
		*/
		void Start()
		{
			//シーン列挙。
			this.scene_list = new TestStatus[]{
				test01.CreateStatus(),
				test02.CreateStatus(),
				test03.CreateStatus(),
				test04.CreateStatus(),
				test05.CreateStatus(),
				test06.CreateStatus(),
				test07.CreateStatus(),
				test08.CreateStatus(),
				test09.CreateStatus(),
				test10.CreateStatus(),
				test11.CreateStatus(),
				test12.CreateStatus(),
				test13.CreateStatus(),
				test14.CreateStatus(),
				test15.CreateStatus(),
				test16.CreateStatus(),
				test17.CreateStatus(),
				test18.CreateStatus(),
				test19.CreateStatus(),
				test20.CreateStatus(),
				test21.CreateStatus(),
			};

			//ライブラリ停止。
			this.DeleteLibInstance();

			//インスタンス作成。
			{
				//２Ｄ描画。
				Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
				Fee.Render2D.Config.ReCalcWH();
				Fee.Render2D.Render2D.CreateInstance();

				//ＵＩ。
				Fee.Ui.Ui.CreateInstance();

				//イベントプレート。
				Fee.EventPlate.EventPlate.CreateInstance();

				//マウス。
				Fee.Input.Mouse.CreateInstance();
			}

			{
				//フォント。
				UnityEngine.Font t_font = UnityEngine.Resources.Load<UnityEngine.Font>(Data.Resources.FONT);
				if(t_font != null){
					Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
				}
			}

			{
				//deleter
				this.deleter = new Fee.Deleter.Deleter();

				//button
				this.button = new Fee.Ui.Button[this.scene_list.Length];
				for(int ii=0;ii<this.scene_list.Length;ii++){

					int t_xx_max = 9;

					int t_xx = ii % t_xx_max;
					int t_yy = ii / t_xx_max;

					int t_x = 30 + t_xx * 100;
					int t_y = 30 + t_yy * 60;
					int t_w = 80;
					int t_h = 40;

					this.button[ii] = new Fee.Ui.Button(this.deleter,0);
					this.button[ii].SetOnButtonClick(this,ii);
					this.button[ii].SetOnButtonChangeOverFlag(this,ii);
					this.button[ii].SetRect(t_x,t_y,t_w,t_h);
					this.button[ii].SetTextureCornerSize(10);
					this.button[ii].SetText(this.scene_list[ii].view);
					this.button[ii].SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button[ii].SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button[ii].SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button[ii].SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					this.button[ii].SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
					this.button[ii].SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
					this.button[ii].SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
					this.button[ii].SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
				}

				//text
				this.text = new Fee.Render2D.Text2D(this.deleter,0);
				this.text.SetRect(30,300,0,0);
				this.text.SetText("---");

				//TODO:
				{
					UnityEngine.TextAsset t_textasset = UnityEngine.Resources.Load<UnityEngine.TextAsset>("test");
					if(t_textasset != null){
						this.text.SetText(t_textasset.text);
					}
				}
			}
		}

		/** ライブラリ停止。
		*/
		public void DeleteLibInstance()
		{
			//アセット。
			{
			}

			//アセットバンドルリスト。
			{
				Fee.AssetBundleList.AssetBundleList.DeleteInstance();
			}

			//オーディオ。
			{
				Fee.Audio.Audio.DeleteInstance();
			}

			//ブルーム。
			{
				Fee.Bloom.Bloom.DeleteInstance();
			}

			//ブラー。
			{
				Fee.Blur.Blur.DeleteInstance();
			}

			//暗号。
			{
				Fee.Crypt.Crypt.DeleteInstance();
			}

			//データ。
			{
				Fee.Data.Data.DeleteInstance();
			}

			//削除管理。
			{
			}

			//深度。
			{
				Fee.Depth.Depth.DeleteInstance();
			}

			//ダイクストラ法。
			{
			}

			//ディレクトリ。
			{
			}

			//エディターツール。
			{
			}

			//イベントプレート。
			{
				Fee.EventPlate.EventPlate.DeleteInstance();
			}

			//エクセル。
			{
			}

			//フェード。
			{
				Fee.Fade.Fade.DeleteInstance();
			}

			//ファイル。
			{
				Fee.File.File.DeleteInstance();
			}

			//関数呼び出し。
			{
				Fee.Function.Function.SetMonoBehaviour(null);
			}

			//入力。
			{
				//マスウ。
				Fee.Input.Mouse.DeleteInstance();
	
				//キー。
				Fee.Input.Key.DeleteInstance();
	
				//パッド。
				Fee.Input.Pad.DeleteInstance();
			}

			//インスタンス作成。
			{
			}

			//ＪＳＯＮ。
			{
			}

			//ＪＳＯＮシート。
			{
			}

			//アセットバンドル作成。
			{
			}

			//モデル。
			{
			}

			//ネットワーク。
			{
				Fee.Network.Network.DeleteInstance();
			}

			//パフォーマンスカウンター。
			{
				Fee.PerformanceCounter.PerformanceCounter.DeleteInstance();
			}

			//プラットフォーム。
			{
			}

			//２Ｄ描画。
			{
				Fee.Render2D.Render2D.DeleteInstance();
			}

			//リスロー。
			{
			}

			//シーン。
			{
				Fee.Scene.Scene.DeleteInstance();
			}

			//サウンドプール。
			{
				Fee.SoundPool.SoundPool.DeleteInstance();
			}

			//タスク。
			{
				Fee.TaskW.TaskW.DeleteInstance();
			}

			//ＵＩ。
			{
				Fee.Ui.Ui.DeleteInstance();
			}

			//ＵＮＩＶＲＭ。
			{
				Fee.UniVrm.UniVrm.DeleteInstance();
			}
		}

		/** 更新。
		*/
		private void FixedUpdate()
		{
			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(int a_id)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(this.scene_list[a_id].scenename);
		}

		/** [Fee.Ui.OnButtonChangeOverFlag_CallBackInterface]クリック。
		*/
		public void OnButtonChangeOverFlag(int a_id,bool a_is_onover)
		{
			this.text.SetText(this.scene_list[a_id].detailtext);
		}

		/** シーン遷移。
		*/
		private void OnDestroy()
		{
			this.deleter.DeleteAll();

			//ライブラリ停止。
			this.DeleteLibInstance();
		}

		/** シーン名。
		*/
		public void CallFromHTML(string a_scene_name)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(a_scene_name);
		}

		/** シーンリスト初期化。
		*/
		#if(UNITY_EDITOR)
		[UnityEditor.MenuItem("Fee/Test/Initialize/EditSceneList")]
		private static void MenuItem_EditSceneList()
		{
			System.Collections.Generic.List<UnityEditor.EditorBuildSettingsScene> t_scene_list = new System.Collections.Generic.List<UnityEditor.EditorBuildSettingsScene>();

			System.Collections.Generic.List<string> t_file_list = Fee.EditorTool.Utility.GetFileNameList("TestScene/");

			//main.unity
			t_scene_list.Add(new UnityEditor.EditorBuildSettingsScene("Assets/TestScene/main.unity",true));

			//testxx.unity
			for(int ii=0;ii<t_file_list.Count;ii++){
				if(t_file_list[ii] == "main.unity"){
				}else if(t_file_list[ii].EndsWith(".unity") == true){
					t_scene_list.Add(new UnityEditor.EditorBuildSettingsScene("Assets/TestScene/" + t_file_list[ii],true));
				}
			}

			UnityEditor.EditorBuildSettings.scenes = t_scene_list.ToArray();
		}
		#endif

		/** パッケージ。作成。
		*/
		#if(UNITY_EDITOR)
		[UnityEditor.MenuItem("Fee/Test/BuildThirdPartyPackage")]
		private static void MenuItem_BuildThirdPartyPackage()
		{
			BuildThirdPartyPackage();
		}
		#endif

		/** パッケージ。作成。
		*/
		#if(UNITY_EDITOR)
		private static void BuildThirdPartyPackage()
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

