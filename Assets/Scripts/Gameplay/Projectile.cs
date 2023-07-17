using Photon.Pun;
using System;
using UnityEngine;

namespace Project.Gameplay
{
	public class Projectile : MonoBehaviour
	{
		public float Damage => _damage;

		public Player Owner { get; private set; }

		[SerializeField] private float _damage;

		[SerializeField] private float _speed;

		private float _timeLiving;

		private void Update()
		{
			transform.Translate(_speed * Time.deltaTime * Vector3.up, Space.Self);

			_timeLiving += Time.deltaTime;

			if (_timeLiving > 5)
				Dispose();
		}

		public void Init(Player owner)
		{
			Owner = owner;
		}

		public void Dispose()
		{
			Destroy(gameObject);
		}
	}
}