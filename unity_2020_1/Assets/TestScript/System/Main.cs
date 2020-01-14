
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

		/** loadscene_request
		*/
		private string loadscene_request;

		/** Step
		*/
		private enum Step
		{
			Init,

			LibCreate,
			Main,
			LibDelete,

			End,

			LoadScene,
		}
		private Step step;
		private int step_time;

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
			this.step = Step.Init;
			this.step_time = 0;
		}

		/** 更新。
		*/
		private void FixedUpdate()
		{
			bool t_next_frame = false;

			while(t_next_frame == false){
				switch(this.step){
				case Step.Init:
					{
						if(this.step_time == 0){
							//開始。
							this.step_time++;
							continue;
						}else{
							this.step_time++;
							if(this.step_time > 3){
								this.step = Step.LibCreate;
								this.step_time = 0;
								continue;
							}
						}
					}break;
				case Step.LibCreate:
					{
						if(this.step_time == 0){
							//ライブラリインスタンス作成。
							this.MainScene_LibCreateInstance();
							this.step_time++;
							continue;
						}else{
							this.step_time++;
							if(this.step_time > 3){
								this.step = Step.Main;
								this.step_time = 0;
								continue;
							}
						}
					}break;
				case Step.Main:
					{
						if(this.step_time == 0){
							//初期化。
							this.MainScene_Initialize();
							this.step_time++;
							continue;
						}else{
							//this.step_time++;

							//更新。
							this.MainScene_FixedUpdate();

							if(this.loadscene_request != null){
								this.step = Step.LibDelete;
								this.step_time = 0;
								continue;
							}
						}
					}break;
				case Step.LibDelete:
					{
						if(this.step_time == 0){
							//ライブラリインスタンス削除。
							this.MainScene_LibDeleteInstance();
							this.step_time++;
							continue;
						}else{
							this.step_time++;
							if(this.step_time > 3){
								this.step = Step.End;
								this.step_time = 0;
								continue;
							}
						}
					}break;
				case Step.End:
					{
						if(this.step_time == 0){
							this.step_time++;
							continue;
						}else{
							this.step_time++;
							if(this.step_time > 3){
								this.step = Step.LoadScene;
								this.step_time = 0;
								continue;
							}
						}
					}break;
				case Step.LoadScene:
					{
						if(this.step_time == 0){
							//ロードシーン。
							this.MainScene_LoadScene();
							this.step_time++;
							continue;
						}
					}break;
				}

				t_next_frame = true;
			}
		}

		/** 更新。
		*/
		private void Update()
		{
			switch(this.step){
			case Step.Main:
				{
					this.MainScene_Update();
				}break;
			}
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(int a_id)
		{
			this.loadscene_request = this.scene_list[a_id].scenename;
		}

		/** [Fee.Ui.OnButtonChangeOverFlag_CallBackInterface]クリック。
		*/
		public void OnButtonChangeOverFlag(int a_id,bool a_is_onover)
		{
			if(this.text != null){
				this.text.SetText(this.scene_list[a_id].detailtext);
			}
		}

		/** ライブラリインスタンス作成。
		*/
		public void MainScene_LibCreateInstance()
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

			//フォント。
			{
				UnityEngine.GameObject t_prefab = UnityEngine.Resources.Load<UnityEngine.GameObject>("FontList");
				Fee.Instantiate.FontList t_fontlist = new Fee.Instantiate.FontList(t_prefab.GetComponent<Fee.Instantiate.FontList_MonoBehaviour>());
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_fontlist.GetFont("FONT"));
			}
		}

		/** 初期化。
		*/
		public void MainScene_Initialize()
		{
			//シーン列挙。
			this.scene_list = SceneList.CreateStatusList();

			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			Fee.Instantiate.TextureList t_texture_list;
			{
				UnityEngine.GameObject t_prefab = UnityEngine.Resources.Load<UnityEngine.GameObject>("TextureList");
				t_texture_list = new Fee.Instantiate.TextureList(t_prefab.GetComponent<Fee.Instantiate.TextureList_MonoBehaviour>());
			}

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
				this.button[ii].SetNormalTexture(t_texture_list.GetTexture("UI_BUTTON"));
				this.button[ii].SetOnTexture(t_texture_list.GetTexture("UI_BUTTON"));
				this.button[ii].SetDownTexture(t_texture_list.GetTexture("UI_BUTTON"));
				this.button[ii].SetLockTexture(t_texture_list.GetTexture("UI_BUTTON"));
				this.button[ii].SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.button[ii].SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.button[ii].SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.button[ii].SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
			}

			//text
			this.text = Fee.Render2D.Text2D.Create(this.deleter,0);
			this.text.SetRect(30,300,0,0);
			this.text.SetText("---");

			//loadscene_request
			this.loadscene_request = null;
		}

		/** ライブラリインスタンス削除。
		*/
		public void MainScene_LibDeleteInstance()
		{
			//全部削除。
			DeleteLibInstance.DeleteAll();
		}

		/** 削除。
		*/
		public void MainScene_Delete()
		{
			this.deleter.DeleteAll();
		}	

		/** ロードシーン。
		*/
		public void MainScene_LoadScene()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(this.loadscene_request);
		}

		/** 更新。
		*/
		public void MainScene_Update()
		{
			//２Ｄ描画。
			if(Fee.Render2D.Render2D.IsCreateInstance() == true){
				Fee.Render2D.Render2D.GetInstance().Main_PreDraw();
			}
		}

		/** 更新。
		*/
		public void MainScene_FixedUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			if(this.loadscene_request != null){
				if(this.deleter != null){
					this.deleter.DeleteAll();
					this.deleter = null;
				}
			}

			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_After();
		}
	}
}

