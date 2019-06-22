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

		/** Step
		*/
		private enum Step
		{
			Init,
			LoadJson,
			
			LoadRequest,
			LoadWait,
			LoadEnd,
		};
		private Step step;

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
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.CreateInstance();

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			//Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//データ。インスタンス作成。
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

			//step
			this.step = Step.Init;

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//text
			this.text = new Fee.Render2D.Text2D(this.deleter,0);
			this.text.SetRect(50,50,0,0);

			//sprite
			this.sprite = new Fee.Render2D.Sprite2D(this.deleter,0);
			this.sprite.SetRect(50,100,100,100);
			this.sprite.SetTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_MAX);
			this.sprite.SetTexture(Texture2D.whiteTexture);
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



			switch(this.step){
			case Step.Init:
				{
					this.step = Step.LoadJson;
				}break;
			case Step.LoadJson:
				{
					#if(UNITY_EDITOR)
					UnityEngine.TextAsset t_textasset = UnityEngine.Resources.Load<UnityEngine.TextAsset>("Editor/Test12/editor_data");
					#else
					UnityEngine.TextAsset t_textasset = UnityEngine.Resources.Load<UnityEngine.TextAsset>("Test12/release_data");
					#endif

					if(t_textasset != null){
						string t_text = t_textasset.text;
						if(t_text != null){
							System.Collections.Generic.Dictionary<string,Fee.Data.ListItem> t_data_list = Fee.JsonItem.Convert.JsonStringToObject<System.Collections.Generic.Dictionary<string,Fee.Data.ListItem>>(t_text);
							foreach(System.Collections.Generic.KeyValuePair<string,Fee.Data.ListItem> t_pair in t_data_list){
								Fee.Data.Data.GetInstance().RegisterResourcesItem(t_pair.Key,t_pair.Value.path_type,t_pair.Value.path);
							}
						}
					}

					this.step = Step.LoadRequest;
				}break;
			case Step.LoadRequest:
				{
					this.item = Fee.Data.Data.GetInstance().RequestNormal("SKYIMAGE");//TODO:
					this.step = Step.LoadWait;
				}break;
			case Step.LoadWait:
				{
					if(this.item.IsBusy() == false){
						if(this.item.GetResultAssetType() == Fee.Asset.AssetType.Texture){
							this.sprite.SetTexture(this.item.GetResultAssetTexture());
						}
						this.item = null;
						this.step = Step.LoadEnd;
					}
				}break;
			case Step.LoadEnd:
				{
				}break;
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

