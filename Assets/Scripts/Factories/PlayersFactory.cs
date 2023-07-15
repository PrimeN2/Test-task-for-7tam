using Photon.Pun;
using Project.Gameplay;
using UnityEngine;
using Zenject;

namespace Project.Factories
{
	public class PlayersFactory : IPlayersFactory
	{
		private DiContainer _container;

		private Object _playerPrefab;

		public PlayersFactory(DiContainer container)
		{
			_container = container;
		}

		[Inject]
		private void Construct(DiContainer container) 
		{
			_container = container;
		}

		public void Load()
		{
			_playerPrefab = Resources.Load(GlobalNames.PlayerPrefab);
		}

		public Player Create()
		{
			var player =
				PhotonNetwork.Instantiate(_playerPrefab.name, Vector2.zero, Quaternion.identity)
				.GetComponent<Player>();

			BindPlayer(player);

			return player;
		}

		private void BindPlayer(Player player)
		{
			_container.InjectGameObject(player.gameObject);

			_container
				.Bind<Player>()
				.FromInstance(player)
				.AsSingle();
		}
	}
}
