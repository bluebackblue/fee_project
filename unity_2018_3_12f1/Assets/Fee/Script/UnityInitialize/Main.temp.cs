

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＴＹ初期化。メイン。
*/


/** Fee.UnityInitialize
*/
#if(USE_DEF_FEE_TEMP)
namespace Fee.UnityInitialize
{
	/** Main
	*/
	public class Main : UnityEngine.MonoBehaviour
	{
		/** forced_quit
		*/
		private static bool s_forced_quit = false;

		/** s_is_focus
		*/
		private static bool s_is_focus = false;

		/** アプリ起動時。
		*/
		[UnityEngine.RuntimeInitializeOnLoadMethod]
		private static void AppInitialize()
		{
		}

		/** アプリ終了時。
		*/
		void OnApplicationQuit()
		{
			s_forced_quit = true;
		}

		/** アプリフォーカス変更時。
		*/
		void OnApplicationFocus(bool a_flag)
		{
			s_is_focus = a_flag;
		}

		/** s_instance
		*/
		private static Main s_instance;

		/** GetInstance
		*/
		public static Main GetInstance()
		{
			return s_instance;
		}

		/** Start
		*/
		void Start()
		{
			//s_instance
			s_instance = this;

			//ライブラリ停止。
			this.DeleteLibInstance();

			//初期化。
			{
				{
					//２Ｄ描画。
					#if(false)
					{
						Fee.Render2D.Config.MAX_LAYER = Setting.RENDER_LAYER_INDEX_MAX;
						Fee.Render2D.Render2D.CreateInstance();
					}
					#endif
				}

				//オーディオ。
				#if(false)
				{
					Fee.Audio.Audio.CreateInstance();
				}
				#endif

				//ブルーム。
				#if(false)
				{
					Fee.Bloom.Bloom.CreateInstance();
				}
				#endif

				//ブラー。
				#if(false)
				{
					Fee.Blur.Blur.CreateInstance();
				}
				#endif

				//暗号。
				#if(false)
				{
					Fee.Crypt.Crypt.CreateInstance();
				}
				#endif

				//削除管理。
				{
				}

				//深度。
				#if(false)
				{
					Fee.Depth.Depth.CreateInstance();
				}
				#endif

				//ダイクストラ法。
				{
				}

				//ディレクトリ。
				{
				}

				//イベントプレート。
				#if(false)
				{
					Fee.EventPlate.EventPlate.CreateInstance();
				}
				#endif

				//フェード。
				#if(false)
				{
					Fee.Fade.Fade.CreateInstance();
					Fee.Fade.Fade.GetInstance().SetSpeed(0.05f);
					Fee.Fade.Fade.GetInstance().SetColor(0.0f,0.0f,0.0f,1.0f);
					Fee.Fade.Fade.GetInstance().SetToColor(0.0f,0.0f,0.0f,1.0f);
					Fee.Fade.Fade.GetInstance().SetAnime();
				}
				#endif

				//ファイル。
				#if(false)
				{
					Fee.File.File.CreateInstance();
				}
				#endif

				//関数呼び出し。
				#if(false)
				{
					Fee.Function.Function.SetMonoBehaviour(this);
				}
				#endif

				//入力。
				{
					#if(false)
					{
						Fee.Input.Mouse.CreateInstance();
					}
					#endif

					//キー。
					#if(false)
					{
						Fee.Input.Key.CreateInstance();
					}
					#endif

					//パッド。
					#if(false)
					{
						Fee.Input.Pad.CreateInstance();
					}
					#endif
				}

				//インスタンス作成。
				{
				}

				//ＪＳＯＮ。
				{
				}

				//アセットバンドル作成。
				{
				}

				//モデル。
				{
				}

				//ネットワーク。
				#if(false)
				{
					Fee.Network.Network.CreateInstance();
				}
				#endif

				//パフォーマンスカウンター。
				#if(false)
				{
					Fee.PerformanceCounter.PerformanceCounter.CreateInstance();
				}
				#endif

				//プラットフォーム。
				{
				}

				//２Ｄ描画。

				//シーン。
				#if(false)
				{
					Fee.Scene.Scene.CreateInstance();
				}
				#endif

				//サウンドプール。
				#if(false)
				{
					Fee.SoundPool.SoundPool.CreateInstance();
				}
				#endif

				//タスク。
				#if(false)
				{
					Fee.TaskW.TaskW.CreateInstance();
				}
				#endif

				//ＵＩ。
				#if(false)
				{
					Fee.Ui.Ui.CreateInstance();
				}
				#endif

				//ＵＮＩＴＹ初期化。
				{
				}

				//ＵＮＩＶＲＭ。
				#if(false)
				{
					Fee.UniVrm.UniVrm.CreateInstance();
				}
				#endif
			}
		}

