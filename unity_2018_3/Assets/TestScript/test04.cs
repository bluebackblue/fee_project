using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** TestScript
*/
namespace TestScript
{
	/** test04

		サウンドプール
			ダウンロード
			ロードローカル
			ロードストリーミングアセット

		テキスト
			セーブローカル
			ダウンロード
			ロードローカル
			ロードストリーミングアセット

		バイナリー
			セーブローカル
			ダウンロード
			ロードローカル
			ロードストリーミングアセット

		テクスチャー
			セーブローカル
			ダウンロード
			ロードローカル
			ロードストリーミングアセット

	*/
	public class test04 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test04",
				"test04",

				@"
				ダウンロード
				セーブローカル
				ロードローカル
				ロードストリーミングアセット
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** CallBackId
		*/
		private enum CallBackId
		{
			DownLoad_SoundPool,
			LoadLocal_SoundPool,
			LoadStreamingAssets_SoundPool,

			SaveLocal_TextFile,
			DownLoad_TextFile,
			LoadLocal_TextFile,
			LoadStreamingAssets_TextFile,

			SaveLocal_BinaryFile,
			DownLoad_BinaryFile,
			LoadLocal_BinaryFile,
			LoadStreamingAssets_BinaryFile,

			SaveLocal_TextureFile,
			DownLoad_TextureFile,
			LoadLocal_TextureFile,
			LoadStreamingAssets_TextureFile,


		}

		/** status
		*/
		private Fee.Render2D.Text2D status_text;

		/** progress_sprite
		*/
		private Fee.Render2D.Sprite2D progress_sprite;

		/** result_sprite
		*/
		private Fee.Render2D.Sprite2D result_sprite;



		/** download_soundpool_button
		*/
		private Fee.Ui.Button download_soundpool_button;

		/** loadlocal_soundpool_button
		*/
		private Fee.Ui.Button loadlocal_soundpool_button;

		/** loadstreamingassets_soundpool_button
		*/
		private Fee.Ui.Button loadstreamingassets_soundpool_button;



		/** savelocal_textfile_button
		*/
		private Fee.Ui.Button savelocal_textfile_button;

		/** download_textfile_button
		*/
		private Fee.Ui.Button download_textfile_button;

		/** loadlocal_textfile_button
		*/
		private Fee.Ui.Button loadlocal_textfile_button;

		/** loadstreamingassets_textfile_button
		*/
		private Fee.Ui.Button loadstreamingassets_textfile_button;



		/** savelocal_binaryfile_button
		*/
		private Fee.Ui.Button savelocal_binaryfile_button;

		/** download_binaryfile_button
		*/
		private Fee.Ui.Button download_binaryfile_button;

		/** loadlocal_binaryfile_button
		*/
		private Fee.Ui.Button loadlocal_binaryfile_button;

		/** loadstreamingassets_binaryfile_button
		*/
		private Fee.Ui.Button loadstreamingassets_binaryfile_button;



		/** savelocal_texturefile_button
		*/
		private Fee.Ui.Button savelocal_texturefile_button;

		/** download_texturefile_button
		*/
		private Fee.Ui.Button download_texturefile_button;

		/** loadlocal_texturefile_button
		*/
		private Fee.Ui.Button loadlocal_texturefile_button;

		/** loadstreamingassets_texturefile_button
		*/
		private Fee.Ui.Button loadstreamingassets_texturefile_button;


		/** Item
		*/
		class Item
		{
			/** item_file
			*/
			private Fee.File.Item item_file;

			/** item_soundpool
			*/
			private Fee.SoundPool.Item item_soundpool;

			/** ResultType
			*/
			public enum ResultType
			{
				None,
				SaveEnd,
				Error,
				Binary,
				Text,
				Texture,
				AssetBundle,
				SoundPool,
			}

			/** constructor
			*/
			public Item(Fee.File.Item a_item)
			{
				this.item_file = a_item;
				this.item_soundpool = null;
			}

			/** constructor
			*/
			public Item(Fee.SoundPool.Item a_item)
			{
				this.item_file = null;
				this.item_soundpool = a_item;
			}

			/** IsBusy
			*/
			public bool IsBusy()
			{
				if(this.item_file != null){
					return this.item_file.IsBusy();
				}
				return this.item_soundpool.IsBusy();
			}

