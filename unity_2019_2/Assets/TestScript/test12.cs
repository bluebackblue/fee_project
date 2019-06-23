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
	public class test12 : MainBase
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

		/** ClickId
		*/
		private enum ClickId
		{
			/** リソース。プレハブ。
			*/
			Resources_Prefab,

			/** リソース。テクスチャー。
			*/
			Resources_Texture,

			/** リソース。テキスト。
			*/
			Resources_Text,

			/** ストリーミングアセット。テクスチャー。
			*/
			StreamingAssets_Texture,

			/** ストリーミングアセット。テキスト。
			*/
			StreamingAssets_Text,

			/** ストリーミングアセット。バイナリー。
			*/
			StreamingAssets_Binary,

			/** ＵＲＬ。テクスチャー。
			*/
			Url_Texture,

			/** ＵＲＬ。テキスト。
			*/
			Url_Text,

			/** ＵＲＬ。バイナリー。
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
			public Scroll_Item(Fee.Deleter.Deleter a_deleter,Fee.Ui.Button_Base.CallBack_Click a_callback_click,ClickId a_callback_click_id)
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
			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
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
				this.scroll = new Fee.Ui.Scroll<Scroll_Item>(this.deleter,t_drawpriority,Fee.Ui.ScrollType.Vertical,30);
				this.scroll.SetRect(50,50,350,350);
				
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,ClickId.Resources_Prefab));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,ClickId.Resources_Texture));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,ClickId.Resources_Text));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,ClickId.StreamingAssets_Texture));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,ClickId.StreamingAssets_Text));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,ClickId.StreamingAssets_Binary));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,ClickId.Url_Texture));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,ClickId.Url_Text));
				this.scroll.PushItem(new Scroll_Item(this.deleter,this.CallBack_Click,ClickId.Url_Binary));
			}

			//item
			this.item = null;

			//text
			this.text = new Fee.Render2D.Text2D(this.deleter,0);
			this.text.SetRect(50,380,0,0);

			//sprite
			this.sprite = new Fee.Render2D.Sprite2D(this.deleter,0);
			this.sprite.SetRect(50,400,100,100);
			this.sprite.SetTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_MAX);
			this.sprite.SetTexture(Texture2D.whiteTexture);
			this.sprite.SetVisible(false);

			{
				#if(UNITY_EDITOR)
				UnityEngine.TextAsset t_textasset = UnityEngine.Resources.Load<UnityEngine.TextAsset>("Editor/Test12/debug_data");
				#else
				UnityEngine.TextAsset t_textasset = UnityEngine.Resources.Load<UnityEngine.TextAsset>("Test12/release_data");
				#endif

				if(t_textasset != null){
					string t_text = t_textasset.text;
					if(t_text != null){
						System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem> t_data_list = Fee.JsonItem.Convert.JsonStringToObject<System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem>>(t_text);
						foreach(System.Collections.Generic.KeyValuePair<string,Fee.Data.JsonListItem> t_pair in t_data_list){
							Fee.Data.Data.GetInstance().RegisterResourcesItem(t_pair.Key,t_pair.Value.path_type,new Fee.File.Path(t_pair.Value.path));
						}
					}
				}
			}
		}

		/** [Button_Base]コールバック。クリック。
		*/
		public void CallBack_Click(int a_id)
		{
			if(this.item == null){
				this.sprite.SetVisible(false);
				this.text.SetText("");

				switch((ClickId)a_id){
				case ClickId.Resources_Prefab:
					{
						//リソース。プレハブ。
						this.item = Fee.Data.Data.GetInstance().RequestNormal("RESOURCES_PREFAB");
					}break;
				case ClickId.Resources_Texture:
					{
						//リソース。テクスチャー。
						this.item = Fee.Data.Data.GetInstance().RequestNormal("RESOURCES_TEXTURE");
					}break;
				case ClickId.Resources_Text:
					{
						//リソース。テキスト。
						this.item = Fee.Data.Data.GetInstance().RequestNormal("RESOURCES_TEXT");
					}break;
				case ClickId.StreamingAssets_Texture:
					{
						//ストリーミングアセット。テクスチャー。
						this.item = Fee.Data.Data.GetInstance().RequestNormal("STREAMINGASSETS_TEXTURE");
					}break;
				case ClickId.StreamingAssets_Text:
					{
						//ストリーミングアセット。テキスト。
						this.item = Fee.Data.Data.GetInstance().RequestNormal("STREAMINGASSETS_TEXT");
					}break;
				case ClickId.StreamingAssets_Binary:
					{
						//ストリーミングアセット。バイナリー。
						this.item = Fee.Data.Data.GetInstance().RequestNormal("STREAMINGASSETS_BINARY");
					}break;
				case ClickId.Url_Texture:
					{
						//ＵＲＬ。テクスチャー。
						this.item = Fee.Data.Data.GetInstance().RequestNormal("URL_TEXTURE");
					}break;
				case ClickId.Url_Text:
					{
						//ＵＲＬ。テキスト。
						this.item = Fee.Data.Data.GetInstance().RequestNormal("URL_TEXT");
					}break;
				case ClickId.Url_Binary:
					{
						//ＵＲＬ。バイナリー。
						this.item = Fee.Data.Data.GetInstance().RequestNormal("URL_BINARY");
					}break;
				}
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

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
	}
}

