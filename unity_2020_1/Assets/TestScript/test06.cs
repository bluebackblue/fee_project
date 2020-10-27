

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
	/** test06

		シーン遷移
		フェード

	*/
	public class test06 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test06",
				"test06",

				@"
				シーン遷移
				フェード
				"
			);
		}

		/** シーンＡ。
		*/
		class SceneA : Fee.Scene.Scene_Base
		{
			/** [Scene.Scene_Base]更新。

				return == true : 終了。

			*/
			public bool Main()
			{
				return Fee.Scene.Scene.GetInstance().IsNextScene();
			}

			/** [Scene_Base]更新。
			*/
			public void Unity_Update(float a_delta)
			{
				if(Fee.Fade.Fade.GetInstance().IsAnime() == false){
					if(Fee.Input.Input.GetInstance().mouse.left.down == true){
						//遷移リクエスト。
						UnityEngine.Debug.Log("SceneA : request");
						Fee.Scene.Scene.GetInstance().SetNextScene(new SceneB());
					}
				}
			}

			/** [Scene_Base]更新。
			*/
			public void Unity_LateUpdate(float a_delta)
			{
			}

			/** [Scene.Scene_Base]削除。
			*/
			public void Delete()
			{
				UnityEngine.Debug.Log("SceneA : Delete");
			}

			/** [Scene.Scene_Base]シーン開始。

				return == true : 開始処理完了。

			*/
			public bool SceneStart()
			{
				UnityEngine.Debug.Log("SceneA : SceneStart");

				Fee.Fade.Fade.GetInstance().FadeIn();
				return true;
			}

			/** [Scene.Scene_Base]シーン終了。

				return == true : 終了処理完了。

			*/
			public bool SceneEnd()
			{
				UnityEngine.Debug.Log("SceneA : SceneEnd");

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

				return == true : 終了。

			*/
			public bool Main()
			{
				return Fee.Scene.Scene.GetInstance().IsNextScene();
			}

			/** [Scene_Base]更新。
			*/
			public void Unity_Update(float a_delta)
			{
				if(Fee.Fade.Fade.GetInstance().IsAnime() == false){
					if(Fee.Input.Input.GetInstance().mouse.left.down == true){
						//遷移リクエスト。
						UnityEngine.Debug.Log("SceneB : request");
						Fee.Scene.Scene.GetInstance().SetNextScene(new SceneA());
					}
				}
			}

			/** [Scene_Base]更新。
			*/
			public void Unity_LateUpdate(float a_delta)
			{
			}

			/** [Scene.Scene_Base]削除。
			*/
			public void Delete()
			{
				UnityEngine.Debug.Log("SceneB : Delete");
			}

			/** [Scene.Scene_Base]シーン開始。

				return == true : 開始処理完了。

			*/
			public bool SceneStart()
			{
				UnityEngine.Debug.Log("SceneB : SceneStart");

				Fee.Fade.Fade.GetInstance().FadeIn();
				return true;
			}

			/** [Scene.Scene_Base]シーン終了。

				return == true : 終了処理完了。

			*/
			public bool SceneEnd()
			{
				UnityEngine.Debug.Log("SceneB : SceneEnd");

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

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** bg
		*/
		private Fee.Render2D.Sprite2D bg;

		/** Start
		*/
		private void Start()
		{
			//プレイヤーループシステム。インスタンス作成。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance(null);
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(UnityEngine.PlayerLoop.PreUpdate.SendMouseEvents));

			//プラットフォーム。インスタンス作成。
			Fee.Platform.Platform.CreateInstance();

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			//Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			//Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.CreateInstance();
			Fee.Function.Function.GetInstance().SetMonoBehaviour(this);
			Fee.Function.Function.GetInstance().SetRowUpdate(this.RowUpdate);

			//入力。インスタンス作成。
			Fee.Input.Input.CreateInstance(true,false,true,false);

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

			//シーン。インスタンス作成。
			Fee.Scene.Scene.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//bg
			this.bg = Fee.Render2D.Sprite2D.Create(this.deleter,0);
			this.bg.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
			this.bg.SetRect(in Fee.Render2D.Config.VIRTUAL_RECT_MAX);
			this.bg.SetColor(0.0f,1.0f,1.0f,1.0f);

			//シーンＡ。
			Fee.Scene.Scene.GetInstance().SetNextScene(new SceneA());
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
		}

		/** Update
		*/
		private void Update()
		{
			//シーン。
			Fee.Scene.Scene.GetInstance().Main();
			Fee.Scene.Scene.GetInstance().Unity_Update(UnityEngine.Time.deltaTime);

			//フェード。
			Fee.Fade.Fade.GetInstance().Main();
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
		}

		/** 強制終了。
		*/
		public override void Shutdown()
		{
			Fee.Function.Function.GetInstance().UnSetRowUpdate(this.RowUpdate);
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
			Fee.Function.Function.GetInstance().UnSetRowUpdate(this.RowUpdate);
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

