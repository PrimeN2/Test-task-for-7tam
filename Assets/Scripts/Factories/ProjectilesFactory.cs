using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Project.Gameplay
{
	public class ProjectilesFactory
	{
		private Object _projectilePrefab;

		public ProjectilesFactory()
		{
			_projectilePrefab = Resources.Load(GlobalNames.ProjectilePrefab);
		}

		public void Create(Vector3 at, Quaternion rotation, Player owner)
		{
			var projectile = Object.Instantiate(_projectilePrefab, at, rotation) as GameObject;
			projectile.GetComponent<Projectile>().Init(owner);
		}
	}
}