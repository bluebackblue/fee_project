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
	/** test14

		アセットバンドル

	*/
	public class test14 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test14",
				"test14",

				@"
				アセットバンドル
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

			Binary_Start,
			Binary_Wait,
			Binary_End,

			Data_Start,
			Data_Wait,
			Data_End,

			Fix,
		}
		private Step step;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** data_item
		*/
		private Fee.Data.Item data_item;

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

			//ファイル。インスタンス作成。
			Fee.File.File.CreateInstance();

			//データ。インスタンス作成。
			Fee.Data.Data.CreateInstance();

			//アセットバンドルリスト。インスタンス作成。
			Fee.AssetBundleList.AssetBundleList.CreateInstance();

			//フォント。
			Font t_font = Resources.Load<Font>(Data.Resources.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//step
			this.step = Step.Init;

			//text
			this.text = new Fee.Render2D.Text2D(this.deleter,0);
			this.text.SetRect(100,100,0,0);

			//data_item
			this.data_item = null;
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

			//ファイル。
			Fee.File.File.GetInstance().Main();

			//データ。
			Fee.Data.Data.GetInstance().Main();

			//アセットバンドルリスト。
			Fee.AssetBundleList.AssetBundleList.GetInstance().Main();

			switch(this.step){
			case Step.Init:
				{
					this.text.SetText("Init");
					
					{
						UnityEngine.TextAsset t_textasset = UnityEngine.Resources.Load<UnityEngine.TextAsset>("Test14/data");
						if(t_textasset != null){
							string t_text = t_textasset.text;
							if(t_text != null){
								System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem> t_data_list = Fee.JsonItem.Convert.JsonStringToObject<System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem>>(t_text);
								foreach(System.Collections.Generic.KeyValuePair<string,Fee.Data.JsonListItem> t_pair in t_data_list){
									Fee.Data.Data.GetInstance().RegisterResourcesItem(t_pair.Key,t_pair.Value.path_type,new Fee.File.Path(t_pair.Value.path),t_pair.Value.assetbundle_name);
								}
							}
						}
					}

					//パスの登録。
					Fee.AssetBundleList.AssetBundleList.GetInstance().AddPath("test.assetbundle", Fee.AssetBundleList.PathType.AssetsAssetBundle,new Fee.File.Path("Editor/AssetBundle/StandaloneWindows/test.assetbundle"));

					this.step = Step.Binary_Start;
				}break;
			case Step.Binary_Start:
				{
					this.text.SetText("Binary_Start");

					//ロードリクエスト。
					Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoad("test.assetbundle");

					this.step = Step.Binary_Wait;
				}break;
			case Step.Binary_Wait:
				{
					if(Fee.AssetBundleList.AssetBundleList.GetInstance().IsBusy() == false){
						this.step = Step.Binary_End;
					}else{
						this.text.SetText("Binary_Wait");
					}
				}break;
			case Step.Binary_End:
				{
					this.text.SetText("Binary_End");

					this.step = Step.Data_Start;
				}break;
			case Step.Data_Start:
				{
					this.text.SetText("Data_Start");

					this.data_item = Fee.Data.Data.GetInstance().RequestFile("RESOURCES_TEXT");

					this.step = Step.Data_Wait;
				}break;
			case Step.Data_Wait:
				{
					this.text.SetText("Data_Wait");

					if(this.data_item.IsBusy() == false){
						this.step = Step.Data_End;
					}
				}break;
			case Step.Data_End:
				{
					if(this.data_item.GetResultAssetType() == Fee.Asset.AssetType.Text){
						this.text.SetText("Data_End : " + this.data_item.GetResultAssetText());
					}else{
						this.text.SetText("Data_End");
					}

					this.step = Step.Fix;
				}break;
			case Step.Fix:
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
	}
}

