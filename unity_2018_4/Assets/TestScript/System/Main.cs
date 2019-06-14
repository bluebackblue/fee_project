
/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief メイン。
*/


/** TestScript
*/
namespace TestScript
{
	/** Main
	*/
	public class Main : UnityEngine.MonoBehaviour
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
				UnityEngine.Font t_font = UnityEngine.Resources.Load<UnityEngine.Font>(Data.FONT);
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

					this.button[ii] = new Fee.Ui.Button(this.deleter,0,Click,ii);
					this.button[ii].SetTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.UI_TEXTURE_BUTTON));
					this.button[ii].SetRect(t_x,t_y,t_w,t_h);
					this.button[ii].SetText(this.scene_list[ii].view);
					this.button[ii].SetChangeOnOverCallBack(ChangeOnOver,ii);
				}

				//text
				this.text = new Fee.Render2D.Text2D(this.deleter,0);
				this.text.SetRect(30,300,0,0);
				this.text.SetText("---");
			}
		}

		/** ライブラリ停止。
		*/
		public void DeleteLibInstance()
		{
			//オーディオ。
			Fee.Audio.Audio.DeleteInstance();

			//ブルーム。
			Fee.Bloom.Bloom.DeleteInstance();

			//ブラー。
			Fee.Blur.Blur.DeleteInstance();

			//イベントプレート。
			Fee.EventPlate.EventPlate.DeleteInstance();

			//フェード。
			Fee.Fade.Fade.DeleteInstance();

			//ファイル。
			Fee.File.File.DeleteInstance();

			//マスウ。
			Fee.Input.Mouse.DeleteInstance();

			//キー。
			Fee.Input.Key.DeleteInstance();

			//パッド。
			Fee.Input.Pad.DeleteInstance();

			//ネットワーク。
			Fee.Network.Network.DeleteInstance();

			//パフォーマンスカウンター。
			Fee.PerformanceCounter.PerformanceCounter.DeleteInstance();

			//２Ｄ描画。
			Fee.Render2D.Render2D.DeleteInstance();

			//シーン。
			Fee.Scene.Scene.DeleteInstance();

			//タスク。
			Fee.TaskW.TaskW.DeleteInstance();

			//ＵＩ。
			Fee.Ui.Ui.DeleteInstance();

			//ＵＮＩＶＲＭ。
			Fee.UniVrm.UniVrm.DeleteInstance();
		}

		/** 更新。
		*/
		private void FixedUpdate()
		{
			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();
		}

		/** クリック。
		*/
		public void Click(int a_id)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(this.scene_list[a_id].scenename);
		}

		/** オンオーバー変更。
		*/
		public void ChangeOnOver(int a_id,bool a_is_onover)
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
	}
}

