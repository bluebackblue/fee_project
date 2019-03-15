

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダイクストラ法。
*/


/** Fee.Dijkstra
*/
namespace Fee.Dijkstra
{
	/** Dijkstra
	*/
	public class Dijkstra<NODEKEY,NODEDATA,LINKDATA>
		where NODEDATA : struct
		where LINKDATA : struct
	{
		/** ノードリスト。
		*/
		public System.Collections.Generic.Dictionary<NODEKEY,NodeEx<NODEDATA,LINKDATA>> node_list;

		/** constructor
		*/
		public Dijkstra()
		{
			this.node_list = new System.Collections.Generic.Dictionary<NODEKEY,NodeEx<NODEDATA,LINKDATA>>();
		}

		/** クリア。
		*/
		public void ClearAll()
		{
			this.node_list.Clear();
		}

		/** ノード追加。
		*/
		public void AddNode(NODEKEY a_nodekey,NodeEx<NODEDATA,LINKDATA> a_node)
		{
			this.node_list.Add(a_nodekey,a_node);
		}

		/** ノード取得。
		*/
		public NodeEx<NODEDATA,LINKDATA> GetNode(NODEKEY a_nodekey)
		{
			NodeEx<NODEDATA,LINKDATA> t_node;

			if(this.node_list.TryGetValue(a_nodekey,out t_node) == true){
				return t_node;
			}

			return null;
		}

		/** ノード削除。
		*/
		public bool RemoveNode(NODEKEY a_nodekey)
		{
			return this.node_list.Remove(a_nodekey);
		}

		/** 計算フラグのリセット。
		*/
		public void ResetCalcFlag()
		{
			foreach(System.Collections.Generic.KeyValuePair<NODEKEY,NodeEx<NODEDATA,LINKDATA>> t_pair in this.node_list){
				t_pair.Value.ResetCalcFlag();
			}
		}

		/** 隣接ノード未計算で到達コストが最小のノードを検索。
		*/
		private NodeEx<NODEDATA,LINKDATA> SearchNoFixNodeMinCost()
		{
			NodeEx<NODEDATA,LINKDATA> t_find = null;

			foreach(System.Collections.Generic.KeyValuePair<NODEKEY,NodeEx<NODEDATA,LINKDATA>> t_pair in this.node_list){
				NodeEx<NODEDATA,LINKDATA> t_node = t_pair.Value;

				if((t_node.GetFixFlag() == false)&&(t_node.GetTotalCost() >= 0)){
					//到達コストあり、隣接ノード未計算。
					if(t_find == null){
						t_find = t_node;
					}else if(t_node.GetTotalCost() < t_find.GetTotalCost()){
						t_find = t_node;
					}						
				}
			}

			return t_find;
		}

		/** 開始ノードに設定。
		*/
		public void SetStartNode(NodeEx<NODEDATA,LINKDATA> a_node_start)
		{
			a_node_start.SetStartNode();
		}

		/** 計算。

			戻り値 == true : 継続。

		*/
		public bool Calc()
		{
			//隣接ノード未計算で到達コストが最小のノードを検索。
			NodeEx<NODEDATA,LINKDATA> t_node_current = this.SearchNoFixNodeMinCost();
			if(t_node_current == null){
				return false;
			}

			//隣接ノード計算開始。
			t_node_current.SetFixFlag(true);

			System.Collections.Generic.List<LinkEx<NODEDATA,LINKDATA>> t_linklist_current = t_node_current.GetLinkList();

			for(int ii=0;ii<t_linklist_current.Count;ii++){
				NodeEx<NODEDATA,LINKDATA> t_node = t_linklist_current[ii].GetToNode();
				long t_total_cost = t_node_current.GetTotalCost() + t_linklist_current[ii].GetToCost();
				if((t_node.GetTotalCost() < 0)||(t_node.GetTotalCost() > t_total_cost)){
					//このノードへ到達するための隣接ノード。
					t_node.SetTotalCost(t_total_cost);
					t_node.SetPrevNode(t_node_current);
				}
			}

			return true;
		}
	}
}

