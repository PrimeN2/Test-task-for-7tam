using Photon.Pun;
using Photon.Realtime;
using Project.Infrastructure;
using Project.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Gameplay
{
	public class GameContext : MonoBehaviourPunCallbacks
	{
		private IGameStateSwitcher _gameStateSwitcher;

		private AwaitingLabel _awaitingLabel;
		private Button _startButton;
		private List<Player> _players;

		private int _playersAmount;

		[Inject]
		private void Construct(IGameStateSwitcher gameStateSwitcher,Button startButton, AwaitingLabel awaitingLabel)
		{
			_gameStateSwitcher = gameStateSwitcher;
			_startButton = startButton;
			_awaitingLabel = awaitingLabel;

			_players = new List<Player>();
			_playersAmount = 1;
		}

		public override void OnEnable()
		{
			_startButton.onClick.AddListener(StartFight);
			base.OnEnable();
		}

		public override void OnDisable()
		{
			_startButton.onClick.RemoveListener(StartFight);
			base.OnDisable();
		}

		public void AddPlayer(Player player)
		{
			_players.Add(player);

			player.SetName($"Player {_players.Count}");

			if (PhotonNetwork.IsMasterClient)
			{
				if (_players.Count > 1)
					_startButton.gameObject.SetActive(true);
			}
		}

		public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
		{

		}

		public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
		{
			
		}


		private void StartFight()
		{
			photonView.RPC(nameof(StartFightOnAllClients), RpcTarget.All);

			PhotonNetwork.CurrentRoom.IsOpen = false;
		}

		[PunRPC]
		private void StartFightOnAllClients()
		{
			foreach(var player in _players)
			{
				player.InitPlayerSystems();
			}

			_startButton.gameObject.SetActive(false);
			_awaitingLabel.gameObject.SetActive(false);
			_gameStateSwitcher.SwitchState<GameplayState>();
		}
	}
}