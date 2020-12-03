using System;
using System.Collections.Generic;
using System.Linq;
using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.CellStateTracker.Interfaces;
using BattleShipStateTracker.Enums;
using BattleShipStateTracker.Exceptions;
using BattleShipStateTracker.Interfaces;

namespace BattleShipStateTracker
{
	public class Board : IBoard
	{
		private const int BoardWidth = 10;
		private const int BoardHeight = 10;
		public IList<ICell> BoardCells { get; set; }
		private int _shipCount = 0;

		public void CreateBoard()
		{
			BoardCells = new List<ICell>();

			for (var x = 1; x <= BoardWidth; x++)
			{
				for (var y = 1; y <= BoardHeight; y++)
				{
					BoardCells.Add(new Cell(x, y)); 
				}
			}
		}

		public void AddShipToBoard(int xStartCoOrdinate, int yStartCoOrdinate, int length, ShipAlignment alignment)
		{
			try
			{
				xStartCoOrdinate.ValidateXStartCoordinate();
				yStartCoOrdinate.ValidateYStartCoordinate();
				length.ValidateLength();

				_shipCount += 1;

				int horizontalEndPosition = xStartCoOrdinate; //Represents the min length
				int verticalEndPosition = yStartCoOrdinate; //represents the min length

				//Works out length based on alignment
				if (alignment == ShipAlignment.Horizontal)
					horizontalEndPosition = xStartCoOrdinate + length - 1;

				if (alignment == ShipAlignment.Vertical)
					verticalEndPosition = yStartCoOrdinate + length - 1;

				for (int x = xStartCoOrdinate; x <= verticalEndPosition; x++)
				{
					for (int y = yStartCoOrdinate; y <= horizontalEndPosition; y++)
					{
						var cell = BoardCells.FirstOrDefault(item => item.XCoordinate == x && item.YCoordinate == y);

						cell.ValidateShipsDontOverlap();

						cell?.State.ChangeState(cell);
					}
				}
			}
			catch (XCoordOutOfBoundsException exception)
			{
				Console.WriteLine(exception.Message);
			}
			catch (YCoordOutOfBoundsException exception)
			{
				Console.WriteLine(exception.Message);
			}
			catch (LengthOutOfBoundsException exception)
			{
				Console.WriteLine(exception.Message);
			}
			catch (ShipsOverlapException exception)
			{
				Console.WriteLine(exception.Message);
			}
		}

		public CellStateName? AttackCellOnBoard(int xCoOrdinate, int yCoOrdinate)
		{
			CellStateName? result = null;

			try
			{
				xCoOrdinate.ValidateXStartCoordinate();
				yCoOrdinate.ValidateYStartCoordinate();

				var cell = BoardCells.FirstOrDefault(item => item.XCoordinate == xCoOrdinate && item.YCoordinate == yCoOrdinate);
				cell.ValidateCellNotHitAlready();

				cell?.State.IncomingAttack(cell);

				if (AllOccupiedBoardCellsHit())
					cell?.State.ChangeState(cell);

				result = cell?.State.ReportState();

				return result;
			}
			catch (XCoordOutOfBoundsException exception)
			{
				Console.WriteLine(exception.Message);
			}
			catch (YCoordOutOfBoundsException exception)
			{
				Console.WriteLine(exception.Message);
			}
			catch (CellHitAlreadyException exception)
			{
				Console.WriteLine(exception.Message);
			}

			return result;
		}

		public CellStateName? FindCellStateOnBoard(int x, int y)
		{
			var cell = BoardCells.FirstOrDefault(item => item.XCoordinate == x && item.YCoordinate == y);
			return cell?.State.ReportState();
		}

		//TODO Is this breaking SRP???
		public int NumberOfShipsOnBoard()
		{
			return _shipCount;
		}

		public bool BoardCreated()
		{
			if (BoardCells == null)
			return false;

			return true;
		}

		//TODO it feels like the two methods below could be merged??
		//If any occupied Ships remain game is still going
		public bool AllOccupiedBoardCellsHit()
		{
			if (BoardCells.Any(x => x.State.ReportState() == CellStateName.Occupied))
				return false;

			return true;
		}

		
		public bool BoardCellsPartiallyHit()
		{
			if (BoardCells.Any(x => x.State.ReportState() == CellStateName.Hit))
				return true;

			//if (BoardCells.Any(x => x.State.ReportState() != CellStateName.Sunk))
			//	return true;

			return false;
		}
	}
}