		/** ライブラリ停止。
		*/
		public void DeleteLibInstance()
		{
			//オーディオ。
			{
				Fee.Audio.Audio.DeleteInstance();
			}

			//ブルーム。
			{
				Fee.Bloom.Bloom.DeleteInstance();
			}

			//ブラー。
			{
				Fee.Blur.Blur.DeleteInstance();
			}

			//暗号。
			{
				Fee.Crypt.Crypt.DeleteInstance();
			}

			//削除管理。
			{
			}

			//深度。
			{
				Fee.Depth.Depth.DeleteInstance();
			}

			//ダイクストラ法。
			{
			}

			//ディレクトリ。
			{
			}

			//イベントプレート。
			{
				Fee.EventPlate.EventPlate.DeleteInstance();
			}

			//フェード。
			{
				Fee.Fade.Fade.DeleteInstance();
			}

			//ファイル。
			{
				Fee.File.File.DeleteInstance();
			}

			//関数呼び出し。
			{
				Fee.Function.Function.SetMonoBehaviour(null);
			}

			//入力。
			{
				//マスウ。
				Fee.Input.Mouse.DeleteInstance();
	
				//キー。
				Fee.Input.Key.DeleteInstance();
	
				//パッド。
				Fee.Input.Pad.DeleteInstance();
			}

			//インスタンス作成。
			{
			}

			//ＪＳＯＮ。
			{
			}

			//アセットバンドル作成。
			{
			}

			//モデル。
			{		
			}

			//ネットワーク。
			{
				Fee.Network.Network.DeleteInstance();
			}

			//パフォーマンスカウンター。
			{
				Fee.PerformanceCounter.PerformanceCounter.DeleteInstance();
			}

			//プラットフォーム。
			{
			}

			//２Ｄ描画。
			{
				Fee.Render2D.Render2D.DeleteInstance();
			}

			//シーン。
			{
				Fee.Scene.Scene.DeleteInstance();
			}

			//サウンドプール。
			{
				Fee.SoundPool.SoundPool.DeleteInstance();
			}

			//タスク。
			{
				Fee.TaskW.TaskW.DeleteInstance();
			}

			//ＵＩ。
			{
				Fee.Ui.Ui.DeleteInstance();
			}

			//ＵＮＩＴＹ初期化。
			{
			}

			//ＵＮＩＶＲＭ。
			{
				Fee.UniVrm.UniVrm.DeleteInstance();
			}
		}

		/** 更新。

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		Update呼び出し前。

		*/
		private void FixedUpdate()
		{
			try{
				{
					//入力。
					{
						//マウス。
						#if(false)
						Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());
						#endif

						//キー。
						#if(false)
						Fee.Input.Key.GetInstance().Main(s_is_focus);
						#endif

						//パッド。
						#if(false)
						Fee.Input.Pad.GetInstance().Main(true);
						#endif
					}

					//イベントプレート。
					#if(false)
					{
						Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);
					}
					#endif

					//ＵＩ。
					#if(false)
					{
						Fee.Ui.Ui.GetInstance().Main();
					}
					#endif

					//シーン。
					#if(false)
					{
						Fee.Scene.Scene.GetInstance().Main();
					}
					#endif
				}

				//オーディオ。
				{
				}

				//ブルーム。
				{
				}

				//ブラー。
				{
				}

				//暗号。
				#if(false)
				{
					Fee.Crypt.Crypt.GetInstance().Main();
				}
				#endif

				//削除管理。
				{
				}

				//深度。
				{
				}

				//ダイクストラ法。
				{
				}

				//ディレクトリ。
				{
				}

				//イベントプレート。

				//フェード。
				#if(false)
				{
					Fee.Fade.Fade.GetInstance().Main_PreDraw();
					Fee.Fade.Fade.GetInstance().Main();
				}
				#endif

				//ファイル。
				#if(false)
				{
					Fee.File.File.GetInstance().Main();
				}
				#endif

				//関数呼び出し。
				{
				}

				//入力。

				//インスタンス作成。
				{
				}

				//ＪＳＯＮ。
				{
				}

				//アセットバンドル作成。
				{
				}

				//モデル。
				{
				}

				//ネットワーク。
				#if(false)
				{
					Fee.Network.Network.GetInstance().Main();
				}
				#endif

				//パフォーマンスカウンター。
				{
				}

				//プラットフォーム。
				{
				}

				//２Ｄ描画。
				{
				}

				//シーン。

				//サウンドプール。
				#if(false)
				{
					Fee.SoundPool.SoundPool.GetInstance().Main();
				}
				#endif

				//タスク。
				{
				}

				//ＵＩ。

				//ＵＮＩＴＹ初期化。
				{
				}

				//ＵＮＩＶＲＭ。
				#if(false)
				{
					Fee.UniVrm.UniVrm.GetInstance().Main();
				}
				#endif

			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.StackTrace + "\n\n" + t_exception.Message);
			}
		}

		/** Update

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		シーンレンダリング前。

		*/
		private void Update()
		{
			//シーン。
			Fee.Scene.Scene.GetInstance().Unity_Update(UnityEngine.Time.deltaTime);
		}

		/** LastUpdate

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		シーンレンダリング前。Update呼び出し後。

		*/
		private void LastUpdate()
		{
			//シーン。
			Fee.Scene.Scene.GetInstance().Unity_Update(UnityEngine.Time.deltaTime);
		}

		/** シーン遷移。
		*/
		private void OnDestroy()
		{
			if(s_forced_quit == true){
				//アプリ終了による強制終了。
			}else{
				//ライブラリ停止。
				this.DeleteLibInstance();

				//s_instance
				s_instance = null;
			}
		}
	}
}
#endif
