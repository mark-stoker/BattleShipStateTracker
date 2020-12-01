using System.Collections.Generic;
using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.GameStatus;

namespace BattleShipStateTracker
{
	public interface IBoard
	{
		void CreateBoard();

		void AddShipToBoard(int xStartCoOrdinate, int yStartCoOrdinate, int length, Alignment alignment);

		CellStateName? AttackCellOnBoard(int xCoOrdinate, int yCoOrdinate);

		CellStateName FindCellStateOnBoard(int x, int y);

		GameStateName GetGameState();

		int NumberOfShipsOnBoard();
	}
}
