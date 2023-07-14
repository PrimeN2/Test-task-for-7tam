using Zenject;

namespace Project.Factories
{
	public class UIFactory
	{
		private DiContainer _container;

		public UIFactory(DiContainer container)
		{
			_container = container;
		}
	}
}
