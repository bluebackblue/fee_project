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
			//プラットフォーム。インスタンス作成。
			Fee.Platform.Platform.CreateInstance();

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
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.CreateInstance();

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//プレハブリスト。
			{
				this.prefablist = new Common.PrefabList();
				this.prefablist.LoadFontList();
				this.prefablist.LoadTextureList();
			}

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont("FONT"));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,this.prefablist.GetTexture("UI_BUTTON"),(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//サウンドプールモード。
			this.soundpool_mode = SoundPool_Mode.Wait;

			//soundpool_loaditem
			this.soundpool_loaditem = null;

			///ステータス。
			this.status = Fee.Render2D.Text2D.Create(this.deleter,0);
			this.status.SetRect(100,50,Fee.Render2D.Config.VIRTUAL_W - 100,200);
			this.status.SetText("-");
			this.status.SetFontSize(13);

			//ステータス。
			this.status_2 = Fee.Render2D.Text2D.Create(this.deleter,0);
			this.status_2.SetRect(100,100,0,0);
			this.status_2.SetText("-");
			this.status.SetFontSize(13);

			int t_xx = 0;

			//ボタン。
			this.button_unload = new Fee.Ui.Button(this.deleter,0);
			this.button_unload.SetOnButtonClick(this,ButtonId.Unload);
			this.button_unload.SetRect(t_xx,130,170,30);
			this.button_unload.SetText("アンロード");
			this.button_unload.SetNormalTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_unload.SetOnTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_unload.SetDownTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_unload.SetLockTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_unload.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_unload.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_unload.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_unload.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_xx += 210;

			//ボタン。
			this.button_assetbundle = new Fee.Ui.Button(this.deleter,0);
			this.button_assetbundle.SetOnButtonClick(this,ButtonId.AudioClip);
			this.button_assetbundle.SetRect(t_xx,130,170,30);
			this.button_assetbundle.SetText("AudioClip SE");
			this.button_assetbundle.SetNormalTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_assetbundle.SetOnTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_assetbundle.SetDownTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_assetbundle.SetLockTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_assetbundle.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_assetbundle.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_assetbundle.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_assetbundle.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_xx += 210;

			//ボタン。
			this.button_soundpool = new Fee.Ui.Button(this.deleter,0);
			this.button_soundpool.SetOnButtonClick(this,ButtonId.SoundPool);
			this.button_soundpool.SetRect(t_xx,130,170,30);
			this.button_soundpool.SetText("SoundPool SE");
			this.button_soundpool.SetNormalTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_soundpool.SetOnTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_soundpool.SetDownTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_soundpool.SetLockTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_soundpool.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_soundpool.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_soundpool.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_soundpool.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_xx += 210;

			//ボタン。
			this.button_bgm = new Fee.Ui.Button(this.deleter,0);
			this.button_bgm.SetOnButtonClick(this,ButtonId.Bgm);
			this.button_bgm.SetRect(t_xx,130,170,30);
			this.button_bgm.SetText("ＢＧＭ");
			this.button_bgm.SetNormalTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_bgm.SetOnTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_bgm.SetDownTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_bgm.SetLockTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.button_bgm.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.button_bgm.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.button_bgm.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.button_bgm.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			int t_yy = 300;

			//スライダー。
			this.slider_master = new Fee.Ui.Slider(this.deleter,0);
			this.slider_master.SetOnSliderChangeValue(this,SliderId.Master);
			this.slider_master.SetRect(100,t_yy,400,40);
			this.slider_master.SetValue(0.0f);
			this.slider_master.SetButtonSize(10,80);
			this.slider_master.SetButtonTextureCornerSize(2);
			this.slider_master.SetTextureCornerSize(10);
			this.slider_master.SetBgNormalTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_master.SetBgLockTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_master.SetValueNormalTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_master.SetValueLockTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_master.SetBgNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_master.SetBgLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.slider_master.SetValueNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.slider_master.SetValueLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
			this.slider_master.SetButtonNormalTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.slider_master.SetButtonLockTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.slider_master.SetButtonNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_master.SetButtonLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_yy += 60;

			//スライダー。
			this.slider_bgm = new Fee.Ui.Slider(this.deleter,0);
			this.slider_bgm.SetOnSliderChangeValue(this,SliderId.Bgm);
			this.slider_bgm.SetRect(100,t_yy,400,40);
			this.slider_bgm.SetValue(0.0f);
			this.slider_bgm.SetButtonSize(10,80);
			this.slider_bgm.SetButtonTextureCornerSize(2);
			this.slider_bgm.SetTextureCornerSize(10);
			this.slider_bgm.SetBgNormalTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_bgm.SetBgLockTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_bgm.SetValueNormalTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_bgm.SetValueLockTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_bgm.SetBgNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_bgm.SetBgLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.slider_bgm.SetValueNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.slider_bgm.SetValueLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
			this.slider_bgm.SetButtonNormalTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.slider_bgm.SetButtonLockTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.slider_bgm.SetButtonNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_bgm.SetButtonLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

			t_yy += 60;

			//スライダー。
			this.slider_se = new Fee.Ui.Slider(this.deleter,0);
			this.slider_se.SetOnSliderChangeValue(this,SliderId.Se);
			this.slider_se.SetRect(100,t_yy,400,40);
			this.slider_se.SetValue(0.0f);
			this.slider_se.SetButtonSize(10,80);
			this.slider_se.SetButtonTextureCornerSize(2);
			this.slider_se.SetTextureCornerSize(10);
			this.slider_se.SetBgNormalTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_se.SetBgLockTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_se.SetValueNormalTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_se.SetValueLockTexture(this.prefablist.GetTexture("UI_SLIDER"));
			this.slider_se.SetBgNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_se.SetBgLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.slider_se.SetValueNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.slider_se.SetValueLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
			this.slider_se.SetButtonNormalTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.slider_se.SetButtonLockTexture(this.prefablist.GetTexture("UI_BUTTON"));
			this.slider_se.SetButtonNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.slider_se.SetButtonLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

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

					GameObject t_prefab = UnityEngine.Resources.Load<GameObject>("PrefabList");
					Fee.Instantiate.PrefabList t_prefablist = new Fee.Instantiate.PrefabList(t_prefab.GetComponent<Fee.Instantiate.PrefabList_MonoBehaviour>());
					Fee.Instantiate.AudioClipList_MonoBehaviour t_audioclip_list = t_prefablist.GetPrefab("TEST11_SE").GetComponent<Fee.Instantiate.AudioClipList_MonoBehaviour>();
					Fee.Instantiate.AudioVolumeList_MonoBehaviour t_audiovolume_list = t_prefablist.GetPrefab("TEST11_SE").GetComponent<Fee.Instantiate.AudioVolumeList_MonoBehaviour>();

					if(t_audioclip_list != null){

						//ボリュームリスト。作成。
						System.Collections.Generic.Dictionary<string,float> t_volume_list = new Dictionary<string, float>();
						if(t_audiovolume_list != null){
							for(int ii=0;ii<t_audiovolume_list.tag_list.Length;ii++){
								t_volume_list.Add(t_audiovolume_list.tag_list[ii],t_audiovolume_list.audiovolume_list[ii]);
							}
						}

						//パック作成。
						Fee.Audio.Pack_AudioClip t_pack = new Fee.Audio.Pack_AudioClip();
						for(int ii=0;ii<t_audioclip_list.tag_list.Length;ii++){
							UnityEngine.AudioClip t_audioclip = t_audioclip_list.audioclip_list[ii];

							float t_volume;
							if(t_volume_list.TryGetValue(t_audioclip_list.tag_list[ii],out t_volume) == false){
								t_volume = 1.0f;
							}

							t_pack.audioclip_list.Add(t_audioclip);
							t_pack.volume_list.Add(t_volume);
						}

						//ロード。
						Fee.Audio.Audio.GetInstance().LoadSe(new Fee.Audio.Bank(t_pack),SE_ID);
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

					GameObject t_prefab = UnityEngine.Resources.Load<GameObject>("PrefabList");
					Fee.Instantiate.PrefabList t_prefablist = new Fee.Instantiate.PrefabList(t_prefab.GetComponent<Fee.Instantiate.PrefabList_MonoBehaviour>());
					Fee.Instantiate.AudioClipList_MonoBehaviour t_audioclip_list = t_prefablist.GetPrefab("TEST11_BGM").GetComponent<Fee.Instantiate.AudioClipList_MonoBehaviour>();
					Fee.Instantiate.AudioVolumeList_MonoBehaviour t_audiovolume_list = t_prefablist.GetPrefab("TEST11_BGM").GetComponent<Fee.Instantiate.AudioVolumeList_MonoBehaviour>();

					if(t_audioclip_list != null){

						//ボリュームリスト。作成。
						System.Collections.Generic.Dictionary<string,float> t_volume_list = new Dictionary<string, float>();
						if(t_audiovolume_list != null){
							for(int ii=0;ii<t_audiovolume_list.tag_list.Length;ii++){
								t_volume_list.Add(t_audiovolume_list.tag_list[ii],t_audiovolume_list.audiovolume_list[ii]);
							}
						}

						//パック作成。
						Fee.Audio.Pack_AudioClip t_pack = new Fee.Audio.Pack_AudioClip();
						for(int ii=0;ii<t_audioclip_list.tag_list.Length;ii++){
							UnityEngine.AudioClip t_audioclip = t_audioclip_list.audioclip_list[ii];

							float t_volume;
							if(t_volume_list.TryGetValue(t_audioclip_list.tag_list[ii],out t_volume) == false){
								t_volume = 1.0f;
							}

							t_pack.audioclip_list.Add(t_audioclip);
							t_pack.volume_list.Add(t_volume);
						}

						//ロード。
						Fee.Audio.Audio.GetInstance().LoadBgm(new Fee.Audio.Bank(t_pack));

						//再生。
						Fee.Audio.Audio.GetInstance().PlayBgm(0);
					}
				}break;
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//ファイル。
			Fee.File.File.GetInstance().Main();

			//サウンドプール。
			Fee.SoundPool.SoundPool.GetInstance().Main();

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(this.is_focus,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

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
							this.status.SetText("soundpool : " + this.soundpool_loaditem.GetResultProgress().ToString());
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
									Fee.Audio.Audio.GetInstance().LoadSe(new Fee.Audio.Bank(t_pack_soundpool),SE_ID);
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
			if(Fee.Geometry.Range.IsRectIn(in Fee.Render2D.Render2D.VIRTUAL_RECT_MAX,in Fee.Input.Mouse.GetInstance().cursor.pos) == true){
				if(Fee.Input.Mouse.GetInstance().left.down == true){
					Fee.Audio.Audio.GetInstance().PlaySe(SE_ID,0);
				}
			}

			this.status_2.SetText("soundpool = " + Fee.Audio.Audio.GetInstance().GetSoundPoolCount().ToString());

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

