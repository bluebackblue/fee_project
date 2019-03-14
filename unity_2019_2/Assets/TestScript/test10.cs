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


/** test10

	ブラー

*/
public class test10 : main_base
{
	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** sprite
	*/
	private Fee.Render2D.Sprite2D sprite;

	/** Mode
	*/
	private enum Mode
	{
		None,
		Blur,
		Bloom,
		BlurBloom,
	}

	/** mode
	*/
	private Mode mode;

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
		Fee.Render2D.Render2D.CreateInstance();

		//ブラー。インスタンス作成。
		Fee.Blur.Blur.CreateInstance();

		//ブルーム。インスタンス作成。
		Fee.Bloom.Bloom.CreateInstance();

		//マウス。インスタンス作成。
		Fee.Input.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		Fee.EventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		//Fee.Ui.Config.LOG_ENABLE = true;
		Fee.Ui.Ui.CreateInstance();

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//スプライト。
		{
			int t_w = 200;
			int t_h = 200;
			int t_x = (Fee.Render2D.Render2D.VIRTUAL_W - t_w) / 2;
			int t_y = (Fee.Render2D.Render2D.VIRTUAL_H - t_h) / 2;

			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;
			this.sprite = new Fee.Render2D.Sprite2D(this.deleter,null,t_drawpriority);
			this.sprite.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite.SetRect(t_x,t_y,t_w,t_h);
			this.sprite.SetTexture(Resources.Load<Texture2D>("IMGP8657"));
		}

		this.mode = Mode.None;
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

		if(Fee.Input.Mouse.GetInstance().left.down == true){
			if(Fee.Input.Mouse.GetInstance().InRectCheck(ref Fee.Render2D.Render2D.VIRTUAL_RECT_MAX)){
				switch(this.mode){
				case Mode.None:			this.mode = Mode.Blur;		break;
				case Mode.Blur:			this.mode = Mode.Bloom;		break;
				case Mode.Bloom:		this.mode = Mode.BlurBloom;	break;
				case Mode.BlurBloom:	this.mode = Mode.None;		break;
				}

				switch(this.mode){
				case Mode.None:
					{
						Fee.Blur.Blur.GetInstance().SetEnable(false);
						Fee.Bloom.Bloom.GetInstance().SetEnable(false);
					}break;
				case Mode.Blur:
					{
						Fee.Blur.Blur.GetInstance().SetEnable(true);
						Fee.Bloom.Bloom.GetInstance().SetEnable(false);
					}break;
				case Mode.Bloom:
					{
						Fee.Blur.Blur.GetInstance().SetEnable(false);
						Fee.Bloom.Bloom.GetInstance().SetEnable(true);
					}break;
				case Mode.BlurBloom:
					{
						Fee.Blur.Blur.GetInstance().SetEnable(true);
						Fee.Bloom.Bloom.GetInstance().SetEnable(true);
					}break;
				}
			}
		}
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

