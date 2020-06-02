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
	/** test02

		セーブロード
		オブジェクトをＪＳＯＮにコンバート
		ＪＳＯＮをオブジェクトにコンバート

	*/
	public class test02 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test02.ButtonId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test02",
				"test02",

				@"
				セーブローカルテキスト
				ロードローカルテキスト
				オブジェクトをＪＳＯＮにコンバート
				ＪＳＯＮをオブジェクトにコンバート
				"
			);
		}

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
			public System.Collections.Generic.Dictionary<string,Item> data_dictionary;

			/** data_list
			*/
			public System.Collections.Generic.List<Item> data_list;
		}

		/** SpeedTest_DictionaryItem
		*/
		public class SpeedTest_DictionaryItem
		{
			/** value_int
			*/
			public int value_int;

			/** constructor
			*/
			public SpeedTest_DictionaryItem()
			{
			}

			/** constructor
			*/
			public SpeedTest_DictionaryItem(bool a_create)
			{
			}
		}

		/** SpeedTest_ListItem
		*/
		public class SpeedTest_ListItem
		{
			/** dictionary
			*/
			public System.Collections.Generic.Dictionary<string,SpeedTest_DictionaryItem> dictionary;

			/** constructor
			*/
			public SpeedTest_ListItem()
			{
			}

			/** constructor
			*/
			public SpeedTest_ListItem(bool a_create)
			{
				this.dictionary = new Dictionary<string, SpeedTest_DictionaryItem>();
				for(int ii=0;ii<20;ii++){
					this.dictionary.Add(ii.ToString(),new SpeedTest_DictionaryItem(a_create));
				}
			}
		}

		/** SpeedTest_ArrayItem
		*/
		public class SpeedTest_ArrayItem
		{
			/** list
			*/
			public System.Collections.Generic.List<SpeedTest_ListItem> list;

			/** constructor
			*/
			public SpeedTest_ArrayItem()
			{
			}

			/** constructor
			*/
			public SpeedTest_ArrayItem(bool a_create)
			{
				this.list = new List<SpeedTest_ListItem>();
				for(int ii=0;ii<20;ii++){
					this.list.Add(new SpeedTest_ListItem(a_create));
				}
			}
		}

		/** SpeedTest_Data
		*/
		private class SpeedTest_Data
		{
			/** array
			*/
			private SpeedTest_ArrayItem[] array;

			[Fee.JsonItem.Ignore]
			public int ignoremember;		

			/** constructor
			*/
			public SpeedTest_Data()
			{
			}

			/** constructor
			*/
			public SpeedTest_Data(bool a_create)
			{
				this.array = new SpeedTest_ArrayItem[20];
				for(int ii=0;ii<20;ii++){
					this.array[ii] = new SpeedTest_ArrayItem(a_create);
				}

				this.ignoremember = 0;
			}

			/** GetItem
			*/
			public SpeedTest_ArrayItem GetItem(int a_index)
			{
				return this.array[a_index];
			}
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** ボタン。
		*/
		private Fee.Ui.Button button_save1;
		private Fee.Ui.Button button_save2;
		private Fee.Ui.Button button_load1;
		private Fee.Ui.Button button_load2;
		private Fee.Ui.Button button_random;

		private Fee.Ui.Button button_speedtest_fee;

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

		/** ButtonId
		*/
		public enum ButtonId
		{
			Save1,
			Save2,
			Load1,
			Load2,
			Random,
			FeeTest
		}

		/** Start
		*/
		private void Start()
		{
			//プレイヤーループシステム。インスタンス作成。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance();

			//プラットフォーム。インスタンス作成。
			Fee.Platform.Platform.CreateInstance();

			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.CreateInstance();
			Fee.Function.Function.GetInstance().SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Ui.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Input.CreateInstance();

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ファイル。インスタンス作成。
			Fee.File.File.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//ボタン。
			{
				this.button_save1 = this.prefablist.CreateButton(this.deleter,0);
				this.button_save1.SetOnButtonClick(this,ButtonId.Save1);
				this.button_save1.SetRect(100 + 110 * 0,100,100,50);
				this.button_save1.SetText("Save1");

				this.button_save2 = this.prefablist.CreateButton(this.deleter,0);
				this.button_save2.SetOnButtonClick(this,ButtonId.Save2);
				this.button_save2.SetRect(100 + 110 * 1,100,100,50);
				this.button_save2.SetText("Save2");

				this.button_load1 = this.prefablist.CreateButton(this.deleter,0);
				this.button_load1.SetOnButtonClick(this,ButtonId.Load1);
				this.button_load1.SetRect(100 + 110 * 2,100,100,50);
				this.button_load1.SetText("Load1");

				this.button_load2 = this.prefablist.CreateButton(this.deleter,0);
				this.button_load2.SetOnButtonClick(this,ButtonId.Load2);
				this.button_load2.SetRect(100 + 110 * 3,100,100,50);
				this.button_load2.SetText("Load2");

				this.button_random = this.prefablist.CreateButton(this.deleter,0);
				this.button_random.SetOnButtonClick(this,ButtonId.Random);
				this.button_random.SetRect(600 + 110*1,100 + 60 * 0,130,50);
				this.button_random.SetText("Random");

				this.button_speedtest_fee = this.prefablist.CreateButton(this.deleter,0);
				this.button_speedtest_fee.SetOnButtonClick(this,ButtonId.FeeTest);
				this.button_speedtest_fee.SetRect(600 + 110*1,100  + 60 * 2,130,50);
				this.button_speedtest_fee.SetText("Fee Test");
			}

			//ステータス。
			{
				this.status = this.prefablist.CreateText(this.deleter,0);
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

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			switch(a_id){
			case ButtonId.Save1:
				{
					//save1

					if(this.savedata != null){
						//オブジェクトをＪＳＯＮ化。
						Fee.JsonItem.JsonItem t_jsonitem = Fee.JsonItem.Convert.ObjectToJsonItem(this.savedata);

						//ＪＳＯＮを文字列化。
						string t_jsonstring = t_jsonitem.ConvertToJsonString();

						//セーブローカル。リクエスト。。
						this.save_item = Fee.File.File.GetInstance().RequestSaveTextFile(Fee.File.File.SaveRequestType.SaveLocalTextFile,new Fee.File.Path("save_1.json"),t_jsonstring);
						if(this.save_item != null){
							this.button_save1.SetLock(true);
							this.button_save2.SetLock(true);
							this.button_load1.SetLock(true);
							this.button_load2.SetLock(true);
							this.button_random.SetLock(true);
						}
					}

				}break;
			case ButtonId.Save2:
				{
					//save2

					if(this.savedata != null){
						//オブジェクトをＪＳＯＮ化。
						Fee.JsonItem.JsonItem t_jsonitem = Fee.JsonItem.Convert.ObjectToJsonItem(this.savedata);

						//ＪＳＯＮを文字列化。
						string t_jsonstring = t_jsonitem.ConvertToJsonString();

						//セーブローカル。リクエスト。。
						this.save_item = Fee.File.File.GetInstance().RequestSaveTextFile(Fee.File.File.SaveRequestType.SaveLocalTextFile,new Fee.File.Path("save_2.json"),t_jsonstring);
						if(this.save_item != null){
							this.button_save1.SetLock(true);
							this.button_save2.SetLock(true);
							this.button_load1.SetLock(true);
							this.button_load2.SetLock(true);
							this.button_random.SetLock(true);
						}
					}
				}break;
			case ButtonId.Load1:
				{
					//load1

					//ロードローカル。リクエスト。
					this.load_item = Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadLocalTextFile,new Fee.File.Path("save_1.json"));
					if(this.load_item != null){
						this.button_save1.SetLock(true);
						this.button_save2.SetLock(true);
						this.button_load1.SetLock(true);
						this.button_load2.SetLock(true);
						this.button_random.SetLock(true);
					}
				}break;
			case ButtonId.Load2:
				{
					//load2

					//ロードローカル。リクエスト。
					this.load_item = Fee.File.File.GetInstance().RequestLoad(Fee.File.File.LoadRequestType.LoadLocalTextFile,new Fee.File.Path("save_2.json"));
					if(this.load_item != null){
						this.button_save1.SetLock(true);
						this.button_save2.SetLock(true);
						this.button_load1.SetLock(true);
						this.button_load2.SetLock(true);
						this.button_random.SetLock(true);
					}
				}break;
			case ButtonId.Random:
				{
					//random

					this.savedata = new SaveData();
					this.savedata.ignore = Random.Range(0,9999);

					this.savedata.maindata.a = Random.Range(0,9999);
					this.savedata.maindata.sub = new SaveData.SubData();
					this.savedata.maindata.sub.a = Random.Range(0,9999);
					this.savedata.maindata.sub.subsub = new SaveData.SubSubData();
					this.savedata.maindata.sub.subsub.a = Random.Range(0,9999);

					this.savedata.data_dictionary = new System.Collections.Generic.Dictionary<string,SaveData.Item>();
					this.savedata.data_dictionary.Add("a",new SaveData.Item(Random.Range(0,9999)));

					this.savedata.data_list = new System.Collections.Generic.List<SaveData.Item>();
					this.savedata.data_list.Add(new SaveData.Item(Random.Range(0,9999)));

					this.SetStatus("Random",this.savedata);
				}break;
			case ButtonId.FeeTest:
				{
					//fee test

					this.SpeedTest_Fee();
				}break;

			}
		}

		/** SpeedTest_Fee
		*/
		private void SpeedTest_Fee()
		{
			SpeedTest_Data t_data_from = new SpeedTest_Data(true);

			string t_jsonstring_fee = "";

			string t_log = "";

			//Fee使用。
			try{
				float t_start = UnityEngine.Time.realtimeSinceStartup;
				{
					t_jsonstring_fee = Fee.JsonItem.Convert.ObjectToJsonString_Fee(t_data_from);
				}
				float t_end = UnityEngine.Time.realtimeSinceStartup;

				t_log += "Fee : ToJsonString : Time = " + (t_end - t_start).ToString() + " : Size = " + t_jsonstring_fee.Length.ToString() + "\n";
			}catch(System.Exception t_exception){
				t_log += "Fee : ToJsonString : ----------------- \n";
				Debug.LogError(t_exception);
			}

			t_log += "\n";

			//Fee使用。
			try{
				int t_value = 0;

				float t_start = UnityEngine.Time.realtimeSinceStartup;
				{
					Fee.JsonItem.JsonItem t_jsonitem = Fee.JsonItem.Convert.JsonStringToJsonItem(t_jsonstring_fee);
					t_value = t_jsonitem.GetItem("array").GetItem(0).GetItem("list").GetItem(0).GetItem("dictionary").GetItem("0").GetItem("value_int").CastToInt32();
				}
				float t_end = UnityEngine.Time.realtimeSinceStartup;

				t_log += "Fee : JsonStringToObject : Time = " + (t_end - t_start).ToString() + " : Value = " + t_value.ToString() + "\n";
			}catch(System.Exception t_exception){
				t_log += "Fee : JsonStringToObject : ----------------- \n";
				Debug.LogError(t_exception);
			}

			this.status.SetText(t_log);
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
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Input.GetInstance().mouse.cursor.pos);

			//ファイル。
			Fee.File.File.GetInstance().Main();

			if(this.load_item != null){
				if(this.load_item.IsBusy() == true){
					//ロード中。
				}else{
					if(this.load_item.GetResultAssetType() != Fee.Asset.AssetType.Text){
						//ロード失敗。
						this.SetStatus("Load : Faild",this.savedata);
					}else{
						//ロード成功。
						SaveData t_savedata = null;

						string t_jsonstring = this.load_item.GetResultAssetText();
						if(t_jsonstring != null){
							//文字列をＪＳＯＮ化。
							Fee.JsonItem.JsonItem t_jsonitem = new Fee.JsonItem.JsonItem(t_jsonstring);
							if(t_jsonitem != null){
								//ＪＳＯＮをオブジェクト化。
								t_savedata = Fee.JsonItem.Convert.JsonItemToObject<SaveData>(t_jsonitem);
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

