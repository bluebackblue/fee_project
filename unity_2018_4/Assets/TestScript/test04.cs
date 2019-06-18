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

			LoadResources_AssetFile,
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

		/** loadItem
		*/
		class loadItem
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
				Asset,
			}

			/** constructor
			*/
			public loadItem(Fee.File.Item a_item)
			{
				this.item_file = a_item;
				this.item_soundpool = null;
			}

			/** constructor
			*/
			public loadItem(Fee.SoundPool.Item a_item)
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

			/** GetResultAsset
			*/
			public UnityEngine.Object GetResultAsset()
			{
				if(this.item_file != null){
					return this.item_file.GetResultAsset();
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
					case Fee.File.Item.ResultType.Asset:
						{
						}return ResultType.Asset;
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

		/** リストアイテム。
		*/
		private class Scroll_Item : Fee.Ui.ScrollItem_Base
		{
			/** button
			*/
			public Fee.Ui.Button button;

			/** GetItemLength
			*/
			public static int GetItemLength()
			{
				return 27;
			}

			/** constructor
			*/
			public Scroll_Item(Fee.Deleter.Deleter a_deleter,Fee.Ui.Button_Base.CallBack_Click a_callback_click,CallBackId a_callback_click_id)
			{
				this.button = new Fee.Ui.Button(a_deleter,1,a_callback_click,(int)a_callback_click_id);
				this.button.SetClip(true);
				this.button.SetDragCancelFlag(true);
				this.button.SetTexture(Resources.Load<Texture2D>(Data.UI_TEXTURE_BUTTON));
				this.button.SetText(a_callback_click_id.ToString());
			}

			/** [Fee.Ui.ScrollItem_Base]矩形。設定。
			*/
			public override void SetY(int a_y)
			{
				this.button.SetY(a_y);
			}

			/** [Fee.Ui.ScrollItem_Base]矩形。設定。
			*/
			public override void SetX(int a_x)
			{
				this.button.SetX(a_x);
			}

			/** [Fee.Ui.ScrollItem_Base]矩形。設定。
			*/
			public override void SetWH(int a_w,int a_h)
			{
				this.button.SetWH(a_w,a_h);
			}

			/** [Fee.Ui.ScrollItem_Base]クリップ矩形。設定。
			*/
			public override void SetClipRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
			{
				this.button.SetClipRect(ref a_rect);
			}

			/** [Fee.Ui.ScrollItem_Base]表示内。
			*/
			public override void OnViewIn()
			{
				this.button.SetVisible(true);
			}

			/** [Fee.Ui.ScrollItem_Base]表示外。
			*/
			public override void OnViewOut()
			{
				this.button.SetVisible(false);
			}
		}

		/** loaditem
		*/
		private loadItem loaditem;

		/** scroll
		*/
		private Fee.Ui.Scroll<Scroll_Item> scroll;

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
			this.result_sprite.SetTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_MAX);

			//progress_sprite
			this.progress_sprite = new Fee.Render2D.Sprite2D(this.deleter,t_drawpriority);
			this.progress_sprite.SetTexture(Texture2D.whiteTexture);
			this.progress_sprite.SetRect(50,90,300,10);

			//loaditem
			this.loaditem = null;

			//scroll_item
			{
				this.scroll = new Fee.Ui.Scroll<Scroll_Item>(this.deleter,t_drawpriority,Fee.Ui.ScrollType.Vertical,30);
				this.scroll.SetRect(50,250,350,300);
				
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.DownLoad_SoundPool));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadLocal_SoundPool));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadStreamingAssets_SoundPool));

				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.SaveLocal_TextFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.DownLoad_TextFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadLocal_TextFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadStreamingAssets_TextFile));

				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.SaveLocal_BinaryFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.DownLoad_BinaryFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadLocal_BinaryFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadStreamingAssets_BinaryFile));

				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.SaveLocal_TextureFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.DownLoad_TextureFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadLocal_TextureFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadStreamingAssets_TextureFile));

				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadResources_AssetFile));
			}
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
					this.loaditem = new loadItem(Fee.SoundPool.SoundPool.GetInstance().RequestDownLoadSoundPool(new Fee.File.Path("https://bbbproject.sakura.ne.jp/www/project_webgl/SoundPool/","se.txt"),null,t_data_version));
				}break;
			case CallBackId.LoadLocal_SoundPool:
				{
					//ロードローカル。サウンドプール。

					this.loaditem = new loadItem(Fee.SoundPool.SoundPool.GetInstance().RequestLoadLocalSoundPool(new Fee.File.Path("se.txt")));
				}break;
			case CallBackId.LoadStreamingAssets_SoundPool:
				{
					//ロードストリーミングアセット。サウンドプール。

					uint t_data_version = 1;
					this.loaditem = new loadItem(Fee.SoundPool.SoundPool.GetInstance().RequestLoadStreamingAssetsSoundPool(new Fee.File.Path("SoundPool/se.txt"),t_data_version));
				}break;
			case CallBackId.SaveLocal_TextFile:
				{
					//セーブローカル。テキストファイル。

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestSaveLocalTextFile(new Fee.File.Path("text.txt"),"xyz"));
				}break;
			case CallBackId.DownLoad_TextFile:
				{
					//ダウンロード。テキストファイル。

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestDownLoadTextFile(new Fee.File.Path("https://bbbproject.sakura.ne.jp/www/project_webgl/Test04/","text.txt"),null));
				}break;
			case CallBackId.LoadLocal_TextFile:
				{
					//ロードローカル。テキストファイル。

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestLoadLocalTextFile(new Fee.File.Path("text.txt")));
				}break;
			case CallBackId.LoadStreamingAssets_TextFile:
				{
					//ロードストリーミングアセット。テキストファイル。

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestLoadStreamingAssetsTextFile(new Fee.File.Path("Test04/text.txt")));
				}break;
			case CallBackId.SaveLocal_BinaryFile:
				{
					//セーブローカル。バイナリファイル。

					byte[] t_binary = new byte[16];
					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestSaveLocalBinaryFile(new Fee.File.Path("binary"),t_binary));
				}break;
			case CallBackId.DownLoad_BinaryFile:
				{
					//ダウンロード。バイナリファイル。

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestDownLoadBinaryFile(new Fee.File.Path("https://bbbproject.sakura.ne.jp/www/project_webgl/Test04/","binary"),null));
				}break;
			case CallBackId.LoadLocal_BinaryFile:
				{
					//ロードローカル。バイナリファイル。

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestLoadLocalBinaryFile(new Fee.File.Path("binary")));
				}break;
			case CallBackId.LoadStreamingAssets_BinaryFile:
				{
					//ロードストリーミングアセット。バイナリファイル。

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestLoadStreamingAssetsBinaryFile(new Fee.File.Path("Test04/binary")));
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

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestSaveLocalTextureFile(new Fee.File.Path("texture.png"),t_texture));
				}break;
			case CallBackId.DownLoad_TextureFile:
				{
					//ダウンロード。テクスチャーファイル。

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestDownLoadTextureFile(new Fee.File.Path("https://bbbproject.sakura.ne.jp/www/project_webgl/Test04/","texture.png"),null));
				}break;
			case CallBackId.LoadLocal_TextureFile:
				{
					//ロードローカル。テクスチャーファイル。

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestLoadLocalTextureFile(new Fee.File.Path("texture.png")));
				}break;
			case CallBackId.LoadStreamingAssets_TextureFile:
				{
					//ロードストリーミングアセット。テクスチャーファイル。

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestLoadStreamingAssetsTextureFile(new Fee.File.Path("Test04/texture.png")));
				}break;
			case CallBackId.LoadResources_AssetFile:
				{
					//ロードリソース。アセットファイル。

					this.loaditem = new loadItem(Fee.File.File.GetInstance().RequestLoadResourcesAssetFile(new Fee.File.Path("Test04/texture")));
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

			//ドラッグ更新。
			this.scroll.DragScrollUpdate();

			if(this.loaditem != null){
				if(this.loaditem.IsBusy() == true){
					this.status_text.SetText("処理中");

					this.progress_sprite.SetW((int)(300 * this.loaditem.GetResultProgressDown()));
				}else{
					switch(this.loaditem.GetResultType()){
					case loadItem.ResultType.Error:
						{
							this.status_text.SetText("結果:エラー : \n" + this.loaditem.GetResultErrorString());
						}break;
					case loadItem.ResultType.None:
						{
							this.status_text.SetText("結果:なし");
						}break;
					case loadItem.ResultType.SaveEnd:
						{
							this.status_text.SetText("結果:セーブ完了");
						}break;
					case loadItem.ResultType.SoundPool:
						{
							string t_responsheader = "";
							if(this.loaditem.GetResultResponseHeader() != null){
								t_responsheader = this.loaditem.GetResultResponseHeader().Count.ToString();
							}

							this.status_text.SetText("結果:サウンドプール取得 : " + t_responsheader);
						}break;
					case loadItem.ResultType.Text:
						{
							string t_responsheader = "";
							if(this.loaditem.GetResultResponseHeader() != null){
								t_responsheader = this.loaditem.GetResultResponseHeader().Count.ToString();
							}

							this.status_text.SetText("結果:テキスト取得 : " + t_responsheader + " : " + this.loaditem.GetResultText());
						}break;
					case loadItem.ResultType.Binary:
						{
							string t_responsheader = "";
							if(this.loaditem.GetResultResponseHeader() != null){
								t_responsheader = this.loaditem.GetResultResponseHeader().Count.ToString();
							}

							this.status_text.SetText("結果:バイナリ取得 : " + t_responsheader);
						}break;
					case loadItem.ResultType.Texture:
						{
							string t_responsheader = "";
							if(this.loaditem.GetResultResponseHeader() != null){
								t_responsheader = this.loaditem.GetResultResponseHeader().Count.ToString();
							}

							this.result_sprite.SetVisible(true);
							this.result_sprite.SetTexture(this.loaditem.GetResultTexture());

							this.status_text.SetText("結果:テクスチャー取得 : " + t_responsheader);
						}break;
					case loadItem.ResultType.Asset:
						{
							string t_responsheader = "";
							if(this.loaditem.GetResultResponseHeader() != null){
								t_responsheader = this.loaditem.GetResultResponseHeader().Count.ToString();
							}

							UnityEngine.Object t_asset = this.loaditem.GetResultAsset();
							if(t_asset is Texture2D){
								this.result_sprite.SetVisible(true);
								this.result_sprite.SetTexture(t_asset as Texture2D);

								this.status_text.SetText("結果:テクスチャー取得 : " + t_responsheader);
							}else{
								this.status_text.SetText("結果:不明アッセット取得 : " + t_responsheader);
							}
						}break;
					}

					this.loaditem = null;
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

