namespace Project.Infrastructure
{
	public class LobbyState : BaseGameState
	{
		private SceneLoader _sceneLoader;

		public LobbyState(IGameStateSwitcher stateSwitcher, SceneLoader sceneLoader) 
			: base(stateSwitcher)
		{
			_sceneLoader = sceneLoader;
		}
		public override void Load()
		{
			_sceneLoader.LoadScene(GlobalNames.LobbyScene);
		}

		public override void Dispose()
		{
			
		}

	}
}
