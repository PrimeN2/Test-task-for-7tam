using Project.Lobby;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI
{
	public class JoinRoomPresenter : MonoBehaviour
	{
		[SerializeField] private Button _button;
		[SerializeField] private TMP_InputField _roomName;

		private IRoomHandler _roomHandler;

		[Inject]
		private void Construct(IRoomHandler roomHandler)
		{
			_roomHandler = roomHandler;
		}

		private void Start()
		{
			_button.onClick.AddListener(JoinRoom);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(JoinRoom);
		}

		private void JoinRoom()
		{
			_roomHandler.JoinRoom(_roomName.text);
		}
	}
}