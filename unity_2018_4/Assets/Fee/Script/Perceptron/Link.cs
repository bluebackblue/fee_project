

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パーセプトロン。
*/


/** Fee.Perceptron
*/
namespace Fee.Perceptron
{
	/** Link
	*/
	public class Link
	{
		/** weight
		*/
		public float weight;

		/** weight_change_rate
		*/
		public float weight_change_rate;

		/** node_from
		*/
		public Node node_from;
		
		/** node_to
		*/
		public Node node_to;

		/** Link
		*/
		public Link(Node a_node_from,Node a_node_to)
		{
			//weight
			this.weight = 0.5f;

			//weight_change_rate
			this.weight_change_rate = 0.02f;

			//node_from
			this.node_from = a_node_from;

			//node_to
			this.node_to = a_node_to;
		}
	}
}

