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
	/** test12
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
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

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

			//フォント。
			Font t_font = Resources.Load<Font>(Data.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");
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
		[UnityEditor.MenuItem("Fee/Test/ConvertFromExcel")]
		private static void MenuItem_ConvertFromExcel()
		{
			/*
			{
				//object
				UnityEditor.AssetBundleBuild[] t_object = new UnityEditor.AssetBundleBuild[1];
				{
					t_object[0].assetBundleName = "se.assetbundle";
					t_object[0].assetBundleVariant = null;
					t_object[0].assetNames = new string[1]{
						"Assets/Data/ConvertFromExcel/excel_to_se_prefab.prefab"
					};
				}

				//outputpath
				string t_output_path = "Assets/Data/AssetBundle";

				//option
				UnityEditor.BuildAssetBundleOptions t_option = UnityEditor.BuildAssetBundleOptions.None;

				UnityEditor.BuildPipeline.BuildAssetBundles(t_output_path,t_object,t_option,UnityEditor.BuildTarget.StandaloneWindows);
			}
			*/

			//エクセルからＪＳＯＮシートを作成。
			Fee.Excel.ExcelToJsonSheet t_excel_to_jsonsheet = new Fee.Excel.ExcelToJsonSheet();
			if(t_excel_to_jsonsheet.Convert(new Fee.File.Path(UnityEngine.Application.streamingAssetsPath + "/","excel.xlsx")) == true){
				Fee.JsonItem.JsonItem t_jsonsheet = t_excel_to_jsonsheet.GetJsonSheet();
				if(t_jsonsheet != null){
					//コンバート。
					if(Fee.JsonSheet.Convert.ConvertFromJsonSheet(t_jsonsheet) == false){
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

