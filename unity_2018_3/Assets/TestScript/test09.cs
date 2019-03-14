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


/** test09
*/
public class test09 : main_base
{
	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** text
	*/
	private Fee.Render2D.Text2D text;

	/** item
	*/
	private Fee.File.Item item;

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

		//マウス。インスタンス作成。
		Fee.Input.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		Fee.EventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		//Fee.Ui.Config.LOG_ENABLE = true;
		Fee.Ui.Ui.CreateInstance();

		//ファイル。インスタンス作成。
		Fee.File.File.CreateInstance();

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//text
		this.text = new Fee.Render2D.Text2D(this.deleter,null,0);
		this.text.SetRect(100,100,0,0);
		this.text.SetText("---");

		//item
		this.item = Fee.File.File.GetInstance().RequestDownLoadTextFile("https://bbbproject.sakura.ne.jp/www/project_discord/main/",null,Fee.File.ProgressMode.DownLoad);
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

		//ファイル。
		Fee.File.File.GetInstance().Main();

		if(this.item != null){
			if(this.item.IsBusy() == true){
			}else{
				if(this.item.GetResultType() == Fee.File.Item.ResultType.Text){
					this.text.SetText(this.item.GetResultText());
				}else{
					this.text.SetText("error");
				}
				this.item = null;
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

