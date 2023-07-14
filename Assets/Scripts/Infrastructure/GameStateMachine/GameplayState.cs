namespace Project.Infrastructure
{
	public class GameplayState : BaseGameState
	{
		public GameplayState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
		{
		}

		public override void Dispose()
		{
			throw new System.NotImplementedException();
		}

		public override void Load()
		{
			throw new System.NotImplementedException();
		}
	}
}