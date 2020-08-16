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
	/** test14
	*/
	public class test14 : MainBase , Fee.Function.UnityOnPreRender_CallBackInterface<int> , Fee.Function.UnityOnPostRender_CallBackInterface<int> , Fee.Function.UnityUpdate_CallBackInterface<int> , Fee.Function.UnityStart_CallBackInterface<int> , Fee.Function.UnityOnDestroy_CallBackInterface<int>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test14",
				"test14",

				@"
				プレイヤーループシステム
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** PlayerLoopSystemType
		*/
		private struct PlayerLoopSystemType
		{
			public struct EarlyUpdate_First{}
			public struct EarlyUpdate_Last{}
			public struct FixedUpdate_First{}
			public struct FixedUpdate_Last{}
			public struct Initialization_First{}
			public struct Initialization_Last{}
			public struct PreLateUpdate_First{}
			public struct PreLateUpdate_Last{}
			public struct PostLateUpdate_First{}
			public struct PostLateUpdate_Last{}
			public struct PreUpdate_First{}
			public struct PreUpdate_Last{}
			public struct Update_First{}
			public struct Update_Last{}
		}

		/** Item
		*/
		private class Item
		{
			/** index
			*/
			public int index;

			/** name
			*/
			public string name;

			/** delta
			*/
			public float delta;
			public float delta_log;

			/** time
			*/
			public float time;
			public float time_log;

			/** テキスト。
			*/
			public Fee.Render2D.Text2D text_name;
			public Fee.Render2D.Text2D text_value;
			public Fee.Render2D.Text2D text_time;

			/** constructor
			*/
			public Item(string a_name)
			{
				this.index = 0;
				this.name = a_name;
				this.delta = 0.0f;
				this.delta_log = 0.0f;
				this.time = 0.0f;
				this.time_log = 0.0f;
				this.text_name = null;
				this.text_value = null;
				this.text_time = null;
			}
		}

		/** フレーム終了待ち。
		*/
		private UnityEngine.WaitForEndOfFrame wait_for_endframe;

		/** list
		*/
		private System.Collections.Generic.LinkedList<Item> list;

		/** time
		*/
		private float time_current;

		/** Temp_MonoBehaviour
		*/
		public class Temp_MonoBehaviour : UnityEngine.MonoBehaviour
		{
		}
		public UnityEngine.GameObject temp;

		/** Start
		*/
		private void Start()
		{
			//プレイヤーループシステム。インスタンス作成。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance(null);
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(UnityEngine.PlayerLoop.PreUpdate.SendMouseEvents));

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
			Fee.Function.Function.GetInstance().SetRowUpdate(this.RowUpdate);

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.LOG_ENABLE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Config.LOG_ENABLE = true;
			Fee.Input.Input.CreateInstance(true,false,true,false);

			//イベントプレート。
			Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//フレーム終了待ち。
			this.wait_for_endframe = new UnityEngine.WaitForEndOfFrame();

			//StartCoroutine
			Fee.Function.Function.GetInstance().StartCoroutine(this.WaitFrame());

			//list
			this.list = new LinkedList<Item>();

			//PlayerLoopSystem
			{
				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddFirst,	typeof(UnityEngine.PlayerLoop.EarlyUpdate),			typeof(PlayerLoopSystemType.EarlyUpdate_First),		this.EarlyUpdate_First);
				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddLast,	typeof(UnityEngine.PlayerLoop.EarlyUpdate),			typeof(PlayerLoopSystemType.EarlyUpdate_Last),		this.EarlyUpdate_Last);
				this.list.AddLast(new Item("EarlyUpdate_First"));
				this.list.AddLast(new Item("EarlyUpdate_Last"));

				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddFirst,	typeof(UnityEngine.PlayerLoop.FixedUpdate),			typeof(PlayerLoopSystemType.FixedUpdate_First),		this.FixedUpdate_First);
				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddLast,	typeof(UnityEngine.PlayerLoop.FixedUpdate),			typeof(PlayerLoopSystemType.FixedUpdate_Last),		this.FixedUpdate_Last);
				this.list.AddLast(new Item("FixedUpdate_First"));
				this.list.AddLast(new Item("FixedUpdate_Last"));

				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddFirst,	typeof(UnityEngine.PlayerLoop.Initialization),		typeof(PlayerLoopSystemType.Initialization_First),	this.Initialization_First);
				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddLast,	typeof(UnityEngine.PlayerLoop.Initialization),		typeof(PlayerLoopSystemType.Initialization_Last),	this.Initialization_Last);
				this.list.AddLast(new Item("Initialization_First"));
				this.list.AddLast(new Item("Initialization_Last"));

				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddFirst,	typeof(UnityEngine.PlayerLoop.PostLateUpdate),		typeof(PlayerLoopSystemType.PostLateUpdate_First),	this.PostLateUpdate_First);
				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddLast,	typeof(UnityEngine.PlayerLoop.PostLateUpdate),		typeof(PlayerLoopSystemType.PostLateUpdate_Last),	this.PostLateUpdate_Last);
				this.list.AddLast(new Item("PostLateUpdate_First"));
				this.list.AddLast(new Item("PostLateUpdate_Last"));

				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddFirst,	typeof(UnityEngine.PlayerLoop.PreLateUpdate),		typeof(PlayerLoopSystemType.PreLateUpdate_First),	this.PreLateUpdate_First);
				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddLast,	typeof(UnityEngine.PlayerLoop.PreLateUpdate),		typeof(PlayerLoopSystemType.PreLateUpdate_Last),	this.PreLateUpdate_Last);
				this.list.AddLast(new Item("PreLateUpdate_First"));
				this.list.AddLast(new Item("PreLateUpdate_Last"));

				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddFirst,	typeof(UnityEngine.PlayerLoop.PreUpdate),			typeof(PlayerLoopSystemType.PreUpdate_First),		this.PreUpdate_First);
				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddLast,	typeof(UnityEngine.PlayerLoop.PreUpdate),			typeof(PlayerLoopSystemType.PreUpdate_Last),		this.PreUpdate_Last);
				this.list.AddLast(new Item("PreUpdate_First"));
				this.list.AddLast(new Item("PreUpdate_Last"));

				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddFirst,	typeof(UnityEngine.PlayerLoop.Update),				typeof(PlayerLoopSystemType.Update_First),			this.Update_First);
				Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Fee.PlayerLoopSystem.AddType.AddLast,	typeof(UnityEngine.PlayerLoop.Update),				typeof(PlayerLoopSystemType.Update_Last),			this.Update_Last);
				this.list.AddLast(new Item("Update_First"));
				this.list.AddLast(new Item("Update_Last"));
			}

			{
				UnityEngine.GameObject t_maincamera_gameobject = UnityEngine.GameObject.Find("Main Camera");

				{
					Fee.Function.UnityOnPreRender_MonoBehaviour t_callback = t_maincamera_gameobject.AddComponent<Fee.Function.UnityOnPreRender_MonoBehaviour>();
					t_callback.SetCallBack(this,0);
					this.list.AddLast(new Item("UnityOnPreRender"));
				}

				{
					Fee.Function.UnityOnPostRender_MonoBehaviour t_callback = t_maincamera_gameobject.AddComponent<Fee.Function.UnityOnPostRender_MonoBehaviour>();
					t_callback.SetCallBack(this,0);
					this.list.AddLast(new Item("UnityOnPostRender"));
				}

				{
					Fee.Function.UnityUpdate_MonoBehaviour t_callback = t_maincamera_gameobject.AddComponent<Fee.Function.UnityUpdate_MonoBehaviour>();
					t_callback.SetCallBack(this,0);
					this.list.AddLast(new Item("UnityUpdate"));
				}

				{
					this.list.AddLast(new Item("UnityStart"));
				}

				{
					this.list.AddLast(new Item("RowUpdate"));
				}

				{
					this.list.AddLast(new Item("UnityOnDestroy"));
				}

				{
					this.list.AddLast(new Item("FixedUpdate"));
				}
			}

			{
				int ii=0;
				System.Collections.Generic.LinkedListNode<Item> t_node = this.list.First;
				while(t_node != null){
					t_node.Value.index = ii;

					t_node.Value.text_name = Fee.Render2D.Text2D.Create(this.deleter,0);
					t_node.Value.text_name.SetRect(0,0,0,0);
					t_node.Value.text_name.SetText(t_node.Value.name);
					t_node.Value.text_name.SetColor(1.0f,1.0f,1.0f,1.0f);
					t_node.Value.text_name.SetOutLineColor(1.0f,1.0f,1.0f,0.3f);
					t_node.Value.text_name.SetOutLine(false);
					t_node.Value.text_name.SetShadow(false);

					t_node.Value.text_value = Fee.Render2D.Text2D.Create(this.deleter,0);
					t_node.Value.text_value.SetRect(0,0,0,0);
					t_node.Value.text_value.SetText("");
					t_node.Value.text_value.SetColor(1.0f,1.0f,1.0f,1.0f);
					t_node.Value.text_value.SetOutLineColor(1.0f,1.0f,1.0f,0.3f);
					t_node.Value.text_value.SetOutLine(false);
					t_node.Value.text_value.SetShadow(false);

					t_node.Value.text_time = Fee.Render2D.Text2D.Create(this.deleter,0);
					t_node.Value.text_time.SetRect(0,0,0,0);
					t_node.Value.text_time.SetText("");
					t_node.Value.text_time.SetColor(1.0f,1.0f,1.0f,1.0f);
					t_node.Value.text_time.SetOutLineColor(1.0f,1.0f,1.0f,0.3f);
					t_node.Value.text_time.SetOutLine(false);
					t_node.Value.text_time.SetShadow(false);

					t_node = t_node.Next;
					ii++;
				}
			}

			//time
			this.time_current = 0;

			//temp
			this.temp = null;
		}
		
		/** Find
		*/
		private Item Find(string a_name)
		{
			System.Collections.Generic.LinkedListNode<Item> t_node = this.list.First;
			while(t_node != null){
				if(t_node.Value.name == a_name){
					return t_node.Value;
				}
				t_node = t_node.Next;
			}
			return null;
		}

		/** Unity_Start
		*/
		public System.Collections.IEnumerator WaitFrame()
		{
			while(this.wait_for_endframe != null){
				//フレーム終了待ち。
				yield return this.wait_for_endframe;
			}
		}

		private void EarlyUpdate_First()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("EarlyUpdate_First");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void EarlyUpdate_Last()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("EarlyUpdate_Last");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void FixedUpdate_First()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("FixedUpdate_First");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void FixedUpdate_Last()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("FixedUpdate_Last");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void Initialization_First()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;

			this.time_current = t_time;

			System.Collections.Generic.LinkedListNode<Item> t_node = this.list.First;
			while(t_node != null){
				t_node.Value.delta_log = t_node.Value.delta;
				t_node.Value.delta = -1;
				t_node.Value.time_log = t_node.Value.time;

				t_node = t_node.Next;
			}


			for(int ii=0;ii<this.list.Count;ii++){
			}

			Item t_item = Find("Initialization_First");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void Initialization_Last()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("Initialization_Last");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void PreLateUpdate_First()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("PreLateUpdate_First");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void PreLateUpdate_Last()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("PreLateUpdate_Last");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void PostLateUpdate_First()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("PostLateUpdate_First");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void PostLateUpdate_Last()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("PostLateUpdate_Last");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void PreUpdate_First()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("PreUpdate_First");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void PreUpdate_Last()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("PreUpdate_Last");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void Update_First()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("Update_First");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		private void Update_Last()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("Update_Last");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		/** [Fee.Graphic.UnityOnPreRender_CallBackInterface]UnityOnPreRender
		*/
		public void UnityOnPreRender(int a_id)
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("UnityOnPreRender");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		/** [Fee.Graphic.UnityOnPostRender_CallBackInterface]UnityOnPostRender
		*/
		public void UnityOnPostRender(int a_id)
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("UnityOnPostRender");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		/** [Fee.Graphic.UnityUpdate_CallBackInterface]UnityUpdate
		*/
		public void UnityUpdate(int a_id)
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("UnityUpdate");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		/** [Fee.Graphic.UnityStart_CallBackInterface]UnityStart
		*/
		public void UnityStart(int a_id)
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("UnityStart");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		/** [Fee.Graphic.UnityOnDestroy_CallBackInterface]UnityOnDestroy
		*/
		public void UnityOnDestroy(int a_id)
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("UnityOnDestroy");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;

		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("RowUpdate");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			float t_time = UnityEngine.Time.realtimeSinceStartup;
			Item t_item = Find("FixedUpdate");
			t_item.delta = t_time - this.time_current;
			t_item.time = t_time;



			if(this.temp == null){
				this.temp = new UnityEngine.GameObject("temp");
				{
					Fee.Function.UnityStart_MonoBehaviour t_callback = this.temp.AddComponent<Fee.Function.UnityStart_MonoBehaviour>();
					t_callback.SetCallBack(this,0);
				}
				{
					Fee.Function.UnityOnDestroy_MonoBehaviour t_callback = this.temp.AddComponent<Fee.Function.UnityOnDestroy_MonoBehaviour>();
					t_callback.SetCallBack(this,0);
				}
			}else{
				UnityEngine.GameObject.Destroy(this.temp);
				this.temp = null;
			}
		}

		/** Update
		*/
		private void Update()
		{
			{
				System.Collections.Generic.LinkedListNode<Item> t_node = this.list.First;
				while(t_node != null){
					t_node.Value.text_time.SetText((t_node.Value.time_log).ToString());
					if(t_node.Value.delta_log >= 0.0f){
						t_node.Value.text_value.SetText((t_node.Value.delta_log * 1000).ToString("{0.0000}"));
						t_node.Value.text_name.SetColor(new UnityEngine.Color(1,1,1,1));
					}else{
						t_node.Value.text_value.SetText("");
						t_node.Value.text_name.SetColor(new UnityEngine.Color(0.8f,0.8f,0.8f,1));
					}

					t_node = t_node.Next;
				}
			}

			{
				System.Collections.Generic.LinkedListNode<Item> t_node = this.list.First;
				while(t_node != null){
					System.Collections.Generic.LinkedListNode<Item> t_node_next = t_node.Next;
					if((t_node != null)&&(t_node_next != null)){
						bool t_switch = false;

						if((t_node.Value.delta_log >= 0.0f)&&(t_node_next.Value.delta_log >= 0.0f)){
							if(t_node.Value.delta_log > t_node_next.Value.delta_log){
								t_switch = true;
							}else if(t_node.Value.delta_log == t_node_next.Value.delta_log){
								if(t_node.Value.index > t_node_next.Value.index){
									t_switch = true;
								}
							}
						}

						if(t_switch == true){
							this.list.Remove(t_node);
							this.list.AddAfter(t_node_next,t_node);
						}
					}

					t_node = t_node_next;
				}
			}

			{
				int ii=0;
				System.Collections.Generic.LinkedListNode<Item> t_node = this.list.First;
				while(t_node != null){
					t_node.Value.index = ii;
					t_node.Value.text_name.SetRect(10,50 + 16 * ii,0,0);
					t_node.Value.text_value.SetRect(300,50 + 16 * ii,0,0);
					t_node.Value.text_time.SetRect(600,50 + 16 * ii,0,0);

					t_node = t_node.Next;
					ii++;
				}
			}
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
			//wait_for_endframe
			this.wait_for_endframe = null;

			Fee.Function.Function.GetInstance().UnSetRowUpdate(this.RowUpdate);
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

