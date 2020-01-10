using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

		/** button
		*/
		private Fee.Ui.Button button;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** sprite
		*/
		private Fee.Render2D.Sprite2D sprite;

		/** filesize_text
		*/
		private Fee.Render2D.Text2D filesize_text;

		/** filepath
		*/
		private string filepath;

		/** file_item
		*/
		private Fee.File.Item file_item;
		private bool file_item_android;

		/** Start
		*/
		private void Start()
		{
			//プラットフォーム。インスタンス作成。
			Fee.Platform.Config.LOG_ENABLE = true;
			Fee.Platform.Platform.CreateInstance();

			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.LOG_ENABLE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Config.LOG_ENABLE = true;
			Fee.Input.Mouse.CreateInstance();

			//イベントプレート。
			Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//ファイル。インスタンス作成。
			Fee.File.File.CreateInstance();

			//フォント。
			UnityEngine.Font t_font = UnityEngine.Resources.Load<UnityEngine.Font>(Data.Resources.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//button
			{
				int t_x = 50;
				int t_y = 200;
				int t_w = 90;
				int t_h = 60;

				//button
				this.button = new Fee.Ui.Button(this.deleter,1);
				this.button.SetOnButtonClick(this,0);
				this.button.SetTextureCornerSize(10);
				this.button.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.button.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.button.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.button.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
				this.button.SetText("開始");
				this.button.SetRect(t_x,t_y,t_w,t_h);
			}

			//text
			{
				int t_x = 50 + 150;
				int t_y = 200 + 20;

				this.text = Fee.Render2D.Text2D.Create(this.deleter,1);
				this.text.SetXY(t_x,t_y);
			}

			//filesize_text
			{
				this.filesize_text = Fee.Render2D.Text2D.Create(this.deleter,1);
				this.filesize_text.SetXY(50,300);
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
			this.file_item_android = false;
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(int a_id)
		{
			this.text.SetText("");
			this.filesize_text.SetText("");
			this.sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);

			Fee.Platform.Platform.GetInstance().OpenFileDialog();
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(this.is_focus,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			{
				string t_full_path = Fee.Platform.Platform.GetInstance().GetOpenFileDialogResult();
				if(t_full_path == null){
					t_full_path = "null";
				}else if(t_full_path == ""){
					t_full_path = "space";
				}
				this.text.SetText(t_full_path);

				byte[] t_data_binary = null;

				if((this.file_item == null)&&(this.file_item_android == false)){
					//開始。

					if(this.filepath != t_full_path){
						this.filepath = t_full_path;
						if((this.filepath != "null")&&(this.filepath != "space")){

							#if((!UNITY_EDITOR)&&(UNITY_ANDROID)) || true //TODO
							{
								this.file_item_android = Fee.Platform.Platform.GetInstance().LoadContentFile_Start(new Fee.File.Path(this.filepath));

								if(this.file_item_android == false){
									this.filesize_text.SetText("LoadContentFile_Start == false");
								}
							}
							#else
							{
								this.file_item = Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadFullPathBinaryFile,new Fee.File.Path(t_full_path));
							}
							#endif
						}
					}
				}else{
					//バイナリ取得。

					if(this.file_item != null){
						if(this.file_item.GetResultType() != Fee.File.Item.ResultType.None){
							t_data_binary = this.file_item.GetResultAssetBinary();

							if(t_data_binary == null){
								this.filesize_text.SetText("GetResultAssetBinary == null");
							}

							this.file_item = null;
						}
					}else if(this.file_item_android == true){
						#if((!UNITY_EDITOR)&&(UNITY_ANDROID)) || true //TODO

						if(Fee.Platform.Platform.GetInstance().LoadContentFile_IsComplate() == true){

							t_data_binary = Fee.Platform.Platform.GetInstance().LoadContentFile_GetResult();
							if(t_data_binary != null){

								Fee.Platform.Platform.GetInstance().LoadContentFile_End();
								this.file_item_android = false;

							}else{

								this.filesize_text.SetText("complate == true : binary == null");

							}
						}else{

							this.filesize_text.SetText("complate == false");

						}

						#endif
					}
				}

				if(t_data_binary != null){
					this.filesize_text.SetText("size = " + t_data_binary.Length.ToString());

					UnityEngine.Texture2D t_texture = Fee.File.BinaryToTexture2D.Convert(t_data_binary);
					if(t_texture != null){
						this.sprite.SetTexture(t_texture);
					}
				}
			}

			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_After();
		}

		/** Update
		*/
		private void Update()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_PreDraw();

			//ファイル。
			Fee.File.File.GetInstance().Main();
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
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

