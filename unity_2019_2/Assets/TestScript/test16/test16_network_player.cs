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
	/** test16_network_player
	*/
	public class test16_network_player : Fee.Network.Pun_Player_MonoBehaviour
	{
		/** s_network_player
		*/
		public static test16_network_player s_network_player = null;

		/** self
		*/
		public bool self;

		/** Awake
		*/
		public void Awake()
		{
			this.self = false;
			{
				Photon.Pun.PhotonView t_view = this.GetComponent<Photon.Pun.PhotonView>();
				if(t_view != null){
					if(t_view.Owner != null){
						if(t_view.Owner.IsLocal == true){
							this.self = true;
						}
					}
				}
			}

			if(this.self == true){
				s_network_player = this;
			}
		}

		/** OnDestroy
		*/
		public void OnDestroy()
		{
			if(this.self == true){
				s_network_player = null;
			}
		}

		/** [Fee.Network.Pun_Player_MonoBehaviour]OnPhotonSerializeView
		*/
		#if (USE_DEF_FEE_PUN)
		public override void OnPhotonSerializeView(Photon.Pun.PhotonStream a_stream,Photon.Pun.PhotonMessageInfo a_info)
		{
		}
		#endif
	}
}

