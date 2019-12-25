

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

			//アセットバンドル。
			{
				Fee.AssetBundleList.AssetBundleList.DeleteInstance();
			}

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

			//関数呼び出し。
			{
				Fee.Function.Function.SetMonoBehaviour(null);
			}

			//入力。
			{
				//マウス。
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

			//ＪＳＯＮシート。
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

			//プレイヤーループシステム。
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

			//タスク。
			{
				Fee.TaskW.TaskW.DeleteInstance();
			}

			//ＵＩ。
			{
				Fee.Ui.Ui.DeleteInstance();
			}

			//ＵＮＩＶＲＭ。
			{
				Fee.UniVrm.UniVrm.DeleteInstance();
			}
		}
	}
}

