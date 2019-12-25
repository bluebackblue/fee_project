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
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** ButtonId
		*/
		public enum ButtonId
		{
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
			public Scroll_Item(Fee.Deleter.Deleter a_deleter,test12 a_this,ButtonId a_click_id)
			{
				this.button = new Fee.Ui.Button(a_deleter,1);
				this.button.SetOnButtonClick(a_this,a_click_id);
				this.button.SetClip(true);
				this.button.SetDragCancelFlag(true);
				this.button.SetText(a_click_id.ToString());
				this.button.SetTextureCornerSize(10);
				this.button.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.button.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.button.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.button.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.button.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
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
			public override void SetClipRect(in Fee.Geometry.Rect2D_R<int> a_rect)
			{
				this.button.SetClipRect(in a_rect);
			}

			/** [Fee.Ui.ScrollItem_Base]描画プライオリティ。設定。
			*/
			public override void SetDrawPriority(long a_drawpriority)
			{
				this.button.SetDrawPriority(a_drawpriority + 1);
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
		private Fee.Data.Item item;

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

			//ファイル。インスタンス作成。
			Fee.File.File.CreateInstance();

			//フォント。
			Font t_font = Resources.Load<Font>(Data.Resources.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//drawpriority
			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;

			//scroll_item
			{
				this.scroll = new Fee.Ui.Scroll<Scroll_Item>(this.deleter,t_drawpriority,Fee.Ui.Scroll_Type.Vertical,30);
				this.scroll.SetRect(50,50,350,350);
				
				this.scroll.PushItem(new Scroll_Item(this.deleter,this,ButtonId.Resources_Prefab));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this,ButtonId.Resources_Texture));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this,ButtonId.Resources_Text));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this,ButtonId.StreamingAssets_Texture));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this,ButtonId.StreamingAssets_Text));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this,ButtonId.StreamingAssets_Binary));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this,ButtonId.Url_Texture));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this,ButtonId.Url_Text));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this,ButtonId.Url_Binary));
			}

			//item
			this.item = null;

			//text
			this.text = Fee.Render2D.Text2D.Create(this.deleter,0);
			this.text.SetRect(50,380,0,0);

			//sprite
			this.sprite = Fee.Render2D.Sprite2D.Create(this.deleter,0);
			this.sprite.SetRect(50,400,100,100);
			this.sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
			this.sprite.SetTexture(Texture2D.whiteTexture);
			this.sprite.SetVisible(false);

			//データリスト。
			{
				#if(UNITY_EDITOR)
				//リソース強制使用。
				UnityEngine.TextAsset t_textasset = UnityEngine.Resources.Load<UnityEngine.TextAsset>("Editor/Test12/debug_data");
				#else
				//アセットバンドル使用。
				UnityEngine.TextAsset t_textasset = UnityEngine.Resources.Load<UnityEngine.TextAsset>("Test12/release_data");
				#endif

				if(t_textasset != null){
					string t_text = t_textasset.text;
					if(t_text != null){
						System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem> t_data_list = Fee.JsonItem.Convert.JsonStringToObject<System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem>>(t_text);
						Fee.Data.Data.GetInstance().RegistDataList(t_data_list);
					}
				}
			}
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			if(this.item == null){
				this.sprite.SetVisible(false);
				this.text.SetText("");

				switch(a_id){
				case ButtonId.Resources_Prefab:
					{
						//リソース。プレハブ。
						this.item = Fee.Data.Data.GetInstance().RequestLoad("RESOURCES_PREFAB");
					}break;
				case ButtonId.Resources_Texture:
					{
						//リソース。テクスチャ。
						this.item = Fee.Data.Data.GetInstance().RequestLoad("RESOURCES_TEXTURE");
					}break;
				case ButtonId.Resources_Text:
					{
						//リソース。テキスト。
						this.item = Fee.Data.Data.GetInstance().RequestLoad("RESOURCES_TEXT");
					}break;
				case ButtonId.StreamingAssets_Texture:
					{
						//ストリーミングアセット。テクスチャ。
						this.item = Fee.Data.Data.GetInstance().RequestLoad("STREAMINGASSETS_TEXTURE");
					}break;
				case ButtonId.StreamingAssets_Text:
					{
						//ストリーミングアセット。テキスト。
						this.item = Fee.Data.Data.GetInstance().RequestLoad("STREAMINGASSETS_TEXT");
					}break;
				case ButtonId.StreamingAssets_Binary:
					{
						//ストリーミングアセット。バイナリ。
						this.item = Fee.Data.Data.GetInstance().RequestLoad("STREAMINGASSETS_BINARY");
					}break;
				case ButtonId.Url_Texture:
					{
						//ＵＲＬ。テクスチャ。
						this.item = Fee.Data.Data.GetInstance().RequestLoad("URL_TEXTURE");
					}break;
				case ButtonId.Url_Text:
					{
						//ＵＲＬ。テキスト。
						this.item = Fee.Data.Data.GetInstance().RequestLoad("URL_TEXT");
					}break;
				case ButtonId.Url_Binary:
					{
						//ＵＲＬ。バイナリ。
						this.item = Fee.Data.Data.GetInstance().RequestLoad("URL_BINARY");
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

			//ファイル。
			Fee.File.File.GetInstance().Main();

			if(this.item != null){
				if(this.item.IsBusy() == true){
					//処理中。
				}else{
					switch(this.item.GetResultAssetType()){
					case Fee.Asset.AssetType.Binary:
						{
							UnityEngine.Debug.Log("Binary");

							this.text.SetText("byte = " + this.item.GetResultAssetBinary().Length.ToString());
						}break;
					case Fee.Asset.AssetType.Prefab:
						{
							UnityEngine.Debug.Log("Prefab");

							UnityEngine.GameObject t_prefab = this.item.GetResultAssetPrefab();

							this.text.SetText("prefab = " + t_prefab.name);

						}break;
					case Fee.Asset.AssetType.Text:
						{
							UnityEngine.Debug.Log("Text");

							this.text.SetText(this.item.GetResultAssetText());
						}break;
					case Fee.Asset.AssetType.Texture:
						{
							UnityEngine.Debug.Log("Texture");

							this.sprite.SetTexture(this.item.GetResultAssetTexture());
							this.sprite.SetVisible(true);
						}break;
					}

					this.item = null;
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

		/** シーンリスト初期化。
		*/
		#if(UNITY_EDITOR)
		[UnityEditor.MenuItem("Fee/Test/Test12/ConvertFromExcel")]
		private static void MenuItem_ConvertFromExcel()
		{
			//エクセルからＪＳＯＮシートを作成。
			Fee.Excel.ExcelToJsonSheet t_excel_to_jsonsheet = new Fee.Excel.ExcelToJsonSheet();
			if(t_excel_to_jsonsheet.Convert(Fee.File.Path.CreateAssetsPath(Data.Assets.EXCEL)) == true){
				Fee.JsonItem.JsonItem t_jsonsheet = t_excel_to_jsonsheet.GetJsonSheet();
				if(t_jsonsheet != null){
					//コンバート。
					if(Fee.JsonSheet.JsonSheet.ConvertFromJsonSheet(t_jsonsheet) == false){
						UnityEngine.Debug.LogError("faild");
					}
				}else{
					UnityEngine.Debug.LogError("faild");
				}
			}else{
				UnityEngine.Debug.LogError("faild");
			}
		}
		#endif

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

