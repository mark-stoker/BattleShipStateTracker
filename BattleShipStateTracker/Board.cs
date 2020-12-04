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

		public void AddShipToBoard(IShip ship)
		{
			try
			{
				int horizontalEndPosition = ship.XStartCoordinate; //Represents the min length
				int verticalEndPosition = ship.YStartCoordinate; //represents the min length

				//Works out length based on alignment
				if (ship.Alignment == ShipAlignment.Horizontal)
					horizontalEndPosition = ship.XStartCoordinate + ship.Length - 1;

				if (ship.Alignment == ShipAlignment.Vertical)
					verticalEndPosition = ship.YStartCoordinate + ship.Length - 1;

				for (int x = ship.XStartCoordinate; x <= verticalEndPosition; x++)
				{
					for (int y = ship.YStartCoordinate; y <= horizontalEndPosition; y++)
					{
						var cell = BoardCells.FirstOrDefault(item => item.XCoordinate == x && item.YCoordinate == y);

						cell.ValidateShipsDontOverlap();

						cell?.State.ChangeState(cell);
					}
				}
			}
			catch (ShipsOverlapException exception)
			{
				Console.WriteLine(exception.Message);
			}
		}

		public CellStateName? AttackCellOnBoard(IAttack attack)
		{
			CellStateName? result = null;

			try
			{
				attack.XCoordinate.ValidateXStartCoordinate();
				attack.YCoordinate.ValidateYStartCoordinate();

				var cell = BoardCells.FirstOrDefault(item => item.XCoordinate == attack.XCoordinate 
				                                             && item.YCoordinate == attack.YCoordinate);
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

		public bool BoardCreated()
		{
			if (BoardCells == null)
			return false;

			return true;
		}

		public bool AllOccupiedBoardCellsHit()
		{
			if (BoardCells.Any(x => x.State.ReportState() == CellStateName.Sunk))
				return true;

			if (BoardCells.All(x => x.State.ReportState() == CellStateName.Water))
				return false;

			if (BoardCells.Any(x => x.State.ReportState() == CellStateName.Occupied))
				return false;

			return true;
		}
		
		public bool BoardCellsPartiallyHit()
		{
			if (BoardCells.Any(x => x.State.ReportState() == CellStateName.Hit))
				return true;

			return false;
		}
	}
}
