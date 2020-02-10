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
	/** test16

		通信

	*/
	public class test16 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test16.ButtonId>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test16",
				"test16",

				@"
				通信
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** ステータス。
		*/
		private Fee.Render2D.Text2D status_text;

		/** player_text
		*/
		private Fee.Render2D.Text2D[] player_text;

		/** player_list
		*/
		private GameObject[] player_list;

		/** 開始ボタン。
		*/
		private Fee.Ui.Button start_button;

		/** 修了ボタン。
		*/
		private Fee.Ui.Button end_button;

		/** Mode
		*/
		/*
		private enum Mode
		{
			Init,
			Wait,
			Start,
			Do,
			DisConnect,
			DisConnectNow,
			ChangeScene,
		};
		*/

		/** mode
		*/
		/*
		private Mode mode;
		*/

		/** InputMode
		*/
		/*
		private enum InputMode
		{
			Position,
			Rotate,
			Scale,
		};
		*/

		/** inputmode
		*/
		/*
		private InputMode inputmode;
		*/

		/** ButtonId
		*/
		public enum ButtonId
		{
			Start,
			End,
		}

		//TODO:
		/** [Fee.Network.OnRemoteCallBack_Base]リモートコール。
		*/
		#if(false)
		public void OnRemoteCallInt(int a_playerlist_index,int a_key,int a_value)
		{
			Debug.Log("OnRemoteCallInt : " + a_playerlist_index.ToString() + " : " + a_key.ToString() + " : " + a_value.ToString());

			Fee.Network.Player_MonoBehaviour t_player = Fee.Network.Network.GetInstance().GetPlayer(a_playerlist_index);
			if(t_player != null){
				if(a_value == 0){
					//白。
					this.player_list[a_playerlist_index].GetComponent<MeshRenderer>().material.color = Color.white;
				}else{
					//赤。
					this.player_list[a_playerlist_index].GetComponent<MeshRenderer>().material.color = Color.red;
				}
			}
		}
		#endif

		//TODO:
		/** [Fee.Network.OnRemoteCallBack_Base]リモートコール。
		*/
		#if(false)
		public void OnRemoteCallString(int a_playerlist_index,int a_key,string a_value)
		{
			Debug.Log("OnRemoteCallString : " + a_playerlist_index.ToString() + " : " + a_key.ToString() + " : " + a_value);
		}
		#endif

		/** Start
		*/
		private void Start()
		{
			//プラットフォーム。インスタンス作成。
			Fee.Platform.Platform.CreateInstance();

			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.CreateInstance();
			Fee.Input.Key.GetInstance().Regist(Fee.Input.Key_Type.Enter);
			Fee.Input.Key.GetInstance().Regist(Fee.Input.Key_Type.Esc);
			Fee.Input.Key.GetInstance().Regist(Fee.Input.Key_Type.Z);
			Fee.Input.Key.GetInstance().Regist(Fee.Input.Key_Type.X);

			//キ。インスタンス作成。
			Fee.Input.Key.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Ui.CreateInstance();

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//TODO:
			//ネットワーク。インスタンス作成。
			/*
			Fee.Network.Config.LOG_ENABLE = true;
			Fee.Network.Network.CreateInstance();
			Fee.Network.Network.GetInstance().SetRecvCallBack(this);
			*/

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//layerindex
			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;

			//ステータス。
			{
				int t_x = 100;
				int t_y = 100;

				this.status_text = this.prefablist.CreateText(this.deleter,t_drawpriority);
				this.status_text.SetRect(t_x,t_y,0,0);
				this.status_text.SetText("");
			}

			//player_text
			{
				int t_x = 100;
				int t_y = 130;

				this.player_text = new Fee.Render2D.Text2D[8];
				for(int ii=0;ii<player_text.Length;ii++){
					this.player_text[ii] = this.prefablist.CreateText(this.deleter,t_drawpriority);
					this.player_text[ii].SetRect(t_x,t_y + 35*ii,0,0);
				}
			}

			//player_list
			{
				this.player_list = new GameObject[8];
				for(int ii=0;ii<this.player_list.Length;ii++){
					this.player_list[ii] = GameObject.Instantiate(this.prefablist.GetPrefab(Common.PrefabType.Test16_Cube));
					this.player_list[ii].SetActive(false);
				}
			}

			//開始ボタン。
			{
				int t_w = 100;
				int t_h = 30;
				int t_x = 100;
				int t_y = 300;

				this.start_button = this.prefablist.CreateButton(this.deleter,t_drawpriority);
				this.start_button.SetOnButtonClick(this,ButtonId.Start);
				this.start_button.SetRect(t_x,t_y,t_w,t_h);
				this.start_button.SetText("接続");
				this.start_button.SetVisible(false);
			}

			//修了ボタン。
			{
				int t_w = 100;
				int t_h = 30;
				int t_x = 100 + 110;
				int t_y = 300;

				this.end_button = this.prefablist.CreateButton(this.deleter,t_drawpriority);
				this.end_button.SetOnButtonClick(this,ButtonId.End);
				this.end_button.SetRect(t_x,t_y,t_w,t_h);
				this.end_button.SetText("切断");
				this.end_button.SetVisible(false);
			}

			//mode
			/*
			this.mode = Mode.Init;
			*/

			//inputmode
			/*
			this.inputmode = InputMode.Position;
			*/
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonId a_id)
		{
			/*
			switch(a_id){
			case ButtonId.Start:
				{
					//開始。
					if(this.mode == Mode.Wait){
						this.mode = Mode.Start;
					}
				}break;
			case ButtonId.End:
				{
					//終了。
					if(this.mode == Mode.Do){
						this.mode = Mode.DisConnect;
					}
				}break;
			}
			*/
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(this.is_focus,Fee.Render2D.Render2D.GetInstance());

			//キー。
			Fee.Input.Key.GetInstance().Main(true);

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//ネットワーク。
			Fee.Network.Network.GetInstance().Main();

			#if(false)
			switch(this.mode){
			case Mode.Init:
				{
					this.status_text.SetText("Mode.Init");
					this.mode = Mode.Wait;
				}break;
			case Mode.Wait:
				{
					this.status_text.SetText("Mode.Wait");

					if(this.IsChangeScene() == true){
						this.mode = Mode.ChangeScene;
					}else{
						this.start_button.SetVisible(true);
					}
				}break;
			case Mode.Start:
				{
					this.status_text.SetText("Mode.Start");

					//開始。
					Fee.Network.Network.GetInstance().Start_AutoJoinRandomRoom();

					//ボタン。
					this.start_button.SetVisible(false);
					this.end_button.SetVisible(true);

					this.mode = Mode.Do;
				}break;
			case Mode.Do:
				{
					List<Fee.Network.Player_MonoBehaviour> t_list = Fee.Network.Network.GetInstance().GetPlayerList();
					Fee.Network.Player_MonoBehaviour t_myplayer = Fee.Network.Network.GetInstance().GetMyPlayer();

					this.status_text.SetText("Mode.Do : " + t_list.Count.ToString());

					{
						if(Fee.Input.Mouse.GetInstance().right.down == true){
							switch(this.inputmode){
							case InputMode.Position:
								{
									this.inputmode = InputMode.Rotate;
								}break;
							case InputMode.Rotate:
								{
									this.inputmode = InputMode.Scale;
								}break;
							case InputMode.Scale:
								{
									this.inputmode = InputMode.Position;
								}break;
							}
						}

						if(Fee.Input.Mouse.GetInstance().left.down == true){
							switch(this.inputmode){
							case InputMode.Position:
								{
									float t_x = ((float)Fee.Input.Mouse.GetInstance().cursor.pos.x - Fee.Render2D.Render2D.VIRTUAL_W / 2) / 100;
									float t_y = ((float)Fee.Input.Mouse.GetInstance().cursor.pos.y - Fee.Render2D.Render2D.VIRTUAL_H / 2) / 100;

									if(t_myplayer != null){
										t_myplayer.SetPosition(t_x,t_y,0.0f);
									}
								}break;
							case InputMode.Rotate:
								{
									float t_angle = Fee.Input.Mouse.GetInstance().cursor.pos.x;

									Quaternion t_quaternion = Quaternion.AngleAxis(t_angle,new Vector3(0.0f,1.0f,0.0f));

									if(t_myplayer != null){
										t_myplayer.SetQuaternion(in t_quaternion);
									}
								}break;
							case InputMode.Scale:
								{
									float t_x = 1.0f + (float)Fee.Input.Mouse.GetInstance().cursor.pos.x / Fee.Render2D.Render2D.VIRTUAL_W;
									float t_y = 1.0f + (float)Fee.Input.Mouse.GetInstance().cursor.pos.y / Fee.Render2D.Render2D.VIRTUAL_H;

									if(t_myplayer != null){
										t_myplayer.SetScale(t_x,t_y,1.0f);
									}
								}break;
							}
						}

						//■送信。
						if(Fee.Input.Key.GetInstance().GetKey(Fee.Input.Key_Type.Enter).digital.down == true){
							if(t_myplayer != null){
								//自分赤。
								t_myplayer.RemoteCallInt(999,1);
								t_myplayer.RemoteCallString(777,"red");
							}
						}else if(Fee.Input.Key.GetInstance().GetKey(Fee.Input.Key_Type.Esc).digital.down == true){
							if(t_myplayer != null){
								//自分白。
								t_myplayer.RemoteCallInt(999,0);
							}
						}

						if(Fee.Input.Key.GetInstance().GetKey(Fee.Input.Key_Type.Z).digital.down == true){
							//全部赤。

							List<Fee.Network.Player_MonoBehaviour> t_player_list = Fee.Network.Network.GetInstance().GetPlayerList();
							for(int ii=0;ii<t_player_list.Count;ii++){
								t_player_list[ii].RemoteCallInt(888,1);
							}
						}else if(Fee.Input.Key.GetInstance().GetKey(Fee.Input.Key_Type.X).digital.down == true){
							//全部白。
							List<Fee.Network.Player_MonoBehaviour> t_player_list = Fee.Network.Network.GetInstance().GetPlayerList();
							for(int ii=0;ii<t_player_list.Count;ii++){
								t_player_list[ii].RemoteCallInt(888,0);
							}
						}
					}

					for(int ii=0;ii<this.player_text.Length;ii++){
						if(ii < t_list.Count){

							string t_text = "";
							t_text += "IsMine = " + t_list[ii].IsMine().ToString() + " ";
							t_text += "IsMasterClient = " + t_list[ii].IsMasterClient().ToString() + " ";
							t_text += "NickName = " + t_list[ii].GetNickName() + " ";
							t_text += "UserID = " + t_list[ii].GetUniqueID().ToString() + "\n";

							t_text += "Pos = " + t_list[ii].GetPosition().ToString() + " ";
							t_text += "Rotate = " + t_list[ii].GetQuaternion().ToString() + " ";
							t_text += "Scale = " + t_list[ii].GetScale().ToString() + " ";

							this.player_text[ii].SetText(t_text);

							this.player_list[ii].SetActive(true);
							this.player_list[ii].transform.position = t_list[ii].GetPosition();
							this.player_list[ii].transform.rotation = t_list[ii].GetQuaternion();
							this.player_list[ii].transform.localScale = t_list[ii].GetScale();
						}else{
							this.player_text[ii].SetText("---");
							this.player_list[ii].SetActive(false);
						}
					}

					if(this.IsChangeScene() == true){
						this.mode = Mode.DisConnect;
					}
				}break;
			case Mode.DisConnect:
				{
					this.status_text.SetText("Mode.DisConnect");

					for(int ii=0;ii<this.player_text.Length;ii++){
						this.player_text[ii].SetText("");
						this.player_list[ii].SetActive(false);
					}

					//切断リクエスト。
					Fee.Network.Network.GetInstance().DisconnectRequest();

					//ボタン。
					this.end_button.SetVisible(false);

					this.mode = Mode.DisConnectNow;
				}break;
			case Mode.DisConnectNow:
				{
					this.status_text.SetText("Mode.DisConnectNow");

					if(Fee.Network.Network.GetInstance().IsBusy() == false){
						this.mode = Mode.Wait;
					}
				}break;
			case Mode.ChangeScene:
				{
					//シーン変更可能。
				}break;
			}
			#endif

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
			//TODO:
			/*
			if(this.mode == Mode.ChangeScene){
				return true;
			}
			return false;
			*/

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

