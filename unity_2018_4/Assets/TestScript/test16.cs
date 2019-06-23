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
	public class test16 : MainBase , Fee.Network.OnRemoteCallBack_Base
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

		/** mode
		*/
		private Mode mode;

		/** InputMode
		*/
		private enum InputMode
		{
			Position,
			Rotate,
			Scale,
		};

		/** inputmode
		*/
		private InputMode inputmode;

		/** [Fee.Network.OnRemoteCallBack_Base]リモートコール。
		*/
		public void OnRemoteCallInt(int a_playerlist_index,int a_key,int a_value)
		{
			Debug.Log("OnRemoteCallInt : " + a_playerlist_index.ToString() + " : " + a_key.ToString() + " : " + a_value.ToString());

			Fee.Network.Player t_player = Fee.Network.Network.GetInstance().GetPlayer(a_playerlist_index);
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

		/** [Fee.Network.OnRemoteCallBack_Base]リモートコール。
		*/
		public void OnRemoteCallString(int a_playerlist_index,int a_key,string a_value)
		{
			Debug.Log("OnRemoteCallString : " + a_playerlist_index.ToString() + " : " + a_key.ToString() + " : " + a_value);
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

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Mouse.CreateInstance();

			//キ。インスタンス作成。
			Fee.Input.Key.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Ui.CreateInstance();

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ネットワーク。インスタンス作成。
			Fee.Network.Config.LOG_ENABLE = true;
			Fee.Network.Network.CreateInstance();
			Fee.Network.Network.GetInstance().SetRecvCallBack(this);

			//フォント。
			Font t_font = Resources.Load<Font>(Data.Resources.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//layerindex
			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;

			//ステータス。
			{
				int t_x = 100;
				int t_y = 100;

				this.status_text = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
				this.status_text.SetRect(t_x,t_y,0,0);
				this.status_text.SetText("");
			}

			//player_text
			{
				int t_x = 100;
				int t_y = 130;

				this.player_text = new Fee.Render2D.Text2D[8];
				for(int ii=0;ii<player_text.Length;ii++){
					this.player_text[ii] = new Fee.Render2D.Text2D(this.deleter,t_drawpriority);
					this.player_text[ii].SetRect(t_x,t_y + 35*ii,0,0);
				}
			}

			//player_list
			{
				GameObject t_prefab = Resources.Load<GameObject>(Data.Resources.PREFAB_CUBE);

				this.player_list = new GameObject[8];
				for(int ii=0;ii<this.player_list.Length;ii++){
					this.player_list[ii] = GameObject.Instantiate(t_prefab);
					this.player_list[ii].SetActive(false);
				}
			}

			//開始ボタン。
			{
				int t_w = 100;
				int t_h = 30;
				int t_x = 100;
				int t_y = 300;

				Texture2D t_texture = Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON);

				this.start_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click_Start,0);
				this.start_button.SetRect(t_x,t_y,t_w,t_h);
				this.start_button.SetTexture(t_texture);
				this.start_button.SetText("接続");
				this.start_button.SetVisible(false);
			}

			//修了ボタン。
			{
				int t_w = 100;
				int t_h = 30;
				int t_x = 100 + 110;
				int t_y = 300;

				Texture2D t_texture = Resources.Load<Texture2D>(Data.Resources.UI_TEXTURE_BUTTON);

				this.end_button = new Fee.Ui.Button(this.deleter,t_drawpriority,this.CallBack_Click_End,0);
				this.end_button.SetRect(t_x,t_y,t_w,t_h);
				this.end_button.SetTexture(t_texture);
				this.end_button.SetText("切断");
				this.end_button.SetVisible(false);
			}

			//mode
			this.mode = Mode.Init;

			//inputmode
			this.inputmode = InputMode.Position;
		}

		/** [Button_Base]コールバック。クリック。開始。
		*/
		private void CallBack_Click_Start(int a_id)
		{
			if(this.mode == Mode.Wait){
				this.mode = Mode.Start;
			}
		}

		/** [Button_Base]コールバック。クリック。終了。
		*/
		private void CallBack_Click_End(int a_id)
		{
			if(this.mode == Mode.Do){
				this.mode = Mode.DisConnect;
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//キー。
			Fee.Input.Key.GetInstance().Main(true);

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//ネットワーク。
			Fee.Network.Network.GetInstance().Main();

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
					List<Fee.Network.Player> t_list = Fee.Network.Network.GetInstance().GetPlayerList();
					Fee.Network.Player t_myplayer = Fee.Network.Network.GetInstance().GetMyPlayer();

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
									float t_x = ((float)Fee.Input.Mouse.GetInstance().pos.x - Fee.Render2D.Render2D.VIRTUAL_W / 2) / 100;
									float t_y = ((float)Fee.Input.Mouse.GetInstance().pos.y - Fee.Render2D.Render2D.VIRTUAL_H / 2) / 100;

									if(t_myplayer != null){
										t_myplayer.SetPosition(t_x,t_y,0.0f);
									}
								}break;
							case InputMode.Rotate:
								{
									float t_angle = Fee.Input.Mouse.GetInstance().pos.x;

									Quaternion t_q = Quaternion.AngleAxis(t_angle,new Vector3(0.0f,1.0f,0.0f));

									if(t_myplayer != null){
										t_myplayer.SetQuaternion(ref t_q);
									}
								}break;
							case InputMode.Scale:
								{
									float t_x = 1.0f + (float)Fee.Input.Mouse.GetInstance().pos.x / Fee.Render2D.Render2D.VIRTUAL_W;
									float t_y = 1.0f + (float)Fee.Input.Mouse.GetInstance().pos.y / Fee.Render2D.Render2D.VIRTUAL_H;

									if(t_myplayer != null){
										t_myplayer.SetScale(t_x,t_y,1.0f);
									}
								}break;
							}
						}

						//■送信。
						if(Fee.Input.Key.GetInstance().enter.down == true){
							if(t_myplayer != null){
								//自分赤。
								t_myplayer.RemoteCallInt(999,1);
								t_myplayer.RemoteCallString(777,"red");
							}
						}else if(Fee.Input.Key.GetInstance().escape.down == true){
							if(t_myplayer != null){
								//自分白。
								t_myplayer.RemoteCallInt(999,0);
							}
						}

						if(Fee.Input.Key.GetInstance().sub1.down == true){
							//全部赤。

							List<Fee.Network.Player> t_player_list = Fee.Network.Network.GetInstance().GetPlayerList();
							for(int ii=0;ii<t_player_list.Count;ii++){
								t_player_list[ii].RemoteCallInt(888,1);
							}
						}else if(Fee.Input.Key.GetInstance().sub2.down == true){
							//全部白。
							List<Fee.Network.Player> t_player_list = Fee.Network.Network.GetInstance().GetPlayerList();
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
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
			if(this.mode == Mode.ChangeScene){
				return true;
			}
			return false;
		}

		/** OnDestroy
		*/
		private void OnDestroy()
		{
			this.deleter.DeleteAll();
		}
	}
}

