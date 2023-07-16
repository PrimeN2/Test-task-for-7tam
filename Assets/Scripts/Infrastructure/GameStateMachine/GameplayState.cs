namespace Project.Infrastructure
{
	public class GameplayState : BaseGameState
	{
		public GameplayState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
		{
		}

		public override void Dispose()
		{
		}

		public override void Load()
		{
		}
	}
}