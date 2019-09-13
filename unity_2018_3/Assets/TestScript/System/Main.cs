
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
			this.scene_list = SceneList.CreateStatusList();

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
				this.text = Fee.Render2D.Render2D.GetInstance().Text2D_PoolNew(this.deleter,0);
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

			//loadscene_request
			this.loadscene_request = null;
		}

		/** 更新。
		*/
		private void FixedUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			///ロードシーン。
			if(this.loadscene_request != null){
				this.deleter.DeleteAll();
			}

			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_After();

			//ロードシーン。
			if(this.loadscene_request != null){

				//ライブラリ停止。
				DeleteLibInstance.DeleteAll();

				//ロードシーン。
				UnityEngine.SceneManagement.SceneManager.LoadScene(this.loadscene_request);
			}
		}

		/** 更新。
		*/
		private void Update()
		{
			//２Ｄ描画。
			if(Fee.Render2D.Render2D.IsCreateInstance() == true){
				Fee.Render2D.Render2D.GetInstance().Main_PreDraw();
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
			this.text.SetText(this.scene_list[a_id].detailtext);
		}
	}
}

