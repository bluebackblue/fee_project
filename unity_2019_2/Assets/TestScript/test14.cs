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

			LoadAssetBundle_Start,
			LoadAssetBundle_Wait,
			LoadAssetBundle_End,

			Load_Text_Start,
			Load_Text_Wait,
			Load_Text_End,

			Load_Prefab_Start,
			Load_Prefab_Wait,
			Load_Prefab_End,

			Load_Texture_Start,
			Load_Texture_Wait,
			Load_Texture_End,

			Fix,
		}
		private Step step;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** sprite
		*/
		private Fee.Render2D.Sprite2D sprite;

		/** assetbundlelist_item
		*/
		private Fee.AssetBundleList.Item assetbundlelist_item;

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
			this.text.SetRect(50,100,0,0);

			//sprite
			this.sprite = new Fee.Render2D.Sprite2D(this.deleter,0);
			this.sprite.SetRect(50,150,100,100);
			this.sprite.SetTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_MAX);

			//assetbundlelist_item
			this.assetbundlelist_item = null;

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
					
					//データリスト。
					{
						UnityEngine.TextAsset t_textasset = UnityEngine.Resources.Load<UnityEngine.TextAsset>("Test14/data");
						if(t_textasset != null){
							string t_text = t_textasset.text;
							if(t_text != null){
								System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem> t_data_list = Fee.JsonItem.Convert.JsonStringToObject<System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem>>(t_text);
								Fee.Data.Data.GetInstance().RegisterDataList(t_data_list);
							}
						}
					}

					//パスの登録。
					#if(UNITY_EDITOR)
					{
						#if(true)
						//ダミーアセットバンドル。
						Fee.AssetBundleList.AssetBundleList.GetInstance().RegisterPath("test.assetbundle",Fee.AssetBundleList.AssetBundlePathList_PathType.AssetsDummyAssetBundle,new Fee.File.Path("Editor/AssetBundle/Dummy/test.assetbundle.json"));
						#else
						//アセットバンドル。
						Fee.AssetBundleList.AssetBundleList.GetInstance().RegisterPath("test.assetbundle",Fee.AssetBundleList.AssetBundlePathList_PathType.AssetsAssetBundle,new Fee.File.Path("Editor/AssetBundle/StandaloneWindows/test.assetbundle"));
						#endif
					}
					#endif

					this.step = Step.LoadAssetBundle_Start;
				}break;
			case Step.LoadAssetBundle_Start:
				{
					//アセットバンドルロード。

					this.assetbundlelist_item = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoadPathAssetBundleItem("test.assetbundle");
					this.step = Step.LoadAssetBundle_Wait;
				}break;
			case Step.LoadAssetBundle_Wait:
				{
					//アセットバンドルロード。

					if(this.assetbundlelist_item.IsBusy() == false){
						this.step = Step.LoadAssetBundle_End;
					}else{
						this.text.SetText("LoadAssetBundle_Wait : " + this.assetbundlelist_item.GetResultProgress().ToString());
					}
				}break;
			case Step.LoadAssetBundle_End:
				{
					//アセットバンドルロード。

					this.step = Step.Load_Text_Start;
				}break;
			case Step.Load_Text_Start:
				{
					//ロード。テキスト。

					this.data_item = Fee.Data.Data.GetInstance().RequestLoad("RESOURCES_TEXT");
					this.step = Step.Load_Text_Wait;
				}break;
			case Step.Load_Text_Wait:
				{
					//ロード。テキスト。

					if(this.data_item.IsBusy() == false){
						this.step = Step.Load_Text_End;
					}else{
						this.text.SetText("Load_Text_Wait : "  + this.data_item.GetResultProgress().ToString());
					}
				}break;
			case Step.Load_Text_End:
				{
					//ロード。テキスト。

					if(this.data_item.GetResultAssetType() == Fee.Asset.AssetType.Text){
						this.text.SetText("Load_Text_End : " + this.data_item.GetResultAssetText());
					}else{
						this.text.SetText("Load_Text_End : Error");
					}

					this.step = Step.Load_Prefab_Start;
				}break;
			case Step.Load_Prefab_Start:
				{
					//ロード。プレハブ。

					this.data_item = Fee.Data.Data.GetInstance().RequestLoad("RESOURCES_PREFAB");
					this.step = Step.Load_Prefab_Wait;
				}break;
			case Step.Load_Prefab_Wait:
				{
					//ロード。プレハブ。

					if(this.data_item.IsBusy() == false){
						this.step = Step.Load_Prefab_End;
					}else{
						this.text.SetText("Load_Prefab_Wait : "  + this.data_item.GetResultProgress().ToString());
					}
				}break;
			case Step.Load_Prefab_End:
				{
					//ロード。プレハブ。

					if(this.data_item.GetResultAssetType() == Fee.Asset.AssetType.Prefab){
						UnityEngine.GameObject t_prefab = this.data_item.GetResultAssetPrefab();

						string t_component_name = "";
						{
							UnityEngine.Component[] t_component_list = t_prefab.GetComponents(typeof(UnityEngine.MonoBehaviour));
							if(t_component_list != null){
								if(t_component_list.Length > 0){
									if(t_component_list[0] != null){
										t_component_name = t_component_list[0].GetType().ToString();
									}
								}
							}
						}

						this.text.SetText("Load_Prefab_End : " + t_component_name);
					}else{
						this.text.SetText("Load_Prefab_End : Error");
					}

					this.step = Step.Load_Texture_Start;
				}break;
			case Step.Load_Texture_Start:
				{
					//ロード。テクスチャ。

					this.data_item = Fee.Data.Data.GetInstance().RequestLoad("RESOURCES_TEXTURE");
					this.step = Step.Load_Texture_Wait;
				}break;
			case Step.Load_Texture_Wait:
				{
					//ロード。テクスチャ。

					if(this.data_item.IsBusy() == false){
						this.step = Step.Load_Texture_End;
					}else{
						this.text.SetText("Load_Texture_Wait : "  + this.data_item.GetResultProgress().ToString());
					}
				}break;
			case Step.Load_Texture_End:
				{
					//ロード。テクスチャ。

					if(this.data_item.GetResultAssetType() == Fee.Asset.AssetType.Texture){
						this.sprite.SetTexture(this.data_item.GetResultAssetTexture());

						this.text.SetText("Load_TextureEnd");
					}else{
						this.text.SetText("Load_Texture_End : Error");
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

