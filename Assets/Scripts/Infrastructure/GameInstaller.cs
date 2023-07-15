using Project.Gameplay;
using UnityEngine;
using Zenject;

namespace Project.Infrastructure
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private Gamefield _gamefield;

		public override void InstallBindings()
		{
			BindGamefield();
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
