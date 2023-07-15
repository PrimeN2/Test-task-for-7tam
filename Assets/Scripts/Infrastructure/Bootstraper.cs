using UnityEngine;
using Zenject;

namespace Project.Infrastructure
{
	public class Bootstraper : MonoBehaviour
	{
		private IGameStateSwitcher _gameStateSwitcher;

		[Inject]
		private void Construct(IGameStateSwitcher gameStateSwitcher)
		{
			_gameStateSwitcher = gameStateSwitcher;
		}

		private void Start()
		{
			_gameStateSwitcher.SwitchState<LobbyState>();
		}
	}

}
