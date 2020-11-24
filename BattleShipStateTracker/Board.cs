using System;
using System.Collections.Generic;
using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.GameStateTracker;

namespace BattleShipStateTracker
{
	public class Board : IBoard
	{
		private const int BoardWidth = 10;
		private const int BoardHeight = 10;
		private bool _firstSuccessfulAttack = true;

		public Cell[,] BoardCells { get; set; }
		public Game Game { get; set; }
		public IList<Ship> Ships { get; set; }

		public Board()
		{
			Game = new Game();
			
			Ships = new List<Ship>();
			CreateBoard();
		}

		public void CreateBoard()
		{
			BoardCells = new Cell[BoardWidth, BoardHeight];

			for (int x = 0; x < BoardWidth; x++)
			{
				for (int y = 0; y < BoardWidth; y++)
				{
					BoardCells[x, y] = new Cell(x + 1, y + 1) { State = new WaterState() };
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
				{
					horizontalLength = xStartCoOrdinate + length - 1;
				}
				else
				{
					verticalLength = yStartCoOrdinate + length - 1;
				}

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

			try
			{
				result = BoardCells[xCoOrdinate - 1, yCoOrdinate - 1].State
					.IncomingAttack(BoardCells[xCoOrdinate - 1, yCoOrdinate - 1], BoardCells);
				
				if (_firstSuccessfulAttack && result == CellStateName.Hit)
					Game.State.ChangeState(Game);

				if (result == CellStateName.Hit)
					_firstSuccessfulAttack = false;

				if (result == CellStateName.Sunk)
					Game.State.ChangeState(Game);

				return BoardCells[xCoOrdinate - 1, yCoOrdinate - 1].State.ReportState();
			}
			catch (System.IndexOutOfRangeException)
			{
				Console.Write("You must attack a cell within the bounds of the 10 x 10 board");
			}

			return result;
		}
	}
}
