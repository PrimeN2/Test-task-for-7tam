using Project.Gameplay;

namespace Project.Factories
{
	public interface IPlayersFactory
	{
		void Load();
		Player Create();
	}
}