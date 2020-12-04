using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.Enums;

namespace BattleShipStateTracker.Interfaces
{
	public interface IGame
	{
		GameStateName GameStateName { get; set; }

		IBoard Board { get; set; }

		void AddShipToBoard(int xStartCoordinate, int yStartCoordinate, int length, ShipAlignment alignment);

		CellStateName? AttackCellOnBoard(int xCoordinate, int yCoordinate);

		GameStateName GetGameState();
	}
}