			/** GetResultProgressDown
			*/
			public float GetResultProgressDown()
			{
				if(this.item_file != null){
					return this.item_file.GetResultProgressDown();
				}
				return this.item_soundpool.GetResultProgressDown();
			}

			/** GetResultErrorString
			*/
			public string GetResultErrorString()
			{
				if(this.item_file != null){
					return this.item_file.GetResultErrorString();
				}
				return this.item_soundpool.GetResultErrorString();
			}

			/** GetResultResponseHeader
			*/
			public System.Collections.Generic.Dictionary<string,string> GetResultResponseHeader()
			{
				if(this.item_file != null){
					return this.item_file.GetResultResponseHeader();
				}
				return this.item_soundpool.GetResultResponseHeader();
			}

			/** GetResultText
			*/
			public string GetResultText()
			{
				if(this.item_file != null){
					return this.item_file.GetResultText();
				}
				return null;
			}

			/** GetResultTexture
			*/
			public Texture2D GetResultTexture()
			{
				if(this.item_file != null){
					return this.item_file.GetResultTexture();
				}
				return null;
			}

			/** GetResultType
			*/
			public ResultType GetResultType()
			{
				if(this.item_file != null){
					switch(this.item_file.GetResultType()){
					case Fee.File.Item.ResultType.None:
						{
						}return ResultType.None;
					case Fee.File.Item.ResultType.SaveEnd:
						{
						}return ResultType.SaveEnd;
					case Fee.File.Item.ResultType.Error:
						{
						}return ResultType.Error;
					case Fee.File.Item.ResultType.Binary:
						{
						}return ResultType.Binary;
					case Fee.File.Item.ResultType.Text:
						{
						}return ResultType.Text;
					case Fee.File.Item.ResultType.Texture:
						{
						}return ResultType.Texture;
					case Fee.File.Item.ResultType.AssetBundle:
						{
						}return ResultType.AssetBundle;
					}
				}

				switch(this.item_soundpool.GetResultType()){
				case Fee.SoundPool.Item.ResultType.None:
					{
					}return ResultType.None;
				case Fee.SoundPool.Item.ResultType.Error:
					{
					}return ResultType.Error;
				case Fee.SoundPool.Item.ResultType.SoundPool:
					{
					}return ResultType.SoundPool;
				}

				return ResultType.None;
			}
		}

		/** item
		*/
		private Item item;

