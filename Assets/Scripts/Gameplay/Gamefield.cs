using UnityEngine;
using Zenject;

namespace Project.Gameplay
{
	public class Gamefield : MonoBehaviour
	{
		public Vector3 GetRandomPositionOnField()
		{
			return new Vector2(Random.Range(8.2f, -8.2f), Random.Range(4.3f, -4.3f));
		}
	}
}