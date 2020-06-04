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

		テキスト
			セーブローカル
			ロードＵＲＬ
			ロードローカル
			ロードストリーミングアセット
			ロードリソース

		バイナリ
			セーブローカル
			ロードＵＲＬ
			ロードローカル
			ロードストリーミングアセット

		テクスチャ
			セーブローカル
			ロードＵＲＬ
			ロードローカル
			ロードストリーミングアセット
			ロードリソース

	*/
	public class test04 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test04.ButtonId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test04",
				"test04",

				@"
				ロードＵＲＬ
				セーブローカル
				ロードローカル
				ロードストリーミングアセット
				ロードリソース
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** ButtonId
		*/
		public enum ButtonId
		{
			/** TextFile
			*/
			SaveLocal_TextFile,
			LoadUrl_TextFile,
			LoadLocal_TextFile,
			LoadStreamingAssets_TextFile,
			LoadResources_TextFile,

			/** BinaryFile
			*/
			SaveLocal_BinaryFile,
			LoadUrl_BinaryFile,
			LoadLocal_BinaryFile,
			LoadStreamingAssets_BinaryFile,

			/** TextureFile
			*/
			SaveLocal_TextureFile,
			LoadUrl_TextureFile,
			LoadLocal_TextureFile,
			LoadStreamingAssets_TextureFile,
			LoadResources_TextureFile,
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
			}

			/** constructor
			*/
			public LoadItem(Fee.File.Item a_item)
			{
				this.item_file = a_item;
			}

			public LoadItem(Fee.Data.Item a_item)
			{
				this.item_file = null;
			}

			/** IsBusy
			*/
			public bool IsBusy()
			{
				if(this.item_file != null){
					return this.item_file.IsBusy();
				}
				return false;
			}

			/** GetResultProgress
			*/
			public float GetResultProgress()
			{
				if(this.item_file != null){
					return this.item_file.GetResultProgress();
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
				return "";
			}

			/** GetResultText
			*/
			public string GetResultText()
			{
				if(this.item_file != null){
					return this.item_file.GetResultAssetText();
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
				return null;
			}

			/** GetResultBinary
			*/
			public byte[] GetResultBinary()
			{
				if(this.item_file != null){
					return this.item_file.GetResultAssetBinary();
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
			public Scroll_Item(Common.PrefabList a_prefablist,Fee.Deleter.Deleter a_deleter,test04 a_this,ButtonId a_button_id)
			{
				this.button = a_prefablist.CreateButton(a_deleter,1);
				this.button.SetOnButtonClick(a_this,a_button_id);
				this.button.SetClip(true);
				this.button.SetDragCancelFlag(true);
				this.button.SetText(a_button_id.ToString());
			}

			/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
			*/
			public override void OnDelete()
			{
			}

			/** [Fee.Ui.ScrollItem_Base]矩形変更。
			*/
			public override void OnChangeRectX(int a_x)
			{
				this.button.SetX(a_x);
			}

			/** [Fee.Ui.ScrollItem_Base]矩形変更。
			*/
			public override void OnChangeRectY(int a_y)
			{
				this.button.SetY(a_y);
			}

			/** [Fee.Ui.ScrollItem_Base]矩形変更。
			*/
			public override void OnChangeParentRectWH(int a_parent_w,int a_parent_h)
			{
				this.button.SetWH(a_parent_w - 5,a_parent_h);
			}

			/** [Fee.Ui.ScrollItem_Base]クリップ矩形変更。
			*/
			public override void OnChangeParentClipRect(in Fee.Geometry.Rect2D_R<int> a_parent_rect)
			{
				this.button.SetClipRect(in a_parent_rect);
			}

			/** [Fee.Ui.ScrollItem_Base]描画プライオリティ変更。
			*/
			public override void OnChangeParentDrawPriority(long a_parent_drawpriority)
			{
				this.button.SetDrawPriority(a_parent_drawpriority + 1);
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
			//プレイヤーループシステム。インスタンス作成。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance(null);
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof( UnityEngine.Experimental.PlayerLoop.PreUpdate.SendMouseEvents));

			//プラットフォーム。インスタンス作成。
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

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//ファイル。インスタンス作成。
			Fee.File.Config.LOG_ENABLE = true;
			Fee.File.File.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Input.CreateInstance(true,false,true,false);

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Ui.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//drawpriority
			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Config.DRAWPRIORITY_STEP;

			//status
			this.status_text = this.prefablist.CreateText(this.deleter,t_drawpriority);
			this.status_text.SetRect(50,60,0,0);
			this.status_text.SetFontSize(13);

			//result_sprite
			this.result_sprite = Fee.Render2D.Sprite2D.Create(this.deleter,t_drawpriority);
			this.result_sprite.SetRect(400,30,64,64);
			this.result_sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);

			//progress_sprite
			this.progress_sprite = Fee.Render2D.Sprite2D.Create(this.deleter,t_drawpriority);
			this.progress_sprite.SetTexture(Texture2D.whiteTexture);
			this.progress_sprite.SetRect(50,90,300,10);

			//loaditem
			this.loaditem = null;

			//scroll_item
			{
				this.scroll = Fee.Ui.Scroll<Scroll_Item>.Create(this.deleter,t_drawpriority,Fee.Ui.Scroll_Type.Vertical,30);
				this.scroll.SetRect(50,250,350,300);
				
				//TextFile
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.SaveLocal_TextFile));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.LoadUrl_TextFile));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.LoadLocal_TextFile));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.LoadStreamingAssets_TextFile));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.LoadResources_TextFile));

				//BinaryFile
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.SaveLocal_BinaryFile));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.LoadUrl_BinaryFile));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.LoadLocal_BinaryFile));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.LoadStreamingAssets_BinaryFile));

				//TextureFile
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.SaveLocal_TextureFile));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.LoadUrl_TextureFile));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.LoadLocal_TextureFile));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.LoadStreamingAssets_TextureFile));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.LoadResources_TextureFile));
			}

			//パブリックキー。
			Fee.File.File.GetInstance().RegistCertificate(Common.Data.Url.CERTIFICATE_NAME,Common.Data.Url.CERTIFICATE_PATTERN,Common.Data.Url.CERTIFICATE);
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			this.result_sprite.SetVisible(false);

			switch(a_id){




			case ButtonId.SaveLocal_TextFile:
				{
					//セーブローカル。テキストファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestSaveTextFile(Fee.File.File.SaveRequestType.SaveLocalTextFile,new Fee.File.Path(Common.Data.Local.TEST04_TEXT),"qwerasdfzxcv"));
				}break;
			case ButtonId.LoadUrl_TextFile:
				{
					//ロードＵＲＬ。テキストファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadUrlTextFile,new Fee.File.Path(Common.Data.Url.TEST04_TEXT)));
				}break;
			case ButtonId.LoadLocal_TextFile:
				{
					//ロードローカル。テキストファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad( Fee.File.File.LoadRequestType.LoadLocalTextFile,new Fee.File.Path(Common.Data.Local.TEST04_TEXT)));
				}break;
			case ButtonId.LoadStreamingAssets_TextFile:
				{
					//ロードストリーミングアセット。テキストファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadStreamingAssetsTextFile,new Fee.File.Path(Common.Data.StreamingAssets.TEST04_TEXT)));
				}break;
			case ButtonId.LoadResources_TextFile:
				{
					//ロードリソース。テキストファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadResourcesTextFile,new Fee.File.Path(Common.Data.Resources.TEST04_TEXT)));
				}break;




			case ButtonId.SaveLocal_BinaryFile:
				{
					//セーブローカル。バイナリファイル。

					byte[] t_binary = new byte[16];
					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestSaveBinaryFile(Fee.File.File.SaveRequestType.SaveLocalBinaryFile,new Fee.File.Path(Common.Data.Local.TEST04_BINARY),t_binary));
				}break;
			case ButtonId.LoadUrl_BinaryFile:
				{
					//ロードＵＲＬ。バイナリファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadUrlBinaryFile,new Fee.File.Path(Common.Data.Url.TEST04_BINARY)));
				}break;
			case ButtonId.LoadLocal_BinaryFile:
				{
					//ロードローカル。バイナリファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadLocalBinaryFile,new Fee.File.Path(Common.Data.Local.TEST04_BINARY)));
				}break;
			case ButtonId.LoadStreamingAssets_BinaryFile:
				{
					//ロードストリーミングアセット。バイナリファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadStreamingAssetsBinaryFile,new Fee.File.Path(Common.Data.StreamingAssets.TEST04_BINARY)));
				}break;




			case ButtonId.SaveLocal_TextureFile:
				{
					//セーブローカル。テクスチャファイル。

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

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestSaveTextureFile(Fee.File.File.SaveRequestType.SaveLocalTextureFile,new Fee.File.Path(Common.Data.Local.TEST04_TEXTURE),t_texture));
				}break;
			case ButtonId.LoadUrl_TextureFile:
				{
					//ロードＵＲＬ。テクスチャファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadUrlTextureFile,new Fee.File.Path(Common.Data.Url.TEST04_TEXTURE)));
				}break;
			case ButtonId.LoadLocal_TextureFile:
				{
					//ロードローカル。テクスチャファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadLocalTextureFile,new Fee.File.Path(Common.Data.Local.TEST04_TEXTURE)));
				}break;
			case ButtonId.LoadStreamingAssets_TextureFile:
				{
					//ロードストリーミングアセット。テクスチャファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadStreamingAssetsTextureFile,new Fee.File.Path(Common.Data.StreamingAssets.TEST04_TEXTURE)));
				}break;
			case ButtonId.LoadResources_TextureFile:
				{
					//ロードリソース。テクスチャファイル。

					this.loaditem = new LoadItem(Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadResourcesTextureFile,new Fee.File.Path(Common.Data.Resources.TEST04_TEXTURE)));
				}break;
			}
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
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

							this.status_text.SetText("結果:テクスチャ取得");
						}break;
					}

					this.loaditem = null;
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
			//ドラッグ更新。
			this.scroll.DragScrollUpdate(0.98f,UnityEngine.Time.deltaTime);
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_PreDraw();
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

