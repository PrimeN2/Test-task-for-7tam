using Photon.Pun;
using Project.Infrastructure;
using UnityEngine;
using Zenject;

namespace Project.Lobby
{
	public class RoomHandler : MonoBehaviour, IRoomHandler
	{
		private IGameStateSwitcher _gameStateSwitcher;

		[Inject]
		private void Construct(IGameStateSwitcher gameStateSwitcher)
		{
			_gameStateSwitcher = gameStateSwitcher;
		}

		public void CreateRoom(string roomName)
		{
			PhotonNetwork.CreateRoom(roomName);
			_gameStateSwitcher.SwitchState<AwaitingState>();
		}

		public void JoinRoom(string roomName)
		{
			PhotonNetwork.JoinRoom(roomName);
			_gameStateSwitcher.SwitchState<AwaitingState>();
		}
	}
}