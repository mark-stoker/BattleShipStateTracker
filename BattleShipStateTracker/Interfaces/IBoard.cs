using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.Enums;

namespace BattleShipStateTracker.Interfaces
{
	public interface IBoard
	{
		void CreateBoard();

		void AddShipToBoard(int xStartCoOrdinate, int yStartCoOrdinate, int length, ShipAlignment alignment);

		CellStateName? AttackCellOnBoard(int xCoOrdinate, int yCoOrdinate);

		CellStateName? FindCellStateOnBoard(int x, int y);

		int NumberOfShipsOnBoard();

		bool BoardCreated();

		bool AllOccupiedBoardCellsHit();

		bool BoardCellsPartiallyHit();
	}
}
