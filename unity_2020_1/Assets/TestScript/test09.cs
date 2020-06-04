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
	/** test09

		ダイクストラ法

	*/
	public class test09 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test09",
				"test09",

				@"
				ダイクストラ法
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** status_text
		*/
		private Fee.Render2D.Text2D status_text;

		/** map
		*/
		private int map_w;
		private int map_h;
		private int map_x;
		private int map_y;
		private int map_tip_w;
		private int map_tip_h;

		/** mode
		*/
		private enum Mode
		{
			Init,
			GoA_Start,
			GoB_Start,
			GoA_Calc,
			GoB_Calc,
			GoA_Move,
			GoB_Move,
		};
		private Mode mode;

		/** time
		*/
		private int time;

		/** cursor
		*/
		private int cursor_x;
		private int cursor_y;
		private Fee.Render2D.Sprite2D cursor_sprite;

		/** DijkstraInstance
		*/
		private class DijkstraInstance : Fee.Dijkstra.Instance_Base
		{
		}

		/** ノード。
		*/
		private struct NodeData : Fee.Dijkstra.Node_Base
		{
			/** x
			*/
			public int x;

			/** y
			*/
			public int y;

			/** tipcost
			*/
			public int tipcost;

			/** calcflag
			*/
			public bool calcflag;

			/** totalcost
			*/
			public long totalcost;

			/** constructor
			*/
			public NodeData(int a_x,int a_y,int a_tipcost)
			{
				this.x = a_x;
				this.y = a_y;
				this.tipcost = a_tipcost;
				this.calcflag = Fee.Dijkstra.Node<int,NodeData,LinkData>.CALCFALG_DEFAULT;
				this.totalcost = Fee.Dijkstra.Node<int,NodeData,LinkData>.TOTALCOST_DEFAULT;
			}

			/** [Node_Base]計算フラグ。設定。
			*/
			public void SetCalcFlag(Fee.Dijkstra.Instance_Base a_instance,bool a_flag)
			{
				this.calcflag = a_flag;
			}

			/** [Node_Base]計算フラグ。取得。
			*/
			public bool GetCalcFlag(Fee.Dijkstra.Instance_Base a_instance)
			{
				return this.calcflag;
			}

			/** [Node_Base]到達コスト。設定。
			*/
			public void SetTotalCost(Fee.Dijkstra.Instance_Base a_instance,long a_totalcost)
			{
				this.totalcost = a_totalcost;
			}

			/** [Node_Base]到達コスト。取得。
			*/
			public long GetTotalCost(Fee.Dijkstra.Instance_Base a_instance)
			{
				return this.totalcost;
			}
		}

		/** リンク。
		*/
		private struct LinkData : Fee.Dijkstra.Link_Base
		{
			/** 接続先ノードへの到達コスト。
			*/
			public long tocost;

			/** [Link_Base]接続先ノードへの到達コスト。取得。
			*/
			public long GetToCost(Fee.Dijkstra.Instance_Base a_instance)
			{
				return this.tocost;
			}

			/** [Link_Base]接続先ノードへの到達コスト。設定。
			*/
			public void SetToCost(Fee.Dijkstra.Instance_Base a_instance,long a_to_cost)
			{
				this.tocost = a_to_cost;
			}
		};

		/** dijkstra
		*/
		private Fee.Dijkstra.Dijkstra<int,NodeData,LinkData> dijkstra;

		/** dijkstra_instance
		*/
		private DijkstraInstance dijkstra_instance;

		/** sprite
		*/
		private Fee.Render2D.Sprite2D[] sprite_map;

		/** Start
		*/
		private void Start()
		{
			//プレイヤーループシステム。インスタンス作成。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance(null);

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

			//イベントプレート。インスタンス作成。
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Ui.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			//status_text
			this.status_text = this.prefablist.CreateText(this.deleter,0);
			this.status_text.SetRect(70,50,0,0);

			//マップ。
			this.map_w = 60;
			this.map_h = 60;
			this.map_x = 70;
			this.map_y = 70;
			this.map_tip_w = 8;
			this.map_tip_h = 8;

			//mode
			this.mode = Mode.Init;

			//time
			this.time = 0;

			//cursor
			this.cursor_x = 0;
			this.cursor_y = 0;
			this.cursor_sprite = Fee.Render2D.Sprite2D.Create(this.deleter,10);
			this.cursor_sprite.SetRect(0,0,0,0);
			this.cursor_sprite.SetColor(0.5f,0.5f,0.5f,0.5f);
			this.cursor_sprite.SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);

			//dijkstra,sprite_map
			this.dijkstra = new Fee.Dijkstra.Dijkstra<int,NodeData,LinkData>();
			this.dijkstra_instance = new DijkstraInstance();
			this.sprite_map = new Fee.Render2D.Sprite2D[this.map_w * this.map_h];
			for(int ii=0;ii<this.sprite_map.Length;ii++){
				int t_tip_x = ii % this.map_w;
				int t_tip_y = ii / this.map_w;

				//スプライト。
				this.sprite_map[ii] = Fee.Render2D.Sprite2D.Create(this.deleter,0);
				this.sprite_map[ii].SetRect(this.map_x + t_tip_x * this.map_tip_w,this.map_y + t_tip_y * this.map_tip_h,this.map_tip_w-1,this.map_tip_h-1);

				//ダイクストラ法、ノード作成。
				int t_key = ii;
				this.dijkstra.AddNode(t_key,new Fee.Dijkstra.Node<int,NodeData,LinkData>(t_key,new NodeData(t_tip_x,t_tip_y,0)));
			}

			//ノードをリンクでつなぐ。コストは仮設定で０。
			for(int ii=0;ii<this.sprite_map.Length;ii++){
				int t_tip_x = ii % this.map_w;
				int t_tip_y = ii / this.map_w;

				Fee.Dijkstra.Node<int,NodeData,LinkData> t_from = this.dijkstra.GetNode(ii);

				//上。
				{
					Fee.Dijkstra.Node<int,NodeData,LinkData> t_to = null;
					int t_to_x = t_tip_x;
					int t_to_y = t_tip_y - 1;
					if(t_to_y >= 0){
						t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
					}
					if(t_to != null){
						t_from.AddLink(new Fee.Dijkstra.Link<int,NodeData,LinkData>(new LinkData(),t_to));
					}
				}

				//下。
				{
					Fee.Dijkstra.Node<int,NodeData,LinkData> t_to = null;
					int t_to_x = t_tip_x;
					int t_to_y = t_tip_y + 1;
					if(t_to_y < this.map_h){
						t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
					}
					if(t_to != null){
						t_from.AddLink(new Fee.Dijkstra.Link<int,NodeData,LinkData>(new LinkData(),t_to));
					}
				}

				//左。
				{
					Fee.Dijkstra.Node<int,NodeData,LinkData> t_to = null;
					int t_to_x = t_tip_x - 1;
					int t_to_y = t_tip_y;
					if(t_to_x >= 0){
						t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
					}
					if(t_to != null){
						t_from.AddLink(new Fee.Dijkstra.Link<int,NodeData,LinkData>(new LinkData(),t_to));
					}
				}

				//右。
				{
					Fee.Dijkstra.Node<int,NodeData,LinkData> t_to = null;
					int t_to_x = t_tip_x + 1;
					int t_to_y = t_tip_y;
					if(t_to_x < this.map_w){
						t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
					}
					if(t_to != null){
						t_from.AddLink(new Fee.Dijkstra.Link<int,NodeData,LinkData>(new LinkData(),t_to));
					}
				}
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			switch(this.mode){
			case Mode.Init:
				{
					this.mode = Mode.GoA_Start;

					this.cursor_x = 0;
					this.cursor_y = 0;
					this.cursor_sprite.SetRect(this.map_x + this.cursor_x * this.map_tip_w,this.map_y + this.cursor_y * this.map_tip_h,this.map_tip_w,this.map_tip_h);
				}break;
			case Mode.GoA_Start:
			case Mode.GoB_Start:
				{
					//コストをランダム設定。
					{
						for(int ii=0;ii<this.sprite_map.Length;ii++){
							if(Random.value < 0.7f){
								this.dijkstra.GetNode(ii).nodedata.tipcost = 1;
							}else{
								this.dijkstra.GetNode(ii).nodedata.tipcost = 100;
							}
						}
						for(int ii=0;ii<this.sprite_map.Length;ii++){
							Fee.Dijkstra.Node<int,NodeData,LinkData> t_node = this.dijkstra.GetNode(ii);
							System.Collections.Generic.List<Fee.Dijkstra.Link<int,NodeData,LinkData>> t_linklist = t_node.GetLinkList();

							for(int jj=0;jj<t_linklist.Count;jj++){
								//リンクの接続先ノードのチップコストをリンクのコストとする。
								Fee.Dijkstra.Node<int,NodeData,LinkData> t_to_node = t_linklist[jj].GetToNode();

								//ゴールからなのでToCostはt_to_node から t_node への移動コスト。
								t_linklist[jj].SetToCost(this.dijkstra_instance,t_node.nodedata.tipcost);
							}
						}
					}
			
					//計算フラグをリセット。
					this.dijkstra.ResetCalcFlag(this.dijkstra_instance);

					//ゴールを設定。
					if(this.mode == Mode.GoA_Start){
						int t_x = this.map_w / 2;
						int t_y = this.map_h / 2;
						int t_key = t_x + t_y * this.map_w;
						this.dijkstra.SetStartNode(this.dijkstra_instance,this.dijkstra.GetNode(t_key));

						this.time = 0;
						this.mode = Mode.GoA_Calc;
					}else{
						int t_x = 0;
						int t_y = 0;
						int t_key = t_x + t_y * this.map_w;
						this.dijkstra.SetStartNode(this.dijkstra_instance,this.dijkstra.GetNode(t_key));

						this.time = 0;
						this.mode = Mode.GoB_Calc;
					}
				}break;
			case Mode.GoA_Calc:
			case Mode.GoB_Calc:
				{
					for(int ii=0;ii<2;ii++){
						this.time++;
						if(this.dijkstra.Calc(this.dijkstra_instance) == true){
							//計算中。
							int t_key_now = this.cursor_x + this.cursor_y * this.map_w;
							long t_totalcost = this.dijkstra.GetNode(t_key_now).GetTotalCost(this.dijkstra_instance);
							if(t_totalcost >= 0){
								//現在位置からゴールまでの経路の計算が終了したので中断。
								this.time = 0;
								if(this.mode == Mode.GoA_Calc){
									this.mode = Mode.GoA_Move;
									break;
								}else{
									this.mode = Mode.GoB_Move;
									break;
								}
							}
						}else{
							this.time = 0;
							if(this.mode == Mode.GoA_Calc){
								this.mode = Mode.GoA_Move;
								break;
							}else{
								this.mode = Mode.GoB_Move;
								break;
							}
						}
					}

					string t_text = "Time = " + this.time.ToString() + " : ListCount = " + this.dijkstra.GetCalcList().Count.ToString(); 
					this.status_text.SetText(t_text);

					for(int ii=0;ii<this.sprite_map.Length;ii++){
						Fee.Dijkstra.Node<int,NodeData,LinkData> t_node = this.dijkstra.GetNode(ii);

						if(t_node.GetCalcFlag(this.dijkstra_instance) == true){
							//計算中。
							this.sprite_map[ii].SetColor(1.0f,0.0f,0.0f,1.0f);
						}else{
							//通常表示。
							this.sprite_map[ii].SetColor((100 - t_node.nodedata.tipcost) / 100.0f,1.0f,1.0f,1.0f);
						}
					}
				}break;
			case Mode.GoA_Move:
			case Mode.GoB_Move:
				{
					this.time++;
					if(this.time >= 5){
						this.time = 0;

						//現在位置。
						int t_key_now = this.cursor_x + this.cursor_y * this.map_w;
						Fee.Dijkstra.Node<int,NodeData,LinkData> t_node_now = this.dijkstra.GetNode(t_key_now);

						//移動先ノード。
						Fee.Dijkstra.Node<int,NodeData,LinkData> t_node_to = t_node_now.GetPrevNode();

						if(t_node_to != null){
							if(t_node_now.nodedata.x < t_node_to.nodedata.x){
								//右へ。
								this.cursor_x++;
							}else if(t_node_now.nodedata.x > t_node_to.nodedata.x){
								//左へ。
								this.cursor_x--;
							}else if(t_node_now.nodedata.y < t_node_to.nodedata.y){
								//下へ。
								this.cursor_y++;
							}else if(t_node_now.nodedata.y > t_node_to.nodedata.y){
								//上へ。
								this.cursor_y--;
							}
							this.cursor_sprite.SetRect(this.map_x + this.cursor_x * this.map_tip_w,this.map_y + this.cursor_y * this.map_tip_h,this.map_tip_w - 1,this.map_tip_h - 1);
						}else{
							//ゴールに到達。
							if(this.mode == Mode.GoA_Move){
								this.mode = Mode.GoB_Start;
								break;
							}else{
								this.mode = Mode.GoA_Start;
								break;
							}
						}
					}
				}break;
			}
		}

		/** InputUpdate
		*/
		private void InputUpdate()
		{
			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main();

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();
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

