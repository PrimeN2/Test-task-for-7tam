using Project.Gameplay;
using Project.Lobby;
using UnityEngine;
using Zenject;

namespace Project.Infrastructure
{
	public class LobbyInstaller : MonoInstaller
	{
		[SerializeField] private RoomHandler _roomHandler;

		public override void InstallBindings()
		{
			BindRoomHandlerService();
		}

		private void BindRoomHandlerService()
		{
			Container
				.Bind<IRoomHandler>()
				.To<RoomHandler>()
				.FromInstance(_roomHandler)
				.AsSingle();
		}
	}
}
