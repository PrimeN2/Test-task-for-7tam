namespace Project.Lobby
{
	public interface IRoomHandler
	{
		void CreateRoom(string roomName);
		void JoinRoom(string roomName);
	}
}