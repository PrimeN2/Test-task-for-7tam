using Photon.Pun;
using Photon.Realtime;
using Project.Gameplay;
using UnityEngine;
using Zenject;

namespace Project.Factories
{
	public class PlayersFactory : MonoBehaviourPun
	{
		private DiContainer _container;
		private Gameplay.Player _currentPlayer;

		[Inject]
		private void Construct(DiContainer container)
		{
			_container = container;
		}

		public void CreatePlayer()
		{
			_currentPlayer =
				PhotonNetwork.Instantiate(GlobalNames.PlayerPrefab, Vector2.zero, Quaternion.identity)
				.GetComponent<Gameplay.Player>();

			_currentPlayer.transform.SetParent(transform);

			_container.InjectGameObject(_currentPlayer.gameObject);
		}
	}
}
