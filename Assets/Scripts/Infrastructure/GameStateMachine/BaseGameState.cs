﻿namespace Project.Infrastructure
{
	public abstract class BaseGameState
	{
		protected IGameStateSwitcher _stateSwitcher;

		protected BaseGameState(IGameStateSwitcher stateSwitcher)
		{
			_stateSwitcher = stateSwitcher;
		}

		public abstract void Load();
		public abstract void Dispose();
	}
}