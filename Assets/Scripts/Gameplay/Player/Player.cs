using Photon.Pun;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.Gameplay
{
	[RequireComponent(typeof(PlayerView), typeof(Health), typeof(PlayerControlls))]
	public class Player : MonoBehaviourPun
	{
		[SerializeField] private Transform _shootingPoint;
		[SerializeField] private Transform _playerModel;
		[SerializeField] private Health _health;

		[SerializeField] private PlayerControlls _playerControlls;
		[SerializeField] private PlayerView _playerView;

		[SerializeField] private float _speed = 10;
		[SerializeField] private float _rotationSpeed = 720f;

		[SerializeField] private float _shootingCooldown = 0.8f;

		public string Name { get; private set; }
		public int Coins { get; private set; }
		public bool IsActive;

		private GameContext _gameContext;
		private TMP_Text _coinsLabel;
		private Gamefield _gamefield;
		private Canvas _canvas;
		private ProjectilesFactory _projectilesFactory;


		[Inject]
		private void Construct(
			Gamefield gamefield, Canvas canvas, ProjectilesFactory projectilesFactory, TMP_Text coinsLabel)
		{
			_gamefield = gamefield;
			_canvas = canvas;
			_projectilesFactory = projectilesFactory;
			_coinsLabel = coinsLabel;

			GetPlayerComponents();
		}

		private void Start()
		{
			IsActive = true;

			GetUninjectableComponents();

			_gameContext.AddPlayer(this);

			if (!photonView.IsMine)
				return;

			transform.position = _gamefield.GetRandomPositionOnField();
		}



		private void OnTriggerEnter2D(Collider2D collision)
		{
			Projectile projectile;
			Coin coin;

			if (collision.TryGetComponent(out projectile) && projectile.Owner != this)
			{
				_health.TakeDamage(projectile.Damage);
				projectile.Dispose();
			}
			else if (collision.TryGetComponent(out coin))
			{
				if (!photonView.IsMine)
					return;

				Coins++;
				_coinsLabel.text = $"Coins: {Coins}";
				coin.Dispose();
			}
		}

		private void OnEnable()
		{
			_health.OnPlayerDied += Dispose;
		}

		private void OnDisable()
		{
			_health.OnPlayerDied -= Dispose;
		}

		public void SetName(string name)
		{
			_playerView.SetName(name);
			Name = name;
		}

		public void InitPlayerSystems()
		{
			_health.Init(10);

			StartCoroutine(Shooting());

			if (!photonView.IsMine)
				return;
			_playerControlls.Init(_canvas);

			StartCoroutine(PlayerMovement());
		}

		private IEnumerator PlayerMovement()
		{
			while (IsActive)
			{
				MovePlayer();
				RotateModel();

				yield return null;
			}
		}

		private IEnumerator Shooting()
		{
			float cooldown = _shootingCooldown;

			while (IsActive)
			{
				cooldown -= Time.deltaTime;

				if (cooldown < 0)
				{
					Shoot();

					cooldown = _shootingCooldown;
				}

				yield return null;
			}
		}

		private void Shoot()
		{
			_projectilesFactory.Create(_shootingPoint.position, _playerModel.transform.rotation, this);
		}

		private void GetPlayerComponents()
		{
			_health = GetComponent<Health>();
			_playerControlls = GetComponent<PlayerControlls>();
			_playerView = GetComponent<PlayerView>();
		}

		private void MovePlayer()
		{
			transform.Translate(_playerControlls.Direction * Time.deltaTime * _speed, Space.World);
		}

		private void RotateModel()
		{
			if (_playerControlls.Direction == Vector3.zero) 
				return;

			Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _playerControlls.Direction);
			_playerModel.rotation =
				Quaternion.RotateTowards(_playerModel.rotation, toRotation, _rotationSpeed * Time.deltaTime);
		}
		private void Dispose()
		{
			_gameContext.RemovePlayer(this);
			photonView.RPC(nameof(RequestOwnerForDispose), RpcTarget.All);
		}

		[PunRPC]
		private void RequestOwnerForDispose()
		{
			if (photonView.IsMine)
				PhotonNetwork.Destroy(gameObject);
		}
		private void GetUninjectableComponents()
		{
			_projectilesFactory = new ProjectilesFactory();
			_gameContext = FindObjectOfType<GameContext>();
		}
	}
}
