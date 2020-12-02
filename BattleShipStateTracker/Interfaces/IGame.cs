using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.Enums;

namespace BattleShipStateTracker.Interfaces
{
	public interface IGame
	{
		GameStateName GameStateName { get; set; }

		void CreateBoard();

		void AddShipToBoard(int xStartCoordinate, int yStartCoordinate, int length, ShipAlignment alignment);

		CellStateName? IncomingAttack(int xCoordinate, int yCoordinate);

		int NumberOfShipsOnBoard();

		bool BoardCreated();
	}
}
