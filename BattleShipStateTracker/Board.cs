﻿using System;
using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.CellStateTracker.Interfaces;
using BattleShipStateTracker.Enums;
using BattleShipStateTracker.Interfaces;

namespace BattleShipStateTracker
{
	public class Board : IBoard
	{
		private const int BoardWidth = 10;
		private const int BoardHeight = 10;

		private readonly ICell[,] _boardCells;
		private readonly IGame _game;
		private int _shipCount = 0;

		public Board()
		{
			_boardCells = new ICell[BoardWidth, BoardHeight];
			_game = new Game();
			CreateBoard();
		}

		public void CreateBoard()
		{
			for (var x = 0; x < BoardWidth; x++)
			{
				for (var y = 0; y < BoardWidth; y++)
				{
					_boardCells[x, y] = new Cell(x + 1, y + 1);
				}
			}
		}

		public void AddShipToBoard(int xStartCoOrdinate, int yStartCoOrdinate, int length, ShipAlignment alignment)
		{
			try
			{
				_shipCount += 1;

				int horizontalLength = xStartCoOrdinate; //Represents the min length
				int verticalLength = yStartCoOrdinate; //represents the min length

				//Works out length based on alignment
				if (alignment == ShipAlignment.Horizontal)
					horizontalLength = xStartCoOrdinate + length - 1;

				if (alignment == ShipAlignment.Vertical)
					verticalLength = yStartCoOrdinate + length - 1;

				for (int x = yStartCoOrdinate - 1; x < verticalLength; x++)
				{
					for (int y = xStartCoOrdinate - 1; y < horizontalLength; y++)
					{
						_boardCells[x, y].State.ChangeState(_boardCells[x, y]);
					}
				}
			}
			catch (System.IndexOutOfRangeException)
			{
				Console.WriteLine("You must add a ship within the bounds of the 10 x 10 board");
			}
		}

		public CellStateName? AttackCellOnBoard(int xCoOrdinate, int yCoOrdinate)
		{
			xCoOrdinate -= 1;
			yCoOrdinate -= 1;

			CellStateName? result = null;
			var cell = _boardCells[xCoOrdinate, yCoOrdinate];

			try
			{
				result = cell.State.IncomingAttack(cell);

				//TODO updating game state here breaks single responsibility
				//if (BoardCellsPartiallyHit())
				//	_game.GameStateName = GameStateName.PartialShipHits;

				if (AllOccupiedBoardCellsHit())
				{
					//_game.GameStateName = GameStateName.AllShipsSunk;
					cell.ChangeState();
				}

				return cell.State.ReportState();
			}
			catch (System.IndexOutOfRangeException)
			{
				Console.WriteLine("You must attack a cell within the bounds of the 10 x 10 board");
			}

			return result;
		}

		public CellStateName FindCellStateOnBoard(int x, int y)
		{
			return _boardCells[x - 1, y - 1].State.ReportState();
		}

		public int NumberOfShipsOnBoard()
		{
			return _shipCount;
		}

		//TODO it feels like the two methods below could be merged??
		private bool AllOccupiedBoardCellsHit()
		{
			for (int col = 0; col < _boardCells.GetLength(1); col++)
			{
				for (int row = 0; row < _boardCells.GetLength(0); row++)
				{
					if (_boardCells[row, col].State.ReportState().ToString() == CellStateName.Occupied.ToString())
						return false;
				}
			}

			return true;
		}

		private bool BoardCellsPartiallyHit()
		{
			for (int col = 0; col < _boardCells.GetLength(1); col++)
			{
				for (int row = 0; row < _boardCells.GetLength(0); row++)
				{
					if (_boardCells[row, col].State.ReportState().ToString() == CellStateName.Hit.ToString())
						return true;
				}
			}

			return false;
		}
	}
}
