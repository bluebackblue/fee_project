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


/** test02

	セーブロード
	オブジェクトをＪＳＯＮにコンバート
	ＪＳＯＮをオブジェクトにコンバート

*/
public class test02 : main_base
{
	/** SaveData
	*/
	private class SaveData
	{
		/** セーブから除外。
		*/
		[Fee.JsonItem.Ignore]
		public int ignore;

		/** SubSubData
		*/
		public class SubSubData
		{
			public int a;
		}

		/** SubData
		*/
		public class SubData
		{
			public int a;
			public SubSubData subsub;
		}

		/** MainData
		*/
		public struct MainData
		{
			public int a;
			public SubData sub;
		}

		/** Item
		*/
		public class Item
		{
			public SubSubData subsub;

			/** constructor
			*/
			public Item()
			{
				this.subsub = null;
			}

			/** constructor
			*/
			public Item(int a_a)
			{
				this.subsub = new SubSubData();
				this.subsub.a = a_a;
			}
		}

		/** maindata
		*/
		public MainData maindata;

		/** data_dictionary
		*/
		public Dictionary<string,Item> data_dictionary;

		/** data_list
		*/
		public List<Item> data_list;
	}

	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** ボタン。
	*/
	private Fee.Ui.Button button_save1;
	private Fee.Ui.Button button_save2;
	private Fee.Ui.Button button_load1;
	private Fee.Ui.Button button_load2;
	private Fee.Ui.Button button_random;

	/** ステータス。
	*/
	private Fee.Render2D.Text2D status;

	/** セーブ。
	*/
	private Fee.File.Item save_item;

	/** ロード。
	*/
	private Fee.File.Item load_item;

	/** セーブデータ。
	*/
	private SaveData savedata = null;

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

		//ＵＩ。インスタンス作成。
		Fee.Ui.Ui.CreateInstance();

		//マウス。インスタンス作成。
		Fee.Input.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		Fee.EventPlate.EventPlate.CreateInstance();

		//ファイル。インスタンス作成。
		Fee.File.File.CreateInstance();

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//ボタン。
		{
			this.button_save1 = new Fee.Ui.Button(this.deleter,null,0,this.CallBack_Click_Save,1);
			this.button_save1.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_save1.SetRect(100 + 110 * 0,100,100,50);
			this.button_save1.SetText("Save1");

			this.button_save2 = new Fee.Ui.Button(this.deleter,null,0,this.CallBack_Click_Save,2);
			this.button_save2.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_save2.SetRect(100 + 110 * 1,100,100,50);
			this.button_save2.SetText("Save2");

			this.button_load1 = new Fee.Ui.Button(this.deleter,null,0,this.CallBack_Click_Load,1);
			this.button_load1.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_load1.SetRect(100 + 110 * 2,100,100,50);
			this.button_load1.SetText("Load1");

			this.button_load2 = new Fee.Ui.Button(this.deleter,null,0,this.CallBack_Click_Load,2);
			this.button_load2.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_load2.SetRect(100 + 110 * 3,100,100,50);
			this.button_load2.SetText("Load2");

			this.button_random = new Fee.Ui.Button(this.deleter,null,0,this.CallBack_Click_Random,-1);
			this.button_random.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_random.SetRect(600,100,100,50);
			this.button_random.SetText("Random");
		}

		//ステータス。
		{
			this.status = new Fee.Render2D.Text2D(this.deleter,null,0);
			this.status.SetRect(100,200,0,0);
			this.status.SetText("");
		}

		//セーブ。
		this.save_item = null;

		//ロード。
		this.load_item = null;

