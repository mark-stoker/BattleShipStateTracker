using System;
using System.Collections.Generic;
using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.GameStatus;

namespace BattleShipStateTracker
{
	public class Board : IBoard
	{
		private const int BoardWidth = 10;
		private const int BoardHeight = 10;

		public ICell[,] BoardCells { get; set; }
		public IGameState GameState { get; set; }
		public IList<Ship> Ships { get; set; }

		public Board()
		{
			GameState = new GameState();
			Ships = new List<Ship>();
			CreateBoard();
		}

		public void CreateBoard()
		{
			BoardCells = new ICell[BoardWidth, BoardHeight];

			for (var x = 0; x < BoardWidth; x++)
			{
				for (var y = 0; y < BoardWidth; y++)
				{
					BoardCells[x, y] = new Cell(x + 1, y + 1);
				}
			}
		}

		public void AddShipToBoard(int xStartCoOrdinate, int yStartCoOrdinate, int length, Alignment alignment)
		{
			try
			{
				var ship = new Ship(xStartCoOrdinate, yStartCoOrdinate, length, alignment);
				Ships.Add(ship);

				int horizontalLength = xStartCoOrdinate; //Represents the min length
				int verticalLength = yStartCoOrdinate; //represents the min length

				//Works out length based on alignment
				if (alignment == Alignment.Horizontal)
					horizontalLength = xStartCoOrdinate + length - 1;

				if (alignment == Alignment.Vertical)
					verticalLength = yStartCoOrdinate + length - 1;

				for (int x = yStartCoOrdinate - 1; x < verticalLength; x++)
				{
					for (int y = xStartCoOrdinate - 1; y < horizontalLength; y++)
					{
						BoardCells[x, y].State.ChangeState(BoardCells[x, y]);
					}
				}

			}
			catch (System.IndexOutOfRangeException)
			{
				Console.Write("You must add a ship within the bounds of the 10 x 10 board");
			}
		}

		public CellStateName? AttackCellOnBoard(int xCoOrdinate, int yCoOrdinate)
		{
			CellStateName? result = null;
			var cell = BoardCells[xCoOrdinate - 1, yCoOrdinate - 1];

			try
			{
				result = cell.State.IncomingAttack(cell);

				if (BoardCellsPartiallyHit())
					GameState.GameStateName = GameStateName.PartialShipHits;

				if (AllOccupiedBoardCellsHit())
				{
					GameState.GameStateName = GameStateName.AllShipsSunk;
					cell.ChangeState();
				}

				return cell.State.ReportState();
			}
			catch (System.IndexOutOfRangeException)
			{
				Console.Write("You must attack a cell within the bounds of the 10 x 10 board");
			}

			return result;
		}

		public CellStateName FindCellStateOnBoard(int x, int y)
		{
			return BoardCells[x - 1, y - 1].State.ReportState();
		}

		private bool AllOccupiedBoardCellsHit()
		{
			for (int col = 0; col < BoardCells.GetLength(1); col++)
			{
				for (int row = 0; row < BoardCells.GetLength(0); row++)
				{
					if (BoardCells[row, col].State.ReportState().ToString() == CellStateName.Occupied.ToString())
						return false;
				}
			}

			return true;
		}

		private bool BoardCellsPartiallyHit()
		{
			for (int col = 0; col < BoardCells.GetLength(1); col++)
			{
				for (int row = 0; row < BoardCells.GetLength(0); row++)
				{
					if (BoardCells[row, col].State.ReportState().ToString() == CellStateName.Hit.ToString())
						return true;
				}
			}

			return false;
		}
	}
}
