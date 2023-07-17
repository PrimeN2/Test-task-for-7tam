using Photon.Pun;
using Project.Factories;
using Project.Gameplay;
using UnityEngine;
using Zenject;

namespace Project.Infrastructure
{
	public class GameplayState : BaseGameState
	{
		private readonly SceneLoader _sceneLoader;

		private PlayersFactory _playersFactory;

		public GameplayState(
			IGameStateSwitcher stateSwitcher, SceneLoader sceneLoader) 
			: base(stateSwitcher)
		{
			_sceneLoader = sceneLoader;
		}

		public override void Load()
		{
			_sceneLoader.OnSceneLoaded += SpawnPlayer;
		}

		public override void Dispose()
		{
			_sceneLoader.OnSceneLoaded -= SpawnPlayer;
		}

		private void SpawnPlayer()
		{
			_playersFactory = Object.FindAnyObjectByType<PlayersFactory>();
			_playersFactory.CreatePlayer();
		}
	}
}