		//セーブデータ。
		this.savedata = null;
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click_Save(int a_id)
	{
		if(this.savedata != null){
			//オブジェクトをＪＳＯＮ化。
			Fee.JsonItem.JsonItem t_jsonitem = Fee.JsonItem.ObjectToJson.Convert(this.savedata);

			//ＪＳＯＮを文字列化。
			string t_jsonstring = t_jsonitem.ConvertJsonString();

			//ローカルセーブリクエスト。
			this.save_item = Fee.File.File.GetInstance().RequestSaveLocalTextFile("save_" + a_id.ToString() + ".json",t_jsonstring);
			if(this.save_item != null){
				this.button_save1.SetLock(true);
				this.button_save2.SetLock(true);
				this.button_load1.SetLock(true);
				this.button_load2.SetLock(true);
				this.button_random.SetLock(true);
			}
		}
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click_Load(int a_id)
	{
		//ローカルロードリクエスト。
		this.load_item = Fee.File.File.GetInstance().RequestLoadLocalTextFile("save_" + a_id.ToString() + ".json");
		if(this.load_item != null){
			this.button_save1.SetLock(true);
			this.button_save2.SetLock(true);
			this.button_load1.SetLock(true);
			this.button_load2.SetLock(true);
			this.button_random.SetLock(true);
		}
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click_Random(int a_id)
	{
		this.savedata = new SaveData();
		this.savedata.ignore = Random.Range(0,9999);

		this.savedata.maindata.a = Random.Range(0,9999);
		this.savedata.maindata.sub = new SaveData.SubData();
		this.savedata.maindata.sub.a = Random.Range(0,9999);
		this.savedata.maindata.sub.subsub = new SaveData.SubSubData();
		this.savedata.maindata.sub.subsub.a = Random.Range(0,9999);

		this.savedata.data_dictionary = new Dictionary<string,SaveData.Item>();
		this.savedata.data_dictionary.Add("a",new SaveData.Item(Random.Range(0,9999)));

		this.savedata.data_list = new List<SaveData.Item>();
		this.savedata.data_list.Add(new SaveData.Item(Random.Range(0,9999)));

		this.SetStatus("Random",this.savedata);
	}

	/** ステータス表示。
	*/
	private void SetStatus(string a_message,SaveData a_data)
	{
		string t_text = "";

		t_text += a_message + "\n";

		if(a_data != null){

			t_text += "ignore = " + a_data.ignore.ToString() + "\n";

			t_text += "maindata.a = " + a_data.maindata.a.ToString() + "\n";
			t_text += "maindata.sub.a = " + a_data.maindata.sub.a.ToString() + "\n";
			if(a_data.maindata.sub.subsub != null){
				t_text += "maindata.sub.sub.a = " + a_data.maindata.sub.subsub.a.ToString() + "\n";
			}
			
			if(a_data.data_dictionary != null){
				foreach(KeyValuePair<string,SaveData.Item> t_pair in a_data.data_dictionary){
					if(t_pair.Value.subsub != null){
						t_text += t_pair.Key.ToString() + " = " +  t_pair.Value.subsub.a.ToString() + "\n";
					}
				}
			}
			if(a_data.data_list != null){
				for(int ii=0;ii<a_data.data_list.Count;ii++){
					if(a_data.data_list[ii].subsub != null){
						t_text += "[" + ii.ToString() + "] = " + a_data.data_list[ii].subsub.a.ToString() + "\n";
					}
				}
			}
		}

		this.status.SetText(t_text);
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//ＵＩ。
		Fee.Ui.Ui.GetInstance().Main();

		//マウス。
		Fee.Input.Mouse.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());

		//イベントプレート。
		Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

		//ファイル。
		Fee.File.File.GetInstance().Main();

		if(this.load_item != null){
			if(this.load_item.IsBusy() == true){
				//ロード中。
			}else{
				if(this.load_item.GetResultType() != Fee.File.Item.ResultType.Text){
					//ロード失敗。
					this.SetStatus("Load : Faild",this.savedata);
				}else{
					//ロード成功。
					SaveData t_savedata = null;

					string t_jsonstring = this.load_item.GetResultText();
					if(t_jsonstring != null){
						//文字列をＪＳＯＮ化。
						Fee.JsonItem.JsonItem t_jsonitem = new Fee.JsonItem.JsonItem(t_jsonstring);
						if(t_jsonitem != null){
							//ＪＳＯＮをオブジェクト化。
							t_savedata = Fee.JsonItem.JsonToObject<SaveData>.Convert(t_jsonitem);
						}
					}

					if(t_savedata != null){
						this.savedata = t_savedata;
						this.SetStatus("Load : Success",this.savedata);
					}else{
						this.SetStatus("Load : Convert Error",this.savedata);
					}
				}

				this.button_save1.SetLock(false);
				this.button_save2.SetLock(false);
				this.button_load1.SetLock(false);
				this.button_load2.SetLock(false);
				this.button_random.SetLock(false);

				this.load_item = null;
			}
		}

		if(this.save_item != null){
			if(this.save_item.IsBusy() == true){
				//セーブ中。
			}else{
				if(this.save_item.GetResultType() == Fee.File.Item.ResultType.SaveEnd){
					//セーブ成功。
					this.SetStatus("Save : Success",this.savedata);
				}else{
					//セーブ失敗。
					this.SetStatus("Save : Faild",this.savedata);
				}

				this.button_save1.SetLock(false);
				this.button_save2.SetLock(false);
				this.button_load1.SetLock(false);
				this.button_load2.SetLock(false);
				this.button_random.SetLock(false);

				this.save_item = null;
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

