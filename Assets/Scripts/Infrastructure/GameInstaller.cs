using Photon.Pun;
using Project.Gameplay;
using Project.UI;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Infrastructure
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private Canvas _canvas;
		[SerializeField] private Button _startButton;
		[SerializeField] private Gamefield _gamefield;
		[SerializeField] private GameContext _gameContext;
		[SerializeField] private AwaitingLabel _awaitingLabel;

		public override void InstallBindings()
		{
			BindAwaitingLabel();
			BindStartButton();
			BindGameContext();
			BindGamefield();
			BindCanvas();
		}

		private void BindAwaitingLabel()
		{
			Container
				.Bind<AwaitingLabel>()
				.FromInstance(_awaitingLabel)
				.AsSingle();
		}

		private void BindStartButton()
		{
			Container
				.Bind<Button>()
				.FromInstance(_startButton)
				.AsSingle();
		}

		private void BindCanvas()
		{
			Container
				.Bind<Canvas>()
				.FromInstance(_canvas)
				.AsSingle();
		}

		private void BindGameContext()
		{
			Container
				.Bind<GameContext>()
				.FromInstance(_gameContext)
				.AsSingle();
		}

		private void BindGamefield()
		{
			Container
				.Bind<Gamefield>()
				.FromInstance(_gamefield)
				.AsSingle();
		}
	}
}
