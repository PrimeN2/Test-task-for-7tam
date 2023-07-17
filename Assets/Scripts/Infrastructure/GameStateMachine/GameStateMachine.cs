using Project.Factories;
using System.Collections.Generic;
using System.Linq;

namespace Project.Infrastructure
{
	public class GameStateMachine : IGameStateSwitcher
	{
		private List<BaseGameState> _states;
		private BaseGameState _currentState;

		public GameStateMachine(SceneLoader sceneLoader)
		{
			_states = new List<BaseGameState>()
			{
				new BootstrapState(this),
				new LobbyState(this, sceneLoader),
				new GameplayState(this, sceneLoader),
			};

			_currentState = _states[0];
			_currentState.Load();
		}


		public void SwitchState<T>() where T : BaseGameState
		{
			var state = _states.FirstOrDefault(s => s is T);

			_currentState.Dispose();
			_currentState = state;
			_currentState.Load();
		}
	}
}