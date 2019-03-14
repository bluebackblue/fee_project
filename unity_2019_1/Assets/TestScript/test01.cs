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


/** test01

	スプライト
	仮想スクリーンサイズと同じサイズのスプライトを設置。

	テキスト
	中央に文字を設置。

	入力フィールド
	テキストの下に入力フィールドを設置。

*/
public class test01 : main_base
{
	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** スプライト。
	*/
	private Fee.Render2D.Sprite2D sprite;

	/** テキスト。
	*/
	private Fee.Render2D.Text2D text;

	/** 入力フィールド。
	*/
	private Fee.Render2D.InputField2D inputfield;

	/** ステータス。
	*/
	private Fee.Render2D.Text2D status;

	/** ボタン。
	*/
	private Fee.Ui.Button button_log;
	private Fee.Ui.Button button_logerror;
	private Fee.Ui.Button button_assert;

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		Fee.TaskW.TaskW.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		Fee.PerformanceCounter.Config.LOG_ENABLE = true;
		Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		Fee.Render2D.Config.LOG_ENABLE = true;
		Fee.Render2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		Fee.Input.Config.LOG_ENABLE = true;
		Fee.Input.Mouse.CreateInstance();

		//イベントプレート。
		Fee.EventPlate.Config.LOG_ENABLE = true;
		Fee.EventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		Fee.Ui.Config.LOG_ENABLE = true;
		Fee.Ui.Ui.CreateInstance();

		//フォント。
		Font t_font = Resources.Load<Font>("mplus-1p-medium");
		if(t_font != null){
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
		}

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//レイヤインデックス。
		int t_layerindex = 0;

		//描画プライオリティ。
		long t_drawpriority = Fee.Render2D.Render2D.DRAWPRIORITY_STEP * t_layerindex;

		{
			//スプライト。
			this.sprite = new Fee.Render2D.Sprite2D(this.deleter,null,t_drawpriority);
			this.sprite.SetTexture(Texture2D.whiteTexture);
			this.sprite.SetTextureRect(0,0,Fee.Render2D.Render2D.TEXTURE_W,Fee.Render2D.Render2D.TEXTURE_H);
			this.sprite.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			this.sprite.SetColor(0.0f,0.5f,0.0f,1.0f);
			this.sprite.SetMaterialType(Fee.Render2D.Config.MaterialType.Simple);
		}

		{
			int t_xx = 700;
			int t_yy = 100;

			//テキスト。
			this.text = new Fee.Render2D.Text2D(this.deleter,null,t_drawpriority);
			this.text.SetRect(t_xx,t_yy,0,0);
			this.text.SetText("abcあいうえおxyz");
			this.text.SetColor(0.0f,0.0f,0.0f,1.0f);
			this.text.SetOutLineColor(1.0f,1.0f,1.0f,0.3f);
			this.text.SetOutLine(true);
			this.text.SetShadow(true);

			t_yy += 50;

			//入力フィールド。
			{
				int t_w = 200;
				int t_h = 200;
				int t_x = t_xx;
				int t_y = t_yy;
		
				this.inputfield = new Fee.Render2D.InputField2D(this.deleter,null,t_drawpriority);
				this.inputfield.SetRect(t_x,t_y,t_w,t_h);
				this.inputfield.SetText("defaultテキスト");
				this.inputfield.SetMultiLine(true);
				this.inputfield.SetImageColor(1.0f,0.6f,0.6f,1.0f);
				this.inputfield.SetTextColor(0.0f,0.0f,0.0f,0.5f);
			}
		}

		{
			string t_text = "";

			t_text += "Screen = "			+ Screen.width.ToString() + " x " + Screen.height.ToString() + "\n";
			t_text += "Data = "				+ Application.dataPath + "\n";
			t_text += "Persistent Data = "	+ Application.persistentDataPath + "\n";
			t_text += "Streaming Assets = "	+ Application.streamingAssetsPath + "\n";
			t_text += "Temporary Cache = "	+ Application.temporaryCachePath + "\n";

			#if(USE_DEF_PUN)
			t_text += "USE_DEF_PUN = true\n";
			#else
			t_text += "USE_DEF_PUN = false\n";
			#endif

			#if(USE_DEF_UNIVRM)
			t_text += "USE_DEF_UNIVRM = true\n";
			#else
			t_text += "USE_DEF_UNIVRM = false\n";
			#endif

			this.status = new Fee.Render2D.Text2D(this.deleter,null,t_drawpriority);
			this.status.SetRect(10,100,0,0);
			this.status.SetFontSize(15);
			this.status.SetText(t_text);
		}

		{
			int t_xx = 150;
			int t_yy = 10;

			this.button_log = new Fee.Ui.Button(this.deleter,null,t_drawpriority + 1,this.CallBack_Click,100);
			this.button_log.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_log.SetRect(t_xx,t_yy,80,50);
			this.button_log.SetText("Log");

			t_xx += 100;

			this.button_logerror = new Fee.Ui.Button(this.deleter,null,t_drawpriority + 1,this.CallBack_Click,200);
			this.button_logerror.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_logerror.SetRect(t_xx,t_yy,80,50);
			this.button_logerror.SetText("LogError");

			t_xx += 100;

			this.button_assert = new Fee.Ui.Button(this.deleter,null,t_drawpriority + 1,this.CallBack_Click,300);
			this.button_assert.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_assert.SetRect(t_xx,t_yy,80,50);
			this.button_assert.SetText("Assert");
		}
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click(int a_id)
	{
		switch(a_id){
		case 100:
			{
				Fee.Render2D.Tool.Log("CallBack_Click","Log");
			}break;
		case 200:
			{
				Fee.Render2D.Tool.LogError("CallBack_Click","LogError");
			}break;
		case 300:
			{
				Fee.Render2D.Tool.Assert(false);
			}break;
		}
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		Fee.Input.Mouse.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());

		//イベントプレート。
		Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

		//ＵＩ。
		Fee.Ui.Ui.GetInstance().Main();
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

