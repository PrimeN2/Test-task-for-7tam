using Project.Factories;
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
			BindGameStateMachine();
			BindUIFactory();
		}

		private void BindUIFactory()
		{
			Container
				.Bind<UIFactory>()
				.AsSingle();
		}

		private void BindGameStateMachine()
		{
			Container
				.Bind<GameStateMachine>()
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
