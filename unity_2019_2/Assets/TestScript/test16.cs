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
	public class test16 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test16.FocusID>
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

		/** FocusID
		*/
		public enum FocusID
		{
			NetworkMaster,
			NetworkLobby,
			NetworkRoom,
			NetworkPlayer,
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** network_master
		*/
		private Fee.Ui.Button network_master_button;
		private Fee.Render2D.Text2D network_master_text;

		/** network_lobby
		*/
		private Fee.Ui.Button network_lobby_button;
		private Fee.Render2D.Text2D network_lobby_text;

		/** network_room
		*/
		private Fee.Ui.Button network_room_button;
		private Fee.Render2D.Text2D network_room_text;

		/** network_player
		*/
		private Fee.Ui.Button network_player_button;
		private Fee.Render2D.Text2D network_player_text;

		/** NetworkPlayer
		*/
		public class NetworkPlayer_MonoBehaviour : Fee.Network.NetworkObject_Player_MonoBehaviour_Base
		{
			public static NetworkPlayer_MonoBehaviour s_myplayer;

			/** [Fee.Network.NetworkObject_Player_Base.OnConnect]プレイヤーが接続。
			*/
			public override void OnConnect()
			{
				if(this.IsSelf() == true){
					s_myplayer = this;
				}
			}

			/** [Fee.Network.NetworkObject_Player_Base.OnDisconnect]プレイヤーが切断。
			*/
			public override void OnDisconnect()
			{
				if(this.IsSelf() == true){
					s_myplayer = null;
				}
			}

			/** [Fee.Network.NetworkObject_Player_Base.OnSendPlayer]送信。
			*/
			public override void OnSendPlayer(Fee.Network.Stream_Base a_stream)
			{
			}

			/** [Fee.Network.NetworkObject_Player_Base.OnRecvPlayer]受信。
			*/
			public override void OnRecvPlayer(Fee.Network.Stream_Base a_stream)
			{
			}

			/** [Fee.Network.NetworkObject_Player_Base.OnSendStatus]送信。
			*/
			public override void OnSendStatus(Fee.Network.Stream_Base a_stream)
			{
			}

			/** [Fee.Network.NetworkObject_Player_Base.OnRecvStatus]受信。
			*/
			public override void OnRecvStatus(Fee.Network.Stream_Base a_stream)
			{
			}
		}

		/** Start
		*/
		private void Start()
		{
			//プレイヤーループシステム。インスタンス作成。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance(null);
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof( UnityEngine.Experimental.PlayerLoop.PreUpdate.SendMouseEvents));

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

			//入力。インスタンス作成。
			Fee.Input.Input.CreateInstance(true,false,true,false);
			Fee.Input.Input.GetInstance().SetCallBack(this.InputUpdate);
			Fee.Input.Input.GetInstance().key.Regist(Fee.Input.Status_Key_Type.Enter);
			Fee.Input.Input.GetInstance().key.Regist(Fee.Input.Status_Key_Type.Esc);
			Fee.Input.Input.GetInstance().key.Regist(Fee.Input.Status_Key_Type.Z);
			Fee.Input.Input.GetInstance().key.Regist(Fee.Input.Status_Key_Type.X);

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Ui.CreateInstance();

			//ネットワーク。インスタンス作成。
			Fee.Network.Config.LOG_ENABLE = true;
			Fee.Network.Network.CreateInstance();
			Fee.Network.Network.GetInstance().SetPlayerType<NetworkPlayer_MonoBehaviour>();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//layerindex
			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * Fee.Render2D.Config.DRAWPRIORITY_STEP;

			//network_master
			{
				this.network_master_button = this.prefablist.CreateButton(this.deleter,0);
				this.network_master_button.SetOnButtonClick(this,FocusID.NetworkMaster);
				this.network_master_button.SetRect(100,30 + 60 * 0,100,50);
				this.network_master_button.SetText("Master");
					
				this.network_master_text = this.prefablist.CreateText(this.deleter,0);
				this.network_master_text.SetRect(300 + 140 * 0,50,0,0);
			}

			//network_lobby
			{
				this.network_lobby_button = this.prefablist.CreateButton(this.deleter,0);
				this.network_lobby_button.SetOnButtonClick(this,FocusID.NetworkLobby);
				this.network_lobby_button.SetRect(100,30 + 60 * 1,100,50);
				this.network_lobby_button.SetText("Lobby");

				this.network_lobby_text = this.prefablist.CreateText(this.deleter,0);
				this.network_lobby_text.SetRect(300 + 140 * 1,50,0,0);
			}

			//network_room
			{
				this.network_room_button = this.prefablist.CreateButton(this.deleter,0);
				this.network_room_button.SetOnButtonClick(this,FocusID.NetworkRoom);
				this.network_room_button.SetRect(100,30 + 60 * 2,100,50);
				this.network_room_button.SetText("Room");

				this.network_room_text = this.prefablist.CreateText(this.deleter,0);
				this.network_room_text.SetRect(300 + 140 * 2,50,0,0);
			}

			//network_player
			{
				this.network_player_button = this.prefablist.CreateButton(this.deleter,0);
				this.network_player_button.SetOnButtonClick(this,FocusID.NetworkPlayer);
				this.network_player_button.SetRect(100,30 + 60 * 3,100,50);
				this.network_player_button.SetText("Player");

				this.network_player_text = this.prefablist.CreateText(this.deleter,0);
				this.network_player_text.SetRect(300 + 140 * 3,50,0,0);
			}
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(FocusID a_id)
		{
			switch(a_id){
			case FocusID.NetworkMaster:
				{
					//マスターに接続。

					if(Fee.Network.Network.GetInstance().IsBusy() == false){
						if(Fee.Network.Network.GetInstance().IsConnectMaster() == false){
							Fee.Network.Network.GetInstance().RequestConnectMaster();
						}else{
							Fee.Network.Network.GetInstance().RequestDisconnectMaster();
						}
					}
				}break;
			case FocusID.NetworkLobby:
				{
					//ロビーに接続。

					if(Fee.Network.Network.GetInstance().IsBusy() == false){
						if(Fee.Network.Network.GetInstance().IsConnectLobby() == false){
							Fee.Network.Network.GetInstance().RequestConnectLobby();
						}else{
							Fee.Network.Network.GetInstance().RequestDisconnectLobby();
						}
					}
				}break;
			case FocusID.NetworkRoom:
				{
					//ルームに接続。

					if(Fee.Network.Network.GetInstance().IsBusy() == false){
						if(Fee.Network.Network.GetInstance().IsConnectRoom() == false){
							Fee.Network.Network.GetInstance().RequestConnectRoom("room_name_1","room_info_1");
						}else{
							Fee.Network.Network.GetInstance().RequestDisconnectRoom();
						}
					}
				}break;
			case FocusID.NetworkPlayer:
				{
					//プレイヤー作成。

					if(Fee.Network.Network.GetInstance().IsBusy() == false){
						if(NetworkPlayer_MonoBehaviour.s_myplayer == null){
							Fee.Network.Network.GetInstance().CreatePlayer();
						}
					}
				}break;
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//ネットワーク。
			Fee.Network.Network.GetInstance().Main();

			{
				this.network_master_text.SetText("Master : " + Fee.Network.Network.GetInstance().IsConnectMaster().ToString());
				this.network_lobby_text.SetText("Lobby : " + Fee.Network.Network.GetInstance().IsConnectLobby().ToString());
				this.network_room_text.SetText("Room : " + Fee.Network.Network.GetInstance().IsConnectRoom().ToString());
				this.network_player_text.SetText("Player : " + (NetworkPlayer_MonoBehaviour.s_myplayer != null).ToString());
			}
		}

		/** InputUpdate
		*/
		private void InputUpdate()
		{
		}

		/** Update
		*/
		private void Update()
		{
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_PreDraw();
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
			if(Fee.Network.Network.GetInstance().IsConnectMaster() == true){
				if(Fee.Network.Network.GetInstance().IsBusy() == false){
					Fee.Network.Network.GetInstance().RequestDisconnectMaster();
				}

				//切断するまでシーンを終了させない。
				return false;
			}else{
				return true;
			}
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

