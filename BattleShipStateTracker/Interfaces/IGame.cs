using BattleShipStateTracker.Enums;

namespace BattleShipStateTracker.Interfaces
{
	public interface IGame
	{
		GameStateName GameStateName { get; set; }

		IBoard Board { get; set; }

		GameStateName GetGameState();

		string OutputGameState();
	}
}
