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
	/** test12

		エクセル
		データ
		アセットバンドル

	*/
	public class test12 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test12.ButtonId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test12",
				"test12",

				@"
				エクセル
				データ
				アセットバンドル
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
			/** アセットバンドルを使用しない。
			*/
			Load_Data_Debug,

			/** アセットバンドルを使用する。
			*/
			Load_Data_Release,




			/** ロード。ダミーアセットバンドル。
			*/
			Load_DummyAssetBundle,

			/** ロード。ストリーミングアセットセットバンドル。
			*/
			Load_StreamingAssetsAssetBundle,

			/** ロード。ＵＲＬアセットバンドル。
			*/
			Load_UrlAssetBundle,

			/** アンロード。アセットバンドル。
			*/
			UnLoad_AssetBundle,




			/** リソース。プレハブ。
			*/
			AssetBundle_Prefab,

			/** リソース。テクスチャ。
			*/
			AssetBundle_Texture,

			/** リソース。テキスト。
			*/
			AssetBundle_Text,




			/** リソース。プレハブ。
			*/
			Resources_Prefab,

			/** リソース。テクスチャ。
			*/
			Resources_Texture,

			/** リソース。テキスト。
			*/
			Resources_Text,




			/** ストリーミングアセット。テクスチャ。
			*/
			StreamingAssets_Texture,

			/** ストリーミングアセット。テキスト。
			*/
			StreamingAssets_Text,

			/** ストリーミングアセット。バイナリ。
			*/
			StreamingAssets_Binary,

			/** ＵＲＬ。テクスチャ。
			*/
			Url_Texture,

			/** ＵＲＬ。テキスト。
			*/
			Url_Text,

			/** ＵＲＬ。バイナリ。
			*/
			Url_Binary,
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
			public Scroll_Item(Common.PrefabList a_prefablist,Fee.Deleter.Deleter a_deleter,test12 a_this,ButtonId a_click_id)
			{
				this.button = a_prefablist.CreateButton(a_deleter,1);
				this.button.SetOnButtonClick(a_this,a_click_id);
				this.button.SetClip(true);
				this.button.SetDragCancelFlag(true);
				this.button.SetText(a_click_id.ToString());
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
				this.button.SetWH(a_parent_w,a_parent_h);
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

		/** scroll
		*/
		private Fee.Ui.Scroll<Scroll_Item> scroll;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** sprite
		*/
		private Fee.Render2D.Sprite2D sprite;

		/** item
		*/
		private Fee.Data.Item item_file;

		/** item_assetbundle
		*/
		private Fee.AssetBundleList.Item item_assetbundlelist;

		/** Start
		*/
		private void Start()
		{
			//プラットフォーム。インスタンス作成。
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
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.CreateInstance();

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			//Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//データ。インスタンス作成。
			Fee.Data.Config.LOG_ENABLE = true;
			Fee.Data.Data.CreateInstance();

			//アセットバンドルリスト。インスタンス作成。
			Fee.AssetBundleList.Config.LOG_ENABLE = true;
			Fee.AssetBundleList.AssetBundleList.CreateInstance();

			//ファイル。インスタンス作成。
			Fee.File.File.CreateInstance();

			//プレハブリスト。
			{
				this.prefablist = new Common.PrefabList();
				this.prefablist.LoadFontList();
				this.prefablist.LoadTextureList();
				this.prefablist.LoadTextAssetList();
			}

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont("FONT"));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//drawpriority
			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;

			//scroll_item
			{
				this.scroll = Fee.Ui.Scroll<Scroll_Item>.Create(this.deleter,t_drawpriority,Fee.Ui.Scroll_Type.Vertical,30);
				this.scroll.SetRect(50,50,350,350);
				this.scroll.SetBarDrawPriorityOffset(100);
				
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.Load_Data_Debug));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.Load_Data_Release));

				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.Load_DummyAssetBundle));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.Load_StreamingAssetsAssetBundle));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.Load_UrlAssetBundle));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.UnLoad_AssetBundle));

				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.AssetBundle_Prefab));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.AssetBundle_Texture));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.AssetBundle_Text));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.Resources_Prefab));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.Resources_Texture));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.Resources_Text));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.StreamingAssets_Texture));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.StreamingAssets_Text));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.StreamingAssets_Binary));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.Url_Texture));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.Url_Text));
				this.scroll.PushItem(new Scroll_Item(this.prefablist,this.deleter,this,ButtonId.Url_Binary));
			}

			//item
			this.item_file = null;
			this.item_assetbundlelist = null;

			//text
			this.text = this.prefablist.CreateText(this.deleter,0);
			this.text.SetRect(50,420,0,0);

			//sprite
			this.sprite = Fee.Render2D.Sprite2D.Create(this.deleter,0);
			this.sprite.SetRect(50,400,100,100);
			this.sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
			this.sprite.SetTexture(Texture2D.whiteTexture);
			this.sprite.SetVisible(false);

			//パブリックキー。
			Fee.File.File.GetInstance().RegistCertificate("blueback","^https\\:\\/\\/blueback\\.ddns\\.net\\:8081\\/.*$",this.prefablist.GetTextAsset("SSLPUBLICKEY").text);
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			if((this.item_file == null)&&(this.item_assetbundlelist == null)){
				this.sprite.SetVisible(false);
				this.text.SetText("");

				switch(a_id){
				case ButtonId.Load_Data_Debug:
					{
						//アセットバンドルを使用しないデータリスト。

						Fee.Data.Data.GetInstance().ClearData();
						
						UnityEngine.TextAsset t_textasset = this.prefablist.GetTextAsset("TEST12_DATA_DEBUG");

						if(t_textasset != null){
							string t_text = t_textasset.text;
							if(t_text != null){
								Fee.Data.Data.GetInstance().RegistDataJson(t_text);
							}
						}
					}break;
				case ButtonId.Load_Data_Release:
					{
						//アセットバンドルを使用するデータリスト。

						Fee.Data.Data.GetInstance().ClearData();
						
						UnityEngine.TextAsset t_textasset = this.prefablist.GetTextAsset("TEST12_DATA_RELEASE");

						if(t_textasset != null){
							string t_text = t_textasset.text;
							if(t_text != null){
								Fee.Data.Data.GetInstance().RegistDataJson(t_text);
							}
						}
					}break;
				case ButtonId.Load_DummyAssetBundle:
					{
						//ダミーアセットバンドル。

						#if(UNITY_EDITOR)
						{
							Fee.AssetBundleList.AssetBundleList.GetInstance().UnRegistPathItem("test.assetbundle");
							Fee.AssetBundleList.AssetBundleList.GetInstance().RegistPathItem("test.assetbundle",Fee.AssetBundleList.AssetBundlePathType.AssetsPathDummyAssetBundle,new Fee.File.Path(Data.Assets.TEST12_ASSETBUNDLE));

							this.item_assetbundlelist = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoadPathItemAssetBundleItem("test.assetbundle");
						}
						#endif
					}break;
				case ButtonId.Load_StreamingAssetsAssetBundle:
					{
						//ストリーミングアセットにあるアセットバンドル。

						//アセットバンドルのパスを設定。
						Fee.AssetBundleList.AssetBundleList.GetInstance().UnRegistPathItem("test.assetbundle");
						Fee.AssetBundleList.AssetBundleList.GetInstance().RegistPathItem("test.assetbundle",Fee.AssetBundleList.AssetBundlePathType.StreamingAssetsAssetBundle,new Fee.File.Path(Data.StreamingAssets.TEST12_ASSETBUNDLE));

						this.item_assetbundlelist = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoadPathItemAssetBundleItem("test.assetbundle");
					}break;
				case ButtonId.Load_UrlAssetBundle:
					{
						//ＵＲＬにあるアセットバンドル。

						//アセットバンドルのパスを設定。
						Fee.AssetBundleList.AssetBundleList.GetInstance().UnRegistPathItem("test.assetbundle");
						Fee.AssetBundleList.AssetBundleList.GetInstance().RegistPathItem("test.assetbundle",Fee.AssetBundleList.AssetBundlePathType.UrlAssetBundle,new Fee.File.Path(Data.Url.TEST12_ASSETBUNDLE));

						this.item_assetbundlelist = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoadPathItemAssetBundleItem("test.assetbundle");
					}break;
				case ButtonId.UnLoad_AssetBundle:
					{
						this.item_assetbundlelist = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestUnLoadAssetBundleItem("test.assetbundle");
					}break;
				case ButtonId.AssetBundle_Prefab:
					{
						//アセットバンドル。プレハブ。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("ASSETBUNDLE_PREFAB");
					}break;
				case ButtonId.AssetBundle_Texture:
					{
						//アセットバンドル。テクスチャ。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("ASSETBUNDLE_TEXTURE");
					}break;
				case ButtonId.AssetBundle_Text:
					{
						//アセットバンドル。テキスト。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("ASSETBUNDLE_TEXT");
					}break;
				case ButtonId.Resources_Prefab:
					{
						//リソース。プレハブ。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("RESOURCES_PREFAB");
					}break;
				case ButtonId.Resources_Texture:
					{
						//リソース。テクスチャ。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("RESOURCES_TEXTURE");
					}break;
				case ButtonId.Resources_Text:
					{
						//リソース。テキスト。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("RESOURCES_TEXT");
					}break;
				case ButtonId.StreamingAssets_Texture:
					{
						//ストリーミングアセット。テクスチャ。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("STREAMINGASSETS_TEXTURE");
					}break;
				case ButtonId.StreamingAssets_Text:
					{
						//ストリーミングアセット。テキスト。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("STREAMINGASSETS_TEXT");
					}break;
				case ButtonId.StreamingAssets_Binary:
					{
						//ストリーミングアセット。バイナリ。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("STREAMINGASSETS_BINARY");
					}break;
				case ButtonId.Url_Texture:
					{
						//ＵＲＬ。テクスチャ。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("URL_TEXTURE");
					}break;
				case ButtonId.Url_Text:
					{
						//ＵＲＬ。テキスト。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("URL_TEXT");
					}break;
				case ButtonId.Url_Binary:
					{
						//ＵＲＬ。バイナリ。
						this.item_file = Fee.Data.Data.GetInstance().RequestLoad("URL_BINARY");
					}break;
				}
			}
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

			//データ。
			Fee.Data.Data.GetInstance().Main();

			//アセットバンドルリスト。
			Fee.AssetBundleList.AssetBundleList.GetInstance().Main();

			//ファイル。
			Fee.File.File.GetInstance().Main();

			//scroll
			this.scroll.DragScrollUpdate(0.98f);

			if(this.item_file != null){
				if(this.item_file.IsBusy() == true){
					//処理中。
				}else{
					if(this.item_file.GetResultType() == Fee.Data.Item.ResultType.Error){
						//エラー。

						this.text.SetText(this.item_file.GetResultErrorString());
					}else{
						switch(this.item_file.GetResultAssetType()){
						case Fee.Asset.AssetType.Binary:
							{
								UnityEngine.Debug.Log("Binary");

								this.text.SetText("byte = " + this.item_file.GetResultAssetBinary().Length.ToString());
							}break;
						case Fee.Asset.AssetType.Prefab:
							{
								UnityEngine.Debug.Log("Prefab");

								UnityEngine.GameObject t_prefab = this.item_file.GetResultAssetPrefab();

								this.text.SetText("prefab = " + t_prefab.name);

							}break;
						case Fee.Asset.AssetType.Text:
							{
								UnityEngine.Debug.Log("Text");

								this.text.SetText(this.item_file.GetResultAssetText());
							}break;
						case Fee.Asset.AssetType.Texture:
							{
								UnityEngine.Debug.Log("Texture");

								this.sprite.SetTexture(this.item_file.GetResultAssetTexture());
								this.sprite.SetVisible(true);
							}break;
						}
					}

					this.item_file = null;
				}
			}

			if(this.item_assetbundlelist != null){
				if(this.item_assetbundlelist.IsBusy() == true){
					//処理中。
				}else{
					if(this.item_assetbundlelist.GetResultType() == Fee.AssetBundleList.Item.ResultType.Error){
						//エラー。

						this.text.SetText(this.item_assetbundlelist.GetResultErrorString());
					}else{
						switch(this.item_assetbundlelist.GetResultType()){
						case Fee.AssetBundleList.Item.ResultType.LoadAssetBundleItem:
							{
								if(this.item_assetbundlelist.GetResultAssetBundleItem().assetbundle_dummy != null){
									this.text.SetText("load dummy assetbundle");
								}else if(this.item_assetbundlelist.GetResultAssetBundleItem().assetbundle_raw != null){
									this.text.SetText("load asetbundle");
								}
							}break;
						case Fee.AssetBundleList.Item.ResultType.UnLoadAssetBundleItem:
							{
								this.text.SetText("unload assetbundle");
							}break;
						}
					}

					this.item_assetbundlelist = null;
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

