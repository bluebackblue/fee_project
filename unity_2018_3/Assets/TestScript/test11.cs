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
	/** test11

		オーディオ

	*/
	public class test11 : MainBase
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

			//ファイル。インスタンス作成。
			Fee.File.Config.LOG_ENABLE = true;
			Fee.File.File.CreateInstance();

			//サウンドプール。インスタンス作成。
			Fee.SoundPool.SoundPool.CreateInstance();

			//オーディオ。インスタンス作成。
			Fee.Audio.Config.LOG_ENABLE = true;
			Fee.Audio.Audio.CreateInstance();

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.CreateInstance();

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//フォント。
			Font t_font = Resources.Load<Font>(Data.Resources.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//サウンドプールモード。
			this.soundpool_mode = SoundPool_Mode.Wait;

			//soundpool_loaditem
			this.soundpool_loaditem = null;

			///ステータス。
			this.status = new Fee.Render2D.Text2D(this.deleter,0);
			this.status.SetRect(100,50,Fee.Render2D.Config.VIRTUAL_W - 100,200);
			this.status.SetText("-");
			this.status.SetFontSize(13);

			//ステータス。
			this.status_2 = new Fee.Render2D.Text2D(this.deleter,0);
			this.status_2.SetRect(100,100,0,0);
			this.status_2.SetText("-");
			this.status.SetFontSize(13);

			int t_xx = 0;

			//ボタン。
			this.button_unload = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click_Unload,-1);
			this.button_unload.SetRect(t_xx,130,170,30);
			this.button_unload.SetText("アンロード");
			this.button_unload.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_unload.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_unload.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_unload.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_unload.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_unload.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_unload.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_unload.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_xx += 210;

			//ボタン。
			this.button_assetbundle = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click_AudioClip,-1);
			this.button_assetbundle.SetRect(t_xx,130,170,30);
			this.button_assetbundle.SetText("AudioClip SE");
			this.button_assetbundle.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_assetbundle.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_assetbundle.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_assetbundle.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_assetbundle.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_assetbundle.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_assetbundle.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_assetbundle.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_xx += 210;

			//ボタン。
			this.button_soundpool = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click_SoundPool,-1);
			this.button_soundpool.SetRect(t_xx,130,170,30);
			this.button_soundpool.SetText("SoundPool SE");
			this.button_soundpool.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_soundpool.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_soundpool.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_soundpool.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_soundpool.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_soundpool.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_soundpool.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_soundpool.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_xx += 210;

			//ボタン。
			this.button_bgm = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click_Bgm,-1);
			this.button_bgm.SetRect(t_xx,130,170,30);
			this.button_bgm.SetText("ＢＧＭ");
			this.button_bgm.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_bgm.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_bgm.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_bgm.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_bgm.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_bgm.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_bgm.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_bgm.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			int t_yy = 300;

			//スライダー。
			this.slider_master = new Fee.Ui.Slider(this.deleter,0,this.CallBack_Change_Master,0);
			this.slider_master.SetRect(100,t_yy,400,40);
			this.slider_master.SetValue(0.0f);
			this.slider_master.SetButtonSize(10,80);
			this.slider_master.SetButtonTextureCornerSize(2);
			this.slider_master.SetBgNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_master.SetBgLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_master.SetValueNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_master.SetValueLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_master.SetBgNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_master.SetBgLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.slider_master.SetValueNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.slider_master.SetValueLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);
			this.slider_master.SetButtonNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.slider_master.SetButtonLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.slider_master.SetButtonNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_master.SetButtonLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_yy += 60;

			//スライダー。
			this.slider_bgm = new Fee.Ui.Slider(this.deleter,0,this.CallBack_Change_Bgm,0);
			this.slider_bgm.SetRect(100,t_yy,400,40);
			this.slider_bgm.SetValue(0.0f);
			this.slider_bgm.SetButtonSize(10,80);
			this.slider_bgm.SetButtonTextureCornerSize(2);
			this.slider_bgm.SetBgNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_bgm.SetBgLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_bgm.SetValueNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_bgm.SetValueLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_bgm.SetBgNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_bgm.SetBgLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.slider_bgm.SetValueNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.slider_bgm.SetValueLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);
			this.slider_bgm.SetButtonNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.slider_bgm.SetButtonLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.slider_bgm.SetButtonNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_bgm.SetButtonLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_yy += 60;

			//スライダー。
			this.slider_se = new Fee.Ui.Slider(this.deleter,0,this.CallBack_Change_Se,0);
			this.slider_se.SetRect(100,t_yy,400,40);
			this.slider_se.SetValue(0.0f);
			this.slider_se.SetButtonSize(10,80);
			this.slider_se.SetButtonTextureCornerSize(2);
			this.slider_se.SetBgNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_se.SetBgLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_se.SetValueNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_se.SetValueLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));
			this.slider_se.SetBgNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_se.SetBgLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.slider_se.SetValueNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.slider_se.SetValueLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);
			this.slider_se.SetButtonNormalTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.slider_se.SetButtonLockTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.slider_se.SetButtonNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_se.SetButtonLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			//値設定。
			this.slider_master.SetValue(Fee.Audio.Audio.GetInstance().GetMasterVolume());
			this.slider_bgm.SetValue(Fee.Audio.Audio.GetInstance().GetBgmVolume());
			this.slider_se.SetValue(Fee.Audio.Audio.GetInstance().GetSeVolume());
		}

		/** [Slider_Base]コールバック。変更。
		*/
		private void CallBack_Change_Master(int a_id,float a_value)
		{
			Fee.Audio.Audio.GetInstance().SetMasterVolume(a_value);

			if(a_value == 0.0f){
				this.slider_bgm.SetLock(true);
				this.slider_se.SetLock(true);
			}else{
				this.slider_bgm.SetLock(false);
				this.slider_se.SetLock(false);
			}
		}

		/** [Slider_Base]コールバック。変更。
		*/
		private void CallBack_Change_Bgm(int a_id,float a_value)
		{
			Fee.Audio.Audio.GetInstance().SetBgmVolume(a_value);
		}

		/** [Slider_Base]コールバック。変更。
		*/
		private void CallBack_Change_Se(int a_id,float a_value)
		{
			Fee.Audio.Audio.GetInstance().SetSeVolume(a_value);
		}

		/** [Button_Base]コールバック。クリック。
		*/
		private void CallBack_Click_Unload(int a_id)
		{
			//ＳＥのアンロード。
			Fee.Audio.Audio.GetInstance().UnLoadSe(SE_ID);

			//ＢＧＭのアンロード。
			Fee.Audio.Audio.GetInstance().UnLoadBgm();
		}

		/** [Button_Base]コールバック。クリック。
		*/
		private void CallBack_Click_AudioClip(int a_id)
		{
			GameObject t_prefab = UnityEngine.Resources.Load<GameObject>(Data.Resources.TEST11_PREFAB_SE);
			Fee.Audio.Pack_AudioClip t_pack_audioclip = t_prefab.GetComponent<Fee.Audio.Pack_AudioClip>();
			if(t_pack_audioclip != null){
				Fee.Audio.Audio.GetInstance().LoadSe(t_pack_audioclip,SE_ID);
			}
		}

		/** [Button_Base]コールバック。クリック。
		*/
		private void CallBack_Click_SoundPool(int a_id)
		{
			if(this.soundpool_mode == SoundPool_Mode.Wait){
				this.soundpool_mode = SoundPool_Mode.Start;
			}
		}

		/** [Button_Base]コールバック。クリック。
		*/
		private void CallBack_Click_Bgm(int a_id)
		{
			GameObject t_prefab = UnityEngine.Resources.Load<GameObject>(Data.Resources.TEST11_PREFAB_BGM);

			if(t_prefab != null){
				Fee.Audio.Pack_AudioClip t_pack_audioclip = t_prefab.GetComponent<Fee.Audio.Pack_AudioClip>();
				if(t_pack_audioclip != null){
					Fee.Audio.Audio.GetInstance().LoadBgm(t_pack_audioclip);
					Fee.Audio.Audio.GetInstance().PlayBgm(0);
				}
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//ファイル。
			Fee.File.File.GetInstance().Main();

			//サウンドプール。
			Fee.SoundPool.SoundPool.GetInstance().Main();

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

			switch(this.soundpool_mode){
			case SoundPool_Mode.Wait:
				{
				}break;
			case SoundPool_Mode.Start:
				{
					this.soundpool_loaditem = Fee.SoundPool.SoundPool.GetInstance().RequestLoadStreamingAssetsSoundPool(new Fee.File.Path(Data.StreamingAssets.TEST11_SOUNDPOOL_SE),SOUNDPOOL_DATA_VERSION);
					this.soundpool_mode = SoundPool_Mode.Now;
				}break;
			case SoundPool_Mode.Now:
				{
					if(this.soundpool_loaditem != null){
						if(this.soundpool_loaditem.IsBusy() == true){
							//ロード中。
							this.status.SetText("soundpool : " + this.soundpool_loaditem.GetResultProgressDown().ToString());
						}else{
							if(this.soundpool_loaditem.GetResultType() == Fee.SoundPool.Item.ResultType.SoundPool){
								Fee.Audio.Pack_SoundPool t_pack_soundpool = this.soundpool_loaditem.GetResultSoundPool();
								if(t_pack_soundpool == null){
									//不正なサウンドプールパック。
									this.status.SetText("Error : " + this.soundpool_mode.ToString());
									this.soundpool_loaditem = null;
									this.soundpool_mode = SoundPool_Mode.Wait;
								}else{
									//成功。
									Fee.Audio.Audio.GetInstance().LoadSe(t_pack_soundpool,SE_ID);
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

			//再生。
			if(Fee.Input.Mouse.GetInstance().InRectCheck(ref Fee.Render2D.Render2D.VIRTUAL_RECT_MAX)){
				if(Fee.Input.Mouse.GetInstance().left.down == true){
					Fee.Audio.Audio.GetInstance().PlaySe(SE_ID,0);
				}
			}

			this.status_2.SetText("soundpool = " + Fee.Audio.Audio.GetInstance().GetSoundPoolCount().ToString());
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

