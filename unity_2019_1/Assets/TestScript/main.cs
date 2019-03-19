using System.Collections;
using System.Collections.Generic;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief メイン。
*/


/** main
*/
public class main : UnityEngine.MonoBehaviour
{
	/** scene_list
	*/
	private string[] scene_list;

	/** シーン数。
	*/
	private static int SCENE_COUNT = 20;

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
		this.scene_list = new string[SCENE_COUNT];
		for(int ii=0;ii<scene_list.Length;ii++){
			this.scene_list[ii] = "test" + string.Format("{0:D2}",ii+1);
		}

		//ライブラリ停止。
		this.DeleteLibInstance();

		//インスタンス作成。
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.CreateInstance();

			//ＵＩ。
			Fee.Ui.Ui.CreateInstance();

			//イベントプレート。
			Fee.EventPlate.EventPlate.CreateInstance();

			//マウス。
			Fee.Input.Mouse.CreateInstance();
		}

		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//button
			this.button = new Fee.Ui.Button[this.scene_list.Length];
			for(int ii=0;ii<this.button.Length;ii++){

				int t_xx_max = 9;

				int t_xx = ii % t_xx_max;
				int t_yy = ii / t_xx_max;

				int t_x = 30 + t_xx * 100;
				int t_y = 30 + t_yy * 60;
				int t_w = 80;
				int t_h = 40;

				string t_name = null;

				if(ii < this.scene_list.Length){
					t_name = this.scene_list[ii];
				}else{
					t_name = "-";
				}

				this.button[ii] = new Fee.Ui.Button(this.deleter,0,Click,ii);
				this.button[ii].SetTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>("button"));
				this.button[ii].SetRect(t_x,t_y,t_w,t_h);
				this.button[ii].SetText(t_name);
			}

			//text
			this.text = new Fee.Render2D.Text2D(this.deleter,0);
			this.text.SetRect(0,0,0,0);
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
		Fee.Input.Mouse.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());

		//イベントプレート。
		Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

		//ＵＩ。
		Fee.Ui.Ui.GetInstance().Main();
	}

	/** クリック。
	*/
	public void Click(int a_id)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(this.scene_list[a_id]);
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
	[UnityEditor.MenuItem("Fee/Initialize/EditSceneList")]
	private static void EditSceneList()
	{
		List<UnityEditor.EditorBuildSettingsScene> t_list = new System.Collections.Generic.List<UnityEditor.EditorBuildSettingsScene>();

		t_list.Add(new UnityEditor.EditorBuildSettingsScene("Assets/TestScene/main.unity",true));

		for(int ii=0;ii<SCENE_COUNT;ii++){
			t_list.Add(new UnityEditor.EditorBuildSettingsScene(string.Format("Assets/TestScene/test{0:D2}.unity",ii+1),true));
		}

		UnityEditor.EditorBuildSettings.scenes = t_list.ToArray();
	}
	#endif

	/** インプットマネージャ初期化。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Fee/Initialize/EditInputManager")]
	private static void MakeInputManager()
	{
		Fee.Input.EditInputManager t_inputmaanger = new Fee.Input.EditInputManager();
		{
			System.Collections.Generic.List<Fee.Input.EditInputManager_Item> t_list = t_inputmaanger.GetList();

			System.Collections.Generic.Dictionary<string,Fee.Input.EditInputManager_Item> t_flag_list = new System.Collections.Generic.Dictionary<string,Fee.Input.EditInputManager_Item>();
			{
				//トリガー。
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateLeftTrigger1Button();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LT1,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateRightTrigger1Button();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RT1,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateLeftTrigger2Button();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LT2,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateRightTrigger2Button();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RT2,t_item);
				}

				//ボタン。
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateDigitalButtonLeft();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LEFT,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateDigitalButtonRight();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RIGHT,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateDigitalButtonUp();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_UP,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateDigitalButtonDown();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_DOWN,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateDigitalButtonEnter();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_ENTER,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateDigitalButtonEscape();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_ESCAPE,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateDigitalButtonSub1();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_SUB1,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateDigitalButtonSub2();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_SUB2,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateDigitalButtonLeftMenu();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LMENU,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateDigitalButtonRightMenu();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RMENU,t_item);
				}

				//スティック。
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateLeftStickAxisX();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LSX,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateLeftStickAxisY();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LSY,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateRightStickAxisX();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RSX,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateRightStickAxisY();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RSY,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateLeftStickButton();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LSB,t_item);
				}
				{
					Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
					t_item.CreateRightStickButton();
					t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RSB,t_item);
				}
			}

			//すでにリストに存在するものはリストから外す。
			for(int ii=0;ii<t_list.Count;ii++){
				Fee.Input.EditInputManager_Item t_item;
				if(t_flag_list.TryGetValue(t_list[ii].m_Name,out t_item) == true){
					//すでに存在する。
					t_flag_list[t_list[ii].m_Name] = null;
				}
			}

			//リストに追加。
			foreach(System.Collections.Generic.KeyValuePair<string,Fee.Input.EditInputManager_Item> t_pair in t_flag_list){
				if(t_pair.Value != null){
					t_list.Add(t_pair.Value);
				}
			}
		}

		//セーブ。
		t_inputmaanger.Save();
	}
	#endif

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Fee/MakeAssetBundle/All")]
	private static void MakeAssetBundle_SWA()
	{
		MakeAssetBundle_StandaloneWindows();
		MakeAssetBundle_WebGL();
		MakeAssetBundle_Android();
		MakeAssetBundle_iOS();
	}
	#endif

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Fee/MakeAssetBundle/StandaloneWindows")]
	private static void MakeAssetBundle_StandaloneWindows()
	{
		if(UnityEditor.BuildPipeline.IsBuildTargetSupported(UnityEditor.BuildTargetGroup.Standalone,UnityEditor.BuildTarget.StandaloneWindows) == true){
			UnityEditor.EditorUtility.DisplayDialog("StandaloneWindows","Start","ok");
			UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/StandaloneWindows",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.StandaloneWindows);
			UnityEditor.EditorUtility.DisplayDialog("StandaloneWindows","End","ok");
		}
	}
	#endif

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Fee/MakeAssetBundle/WebGL")]
	private static void MakeAssetBundle_WebGL()
	{
		if(UnityEditor.BuildPipeline.IsBuildTargetSupported(UnityEditor.BuildTargetGroup.WebGL,UnityEditor.BuildTarget.WebGL) == true){
			UnityEditor.EditorUtility.DisplayDialog("WebGL","Start","ok");
			UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/WebGL",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.WebGL);
			UnityEditor.EditorUtility.DisplayDialog("WebGL","End","ok");
		}
	}
	#endif

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Fee/MakeAssetBundle/Android")]
	private static void MakeAssetBundle_Android()
	{
		if(UnityEditor.BuildPipeline.IsBuildTargetSupported(UnityEditor.BuildTargetGroup.Android,UnityEditor.BuildTarget.Android) == true){
			UnityEditor.EditorUtility.DisplayDialog("Android","Start","ok");
			UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/Android",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.Android);
			UnityEditor.EditorUtility.DisplayDialog("Android","End","ok");
		}
	}
	#endif

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Fee/MakeAssetBundle/iOS")]
	private static void MakeAssetBundle_iOS()
	{
		if(UnityEditor.BuildPipeline.IsBuildTargetSupported(UnityEditor.BuildTargetGroup.iOS,UnityEditor.BuildTarget.iOS) == true){
			UnityEditor.EditorUtility.DisplayDialog("iOS","Start","ok");
			UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/iOS",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.iOS);
			UnityEditor.EditorUtility.DisplayDialog("iOS","End","ok");
		}
	}
	#endif
}


/** main_base
*/
public class main_base : UnityEngine.MonoBehaviour
{
	/** is_changescene
	*/
	public bool is_changescene = false;

