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


/** test04

	バイナリ。セーブ。ロード。
	テキスト。セーブ。ロード。
	ＰＮＧ。セーブ。ロード。

*/
public class test04 : main_base
{
	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** CallBackId
	*/
	private enum CallBackId
	{
		DownLoad_SoundPool,
		LoadLocal_SoundPool,
		SaveLocal_SoundPool,

		/*
		SaveBinaryNow,
		LoadBinaryNow,

		SaveTextNow,
		LoadTextNow,

		SavePngNow,
		LoadPngNow,

		*/
	}

	/** status
	*/
	private Fee.Render2D.Text2D status;

	/** download_soundpool_button
	*/
	private Fee.Ui.Button download_soundpool_button;

	/** loadlocal_soundpool_button
	*/
	private Fee.Ui.Button loadlocal_soundpool_button;

	/** file_item
	*/
	private Fee.File.Item file_item;

	/** soundpool_item
	*/
	private Fee.SoundPool.Item soundpool_item;

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
		Fee.Render2D.Render2D.CreateInstance();

		//ファイル。インスタンス作成。
		Fee.File.Config.LOG_ENABLE = true;
		Fee.File.File.CreateInstance();

		//マウス。インスタンス作成。
		Fee.Input.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		Fee.EventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		Fee.Ui.Ui.CreateInstance();

		//サウンドプール。インスタンス作成。
		Fee.SoundPool.Config.LOG_ENABLE = true;
		Fee.SoundPool.Config.USE_DOWNLOAD_SOUNDPOOL_CACHE = false;
		Fee.SoundPool.SoundPool.CreateInstance();
	
		//deleter
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//drawpriority
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;

		//status
		this.status = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
		this.status.SetRect(Fee.Render2D.Config.VIRTUAL_W/2,50,0,0);
		this.status.SetCenter(true,false);
		this.status.SetFontSize(9);

		int t_y = 100;

