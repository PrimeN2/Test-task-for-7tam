using Unity.VisualScripting;
using UnityEngine;


namespace Project.Gameplay
{
	public class PlayerControlls : MonoBehaviour
	{
		[SerializeField] private GameObject _joystickPrefab;

		public Vector3 Direction { get => _currentJoystick.Direction; }

		private FloatingJoystick _currentJoystick;

		public void Init(Canvas canvas)
		{
			if (!_currentJoystick)
			{
				_currentJoystick =
					Instantiate(_joystickPrefab, canvas.transform)
					.GetComponentInChildren<FloatingJoystick>();
			}
		}
	}
}