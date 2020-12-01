using System.Collections.Generic;
using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.Game;

namespace BattleShipStateTracker
{
	public interface IBoard
	{
		ICell[,] BoardCells { get; set; }
		IGameState GameState { get; set; }
		IList<Ship> Ships { get; set; }

		void CreateBoard();

		void AddShipToBoard(int xStartCoOrdinate, int yStartCoOrdinate, int length, Alignment alignment);

		CellStateName? AttackCellOnBoard(int xCoOrdinate, int yCoOrdinate);
	}
}