		//download_soundpool_button
		this.download_soundpool_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.DownLoad_SoundPool);
		this.download_soundpool_button.SetRect(50,t_y,170,30);
		this.download_soundpool_button.SetTexture(Resources.Load<Texture2D>("button"));
		this.download_soundpool_button.SetText("DownLoad SoundPool");
		this.download_soundpool_button.SetFrontSize(15);

		t_y += 35;

		//loadlocal_soundpool_button
		this.loadlocal_soundpool_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click,(int)CallBackId.LoadLocal_SoundPool);
		this.loadlocal_soundpool_button.SetRect(50,t_y,170,30);
		this.loadlocal_soundpool_button.SetTexture(Resources.Load<Texture2D>("button"));
		this.loadlocal_soundpool_button.SetText("LoadLocal SoundPool");
		this.loadlocal_soundpool_button.SetFrontSize(15);

		//file_item
		this.file_item = null;
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click(int a_id)
	{
		switch((CallBackId)a_id){
		case CallBackId.DownLoad_SoundPool:
			{
				//ダウンロード。サウンドプール。

				uint t_data_version = 1;
				this.soundpool_item = Fee.SoundPool.SoundPool.GetInstance().RequestDownLoadSoundPool(new Fee.File.Path("https://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/Raw/","se.txt"),null,t_data_version);
			}break;
		case CallBackId.LoadLocal_SoundPool:
			{
				//ロードローカル。サウンドプール。

				this.soundpool_item = Fee.SoundPool.SoundPool.GetInstance().RequestLoadLocalSoundPool(new Fee.File.Path("se.txt"));
			}break;
		case CallBackId.SaveLocal_SoundPool:
			{
				//サーブローカル。サウンドプール。

				Fee.Audio.Pack_SoundPool t_soundpool = new Fee.Audio.Pack_SoundPool();
				{
					t_soundpool.data_hash = 0;
					t_soundpool.data_version = 1;
				}

				this.soundpool_item = Fee.SoundPool.SoundPool.GetInstance().RequestSaveLocalSoundPool(new Fee.File.Path("se.txt"),t_soundpool);
			}break;
		}
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//ファイル。
		Fee.File.File.GetInstance().Main();

		//マウス。インスタンス作成。
		Fee.Input.Mouse.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());

		//イベントプレート。インスタンス作成。
		Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

		//ＵＩ。インスタンス作成。
		Fee.Ui.Ui.GetInstance().Main();

		//サウンドプール。インスタンス作成。
		Fee.SoundPool.SoundPool.GetInstance().Main();

		if(this.soundpool_item != null){
			if(this.soundpool_item.IsBusy() == true){
				this.status.SetText("処理中");
			}else{
				switch(this.soundpool_item.GetResultType()){
				case Fee.SoundPool.Item.ResultType.Error:
					{
						this.status.SetText("結果:エラー : \n" + this.soundpool_item.GetResultErrorString());
					}break;
				case Fee.SoundPool.Item.ResultType.None:
					{
						this.status.SetText("結果:なし");
					}break;
				case Fee.SoundPool.Item.ResultType.SaveEnd:
					{
						this.status.SetText("結果:セーブ完了");
					}break;
				case Fee.SoundPool.Item.ResultType.SoundPool:
					{
						this.status.SetText("結果:サウンドプール取得");
					}break;
				}
				this.soundpool_item = null;
			}
		}

		/*
		switch(this.step){
		case Step.Start:
			{
				{
					string t_log_text = this.step.ToString();
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text);
				}

				this.step = Step.SaveBinaryStart;
			}break;
		case Step.SaveBinaryStart:
			{
				//ファイル名。
				string t_filename = "test_binary.bin";

				//データ。
				byte[] t_binary = new byte[1 * 1024 * 1024 * 100];
				for(int ii=0;ii<t_binary.Length;ii++){
					t_binary[ii] = (byte)(ii % 256);
				}

				this.saveload_item = Fee.File.File.GetInstance().RequestSaveLocalBinaryFile(t_filename,t_binary);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename + " : size = " + t_binary.Length.ToString();
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text);
				}

				this.step = Step.SaveBinaryNow;
			}break;
		case Step.SaveBinaryNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//セーブ中。
				
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultType() == Fee.File.Item.ResultType.SaveEnd){
						//成功。

						{
							string t_log_text = this.step.ToString() + " : Success";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.LoadBinaryStart;
					}else{
						//失敗。

						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.LoadBinaryStart:
			{
				//ファイル名。
				string t_filename = "test_binary.bin";

				this.saveload_item = Fee.File.File.GetInstance().RequestLoadLocalBinaryFile(t_filename);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename;
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text); 
				}

				this.step = Step.LoadBinaryNow;
			}break;
		case Step.LoadBinaryNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//ロード中。
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultType() == Fee.File.Item.ResultType.Binary){
						//成功。

						//チェック。
						bool t_error = false;
						byte[] t_binary = this.saveload_item.GetResultBinary();

						for(int ii=0;ii<t_binary.Length;ii++){
							if(t_binary[ii] != (byte)(ii % 256)){
								t_error = true;
							}
						}

						{
							string t_log_text = this.step.ToString() + " : Success : size = " +  this.saveload_item.GetResultBinary().Length.ToString() + " error = " + t_error.ToString();
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.SaveTextStart;
					}else{
						//失敗。

						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.SaveTextStart:
			{
				//ファイル名。
				string t_filename = "test_text.txt";

				//データ。
				string t_text = Random.value.ToString();

				this.saveload_item = Fee.File.File.GetInstance().RequestSaveLocalTextFile(t_filename,t_text);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename + " : text = " + t_text;
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text); 
				}

				this.step = Step.SaveTextNow;
			}break;
		case Step.SaveTextNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//セーブ中。
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultType() == Fee.File.Item.ResultType.SaveEnd){
						//成功。

						{
							string t_log_text = this.step.ToString() + " : Success";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.LoadTextStart;
					}else{
						//失敗。

						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.LoadTextStart:
			{
				//ファイル名。
				string t_filename = "test_text.txt";

				this.saveload_item = Fee.File.File.GetInstance().RequestLoadLocalTextFile(t_filename);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename;
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text); 
				}

				this.step = Step.LoadTextNow;
			}break;
		case Step.LoadTextNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//ロード中。
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultType() == Fee.File.Item.ResultType.Text){
						//成功。

						{
							string t_log_text = this.step.ToString() + " : Success : = " + this.saveload_item.GetResultText();
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.SavePngStart;
					}else{
						//失敗。
						
						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.SavePngStart:
			{
				//ファイル名。
				string t_filename = "test_png.png";

				Texture2D t_texture = new Texture2D(64,64);
				{
					t_texture.filterMode = FilterMode.Point;
					t_texture.wrapMode = TextureWrapMode.Clamp;
					for(int xx=0;xx<t_texture.width;xx++){
						for(int yy=0;yy<t_texture.height;yy++){
							t_texture.SetPixel(xx,yy,new Color((float)xx / t_texture.width,(float)yy / t_texture.height,0.0f,(float)xx / t_texture.width));
						}
					}
					t_texture.Apply();
				}

				this.saveload_item = Fee.File.File.GetInstance().RequestSaveLocalTextureFile(t_filename,t_texture);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename;
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text); 
				}

				this.step = Step.SavePngNow;
			}break;
		case Step.SavePngNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//セーブ中。
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultType() == Fee.File.Item.ResultType.SaveEnd){
						//成功。

						{
							string t_log_text = this.step.ToString() + " : Success";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.LoadPngStart;
					}else{
						//失敗。

						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.LoadPngStart:
			{
				//ファイル名。
				string t_filename = "test_png.png";

				this.saveload_item = Fee.File.File.GetInstance().RequestLoadLocalTextureFile(t_filename);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename;
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text); 
				}

				this.step = Step.LoadPngNow;
			}break;
		case Step.LoadPngNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//ロード中。
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultType() == Fee.File.Item.ResultType.Texture){
						//成功。

						Texture2D t_load_texture = this.saveload_item.GetResultTexture();
						{
							t_load_texture.filterMode = FilterMode.Point;
							t_load_texture.wrapMode = TextureWrapMode.Clamp;
						}
						this.sprite.SetTexture(t_load_texture);

						{
							string t_log_text = this.step.ToString() + " : success";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}else{
						//失敗。

						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.End:
			{
				this.step = Step.None;
			}break;
		}
		*/
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

