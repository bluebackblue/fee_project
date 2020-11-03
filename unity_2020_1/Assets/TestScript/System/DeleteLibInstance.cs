

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ライブラリインスタンス削除。
*/


/** TestScript
*/
namespace TestScript
{
	/** DeleteLibInstance
	*/
	public class DeleteLibInstance
	{
		/** 全部削除。
		*/
		public static void DeleteAll()
		{
			//アセット。
			{
			}

			//アセットバンドルリスト。
			{
				Fee.AssetBundleList.AssetBundleList.DeleteInstance();
			}

			//オーディオ。
			{
				Fee.Audio.Audio.DeleteInstance();
			}

			//ブルーム。
			{
			}

			//ブラー。
			{
			}

			//暗号。
			{
				Fee.Crypt.Crypt.DeleteInstance();
			}

			//データ。
			{
				Fee.Data.Data.DeleteInstance();
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

			//エディターツール。
			{
			}

			//イベントプレート。
			{
				Fee.EventPlate.EventPlate.DeleteInstance();
			}

			//エクセル。
			{
			}

			//フェード。
			{
				Fee.Fade.Fade.DeleteInstance();
			}

			//ファイル。
			{
				Fee.File.File.DeleteInstance();
			}

			//フォーカス。
			{
				Fee.Focus.Focus.DeleteInstance();
			}

			//関数呼び出し。順序変更。
			{
			}

			//ジオメトリ。
			{
			}

			//入力。
			{
				Fee.Input.Input.DeleteInstance();
			}

			//インスタンス作成。
			{
			}

			//ＪＳＯＮ。
			{
			}

			//ＪＳＯＮシート。
			{
			}

			//キー。
			{
			}

			//マテリアル。
			{
			}

			//ＭＤ５。
			{
			}

			//ミラー。
			{
				Fee.Mirror.Mirror.DeleteInstance();
			}

			//モデル。
			{
			}

			//ネットワーク。
			{
				Fee.Network.Network.DeleteInstance();
			}

			//パターン。
			{
			}

			//パーセプトロン。
			{
			}

			//パフォーマンスカウンター。
			{
				Fee.PerformanceCounter.PerformanceCounter.DeleteInstance();
			}

			//プラットフォーム。
			{
				Fee.Platform.Platform.DeleteInstance();
			}

			//プレイヤーループシステム。順序変更。
			{
			}

			//プール。
			{
			}

			//リフレクションツール。
			{
			}

			//２Ｄ描画。
			{
				Fee.Render2D.Render2D.DeleteInstance();
			}

			//リスロー。
			{
			}

			//シーン。
			{
				Fee.Scene.Scene.DeleteInstance();
			}

			//サウンドプール。
			{
				Fee.SoundPool.SoundPool.DeleteInstance();
			}

			//文字列コンバート。
			{
			}

			//タスク。
			{
				Fee.TaskW.TaskW.DeleteInstance();
			}

			//タイム。
			{
				Fee.Time.Time.DeleteInstance();
			}

			//ＵＩ。
			{
				Fee.Ui.Ui.DeleteInstance();
			}

			//ＵＮＩＶＲＭ。
			{
				Fee.UniVrm.UniVrm.DeleteInstance();
			}

			//ビデオ。
			{
				Fee.Video.Video.DeleteInstance();
			}

			//順序変更。
			{
				//関数呼び出し。
				{
					Fee.Function.Function.DeleteInstance();
				}

				//プレイヤーループシステム。
				{
					Fee.PlayerLoopSystem.PlayerLoopSystem.DeleteInstance();
				}
			}
		}
	}
}

