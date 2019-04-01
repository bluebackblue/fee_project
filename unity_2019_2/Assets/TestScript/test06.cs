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


/** test06

	シーン遷移
	フェード

*/
public class test06 : main_base
{
	/** シーンＡ。
	*/
	class SceneA : Fee.Scene.Scene_Base
	{
		/** [Scene.Scene_Base]更新。

		戻り値 = true : 終了。

		*/
		public bool Main()
		{
			if(Fee.Fade.Fade.GetInstance().IsAnime() == false){
				if(Fee.Input.Mouse.GetInstance().left.down == true){
					Debug.Log("SceneA : request");
					//遷移リクエスト。
					Fee.Scene.Scene.GetInstance().SetNextScene(new SceneB());
				}
			}

			return Fee.Scene.Scene.GetInstance().IsNextScene();
		}

		/** [Scene_Base]更新。
		*/
		public void Unity_Update(float a_delta)
		{
		}

		/** [Scene_Base]更新。
		*/
		public void Unity_LastUpdate()
		{
		}

		/** [Scene.Scene_Base]削除。
		*/
		public void Delete()
		{
			Debug.Log("SceneA : Delete");
		}

		/** [Scene.Scene_Base]シーン開始。

		戻り値 = true : 開始処理完了。

		*/
		public bool SceneStart()
		{
			Debug.Log("SceneA : SceneStart");

			Fee.Fade.Fade.GetInstance().FadeIn();
			return true;
		}

		/** [Scene.Scene_Base]シーン終了。

		戻り値 = true : 終了処理完了。

		*/
		public bool SceneEnd()
		{
			Debug.Log("SceneA : SceneEnd");

			Fee.Fade.Fade.GetInstance().FadeOut();

			if(Fee.Fade.Fade.GetInstance().IsAnime() == false){
				return true;
			}

			return false;
		}
	};

	/** シーンＢ。
	*/
	class SceneB : Fee.Scene.Scene_Base
	{
		/** [Scene.Scene_Base]更新。

		戻り値 = true : 終了。

		*/
		public bool Main()
		{
			if(Fee.Fade.Fade.GetInstance().IsAnime() == false){
				if(Fee.Input.Mouse.GetInstance().left.down == true){
					//遷移リクエスト。
					Debug.Log("SceneB : request");
					Fee.Scene.Scene.GetInstance().SetNextScene(new SceneA());
				}
			}

			return Fee.Scene.Scene.GetInstance().IsNextScene();
		}

		/** [Scene_Base]更新。
		*/
		public void Unity_Update(float a_delta)
		{
		}

		/** [Scene_Base]更新。
		*/
		public void Unity_LastUpdate()
		{
		}

		/** [Scene.Scene_Base]削除。
		*/
		public void Delete()
		{
			Debug.Log("SceneB : Delete");
		}

		/** [Scene.Scene_Base]シーン開始。

		戻り値 = true : 開始処理完了。

		*/
		public bool SceneStart()
		{
			Debug.Log("SceneB : SceneStart");

			Fee.Fade.Fade.GetInstance().FadeIn();
			return true;
		}

		/** [Scene.Scene_Base]シーン終了。

		戻り値 = true : 終了処理完了。

		*/
		public bool SceneEnd()
		{
			Debug.Log("SceneB : SceneEnd");

			Fee.Fade.Fade.GetInstance().FadeOut();

			if(Fee.Fade.Fade.GetInstance().IsAnime() == false){
				return true;
			}
			return false;
		}
	};

	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。インスタンス作成。
		Fee.Render2D.Render2D.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		//Fee.PerformanceCounter.Config.LOG_ENABLE = true;
		//Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

		//関数呼び出し。
		Fee.Function.Function.SetMonoBehaviour(this);

		//イベントプレート。
		Fee.EventPlate.Config.LOG_ENABLE = true;
		Fee.EventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		Fee.Ui.Config.LOG_ENABLE = true;
		Fee.Ui.Ui.CreateInstance();

		//フェード。インスタンス作成。
		Fee.Fade.Fade.CreateInstance();
		Fee.Fade.Fade.GetInstance().SetSpeed(0.05f);
		Fee.Fade.Fade.GetInstance().SetColor(0.0f,0.0f,0.0f,1.0f);
		Fee.Fade.Fade.GetInstance().SetToColor(0.0f,0.0f,0.0f,1.0f);
		Fee.Fade.Fade.GetInstance().SetAnime();

		//マウス。インスタンス作成。
		Fee.Input.Mouse.CreateInstance();

		//シーン。インスタンス作成。
		Fee.Scene.Scene.CreateInstance();

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//シーンＡ。
		Fee.Scene.Scene.GetInstance().SetNextScene(new SceneA());
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//パフォーマンスカウンター。インスタンス作成。
		Fee.PerformanceCounter.Config.LOG_ENABLE = true;
		Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

		//フェード。
		Fee.Fade.Fade.GetInstance().Main_PreDraw();
		Fee.Fade.Fade.GetInstance().Main();

		//マウス。
		Fee.Input.Mouse.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());

		//イベントプレート。
		Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

		//ＵＩ。
		Fee.Ui.Ui.GetInstance().Main();

		//シーン。
		Fee.Scene.Scene.GetInstance().Main();
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

