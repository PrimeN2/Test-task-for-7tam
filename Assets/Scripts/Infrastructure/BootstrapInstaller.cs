using Project.Factories;
using UnityEngine;
using Zenject;

namespace Project.Infrastructure
{
	public class BootstrapInstaller : MonoInstaller
	{
		[SerializeField] private GameObject _sceneLoaderPrefab;

		public override void InstallBindings()
		{
			BindPlayersFactory();
			BindSceneLoader();
			BindGameStateSwitcher();
			BindUIFactory();
		}


		private void BindPlayersFactory()
		{
			Container
				.Bind<IPlayersFactory>()
				.To<PlayersFactory>()
				.AsSingle();
		}

		private void BindUIFactory()
		{
			Container
				.Bind<UIFactory>()
				.AsSingle();
		}

		private void BindGameStateSwitcher()
		{
			Container
				.Bind<IGameStateSwitcher>()
				.To<GameStateMachine>()
				.AsSingle();
		}

		private void BindSceneLoader()
		{
			var sceneLoader = Container
				.InstantiatePrefabForComponent<SceneLoader>(_sceneLoaderPrefab);

			Container
				.Bind<SceneLoader>()
				.FromInstance(sceneLoader)
				.AsSingle();
		}
	}

}