	/** 戻るボタン。
	*/
	Fee.Ui.Button return_button = null;

	/** 戻るボタン作成。
	*/
	public void CreateReturnButton(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
	{
		this.return_button = new Fee.Ui.Button(a_deleter,a_drawpriority,Click,0);
		this.return_button.SetTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>("button"));
		this.return_button.SetText("Return");
		this.return_button.SetRect(0,0,80,40);
	}

	/** クリック。
	*/
	public void Click(int a_id)
	{
		this.is_changescene = true;
		this.StartCoroutine(ChangeScene());	
	}

	/** 削除前。
	*/
	public virtual bool PreDestroy(bool a_first)
	{
		return true;
	}

	/** シーン切り替え。チェック。
	*/
	public bool IsChangeScene()
	{
		return this.is_changescene;
	}

	/** シーン切り替え。
	*/
	public System.Collections.IEnumerator ChangeScene()
	{
		bool t_first = true;

		while(this.PreDestroy(t_first) == false){
			t_first = false;
			yield return null;
		}

		bool t_ok = false;
		while(t_ok == false){
			t_ok = true;

			if(Fee.File.File.IsCreateInstance() == true){
				if(Fee.File.File.GetInstance().IsBusy() == true){
					t_ok = false;
				}
			}

			if(Fee.Network.Network.IsCreateInstance() == true){
				if(Fee.Network.Network.GetInstance().IsBusy() == true){
					t_ok = false;
				}
			}

			if(t_ok == false){
				yield return null;
			}
		}

		UnityEngine.GameObject.Destroy(this.gameObject);

		UnityEngine.SceneManagement.SceneManager.LoadScene("main");

		yield break;
	}
}

