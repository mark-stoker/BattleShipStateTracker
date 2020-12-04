using BattleShipStateTracker.CellStateTracker.Enums;

namespace BattleShipStateTracker.Interfaces
{
	public interface IBoard
	{
		void CreateBoard();

		void AddShipToBoard(IShip ship);

		CellStateName? AttackCellOnBoard(IAttack attack);

		CellStateName? FindCellStateOnBoard(int x, int y);

		bool BoardCreated();

		bool AllOccupiedBoardCellsHit();

		bool BoardCellsPartiallyHit();}
}
