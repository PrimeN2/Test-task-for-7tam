﻿namespace Project.Infrastructure
{
	public interface IGameStateSwitcher
	{
		void SwitchState<T>() where T : BaseGameState;
	}
}