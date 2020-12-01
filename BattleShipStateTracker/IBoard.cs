using System.Collections.Generic;
using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.GameStatus;

namespace BattleShipStateTracker
{
	public interface IBoard
	{
		//Todo remove these and make them private
		ICell[,] BoardCells { get; set; }
		IGameState GameState { get; set; }
		IList<Ship> Ships { get; set; }

		void CreateBoard();

		void AddShipToBoard(int xStartCoOrdinate, int yStartCoOrdinate, int length, Alignment alignment);

		CellStateName? AttackCellOnBoard(int xCoOrdinate, int yCoOrdinate);

		CellStateName FindCellStateOnBoard(int x, int y);
	}
}
