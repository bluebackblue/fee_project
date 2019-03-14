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


/** test08

	ディレクトリ探査

*/
public class test08 : main_base
{
	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		Fee.TaskW.TaskW.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		Fee.Render2D.Render2D.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		Fee.PerformanceCounter.Config.LOG_ENABLE = true;
		Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

		//マウス。インスタンス作成。
		Fee.Input.Config.LOG_ENABLE = true;
		Fee.Input.Mouse.CreateInstance();

		//イベントプレート。
		Fee.EventPlate.Config.LOG_ENABLE = true;
		Fee.EventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		Fee.Ui.Config.LOG_ENABLE = true;
		Fee.Ui.Ui.CreateInstance();

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		Fee.Directory.Item t_item_root = Fee.Directory.Directory.GetDirectoryItem(Application.dataPath);

		//ルートのフルパス。
		Debug.Log(t_item_root.GetRoot().GetFullPath());

		Debug.Log("----------");

		//ルート。
		{
			List<Fee.Directory.Item> t_directory_list = t_item_root.GetDirectoryList();
			for(int ii=0;ii<t_directory_list.Count;ii++){
				Debug.Log(t_directory_list[ii].GetName());
			}
			Debug.Log("----------");
		}

		//ルート => Fee
		Fee.Directory.Item t_item_root_fee = t_item_root.FindDirectory("Fee");
		{
			if(t_item_root_fee != null){
				List<Fee.Directory.Item> t_directory_list = t_item_root_fee.GetDirectoryList();
				for(int ii=0;ii<t_directory_list.Count;ii++){
					Debug.Log(t_directory_list[ii].GetName());
				}
				Debug.Log("----------");
			}
		}

		//ルート => Fee => フォント。
		Fee.Directory.Item t_item_root_fee_font = t_item_root_fee.FindDirectory("Font");
		{
			if(t_item_root_fee_font != null){
				List<Fee.Directory.Item> t_file_list = t_item_root_fee_font.GetFileList();
				for(int ii=0;ii<t_file_list.Count;ii++){
					Debug.Log(t_file_list[ii].GetName());
				}
				Debug.Log("----------");
			}
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

