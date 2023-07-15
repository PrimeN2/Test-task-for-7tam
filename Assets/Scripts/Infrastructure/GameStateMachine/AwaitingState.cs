using Photon.Pun;
using Project.Factories;
using Project.Gameplay;
using UnityEngine;
using Zenject;

namespace Project.Infrastructure
{
	public class AwaitingState : BaseGameState
	{
		private readonly IPlayersFactory _playersFactory;
		private readonly SceneLoader _sceneLoader;

		public AwaitingState(
			IGameStateSwitcher stateSwitcher, SceneLoader sceneLoader, IPlayersFactory playersFactory) 
			: base(stateSwitcher)
		{
			_sceneLoader = sceneLoader;
			_playersFactory = playersFactory;
		}
		public override void Load()
		{
			_sceneLoader.OnSceneLoaded += GeneratePlayer;
		}

		private void GeneratePlayer()
		{
			_playersFactory.Load();
			_playersFactory.Create();
		}

		public override void Dispose()
		{
			_sceneLoader.OnSceneLoaded -= GeneratePlayer;
		}

	}
}
