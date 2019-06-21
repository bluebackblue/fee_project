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
			ロードリソース

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
			ロードリソース

		アセット
			ロードリソース

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
			/** SoundPool
			*/
			DownLoad_SoundPool,
			LoadLocal_SoundPool,
			LoadStreamingAssets_SoundPool,

			/** TextFile
			*/
			SaveLocal_TextFile,
			DownLoad_TextFile,
			LoadLocal_TextFile,
			LoadStreamingAssets_TextFile,
			LoadResources_TextFile,

			/** BinaryFile
			*/
			SaveLocal_BinaryFile,
			DownLoad_BinaryFile,
			LoadLocal_BinaryFile,
			LoadStreamingAssets_BinaryFile,

			/** TextureFile
			*/
			SaveLocal_TextureFile,
			DownLoad_TextureFile,
			LoadLocal_TextureFile,
			LoadStreamingAssets_TextureFile,
			LoadResources_TextureFile,

			/** AnythingFile
			*/
			LoadResources_AnythingFile,

			/** Data
			*/
			LoadData,

		}

		/** status_text
		*/
		private Fee.Render2D.Text2D status_text;

		/** progress_sprite
		*/
		private Fee.Render2D.Sprite2D progress_sprite;

		/** result_sprite
		*/
		private Fee.Render2D.Sprite2D result_sprite;

		/** LoadItem
		*/
		class LoadItem
		{
			/** item_file
			*/
			private Fee.File.Item item_file;

			/** item_soundpool
			*/
			private Fee.SoundPool.Item item_soundpool;

			/** item_data
			*/
			private Fee.Data.Item item_data;

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
				Anything,
			}

			/** constructor
			*/
			public LoadItem(Fee.File.Item a_item)
			{
				this.item_file = a_item;
				this.item_soundpool = null;
				this.item_data = null;
			}

			/** constructor
			*/
			public LoadItem(Fee.SoundPool.Item a_item)
			{
				this.item_file = null;
				this.item_soundpool = a_item;
				this.item_data = null;
			}

			public LoadItem(Fee.Data.Item a_item)
			{
				this.item_file = null;
				this.item_soundpool = null;
				this.item_data = a_item;
			}

			/** IsBusy
			*/
			public bool IsBusy()
			{
				if(this.item_file != null){
					return this.item_file.IsBusy();
				}
				if(this.item_soundpool != null){
					return this.item_soundpool.IsBusy();
				}
				if(this.item_data != null){
					return this.item_data.IsBusy();
				}
				return false;
			}

			/** GetResultProgress
			*/
			public float GetResultProgress()
			{
				if(this.item_file != null){
					return this.item_file.GetResultProgressDown();
				}
				if(this.item_soundpool != null){
					return this.item_soundpool.GetResultProgressDown();
				}
				if(this.item_data != null){
					return this.item_data.GetResultProgress();
				}
				return 0.0f;
			}

			/** GetResultErrorString
			*/
			public string GetResultErrorString()
			{
				if(this.item_file != null){
					return this.item_file.GetResultErrorString();
				}
				if(this.item_soundpool != null){
					return this.item_soundpool.GetResultErrorString();
				}
				if(this.item_data != null){
					return this.item_data.GetResultErrorString();
				}
				return "";
			}

			/** GetResultText
			*/
			public string GetResultText()
			{
				if(this.item_file != null){
					return this.item_file.GetResultAssetText();
				}
				if(this.item_data != null){
					return this.item_data.GetResultAssetText();
				}
				return null;
			}

			/** GetResultTexture
			*/
			public Texture2D GetResultTexture()
			{
				if(this.item_file != null){
					return this.item_file.GetResultAssetTexture();
				}
				if(this.item_data != null){
					return this.item_data.GetResultAssetTexture();
				}
				return null;
			}

			/** GetResultAnything
			*/
			public object GetResultAnything()
			{
				if(this.item_file != null){
					return this.item_file.GetResultAssetAnything();
				}
				if(this.item_data != null){
					return this.item_data.GetResultAssetAnything();
				}
				return null;
			}

			/** GetResultBinary
			*/
			public byte[] GetResultBinary()
			{
				if(this.item_file != null){
					return this.item_file.GetResultAssetBinary();
				}
				if(this.item_data != null){
					return this.item_data.GetResultAssetBinary();
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
					case Fee.File.Item.ResultType.Asset:
						{
							switch(this.item_file.GetResultAssetType()){
							case Fee.Asset.AssetType.None:
								{
								}return ResultType.None;
							case Fee.Asset.AssetType.Binary:
								{
								}return ResultType.Binary;
							case Fee.Asset.AssetType.Text:
								{
								}return ResultType.Text;
							case Fee.Asset.AssetType.Texture:
								{
								}return ResultType.Texture;
							case Fee.Asset.AssetType.Anything:
								{
								}return ResultType.Anything;
							}
						}break;
					case Fee.File.Item.ResultType.AssetBundle:
						{
						}return ResultType.AssetBundle;
					}
				}

				if(this.item_soundpool != null){
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
				}

				if(this.item_data != null){
					switch(this.item_data.GetResultType()){
					case Fee.Data.Item.ResultType.None:
						{
						}return ResultType.None;
					case Fee.Data.Item.ResultType.Error:
						{
						}return ResultType.Error;
					case Fee.Data.Item.ResultType.Asset:
						{
							switch(this.item_data.GetResultAssetType()){
							case Fee.Asset.AssetType.None:
								{
								}return ResultType.None;
							case Fee.Asset.AssetType.Binary:
								{
								}return ResultType.Binary;
							case Fee.Asset.AssetType.Text:
								{
								}return ResultType.Text;
							case Fee.Asset.AssetType.Texture:
								{
								}return ResultType.Texture;
							case Fee.Asset.AssetType.Anything:
								{
								}return ResultType.Anything;
							}
						}break;
					}
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
				this.button.SetTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
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

		/** LoadItem
		*/
		private LoadItem loaditem;

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

			//データ。インスタンス作成。
			Fee.Data.Data.CreateInstance();

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
			Font t_font = Resources.Load<Font>(Data.Resources.FONT);
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
				
				//SoundPool
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.DownLoad_SoundPool));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadLocal_SoundPool));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadStreamingAssets_SoundPool));

				//TextFile
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.SaveLocal_TextFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.DownLoad_TextFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadLocal_TextFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadStreamingAssets_TextFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadResources_TextFile));

				//BinaryFile
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.SaveLocal_BinaryFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.DownLoad_BinaryFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadLocal_BinaryFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadStreamingAssets_BinaryFile));

				//TextureFile
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.SaveLocal_TextureFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.DownLoad_TextureFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadLocal_TextureFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadStreamingAssets_TextureFile));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadResources_TextureFile));
				
				//AssetFile
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadResources_AnythingFile));

				//Data
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,CallBackId.LoadData));

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
					this.loaditem = new LoadItem(Fee.SoundPool.SoundPool.GetInstance().RequestDownLoadSoundPool(new Fee.File.Path(Data.Url.SOUNDPOOL_SE),null,t_data_version));
				}break;
			case CallBackId.LoadLocal_SoundPool:
				{
					//ロードローカル。サウンドプール。

					this.loaditem = new LoadItem(Fee.SoundPool.SoundPool.GetInstance().RequestLoadLocalSoundPool(new Fee.File.Path(Data.Local.SOUNDPOOL_SE)));
				}break;
			case CallBackId.LoadStreamingAssets_SoundPool:
				{
					//ロードストリーミングアセット。サウンドプール。

					uint t_data_version = 1;
					this.loaditem = new LoadItem(Fee.SoundPool.SoundPool.GetInstance().RequestLoadStreamingAssetsSoundPool(new Fee.File.Path(Data.StreamingAssets.SOUNDPOOL_SE),t_data_version));
				}break;




			case CallBackId.SaveLocal_TextFile:
				{
					//セーブローカル。テキストファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestSaveLocalTextFile(new Fee.File.Path(Data.Local.TEST04_TEXT),"qwerasdfzxcv"));
				}break;
			case CallBackId.DownLoad_TextFile:
				{
					//ダウンロード。テキストファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.DownLoadTextFile,new Fee.File.Path(Data.Url.TEST04_TEXT)));
				}break;
			case CallBackId.LoadLocal_TextFile:
				{
					//ロードローカル。テキストファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad( Fee.File.File.LoadRequestType.LoadLocalTextFile,new Fee.File.Path(Data.Local.TEST04_TEXT)));
				}break;
			case CallBackId.LoadStreamingAssets_TextFile:
				{
					//ロードストリーミングアセット。テキストファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadStreamingAssetsTextFile,new Fee.File.Path(Data.StreamingAssets.TEST04_TEXT)));
				}break;
			case CallBackId.LoadResources_TextFile:
				{
					//ロードリソース。テキストファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadResourcesTextFile,new Fee.File.Path(Data.Resources.TEST04_TEXT)));
				}break;




			case CallBackId.SaveLocal_BinaryFile:
				{
					//セーブローカル。バイナリファイル。

					byte[] t_binary = new byte[16];
					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestSaveLocalBinaryFile(new Fee.File.Path(Data.Local.TEST04_BINARY),t_binary));
				}break;
			case CallBackId.DownLoad_BinaryFile:
				{
					//ダウンロード。バイナリファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.DownLoadBinaryFile,new Fee.File.Path(Data.Url.TEST04_BINARY)));
				}break;
			case CallBackId.LoadLocal_BinaryFile:
				{
					//ロードローカル。バイナリファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadLocalBinaryFile,new Fee.File.Path(Data.Local.TEST04_BINARY)));
				}break;
			case CallBackId.LoadStreamingAssets_BinaryFile:
				{
					//ロードストリーミングアセット。バイナリファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadStreamingAssetsBinaryFile,new Fee.File.Path(Data.StreamingAssets.TEST04_BINARY)));
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

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestSaveLocalTextureFile(new Fee.File.Path(Data.Local.TEST04_TEXTURE),t_texture));
				}break;
			case CallBackId.DownLoad_TextureFile:
				{
					//ダウンロード。テクスチャーファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.DownLoadTextureFile,new Fee.File.Path(Data.Url.TEST04_TEXTURE)));
				}break;
			case CallBackId.LoadLocal_TextureFile:
				{
					//ロードローカル。テクスチャーファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadLocalTextureFile,new Fee.File.Path(Data.Local.TEST04_TEXTURE)));
				}break;
			case CallBackId.LoadStreamingAssets_TextureFile:
				{
					//ロードストリーミングアセット。テクスチャーファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadStreamingAssetsTextureFile,new Fee.File.Path(Data.StreamingAssets.TEST04_TEXTURE)));
				}break;
			case CallBackId.LoadResources_TextureFile:
				{
					//ロードリソース。テクスチャーファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadResourcesTextureFile,new Fee.File.Path(Data.Resources.TEST04_TEXTURE)));
				}break;




			case CallBackId.LoadResources_AnythingFile:
				{
					//ロードリソース。アセットファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadResourcesAnythingFile,new Fee.File.Path(Data.Resources.TEST04_TEXTURE)));
				}break;


			case CallBackId.LoadData:
				{
					//ロードデータ。

					this.loaditem = new LoadItem(Fee.Data.Data.GetInstance().RequestNormal("ui_button"));
				}break;


			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//ファイル。
			Fee.File.File.GetInstance().Main();

			//データ。
			Fee.Data.Data.GetInstance().Main();

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

					this.progress_sprite.SetW((int)(300 * this.loaditem.GetResultProgress()));
				}else{
					switch(this.loaditem.GetResultType()){
					case LoadItem.ResultType.Error:
						{
							this.status_text.SetText("結果:エラー : \n" + this.loaditem.GetResultErrorString());
						}break;
					case LoadItem.ResultType.None:
						{
							this.status_text.SetText("結果:なし");
						}break;
					case LoadItem.ResultType.SaveEnd:
						{
							this.status_text.SetText("結果:セーブ完了");
						}break;
					case LoadItem.ResultType.SoundPool:
						{
							this.status_text.SetText("結果:サウンドプール取得");
						}break;
					case LoadItem.ResultType.Text:
						{
							this.status_text.SetText("結果:テキスト取得 : " + this.loaditem.GetResultText());
						}break;
					case LoadItem.ResultType.Binary:
						{
							this.status_text.SetText("結果:バイナリ取得 : size = " + this.loaditem.GetResultBinary().Length.ToString());
						}break;
					case LoadItem.ResultType.Texture:
						{
							this.result_sprite.SetVisible(true);
							this.result_sprite.SetTexture(this.loaditem.GetResultTexture());

							this.status_text.SetText("結果:テクスチャー取得");
						}break;
					case LoadItem.ResultType.Anything:
						{
							object t_asset = this.loaditem.GetResultAnything();
							if(t_asset is Texture2D){
								this.result_sprite.SetVisible(true);
								this.result_sprite.SetTexture(t_asset as Texture2D);

								this.status_text.SetText("結果:テクスチャー取得");
							}else{
								this.status_text.SetText("結果:不明アッセット取得");
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

