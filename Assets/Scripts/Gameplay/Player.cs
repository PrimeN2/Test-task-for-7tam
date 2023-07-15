using Photon.Pun;
using Project.Infrastructure;
using UnityEngine;
using Zenject;

namespace Project.Gameplay
{
	[RequireComponent(typeof(PhotonView), typeof(PlayerView))]
	public class Player : MonoBehaviour
	{
		[SerializeField] private PlayerControlls _playerControlls;

		[SerializeField] private PlayerView _playerView;
		[SerializeField] private PhotonView _networkView;

		[SerializeField] private float _speed = 10;
		[SerializeField] private float _rotationSpeed = 720f;
		[SerializeField] private int _health = 10;

		private int _coins;

		[Inject]
		private void Construct()
		{
			_playerView = GetComponent<PlayerView>();
			_networkView = GetComponent<PhotonView>();

			if (_networkView.IsMine)
				_playerControlls.Init();
		}

		private void Start()
		{
			if (!_networkView.IsMine) 
				return;
				
			transform.position = new Vector2(Random.Range(8.4f, -8.4f), Random.Range(4.5f, -4.5f));
		}

		private void Update()
		{
			if (!_networkView.IsMine)
				return;

			Move();
			Rotate();
		}

		private void Move()
		{
			transform.Translate(_playerControlls.Direction * Time.deltaTime * _speed, Space.World);
		}

		private void Rotate()
		{
			if (_playerControlls.Direction == Vector3.zero) 
				return;

			Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _playerControlls.Direction);
			transform.rotation =
				Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
		}
	}
}
