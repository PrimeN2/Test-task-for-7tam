using UnityEngine;
using Zenject;

namespace Project.Gameplay
{
	public class Gamefield : MonoBehaviour
	{
		public Vector3 GetRandomPositionOnField()
		{
			return new Vector2(Random.Range(8.4f, -8.4f), Random.Range(4.5f, -4.5f));
		}
	}
}