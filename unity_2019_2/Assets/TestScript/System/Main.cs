
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

		/** prefablist
		*/
		private Common.PrefabList prefablist;

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
			this.prefablist = new Common.PrefabList();
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
							if(this.loadscene_request != null){

								if(this.deleter != null){
									this.deleter.DeleteAll();
									this.deleter = null;
								}

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
							UnityEngine.SceneManagement.SceneManager.LoadScene(this.loadscene_request);
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
				}break;
			}
		}

		/** 更新。
		*/
		private void LateUpdate()
		{
			switch(this.step){
			case Step.Main:
				{
					//２Ｄ描画。
					if(Fee.Render2D.Render2D.IsCreateInstance() == true){
						Fee.Render2D.Render2D.GetInstance().Main_PreDraw();
					}
				}break;
			}
		}

		/** InputUpdate
		*/
		public void InputUpdate()
		{
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
			//プレイヤーループシステム。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance(null);
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof( UnityEngine.Experimental.PlayerLoop.PreUpdate.SendMouseEvents));

			//２Ｄ描画。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//入力。
			Fee.Input.Input.CreateInstance(true,true,true,true);
			Fee.Input.Input.GetInstance().SetCallBack(this.InputUpdate);

			//イベントプレート。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。
			Fee.Ui.Ui.CreateInstance();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));
		}

		/** 初期化。
		*/
		public void MainScene_Initialize()
		{
			//シーン列挙。
			this.scene_list = SceneList.CreateStatusList();

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

				this.button[ii] = this.prefablist.CreateButton(this.deleter,0);
				this.button[ii].SetOnButtonClick(this,ii);
				this.button[ii].SetOnButtonChangeOverFlag(this,ii);
				this.button[ii].SetRect(t_x,t_y,t_w,t_h);
				this.button[ii].SetText(this.scene_list[ii].view);
			}

			//text
			this.text = this.prefablist.CreateText(this.deleter,0);
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
	}
}

