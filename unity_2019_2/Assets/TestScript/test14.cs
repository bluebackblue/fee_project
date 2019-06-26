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
		}
		private Step step;

		/** text
		*/
		private Fee.Render2D.Text2D text;

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

			//アセットバンドル。インスタンス作成。
			Fee.AssetBundle.AssetBundle.CreateInstance();

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

			//アセットバンドル。
			Fee.AssetBundle.AssetBundle.GetInstance().Main();

			switch(this.step){
			case Step.Init:
				{
					this.text.SetText("Init");

					Fee.AssetBundle.AssetBundle.GetInstance().AddPath("id", Fee.AssetBundle.AssetBundle.PathType.AssetsPath,new Fee.File.Path("Editor/AssetBundle/StandaloneWindows/test12"));
					this.step = Step.Binary_Start;
				}break;
			case Step.Binary_Start:
				{
					this.text.SetText("Start");

					Fee.AssetBundle.AssetBundle.GetInstance().RequestLoad("id");
					this.step = Step.Binary_Wait;
				}break;
			case Step.Binary_Wait:
				{
					if(Fee.AssetBundle.AssetBundle.GetInstance().IsBusy() == false){
						this.step = Step.Binary_End;
					}else{
						this.text.SetText("Wait");
					}
				}break;
			case Step.Binary_End:
				{
					this.text.SetText("End");
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

