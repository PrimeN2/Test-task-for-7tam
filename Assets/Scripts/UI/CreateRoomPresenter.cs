using Project.Lobby;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI
{
	public class CreateRoomPresenter : MonoBehaviour
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
			_button.onClick.AddListener(CreateRoom);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(CreateRoom);
		}

		private void CreateRoom()
		{
			_roomHandler.CreateRoom(_roomName.text);
		}
	}
}