		/** Start
		*/
		private void Start()
		{
			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Render2D.CreateInstance();

			//ファイル。インスタンス作成。
			Fee.File.Config.LOG_ENABLE = true;
			Fee.File.File.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.CreateInstance();

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Ui.CreateInstance();

			//サウンドプール。インスタンス作成。
			Fee.SoundPool.Config.LOG_ENABLE = true;
			Fee.SoundPool.Config.USE_DOWNLOAD_SOUNDPOOL_CACHE = false;
			Fee.SoundPool.Config.USE_LOADSTREAMINGASSETS_SOUNDPOOL_CACHE = false;
			Fee.SoundPool.SoundPool.CreateInstance();
	
			//フォント。
			Font t_font = Resources.Load<Font>(Data.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//drawpriority
			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;

			//status
			this.status_text = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
			this.status_text.SetRect(50,60,0,0);
			this.status_text.SetFontSize(13);

			//result_sprite
			this.result_sprite = new Fee.Render2D.Sprite2D(this.deleter,t_drawpriority);
			this.result_sprite.SetRect(400,30,64,64);

			//progress_sprite
			this.progress_sprite = new Fee.Render2D.Sprite2D(this.deleter,t_drawpriority);
			this.progress_sprite.SetTexture(Texture2D.whiteTexture);
			this.progress_sprite.SetRect(50,90,300,10);

			int t_y = 120;

			int t_button_h = 27;

			//download_soundpool_button
			this.download_soundpool_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.DownLoad_SoundPool);
			this.download_soundpool_button.SetRect(50,t_y,300,t_button_h);
			this.download_soundpool_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.download_soundpool_button.SetText("DownLoad SoundPool");
			this.download_soundpool_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//loadlocal_soundpool_button
			this.loadlocal_soundpool_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.LoadLocal_SoundPool);
			this.loadlocal_soundpool_button.SetRect(50,t_y,300,t_button_h);
			this.loadlocal_soundpool_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.loadlocal_soundpool_button.SetText("LoadLocal SoundPool");
			this.loadlocal_soundpool_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//loadstreamingassets_soundpool_button
			this.loadstreamingassets_soundpool_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.LoadStreamingAssets_SoundPool);
			this.loadstreamingassets_soundpool_button.SetRect(50,t_y,300,t_button_h);
			this.loadstreamingassets_soundpool_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.loadstreamingassets_soundpool_button.SetText("LoadStreamingAssets SoundPool");
			this.loadstreamingassets_soundpool_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//savelocal_textfile_button
			this.savelocal_textfile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.SaveLocal_TextFile);
			this.savelocal_textfile_button.SetRect(50,t_y,300,t_button_h);
			this.savelocal_textfile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.savelocal_textfile_button.SetText("SaveLocal Text");
			this.savelocal_textfile_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//download_textfile_button
			this.download_textfile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.DownLoad_TextFile);
			this.download_textfile_button.SetRect(50,t_y,300,t_button_h);
			this.download_textfile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.download_textfile_button.SetText("DownLoad Text");
			this.download_textfile_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//loadlocal_textfile_button
			this.loadlocal_textfile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.LoadLocal_TextFile);
			this.loadlocal_textfile_button.SetRect(50,t_y,300,t_button_h);
			this.loadlocal_textfile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.loadlocal_textfile_button.SetText("LoadLocal Text");
			this.loadlocal_textfile_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//loadstreamingassets_textfile_button
			this.loadstreamingassets_textfile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.LoadStreamingAssets_TextFile);
			this.loadstreamingassets_textfile_button.SetRect(50,t_y,300,30);
			this.loadstreamingassets_textfile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.loadstreamingassets_textfile_button.SetText("LoadStreamingAssets Text");
			this.loadstreamingassets_textfile_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//savelocal_binaryfile_button
			this.savelocal_binaryfile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.SaveLocal_BinaryFile);
			this.savelocal_binaryfile_button.SetRect(50,t_y,300,t_button_h);
			this.savelocal_binaryfile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.savelocal_binaryfile_button.SetText("SaveLocal Binary");
			this.savelocal_binaryfile_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//download_binaryfile_button
			this.download_binaryfile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.DownLoad_BinaryFile);
			this.download_binaryfile_button.SetRect(50,t_y,300,t_button_h);
			this.download_binaryfile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.download_binaryfile_button.SetText("DownLoad Binary");
			this.download_binaryfile_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//loadlocal_binaryfile_button
			this.loadlocal_binaryfile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.LoadLocal_BinaryFile);
			this.loadlocal_binaryfile_button.SetRect(50,t_y,300,t_button_h);
			this.loadlocal_binaryfile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.loadlocal_binaryfile_button.SetText("LoadLocal Binary");
			this.loadlocal_binaryfile_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//loadstreamingassets_binaryfile_button
			this.loadstreamingassets_binaryfile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.LoadStreamingAssets_BinaryFile);
			this.loadstreamingassets_binaryfile_button.SetRect(50,t_y,300,t_button_h);
			this.loadstreamingassets_binaryfile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.loadstreamingassets_binaryfile_button.SetText("LoadStreamingAssets Binary");
			this.loadstreamingassets_binaryfile_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//savelocal_texturefile_button
			this.savelocal_texturefile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.SaveLocal_TextureFile);
			this.savelocal_texturefile_button.SetRect(50,t_y,300,t_button_h);
			this.savelocal_texturefile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.savelocal_texturefile_button.SetText("SaveLocal Texture");
			this.savelocal_texturefile_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//download_texturefile_button
			this.download_texturefile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.DownLoad_TextureFile);
			this.download_texturefile_button.SetRect(50,t_y,300,t_button_h);
			this.download_texturefile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.download_texturefile_button.SetText("DownLoad Texture");
			this.download_texturefile_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//loadlocal_texturefile_button
			this.loadlocal_texturefile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.LoadLocal_TextureFile);
			this.loadlocal_texturefile_button.SetRect(50,t_y,300,t_button_h);
			this.loadlocal_texturefile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.loadlocal_texturefile_button.SetText("LoadLocal Texture");
			this.loadlocal_texturefile_button.SetFontSize(15);

			t_y += t_button_h + 3;

			//loadstreamingassets_texturefile_button
			this.loadstreamingassets_texturefile_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.LoadStreamingAssets_TextureFile);
			this.loadstreamingassets_texturefile_button.SetRect(50,t_y,300,t_button_h);
			this.loadstreamingassets_texturefile_button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.loadstreamingassets_texturefile_button.SetText("LoadStreamingAssets Texture");
			this.loadstreamingassets_texturefile_button.SetFontSize(15);

			//item
			this.item = null;
		}

		/** [Button_Base]コールバック。クリック。
		*/
		private void CallBack_Click(int a_id)
		{
			this.result_sprite.SetVisible(false);

			switch((CallBackId)a_id){
			case CallBackId.DownLoad_SoundPool:
				{
					//ダウンロード。サウンドプール。

					uint t_data_version = 1;
					this.item = new Item(Fee.SoundPool.SoundPool.GetInstance().RequestDownLoadSoundPool(new Fee.File.Path("https://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/Raw/","se.txt"),null,t_data_version));
				}break;
			case CallBackId.LoadLocal_SoundPool:
				{
					//ロードローカル。サウンドプール。

					this.item = new Item(Fee.SoundPool.SoundPool.GetInstance().RequestLoadLocalSoundPool(new Fee.File.Path("se.txt")));
				}break;
			case CallBackId.LoadStreamingAssets_SoundPool:
				{
					//ロードストリーミングアセット。サウンドプール。

					uint t_data_version = 1;
					this.item = new Item(Fee.SoundPool.SoundPool.GetInstance().RequestLoadStreamingAssetsSoundPool(new Fee.File.Path("se.txt"),t_data_version));
				}break;
			case CallBackId.SaveLocal_TextFile:
				{
					//セーブローカル。テキストファイル。

					this.item = new Item(Fee.File.File.GetInstance().RequestSaveLocalTextFile(new Fee.File.Path("text.txt"),"xyz"));
				}break;
			case CallBackId.DownLoad_TextFile:
				{
					//ダウンロード。テキストファイル。

					this.item = new Item(Fee.File.File.GetInstance().RequestDownLoadTextFile(new Fee.File.Path("https://bbbproject.sakura.ne.jp/www/project_webgl/fee/Data/","text.txt"),null));
				}break;
			case CallBackId.LoadLocal_TextFile:
				{
					//ロードローカル。テキストファイル。

					this.item = new Item(Fee.File.File.GetInstance().RequestLoadLocalTextFile(new Fee.File.Path("text.txt")));
				}break;
			case CallBackId.LoadStreamingAssets_TextFile:
				{
					//ロードストリーミングアセット。テキストファイル。

					this.item = new Item(Fee.File.File.GetInstance().RequestLoadStreamingAssetsTextFile(new Fee.File.Path("text.txt")));
				}break;
			case CallBackId.SaveLocal_BinaryFile:
				{
					//セーブローカル。バイナリファイル。

					byte[] t_binary = new byte[16];
					this.item = new Item(Fee.File.File.GetInstance().RequestSaveLocalBinaryFile(new Fee.File.Path("binary"),t_binary));
				}break;
			case CallBackId.DownLoad_BinaryFile:
				{
					//ダウンロード。バイナリファイル。

					this.item = new Item(Fee.File.File.GetInstance().RequestDownLoadBinaryFile(new Fee.File.Path("https://bbbproject.sakura.ne.jp/www/project_webgl/fee/Data/","binary"),null));
				}break;
			case CallBackId.LoadLocal_BinaryFile:
				{
					//ロードローカル。バイナリファイル。

					this.item = new Item(Fee.File.File.GetInstance().RequestLoadLocalBinaryFile(new Fee.File.Path("binary")));
				}break;
			case CallBackId.LoadStreamingAssets_BinaryFile:
				{
					//ロードストリーミングアセット。バイナリファイル。

					this.item = new Item(Fee.File.File.GetInstance().RequestLoadStreamingAssetsBinaryFile(new Fee.File.Path("binary")));
				}break;
			case CallBackId.SaveLocal_TextureFile:
				{
					//セーブローカル。テクスチャーファイル。

					Texture2D t_texture = new Texture2D(64,64);
					{
						t_texture.filterMode = FilterMode.Point;
						t_texture.wrapMode = TextureWrapMode.Clamp;
						for(int xx=0;xx<t_texture.width;xx++){
							for(int yy=0;yy<t_texture.height;yy++){
								t_texture.SetPixel(xx,yy,new Color((float)xx / t_texture.width,(float)yy / t_texture.height,0.0f,(float)xx / t_texture.width));
							}
						}
						t_texture.Apply();
					}

					this.item = new Item(Fee.File.File.GetInstance().RequestSaveLocalTextureFile(new Fee.File.Path("texture.png"),t_texture));
				}break;
			case CallBackId.DownLoad_TextureFile:
				{
					//ダウンロード。テクスチャーファイル。

					this.item = new Item(Fee.File.File.GetInstance().RequestDownLoadTextureFile(new Fee.File.Path("https://bbbproject.sakura.ne.jp/www/project_webgl/fee/Data/","texture.png"),null));
				}break;
			case CallBackId.LoadLocal_TextureFile:
				{
					//ロードローカル。テクスチャーファイル。

					this.item = new Item(Fee.File.File.GetInstance().RequestLoadLocalTextureFile(new Fee.File.Path("texture.png")));
				}break;
			case CallBackId.LoadStreamingAssets_TextureFile:
				{
					//ロードストリーミングアセット。テクスチャーファイル。

					this.item = new Item(Fee.File.File.GetInstance().RequestLoadStreamingAssetsTextureFile(new Fee.File.Path("texture.png")));
				}break;
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//ファイル。
			Fee.File.File.GetInstance().Main();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

			//ＵＩ。インスタンス作成。
			Fee.Ui.Ui.GetInstance().Main();

			//サウンドプール。インスタンス作成。
			Fee.SoundPool.SoundPool.GetInstance().Main();

			if(this.item != null){
				if(this.item.IsBusy() == true){
					this.status_text.SetText("処理中");

					this.progress_sprite.SetW((int)(300 * this.item.GetResultProgressDown()));
				}else{
					switch(this.item.GetResultType()){
					case Item.ResultType.Error:
						{
							this.status_text.SetText("結果:エラー : \n" + this.item.GetResultErrorString());
						}break;
					case Item.ResultType.None:
						{
							this.status_text.SetText("結果:なし");
						}break;
					case Item.ResultType.SaveEnd:
						{
							this.status_text.SetText("結果:セーブ完了");
						}break;
					case Item.ResultType.SoundPool:
						{
							string t_responsheader = "";
							if(this.item.GetResultResponseHeader() != null){
								t_responsheader = this.item.GetResultResponseHeader().Count.ToString();
							}

							this.status_text.SetText("結果:サウンドプール取得 : " + t_responsheader);
						}break;
					case Item.ResultType.Text:
						{
							string t_responsheader = "";
							if(this.item.GetResultResponseHeader() != null){
								t_responsheader = this.item.GetResultResponseHeader().Count.ToString();
							}

							this.status_text.SetText("結果:テキスト取得 : " + t_responsheader + " : " + this.item.GetResultText());
						}break;
					case Item.ResultType.Binary:
						{
							string t_responsheader = "";
							if(this.item.GetResultResponseHeader() != null){
								t_responsheader = this.item.GetResultResponseHeader().Count.ToString();
							}

							this.status_text.SetText("結果:バイナリ取得 : " + t_responsheader);
						}break;
					case Item.ResultType.Texture:
						{
							string t_responsheader = "";
							if(this.item.GetResultResponseHeader() != null){
								t_responsheader = this.item.GetResultResponseHeader().Count.ToString();
							}

							this.result_sprite.SetVisible(true);
							this.result_sprite.SetTexture(this.item.GetResultTexture());

							this.status_text.SetText("結果:テクスチャー取得 : " + t_responsheader);
						}break;
					}

					this.item = null;
				}
			}
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
			return true;
		}

		/** OnDestroy
		*/
		private void OnDestroy()
		{
			this.deleter.DeleteAll();
		}
	}
}

