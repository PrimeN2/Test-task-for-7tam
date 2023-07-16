using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Gameplay
{
	public class Health : MonoBehaviour
	{
		public event Action OnPlayerDied;

		[SerializeField] private Image _healthBar;

		private float _health;
		private float _defaultHealth;

		public void Init(int defaultHealth)
		{
			_defaultHealth = defaultHealth;
			_health = defaultHealth;
		}

		public void TakeDamage(float damage)
		{
			_health -= damage;

			if (_health <= 0)
				OnPlayerDied?.Invoke();

			_healthBar.fillAmount -= damage / _defaultHealth;
		}
	}

}
