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

		/** texturelist
		*/
		private Fee.Instantiate.TextureList texturelist;

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

			//ファイル。インスタンス作成。
			Fee.File.Config.LOG_ENABLE = true;
			Fee.File.File.CreateInstance();

			//データ。インスタンス作成。
			Fee.Data.Data.CreateInstance();

			//アセットバンドルリスト。インスタンス作成。
			Fee.AssetBundleList.AssetBundleList.CreateInstance();

			//フォント。
			{
				UnityEngine.GameObject t_prefab = UnityEngine.Resources.Load<UnityEngine.GameObject>("FontList");
				Fee.Instantiate.FontList t_fontlist = new Fee.Instantiate.FontList(t_prefab.GetComponent<Fee.Instantiate.FontList_MonoBehaviour>());
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_fontlist.GetFont("FONT"));
			}

			//テクスチャーリスト。
			{
				UnityEngine.GameObject t_prefab = UnityEngine.Resources.Load<UnityEngine.GameObject>("TextureList");
				this.texturelist = new Fee.Instantiate.TextureList(t_prefab.GetComponent<Fee.Instantiate.TextureList_MonoBehaviour>());
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,this.texturelist.GetTexture("UI_BUTTON"),(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//step
			this.step = Step.Init;

			//text
			this.text = Fee.Render2D.Text2D.Create(this.deleter,0);
			this.text.SetRect(50,100,0,0);

			//sprite
			this.sprite = Fee.Render2D.Sprite2D.Create(this.deleter,0);
			this.sprite.SetRect(50,150,100,100);
			this.sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);

			//assetbundlelist_item
			this.assetbundlelist_item = null;

			//data_item
			this.data_item = null;
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
						UnityEngine.TextAsset t_textasset = UnityEngine.Resources.Load<UnityEngine.TextAsset>(Data.Resources.TEST14_DATA);
						if(t_textasset != null){
							string t_text = t_textasset.text;
							if(t_text != null){
								System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem> t_data_list = Fee.JsonItem.Convert.JsonStringToObject<System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem>>(t_text);
								Fee.Data.Data.GetInstance().RegistDataList(t_data_list);
							}
						}
					}

					//アセットバンドルリスト。
					{
						#if(UNITY_EDITOR)
						{
							#if(UNITY_EDITOR) && false
							{
								//ダミーアセットバンドル。
								Fee.AssetBundleList.AssetBundleList.GetInstance().RegistPathItem("test.assetbundle",Fee.AssetBundleList.AssetBundlePathType.AssetsPathDummyAssetBundle,new Fee.File.Path("Editor/data/create_from_excel_dummyassetbundle_test.assetbundle.json"));
							}
							#else
							{
								//ストリーミングアセット。
								Fee.AssetBundleList.AssetBundleList.GetInstance().RegistPathItem("test.assetbundle",Fee.AssetBundleList.AssetBundlePathType.StreamingAssetsAssetBundle,new Fee.File.Path("AssetBundle_StandaloneWindows/test"));
							}
							#endif
						}
						#else
						{
							//ストリーミングアセット。
							Fee.AssetBundleList.AssetBundleList.GetInstance().RegistPathItem("test.assetbundle",Fee.AssetBundleList.AssetBundlePathType.StreamingAssetsAssetBundle,new Fee.File.Path("AssetBundle_StandaloneWindows/test"));
						}
						#endif
					}

					this.step = Step.LoadAssetBundle_Start;
				}break;
			case Step.LoadAssetBundle_Start:
				{
					//アセットバンドルロード。

					//アセットバンドルリストに登録したアセットバンドル、ダミーアセットバンドルがロードされる。

					this.assetbundlelist_item = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoadPathItemPackItem("test.assetbundle");
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

					//データリストに登録した、パス、アセットバンドルからロードされる。
					//アセットバンドルの場合、事前にアセットバンドルリストにロードしておく必要がある。

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
						this.text.SetText("Load_Text_End : Error : " + this.data_item.GetResultErrorString());
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
						this.text.SetText("Load_Prefab_End : Error : " + this.data_item.GetResultErrorString());
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
						this.text.SetText("Load_Texture_End : Error : " + this.data_item.GetResultErrorString());
					}

					this.step = Step.Fix;
				}break;
			case Step.Fix:
				{
				}break;
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

