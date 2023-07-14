using UnityEngine;
using Zenject;

namespace Project.Infrastructure
{
	public class Bootstraper : MonoBehaviour
	{
		private GameStateMachine _gameStateMachine;

		[Inject]
		private void Construct(GameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;
		}

		private void Start()
		{
			_gameStateMachine.SwitchState<LobbyState>();
		}
	}

}
