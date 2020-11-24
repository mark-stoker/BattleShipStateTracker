using System.Collections.Generic;
using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.GameStateTracker;

namespace BattleShipStateTracker
{
	public interface IBoard
	{
		Cell[,] BoardCells { get; set; }
		Game Game { get; set; }
		IList<Ship> Ships { get; set; }

		void CreateBoard();

		void AddShipToBoard(int xStartCoOrdinate, int yStartCoOrdinate, int length, Alignment alignment);

		CellStateName? AttackCellOnBoard(int xCoOrdinate, int yCoOrdinate);
	}
}
