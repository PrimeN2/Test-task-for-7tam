using Photon.Pun;
using UnityEngine;

namespace Project.Gameplay
{
	public class Coin : MonoBehaviourPun
	{
		public void Dispose()
		{
			photonView.RPC(nameof(RequestMasterForDispose), RpcTarget.All);
		}

		[PunRPC]
		private void RequestMasterForDispose()
		{
			if (PhotonNetwork.IsMasterClient)
				PhotonNetwork.Destroy(gameObject);
		}
	}
}