using Project.Factories;
using Project.Gameplay;
using System;
using UnityEngine;
using Zenject;

namespace Project.Infrastructure
{
	public class BootstrapInstaller : MonoInstaller
	{
		[SerializeField] private GameObject _sceneLoaderPrefab;

		public override void InstallBindings()
		{
			BindSceneLoader();
			BindGameStateSwitcher();
			BindUIFactory();
			BindProjectilesFactory();
		}

		private void BindProjectilesFactory()
		{
			Container
				.Bind<ProjectilesFactory>()
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
