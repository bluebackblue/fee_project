

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief テスト。
*/


/** TestScript
*/
namespace TestScript
{
	/** test21
	*/
	public class test21 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<int>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test21",
				"test21",

				@"
				オープンファイルダイアログ
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** button
		*/
		private Fee.Ui.Button button;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** sprite
		*/
		private Fee.Render2D.Sprite2D sprite;

		/** filepath
		*/
		private string filepath;

		/** file_item
		*/
		private Fee.File.Item file_item;

		/** Start
		*/
		private void Start()
		{
			//プレイヤーループシステム。インスタンス作成。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance(null);
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(UnityEngine.PlayerLoop.PreUpdate.SendMouseEvents));

			//プラットフォーム。インスタンス作成。
			Fee.Platform.Config.LOG_ENABLE = true;
			Fee.Platform.Platform.CreateInstance();

			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.CreateInstance();
			Fee.Function.Function.GetInstance().SetMonoBehaviour(this);
			Fee.Function.Function.GetInstance().SetRowUpdate(this.RowUpdate);

			//インスタンス作成。
			Fee.Instantiate.Instantiate.CreateInstance();

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.LOG_ENABLE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Config.LOG_ENABLE = true;
			Fee.Input.Input.CreateInstance(true,false,true,false);

			//イベントプレート。
			Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//ファイル。インスタンス作成。
			Fee.File.File.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//button
			{
				int t_x = 50;
				int t_y = 200;
				int t_w = 90;
				int t_h = 60;

				//button
				this.button = this.prefablist.CreateButton(this.deleter,1);
				this.button.SetOnButtonClick(this,0);
				this.button.SetText("開始");
				this.button.SetRect(t_x,t_y,t_w,t_h);
			}

			//text
			{
				int t_x = 50 + 150;
				int t_y = 200 + 20;

				this.text = this.prefablist.CreateText(this.deleter,1);
				this.text.SetXY(t_x,t_y);
			}

			//sprite
			{
				this.sprite = Fee.Render2D.Sprite2D.Create(this.deleter,1);
				this.sprite.SetRect(50,350,100,100);
				this.sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
				this.sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
			}

			//filepath
			this.filepath = null;

			//file_item
			this.file_item = null;
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(int a_id)
		{
			this.text.SetText("");
			this.sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);

			//title
			string t_title = "Open File";

			//extension
			string t_extension = Fee.Platform.Platform.GetInstance().CreateOpenFileDialogExtension(new string[]{"jpg","png","bmp"});
			
			Fee.Platform.Platform.GetInstance().OpenFileDialog(t_title,t_extension);
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
			string t_full_path = Fee.Platform.Platform.GetInstance().GetOpenFileDialogResult();
			if(t_full_path == null){
				t_full_path = "null";
			}else if(t_full_path == ""){
				t_full_path = "space";
			}
			this.text.SetText(t_full_path);

			if(this.file_item == null){
				//開始。

				if(this.filepath != t_full_path){
					this.filepath = t_full_path;
					if((this.filepath != "null")&&(this.filepath != "space")){
						this.file_item = Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadFullPathTextureFile,new Fee.File.Path(this.filepath));
					}
				}
			}else{
				//バイナリ取得。

				if(this.file_item != null){
					if(this.file_item.GetResultType() != Fee.File.Item.ResultType.None){
						UnityEngine.Texture2D t_texture = this.file_item.GetResultAssetTexture();

						if(t_texture == null){
							this.sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
						}else{
							this.sprite.SetTexture(t_texture);
						}

						this.file_item = null;
					}
				}
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
		}

		/** Update
		*/
		private void Update()
		{
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
		}

		/** 強制終了。
		*/
		public override void Shutdown()
		{
			Fee.Function.Function.GetInstance().UnSetRowUpdate(this.RowUpdate);
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
			Fee.Function.Function.GetInstance().UnSetRowUpdate(this.RowUpdate);
			return true;
		}

		/** 削除。
		*/
		public override void Destroy()
		{
			//削除。
			this.deleter.DeleteAll();

			//ライブラリ停止。
			DeleteLibInstance.DeleteAll();
		}
	}
}

