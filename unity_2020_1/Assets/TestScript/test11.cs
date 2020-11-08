

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
	/** test11

		オーディオ

	*/
	public class test11 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test11.ButtonId> , Fee.Ui.OnSliderChangeValue_CallBackInterface<test11.SliderId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test11",
				"test11",

				@"
				オーディオ
				"
			);
		}

		/** SOUNDPOOL_DATA_VERSION
		*/
		private const int SOUNDPOOL_DATA_VERSION = 5;

		/** SE_ID
		*/
		private const long SE_ID = 0x00000001;

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** SoundPool_Mode
		*/
		private enum SoundPool_Mode
		{
			/** 待ち。
			*/
			Wait,

			/** Start
			*/
			Start,

			/** Now
			*/
			Now,
		};

		private SoundPool_Mode soundpool_mode;

		/** soundpool_loaditem
		*/
		private Fee.SoundPool.Item soundpool_loaditem;

		/** ステータス。
		*/
		private Fee.Render2D.Text2D status;

		/** ステータス。
		*/
		private Fee.Render2D.Text2D status_2;

		/** ボタン。アンロード。
		*/
		private Fee.Ui.Button button_unload;

		/** ボタン。
		*/
		private Fee.Ui.Button button_assetbundle;

		/** ボタン。
		*/
		private Fee.Ui.Button button_soundpool;

		/** ボタン。
		*/
		private Fee.Ui.Button button_bgm;

		/** スライダー。
		*/
		private Fee.Ui.Slider slider_master;

		/** スライダー。
		*/
		private Fee.Ui.Slider slider_bgm;

		/** slider_se
		*/
		private Fee.Ui.Slider slider_se;

		/** ButtonId
		*/
		public enum ButtonId
		{
			/** Unload
			*/
			Unload,

			/** AudioClip
			*/
			AudioClip,

			/** SoundPool
			*/
			SoundPool,

			/** Bgm
			*/
			Bgm
		}

		/** SliderId
		*/
		public enum SliderId
		{
			Master,
			Bgm,
			Se,
		}

		/** Start
		*/
		private void Start()
		{
			//プレイヤーループシステム。インスタンス作成。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance(null);
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(UnityEngine.PlayerLoop.PreUpdate.SendMouseEvents));

			//プラットフォーム。インスタンス作成。
			Fee.Platform.Platform.CreateInstance();

			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.CreateInstance();
			Fee.Function.Function.GetInstance().SetMonoBehaviour(this);
			Fee.Function.Function.GetInstance().SetRowUpdate(this.RowUpdate);

			//ファイル。インスタンス作成。
			Fee.File.Config.LOG_ENABLE = true;
			Fee.File.File.CreateInstance();

			//サウンドプール。インスタンス作成。
			Fee.SoundPool.SoundPool.CreateInstance();

			//オーディオ。インスタンス作成。
			Fee.Audio.Config.LOG_ENABLE = true;
			Fee.Audio.Config.BGM_PLAY_FADEIN = true;
			Fee.Audio.Config.BGM_CROSSFADE_SPEED = 0.004f;
			Fee.Audio.Audio.CreateInstance();

			//インスタンス作成。
			Fee.Instantiate.Instantiate.CreateInstance();

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Input.CreateInstance(true,false,true,false);

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//サウンドプールモード。
			this.soundpool_mode = SoundPool_Mode.Wait;

			//soundpool_loaditem
			this.soundpool_loaditem = null;

			///ステータス。
			this.status = this.prefablist.CreateText(this.deleter,0);
			this.status.SetRect(100,50,Fee.Render2D.Config.VIRTUAL_W - 100,200);
			this.status.SetText("-");
			this.status.SetFontSize(13);

			//ステータス。
			this.status_2 = this.prefablist.CreateText(this.deleter,0);
			this.status_2.SetRect(100,100,0,0);
			this.status_2.SetText("-");
			this.status.SetFontSize(13);

			int t_xx = 0;

			//ボタン。
			this.button_unload = this.prefablist.CreateButton(this.deleter,0);
			this.button_unload.SetOnButtonClick(this,ButtonId.Unload);
			this.button_unload.SetRect(t_xx,130,170,30);
			this.button_unload.SetText("アンロード");

			t_xx += 210;

			//ボタン。
			this.button_assetbundle = this.prefablist.CreateButton(this.deleter,0);
			this.button_assetbundle.SetOnButtonClick(this,ButtonId.AudioClip);
			this.button_assetbundle.SetRect(t_xx,130,170,30);
			this.button_assetbundle.SetText("AudioClip SE");

			t_xx += 210;

			//ボタン。
			this.button_soundpool = this.prefablist.CreateButton(this.deleter,0);
			this.button_soundpool.SetOnButtonClick(this,ButtonId.SoundPool);
			this.button_soundpool.SetRect(t_xx,130,170,30);
			this.button_soundpool.SetText("SoundPool SE");

			t_xx += 210;

			//ボタン。
			this.button_bgm = this.prefablist.CreateButton(this.deleter,0);
			this.button_bgm.SetOnButtonClick(this,ButtonId.Bgm);
			this.button_bgm.SetRect(t_xx,130,170,30);
			this.button_bgm.SetText("ＢＧＭ");

			int t_yy = 300;

			//スライダー。
			this.slider_master = this.prefablist.CreateSlider(this.deleter,0);
			this.slider_master.SetOnSliderChangeValue(this,SliderId.Master);
			this.slider_master.SetRect(100,t_yy,400,40);
			this.slider_master.SetValue(0.0f);
			this.slider_master.SetButtonSize(10,80);

			t_yy += 60;

			//スライダー。
			this.slider_bgm = this.prefablist.CreateSlider(this.deleter,0);
			this.slider_bgm.SetOnSliderChangeValue(this,SliderId.Bgm);
			this.slider_bgm.SetRect(100,t_yy,400,40);
			this.slider_bgm.SetValue(0.0f);
			this.slider_bgm.SetButtonSize(10,80);

			t_yy += 60;

			//スライダー。
			this.slider_se = this.prefablist.CreateSlider(this.deleter,0);
			this.slider_se.SetOnSliderChangeValue(this,SliderId.Se);
			this.slider_se.SetRect(100,t_yy,400,40);
			this.slider_se.SetValue(0.0f);
			this.slider_se.SetButtonSize(10,80);

			//値設定。
			this.slider_master.SetValue(Fee.Audio.Audio.GetInstance().GetMasterVolume());
			this.slider_bgm.SetValue(Fee.Audio.Audio.GetInstance().GetBgmVolume());
			this.slider_se.SetValue(Fee.Audio.Audio.GetInstance().GetSeVolume());
		}

		/** [Fee.Ui.OnSliderChangeValue_CallBackInterface]値変更。
		*/
		public void OnSliderChangeValue(SliderId a_id,float a_value)
		{
			switch(a_id){
			case SliderId.Master:
				{
					Fee.Audio.Audio.GetInstance().SetMasterVolume(a_value);

					if(a_value == 0.0f){
						this.slider_bgm.SetLock(true);
						this.slider_se.SetLock(true);
					}else{
						this.slider_bgm.SetLock(false);
						this.slider_se.SetLock(false);
					}
				}break;
			case SliderId.Bgm:
				{
					Fee.Audio.Audio.GetInstance().SetBgmVolume(a_value);
				}break;
			case SliderId.Se:
				{
					Fee.Audio.Audio.GetInstance().SetSeVolume(a_value);
				}break;
			}
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			switch(a_id){
			case ButtonId.Unload:
				{
					//アンロード。

					Fee.Audio.Audio.GetInstance().UnLoadSe(SE_ID);
					Fee.Audio.Audio.GetInstance().UnLoadBgm();
				}break;
			case ButtonId.AudioClip:
				{
					//オーディオクリップ。

					//ロード。
					Fee.Audio.Bank t_bank = this.prefablist.GetSeBank();
					if(t_bank != null){
						Fee.Audio.Audio.GetInstance().LoadSe(t_bank,SE_ID);
					}
				}break;
			case ButtonId.SoundPool:
				{
					//サウンドプール。

					if(this.soundpool_mode == SoundPool_Mode.Wait){
						this.soundpool_mode = SoundPool_Mode.Start;
					}
				}break;
			case ButtonId.Bgm:
				{
					//ＢＧＭ。

					if(Fee.Audio.Audio.GetInstance().GetBgmPlayIndex() < 0){

						//ロード。
						Fee.Audio.Bank t_bank = this.prefablist.GetBgmBank();
						if(t_bank != null){
							//ロード。
							Fee.Audio.Audio.GetInstance().LoadBgm(t_bank);

							//再生。
							Fee.Audio.Audio.GetInstance().PlayBgm(1);
						}
					}else if(Fee.Audio.Audio.GetInstance().GetBgmPlayIndex() == 0){
						//再生。
						Fee.Audio.Audio.GetInstance().PlayBgm(1);
					}else{
						//再生。
						Fee.Audio.Audio.GetInstance().PlayBgm(0);
					}
				}break;
			}
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
			switch(this.soundpool_mode){
			case SoundPool_Mode.Wait:
				{
				}break;
			case SoundPool_Mode.Start:
				{
					this.soundpool_loaditem = Fee.SoundPool.SoundPool.GetInstance().RequestLoadStreamingAssetsPack(new Fee.File.Path(Common.Data.StreamingAssets.TEST11_SOUNDPOOL_SE),SOUNDPOOL_DATA_VERSION);
					this.soundpool_mode = SoundPool_Mode.Now;
				}break;
			case SoundPool_Mode.Now:
				{
					if(this.soundpool_loaditem != null){
						if(this.soundpool_loaditem.IsBusy() == true){
							//ロード中。
							this.status.SetText("soundpool : " + this.soundpool_loaditem.GetResultProgress().ToString());
						}else{
							if(this.soundpool_loaditem.GetResultType() == Fee.SoundPool.Item.ResultType.Pack){
								Fee.SoundPool.Pack t_pack = this.soundpool_loaditem.GetResultPack();
								if(t_pack == null){
									//不正なパック。
									this.status.SetText("Error : " + this.soundpool_mode.ToString());
									this.soundpool_loaditem = null;
									this.soundpool_mode = SoundPool_Mode.Wait;
								}else{
									//成功。
									Fee.Audio.Audio.GetInstance().LoadSe(new Fee.Audio.Bank(t_pack),SE_ID);
									this.status.SetText("Success");
									this.soundpool_loaditem = null;
									this.soundpool_mode = SoundPool_Mode.Wait;
								}
							}else{
								//ロード失敗。
								this.status.SetText("Error : " + this.soundpool_loaditem.GetResultErrorString());
								this.soundpool_loaditem = null;
								this.soundpool_mode = SoundPool_Mode.Wait;
							}
						}
					}
				}break;
			}

			this.status_2.SetText("soundpool = " + Fee.SoundPool.SoundPool.GetInstance().GetPlayer().GetCount().ToString());
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
			//再生。
			if(Fee.Geometry.Range.IsRectIn(in Fee.Render2D.Config.VIRTUAL_RECT_MAX,in Fee.Input.Input.GetInstance().mouse.cursor.pos) == true){
				if(Fee.Input.Input.GetInstance().mouse.left.down == true){
					Fee.Audio.Audio.GetInstance().PlaySe(SE_ID,0);
				}
			}
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

