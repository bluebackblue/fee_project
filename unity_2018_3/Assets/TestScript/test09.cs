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

ダイクストラ法

*/
public class test09 : main_base
{
	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** map
	*/
	private int map_w;
	private int map_h;

	/** mode
	*/
	private enum Mode
	{
		Init,
		GotoA,
		GotoB,
		MoveToA,
		MoveToB,
	};
	private Mode mode;

	/** time
	*/
	private int time;

	/** xy
	*/
	private int x;
	private int y;

	/** sprite_cursor
	*/
	private Fee.Render2D.Sprite2D sprite_cursor;

	/** Node
	*/
	private class Node : Fee.Dijkstra.NodeBase
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

		/** constructor
		*/
		public Node(int a_x,int a_y)
		{
			this.x = a_x;
			this.y = a_y;
			this.tipcost = 0;
		}

		/** GetPrevNode
		*/
		public Node GetPrevNode()
		{
			return this.prev_node as Node;
		}
	}

	/** Link
	*/
	private class Link : Fee.Dijkstra.LinkBase
	{
		/** constructor
		*/
		public Link(Node a_to_node,long a_to_cost)
			:
			base(a_to_node,a_to_cost)
		{
		}
	}

	/** dijkstra
	*/
	private Fee.Dijkstra.Dijkstra<int,Node,Link> dijkstra;

	/** sprite
	*/
	private Fee.Render2D.Sprite2D[] sprite_map;

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

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//マップ。
		this.map_w = 10;
		this.map_h = 10;

		//mode
		this.mode = Mode.Init;

		//time
		this.time = 0;

		//xy
		this.x = 0;
		this.y = 0;

		//sprite_cursor
		this.sprite_cursor = new Fee.Render2D.Sprite2D(this.deleter,null,10);
		this.sprite_cursor.SetRect(0,0,0,0);
		this.sprite_cursor.SetColor(0.5f,0.5f,0.5f,0.5f);
		this.sprite_cursor.SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);

		//dijkstra,sprite_map
		this.dijkstra = new Fee.Dijkstra.Dijkstra<int,Node,Link>();
		this.sprite_map = new Fee.Render2D.Sprite2D[this.map_w * this.map_h];
		for(int ii=0;ii<this.sprite_map.Length;ii++){
			int t_tip_x = ii % this.map_w;
			int t_tip_y = ii / this.map_w;

			//スプライト。
			this.sprite_map[ii] = new Fee.Render2D.Sprite2D(this.deleter,null,0);
			this.sprite_map[ii].SetRect(50 + t_tip_x * 32,50 + t_tip_y * 32,32-1,32-1);

			//ダイクストラ法、ノード作成。
			int t_key = ii;
			this.dijkstra.AddNode(t_key,new Node(t_tip_x,t_tip_y));
		}

		//ノードをリンクでつなぐ。コストは仮設定で０。
		for(int ii=0;ii<this.sprite_map.Length;ii++){
			int t_tip_x = ii % this.map_w;
			int t_tip_y = ii / this.map_w;

			Node t_from = this.dijkstra.GetNode(ii);

			//上。
			{
				Node t_to = null;
				int t_to_x = t_tip_x;
				int t_to_y = t_tip_y - 1;
				if(t_to_y >= 0){
					t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
				}
				if(t_to != null){
					t_from.AddLink(new Link(t_to,0));
				}
			}

			//下。
			{
				Node t_to = null;
				int t_to_x = t_tip_x;
				int t_to_y = t_tip_y + 1;
				if(t_to_y < this.map_h){
					t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
				}
				if(t_to != null){
					t_from.AddLink(new Link(t_to,0));
				}
			}

			//左。
			{
				Node t_to = null;
				int t_to_x = t_tip_x - 1;
				int t_to_y = t_tip_y;
				if(t_to_x >= 0){
					t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
				}
				if(t_to != null){
					t_from.AddLink(new Link(t_to,0));
				}
			}

			//右。
			{
				Node t_to = null;
				int t_to_x = t_tip_x + 1;
				int t_to_y = t_tip_y;
				if(t_to_x < this.map_w){
					t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
				}
				if(t_to != null){
					t_from.AddLink(new Link(t_to,0));
				}
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

		switch(this.mode){
		case Mode.Init:
			{
				this.mode = Mode.GotoA;

				this.x = 0;
				this.y = 0;
				this.sprite_cursor.SetRect(50 + this.x * 32,50 + this.y * 32,32,32);
			}break;
		case Mode.GotoA:
		case Mode.GotoB:
			{
				//コストをランダム設定。
				{
					for(int ii=0;ii<this.sprite_map.Length;ii++){
						if(Random.value < 0.7f){
							this.dijkstra.GetNode(ii).tipcost = 1;
						}else{
							this.dijkstra.GetNode(ii).tipcost = 100;
						}
					}
					for(int ii=0;ii<this.sprite_map.Length;ii++){
						Node t_node = this.dijkstra.GetNode(ii);

						for(int jj=0;jj<t_node.link.Count;jj++){
							//リンクの接続先ノードのチップコストをリンクのコストとする。
							Node t_to_node = t_node.link[jj].to_node as Node;
							t_node.link[jj].to_cost = t_to_node.tipcost;
						}

						this.sprite_map[ii].SetColor((100 - t_node.tipcost) / 100.0f,1.0f,1.0f,1.0f);
					}
				}
			
				//計算フラグをリセット。
				this.dijkstra.ResetCalcFlag();

				//ゴールを設定。
				if(this.mode == Mode.GotoA){
					int t_x = this.map_w - 1;
					int t_y = this.map_h - 1;
					int t_key = t_x + t_y * this.map_w;
					this.dijkstra.SetStartNode(this.dijkstra.GetNode(t_key));
				}else{
					int t_x = 0;
					int t_y = 0;
					int t_key = t_x + t_y * this.map_w;
					this.dijkstra.SetStartNode(this.dijkstra.GetNode(t_key));
				}

				//計算。
				while(this.dijkstra.Calc() == true){}

				this.time = 0;

				if(this.mode == Mode.GotoA){
					this.mode = Mode.MoveToA;
				}else{
					this.mode = Mode.MoveToB;
				}
			}break;
		case Mode.MoveToA:
		case Mode.MoveToB:
			{
				this.time++;
				if(this.time >= 10){
					this.time = 0;

					//現在位置。
					int t_key_now = this.x + this.y * this.map_w;
					Node t_node_now = this.dijkstra.GetNode(t_key_now);

					//移動先ノード。
					Node t_node_to = t_node_now.GetPrevNode();

					if(t_node_to != null){
						if(t_node_now.x < t_node_to.x){
							//右へ。
							this.x++;
						}else if(t_node_now.x > t_node_to.x){
							//左へ。
							this.x--;
						}else if(t_node_now.y < t_node_to.y){
							//下へ。
							this.y++;
						}else if(t_node_now.y > t_node_to.y){
							//上へ。
							this.y--;
						}
						this.sprite_cursor.SetRect(50 + this.x * 32,50 + this.y * 32,32,32);
					}else{
						//ゴールに到達。
						if(this.mode == Mode.MoveToA){
							this.mode = Mode.GotoB;
						}else{
							this.mode = Mode.GotoA;
						}
					}
				}
			}break;
		}

		//if(this.item != null){
		//	if(this.item.IsBusy() == true){
		//	}else{
		//		if(this.item.GetResultType() == Fee.File.Item.ResultType.Text){
		//			this.text.SetText(this.item.GetResultText());
		//		}else{
		//			this.text.SetText("error");
		//		}
		//		this.item = null;
		//	}
		//}
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

