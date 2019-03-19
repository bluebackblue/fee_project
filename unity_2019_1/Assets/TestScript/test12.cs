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


/** test12
*/
public class test12 : main_base
{
	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** root
	*/
	private Transform root;

	/**
	*/
	private int pos_m;
	private int pos_v;
	private int pos_u;

	/**
	*/
	private class DataBase_Block
	{
	}

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

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//find
		GameObject t_prefab_sphere = Resources.Load<GameObject>("sphere");
		GameObject t_prefab_cube = Resources.Load<GameObject>("cube");

		//root
		{
			GameObject t_gameobject = GameObject.Instantiate<GameObject>(t_prefab_sphere,new Vector3(0,0,0),Quaternion.identity);
			this.root = t_gameobject.GetComponent<Transform>();
		}

		//a
		{
			GameObject t_gameobject = GameObject.Instantiate<GameObject>(t_prefab_cube,new Vector3(0,0,0),Quaternion.identity);
			t_gameobject.name = "a";
			Transform t_transform = t_gameobject.GetComponent<Transform>();
			t_transform.SetParent(this.root);
			t_transform.localPosition = new Vector3(0,0.5f,0);
			t_transform.localScale = new Vector3(0.1f,0.1f,0.1f);
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

