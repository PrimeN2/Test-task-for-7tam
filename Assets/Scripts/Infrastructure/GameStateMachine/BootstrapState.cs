using Photon.Pun;

namespace Project.Infrastructure
{
	public class BootstrapState : BaseGameState
	{
		public BootstrapState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
		{

		}
		public override void Load()
		{
			PhotonNetwork.ConnectUsingSettings();
		}

		public override void Dispose()
		{
		}

	}
}
