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

		オーディオクリップパックのアセットバンドル作成
		サウンドプールパックの作成

		オーディオクリップパックのアセットバンドルロード
		サウンドプールパックのロード

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

		/** DATA_VERSION
		*/
		private const int DATA_VERSION = 5;

		/** ASSETBUNDLE_ID_BGM
		*/
		private const long ASSETBUNDLE_ID_BGM = 0x00000001;

		/** ASSETBUNDLE_ID_SE
		*/
		private const long ASSETBUNDLE_ID_SE = 0x00000002;

		/** SE_ID
		*/
		private const long SE_ID = 0x00000001;

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** Mode
		*/
		private enum Mode
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

			/** Fix
			*/
			Fix,
		};

		private Mode mode;

		/** soundpool_flag
		*/
		private bool soundpool_flag;

		/** ロード。
		*/
		private Fee.File.Item load_item_se_binary;

		/** ロード。
		*/
		private Fee.File.Item load_item_bgm_binary;

		/** ロードサウンドプール。
		*/
		private Fee.SoundPool.Item load_item_se_soundpool;

		/** オーディオクリップパック。
		*/
		private Fee.Audio.Pack_AudioClip pack_audioclip;

		/** サウンドプールパック。
		*/
		private Fee.Audio.Pack_SoundPool pack_soundpool;

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

			//モード。
			this.mode = Mode.Wait;

			//soundpool_flag
			this.soundpool_flag = false;

			//ロード。
			this.load_item_se_binary = null;

			//ロード。
			this.load_item_bgm_binary = null;

			//ロードサウンドプール。
			this.load_item_se_soundpool = null;

			//パック。
			this.pack_audioclip = null;

			//パック。
			this.pack_soundpool = null;

			//ステータス。
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
			this.button_unload.SetTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_unload.SetRect(t_xx,130,170,30);
			this.button_unload.SetText("アンロード");

			t_xx += 210;

			//ボタン。
			this.button_assetbundle = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click_AssetBundle,-1);
			this.button_assetbundle.SetTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_assetbundle.SetRect(t_xx,130,170,30);
			this.button_assetbundle.SetText("AssetBundleロード");

			t_xx += 210;

			//ボタン。
			this.button_soundpool = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click_SoundPool,-1);
			this.button_soundpool.SetTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_soundpool.SetRect(t_xx,130,170,30);
			this.button_soundpool.SetText("SoundPoolロード");

			t_xx += 210;

			//ボタン。
			this.button_bgm = new Fee.Ui.Button(this.deleter,0,this.CallBack_Click_Bgm,-1);
			this.button_bgm.SetTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.button_bgm.SetRect(t_xx,130,170,30);
			this.button_bgm.SetText("ＢＧＭ");

			int t_yy = 300;

			//スライダー。
			this.slider_master = new Fee.Ui.Slider(this.deleter,0,this.CallBack_Change_Master,0);
			this.slider_master.SetRect(100,t_yy,400,40);
			this.slider_master.SetValue(0.0f);
			this.slider_master.SetButtonTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.slider_master.SetButtonSize(10,80);
			this.slider_master.SetButtonTextureCornerSize(2);
			this.slider_master.SetTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));

			t_yy += 60;

			//スライダー。
			this.slider_bgm = new Fee.Ui.Slider(this.deleter,0,this.CallBack_Change_Bgm,0);
			this.slider_bgm.SetRect(100,t_yy,400,40);
			this.slider_bgm.SetValue(0.0f);
			this.slider_bgm.SetButtonTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.slider_bgm.SetButtonSize(10,80);
			this.slider_bgm.SetButtonTextureCornerSize(2);
			this.slider_bgm.SetTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));

			t_yy += 60;

			//スライダー。
			this.slider_se = new Fee.Ui.Slider(this.deleter,0,this.CallBack_Change_Se,0);
			this.slider_se.SetRect(100,t_yy,400,40);
			this.slider_se.SetValue(0.0f);
			this.slider_se.SetButtonTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.slider_se.SetButtonSize(10,80);
			this.slider_se.SetButtonTextureCornerSize(2);
			this.slider_se.SetTexture(Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_SLIDER));

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

			//アセットバンドルのアンロード。
			Fee.File.File.GetInstance().GetAssetBundleList().UnloadAssetBundle(ASSETBUNDLE_ID_SE);
			Fee.File.File.GetInstance().GetAssetBundleList().UnloadAssetBundle(ASSETBUNDLE_ID_BGM);
		}

		/** [Button_Base]コールバック。クリック。
		*/
		private void CallBack_Click_AssetBundle(int a_id)
		{
			if(this.mode == Mode.Wait){
				this.soundpool_flag = false;
				this.mode = Mode.Start;
			}
		}

		/** [Button_Base]コールバック。クリック。
		*/
		private void CallBack_Click_SoundPool(int a_id)
		{
			if(this.mode == Mode.Wait){
				this.soundpool_flag = true;
				this.mode = Mode.Start;
			}
		}

		/** [Button_Base]コールバック。クリック。
		*/
		private void CallBack_Click_Bgm(int a_id)
		{
			if(this.load_item_bgm_binary == null){

				//バイナリ。ＢＧＭ。

				#if((UNITY_STANDALONE_WIN)||(UNITY_EDITOR_WIN))
				Fee.File.Path t_path = new Fee.File.Path(Data.StreamingAssets.BGM_STANDALONEWINDOWS);
				#elif(UNITY_WEBGL)
				Fee.File.Path t_path = new Fee.File.Path(Data.StreamingAssets.BGM_WEBGL);
				#elif(UNITY_ANDROID)
				Fee.File.Path t_path = new Fee.File.Path(Data.StreamingAssets.BGM_ANDROID);
				#else
				Fee.File.Path t_path = new Fee.File.Path(Data.StreamingAssets.BGM_STANDALONEWINDOWS);
				#endif

				this.load_item_bgm_binary = Fee.File.File.GetInstance().RequestLoadStreamingAssetsBinaryFile(t_path);
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

			//ＢＧＭ。
			if(this.load_item_bgm_binary != null){
				if(this.load_item_bgm_binary.IsBusy() == true){
					//ロード中。
					this.status.SetText("bgm : " + this.load_item_bgm_binary.GetResultProgressDown().ToString());
				}else{
					AssetBundle t_assetbundle = null;

					if(this.load_item_bgm_binary.GetResultType() == Fee.File.Item.ResultType.Binary){
						//ロード成功。バイナリ。

						if(Fee.File.File.GetInstance().GetAssetBundleList().GetAssetBundle(ASSETBUNDLE_ID_BGM) == null){
							t_assetbundle = AssetBundle.LoadFromMemory(this.load_item_bgm_binary.GetResultBinary());
							Fee.File.File.GetInstance().GetAssetBundleList().Regist(ASSETBUNDLE_ID_BGM,t_assetbundle);
						}
					}

					if(t_assetbundle != null){
						GameObject t_prefab = t_assetbundle.LoadAsset<GameObject>("bgm");
						if(t_prefab != null){
							Fee.Audio.Pack_AudioClip t_pack_audioclip = t_prefab.GetComponent<Fee.Audio.Pack_AudioClip>();
							if(t_pack_audioclip != null){
								Fee.Audio.Audio.GetInstance().LoadBgm(t_pack_audioclip);
								Fee.Audio.Audio.GetInstance().PlayBgm(0);
							}else{
								//エラー。
								GameObject t_object = Instantiate(t_prefab);
								t_object.name = "load_bgm";
							}
						}

						this.status.SetText("bgm : end");
					}else{
						this.status.SetText("bgm : error");
					}

					this.load_item_bgm_binary = null;
				}
			}

			switch(this.mode){
			case Mode.Wait:
				{
				}break;
			case Mode.Start:
				{
					if(this.soundpool_flag == true){

						//サウンドプール。ＳＥ。

						Fee.File.Path t_path = new Fee.File.Path(Data.StreamingAssets.SOUNDPOOL_SE);

						this.load_item_se_soundpool = Fee.SoundPool.SoundPool.GetInstance().RequestLoadStreamingAssetsSoundPool(t_path,DATA_VERSION);

					}else{

						//バイナリ。ＳＥ。

						#if((UNITY_STANDALONE_WIN)||(UNITY_EDITOR_WIN))
						Fee.File.Path t_path = new Fee.File.Path(Data.StreamingAssets.SE_STANDALONEWINDOWS);
						#elif(UNITY_WEBGL)
						Fee.File.Path t_path = new Fee.File.Path(Data.StreamingAssets.SE_WEBGL);
						#elif(UNITY_ANDROID)
						Fee.File.Path t_path = new Fee.File.Path(Data.StreamingAssets.SE_ANDROID);
						#else
						Fee.File.Path t_path = new Fee.File.Path(Data.StreamingAssets.SE_STANDALONEWINDOWS);
						#endif

						this.load_item_se_binary = Fee.File.File.GetInstance().RequestLoadStreamingAssetsBinaryFile(t_path);
					}

					this.mode = Mode.Now;
				}break;
			case Mode.Now:
				{
					if(this.load_item_se_soundpool != null){
						if(this.load_item_se_soundpool.IsBusy() == true){
							//ロード中。
							this.status.SetText("se : " + this.load_item_se_soundpool.GetResultProgressDown().ToString());
						}else{
							if(this.load_item_se_soundpool.GetResultType() == Fee.SoundPool.Item.ResultType.SoundPool){
								//ロード成功。サウンドプール。

								this.pack_soundpool = this.load_item_se_soundpool.GetResultSoundPool();
								if(this.pack_soundpool == null){
									//不正なサウンドプールパック。
									this.status.SetText("Error : " + this.mode.ToString());
									this.load_item_se_soundpool = null;
									this.mode = Mode.Wait;
								}else{
									this.load_item_se_soundpool = null;
									this.mode = Mode.Fix;
								}
							}else{
								//ロード失敗。
								this.status.SetText("Error : " + this.load_item_se_soundpool.GetResultErrorString());
								this.load_item_se_soundpool = null;
							}
						}
					}

					if(this.load_item_se_binary != null){
						if(this.load_item_se_binary.IsBusy() == true){
							//ロード中。
							this.status.SetText("se : " + this.load_item_se_binary.GetResultProgressDown().ToString());
						}else{
							if(this.load_item_se_binary.GetResultType() == Fee.File.Item.ResultType.Binary){
								//成功。

								//LoadFromMemory
								AssetBundle t_assetbundle = AssetBundle.LoadFromMemory(this.load_item_se_binary.GetResultBinary());

								//Regist
								Fee.File.File.GetInstance().GetAssetBundleList().Regist(ASSETBUNDLE_ID_SE,t_assetbundle);

								//Pack_AudioClip
								if(t_assetbundle != null){
									GameObject t_prefab = t_assetbundle.LoadAsset<GameObject>("se");
									if(t_prefab != null){
										this.pack_audioclip = t_prefab.GetComponent<Fee.Audio.Pack_AudioClip>();
									}
								}

								if(this.pack_audioclip == null){
									//不正なオーディオクリップパック。
									this.status.SetText("Error : " + this.mode.ToString());
									this.load_item_se_binary = null;
									this.mode = Mode.Wait;
								}else{
									this.load_item_se_binary = null;
									this.mode = Mode.Fix;
								}

							}else{
								//ロード失敗。
								this.status.SetText("Error : " + this.load_item_se_binary.GetResultErrorString());
								this.load_item_se_binary = null;
								this.mode = Mode.Wait;
							}
						}
					}
				}break;
			case Mode.Fix:
				{
					this.status.SetText("Success");

					if(this.soundpool_flag == true){
						Fee.Audio.Audio.GetInstance().LoadSe(this.pack_soundpool,SE_ID);
					}else{
						Fee.Audio.Audio.GetInstance().LoadSe(this.pack_audioclip,SE_ID);
					}

					this.pack_audioclip = null;
					this.pack_soundpool = null;
					this.mode = Mode.Wait;
				}break;
			}

			//再生。
			if(Fee.Input.Mouse.GetInstance().InRectCheck(ref Fee.Render2D.Render2D.VIRTUAL_RECT_MAX)){
				if(Fee.Input.Mouse.GetInstance().left.down == true){
					Fee.Audio.Audio.GetInstance().PlaySe(SE_ID,0);
				}
			}

			this.status_2.SetText("soundpool = " + Fee.Audio.Audio.GetInstance().GetSoundPoolCount().ToString() + " assetbundle = " + Fee.File.File.GetInstance().GetAssetBundleCount().ToString());
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

		/** ファイルコピー。
		*/
		/*
		#if(UNITY_EDITOR)
		private static void CopyFile(string a_filename,string a_path_from,string a_path_to)
		{
			byte[] t_binary = null;

			{
				System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_path_from + "/" + a_filename);
				t_binary = new byte[t_fileinfo.Length];

				System.IO.FileStream t_filestream_read = null;

				try{
					//open
					t_filestream_read = t_fileinfo.OpenRead();

					//read
					if(t_filestream_read != null){
						t_filestream_read.Read(t_binary,0,t_binary.Length);
					}
				}catch(System.Exception){
					t_binary = null;

					Debug.Assert(false);
				}

				//close
				if(t_filestream_read != null){
					t_filestream_read.Close();
					t_filestream_read = null;
				}
			}

			if(t_binary != null){
				System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_path_to + "/" + a_filename);
				System.IO.FileStream t_filestream_write = null;

				try{
					//open
					t_filestream_write = t_fileinfo.OpenWrite();

					//write
					if(t_filestream_write != null){
						t_filestream_write.Write(t_binary,0,t_binary.Length);
					}
				}catch(System.Exception){
					Debug.Assert(false);
				}

				//close
				if(t_filestream_write != null){
					t_filestream_write.Close();
					t_filestream_write = null;
				}
			}else{
				Debug.Assert(false);
			}
		}
		#endif
		*/

		/** 作成。
		*/
		/*
		#if(UNITY_EDITOR)
		[UnityEditor.MenuItem("Fee/Test/Test11/MekeSoundPool")]
		private static void MenuItem_MekeSoundPool()
		{
			string t_assetbundle_name = "se";

			Fee.Audio.Pack_SoundPool t_pack_soundpool = new Fee.Audio.Pack_SoundPool();
			GameObject t_prefab_se = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AssetBundleData/" + t_assetbundle_name + ".prefab");
			if(t_prefab_se != null){
				Fee.Audio.Pack_AudioClip t_pack_audioclip = t_prefab_se.GetComponent<Fee.Audio.Pack_AudioClip>();
				if(t_pack_audioclip != null){
					for(int ii=0;ii<t_pack_audioclip.audioclip_list.Count;ii++){

						//volume
						float t_audio_volume = 1.0f;
						if(ii<t_pack_audioclip.volume_list.Count){
							t_audio_volume = t_pack_audioclip.volume_list[ii];
						}

						//name
						string t_asset_fullpath = "";
						string t_audio_name = "";
						if(t_pack_audioclip.audioclip_list[ii] != null){
							string t_asset_path = UnityEditor.AssetDatabase.GetAssetPath(t_pack_audioclip.audioclip_list[ii]);
							if(t_asset_path != null){
								string t_name = System.IO.Path.GetFileName(t_asset_path);
								if(t_name != null){
									t_audio_name = t_name;
								}
								t_asset_fullpath = Application.dataPath + "/" + System.IO.Path.GetDirectoryName(t_asset_path).Substring(7);
							}
						}

						//volume
						t_pack_soundpool.name_list.Add(t_audio_name);

						//name
						t_pack_soundpool.volume_list.Add(t_audio_volume);

						//Rawフォルダにコピー。
						if(t_asset_fullpath != null){
							CopyFile(t_audio_name,t_asset_fullpath,Application.dataPath + "/AssetBundle/Raw");
						}
					}
				}

				t_pack_soundpool.data_hash = t_pack_soundpool.GetHashCode();
			}

			//Rawフォルダに保存。
			{
				Fee.JsonItem.JsonItem t_jsonitem = Fee.JsonItem.Convert.ObjectToJsonItem(t_pack_soundpool);
				string t_json_string = t_jsonitem.ConvertJsonString();

				System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(Application.dataPath + "/AssetBundle/Raw/" + t_assetbundle_name + ".txt");
				System.IO.StreamWriter t_stream_writer = null;

				try{
					//open
					t_stream_writer = t_fileinfo.CreateText();

					//write
					if(t_stream_writer != null){
						t_stream_writer.Write(t_json_string);
						t_stream_writer.Flush();
					}
				}catch(System.Exception){
				}

				//close
				if(t_stream_writer != null){
					t_stream_writer.Close();
					t_stream_writer = null;
				}
			}

			UnityEditor.AssetDatabase.Refresh();
		}
		#endif
		*/
	}
}

