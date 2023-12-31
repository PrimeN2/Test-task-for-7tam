﻿using Photon.Pun;
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
		private VictoryMenu _victoryMenu;
		private Button _startButton;
		private List<Player> _players;

		[Inject]
		private void Construct(
			IGameStateSwitcher gameStateSwitcher, Button startButton, AwaitingLabel awaitingLabel,
			VictoryMenu victoryMenu)
		{
			_gameStateSwitcher = gameStateSwitcher;
			_startButton = startButton;
			_awaitingLabel = awaitingLabel;
			_victoryMenu = victoryMenu;

			_players = new List<Player>();
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
				{
					_startButton.gameObject.SetActive(true);

					SyncPlayersNames();
				}
			}
		}

		private void SyncPlayersNames()
		{
			object[] names = new object[_players.Count];

			for (int i = 0; i < _players.Count; i++)
			{
				names[i] = _players[i].Name;
			}

			photonView.RPC(nameof(RequestSyncPlayersNames), RpcTarget.All, names);
		}

		[PunRPC]
		private void RequestSyncPlayersNames(object[] names)
		{
			if (PhotonNetwork.IsMasterClient)
				return;

			for (int i = _players.Count - 1; i >= 0; i--)
			{
				_players[i].SetName((string)names[i]);
			}
		}

		public void RemovePlayer(Player player)
		{
			_players.Remove(player);

			if (_players.Count < 2)
			{
				_players[0].IsActive = false;

				if(_players[0].photonView.IsMine)
					photonView.RPC(
						nameof(RequestOwnerForPlayer), RpcTarget.All, _players[0].Name, _players[0].Coins);
			}

		}

		[PunRPC]
		private void RequestOwnerForPlayer(string name, int coins)
		{
			_victoryMenu.TurnOn(name, coins);
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