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

	/** ノード追加情報。
	*/
	private struct NodeData
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
		public NodeData(int a_x,int a_y,int a_tipcost)
		{
			this.x = a_x;
			this.y = a_y;
			this.tipcost = a_tipcost;
		}
	};

	/** リンク追加情報。
	*/
	private struct LinkData
	{
	};

	/** dijkstra
	*/
	private Fee.Dijkstra.Dijkstra<int,NodeData,LinkData> dijkstra;

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
		Fee.Ui.Ui.CreateInstance();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//マップ。
		this.map_w = 30;
		this.map_h = 30;
		this.map_x = 50;
		this.map_y = 50;
		this.map_tip_w = 16;
		this.map_tip_h = 16;

		//mode
		this.mode = Mode.Init;

		//time
		this.time = 0;

		//cursor
		this.cursor_x = 0;
		this.cursor_y = 0;
		this.cursor_sprite = new Fee.Render2D.Sprite2D(this.deleter,null,10);
		this.cursor_sprite.SetRect(0,0,0,0);
		this.cursor_sprite.SetColor(0.5f,0.5f,0.5f,0.5f);
		this.cursor_sprite.SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);

		//dijkstra,sprite_map
		this.dijkstra = new Fee.Dijkstra.Dijkstra<int,NodeData,LinkData>();
		this.sprite_map = new Fee.Render2D.Sprite2D[this.map_w * this.map_h];
		for(int ii=0;ii<this.sprite_map.Length;ii++){
			int t_tip_x = ii % this.map_w;
			int t_tip_y = ii / this.map_w;

			//スプライト。
			this.sprite_map[ii] = new Fee.Render2D.Sprite2D(this.deleter,null,0);
			this.sprite_map[ii].SetRect(this.map_x + t_tip_x * this.map_tip_w,this.map_y + t_tip_y * this.map_tip_h,this.map_tip_w-1,this.map_tip_h-1);

			//ダイクストラ法、ノード作成。
			int t_key = ii;
			this.dijkstra.AddNode(t_key,new Fee.Dijkstra.NodeEx<NodeData,LinkData>(new NodeData(t_tip_x,t_tip_y,0)));
		}

		//ノードをリンクでつなぐ。コストは仮設定で０。
		for(int ii=0;ii<this.sprite_map.Length;ii++){
			int t_tip_x = ii % this.map_w;
			int t_tip_y = ii / this.map_w;

			Fee.Dijkstra.NodeEx<NodeData,LinkData> t_from = this.dijkstra.GetNode(ii);

			//上。
			{
				Fee.Dijkstra.NodeEx<NodeData,LinkData> t_to = null;
				int t_to_x = t_tip_x;
				int t_to_y = t_tip_y - 1;
				if(t_to_y >= 0){
					t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
				}
				if(t_to != null){
					t_from.AddLink(new Fee.Dijkstra.LinkEx<NodeData,LinkData>(new LinkData(),t_to,0));
				}
			}

			//下。
			{
				Fee.Dijkstra.NodeEx<NodeData,LinkData> t_to = null;
				int t_to_x = t_tip_x;
				int t_to_y = t_tip_y + 1;
				if(t_to_y < this.map_h){
					t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
				}
				if(t_to != null){
					t_from.AddLink(new Fee.Dijkstra.LinkEx<NodeData,LinkData>(new LinkData(),t_to,0));
				}
			}

			//左。
			{
				Fee.Dijkstra.NodeEx<NodeData,LinkData> t_to = null;
				int t_to_x = t_tip_x - 1;
				int t_to_y = t_tip_y;
				if(t_to_x >= 0){
					t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
				}
				if(t_to != null){
					t_from.AddLink(new Fee.Dijkstra.LinkEx<NodeData,LinkData>(new LinkData(),t_to,0));
				}
			}

			//右。
			{
				Fee.Dijkstra.NodeEx<NodeData,LinkData> t_to = null;
				int t_to_x = t_tip_x + 1;
				int t_to_y = t_tip_y;
				if(t_to_x < this.map_w){
					t_to = this.dijkstra.GetNode(t_to_x + t_to_y * this.map_w);
				}
				if(t_to != null){
					t_from.AddLink(new Fee.Dijkstra.LinkEx<NodeData,LinkData>(new LinkData(),t_to,0));
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
						Fee.Dijkstra.NodeEx<NodeData,LinkData> t_node = this.dijkstra.GetNode(ii);
						System.Collections.Generic.List<Fee.Dijkstra.LinkEx<NodeData,LinkData>> t_linklist = t_node.GetLinkList();

						for(int jj=0;jj<t_linklist.Count;jj++){
							//リンクの接続先ノードのチップコストをリンクのコストとする。
							Fee.Dijkstra.NodeEx<NodeData,LinkData> t_to_node = t_linklist[jj].GetToNode();
							t_linklist[jj].SetToCost(t_to_node.nodedata.tipcost);
						}

						this.sprite_map[ii].SetColor((100 - t_node.nodedata.tipcost) / 100.0f,1.0f,1.0f,1.0f);
					}
				}
			
				//計算フラグをリセット。
				this.dijkstra.ResetCalcFlag();

				//ゴールを設定。
				if(this.mode == Mode.GoA_Start){
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

				if(this.mode == Mode.GoA_Start){
					this.mode = Mode.GoA_Move;
				}else{
					this.mode = Mode.GoB_Move;
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
					Fee.Dijkstra.NodeEx<NodeData,LinkData> t_node_now = this.dijkstra.GetNode(t_key_now);

					//移動先ノード。
					Fee.Dijkstra.NodeEx<NodeData,LinkData> t_node_to = t_node_now.GetPrevNode();

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
						}else{
							this.mode = Mode.GoA_Start;
						}
					}
				}
			}break;
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

