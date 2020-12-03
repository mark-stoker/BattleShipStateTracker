using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.CellStateTracker.Interfaces;

namespace BattleShipStateTracker.Exceptions
{
	public static class ExceptionExtensions
	{
		public static void ValidateXStartCoordinate(this int xCoordinate)
		{
			if (xCoordinate < 1 || xCoordinate > 10)
				throw new XCoordOutOfBoundsException("The X Coordinate must be within the bounds of the 10 x 10 board");
		}

		public static void ValidateYStartCoordinate(this int yStartCoOrdinate)
		{
			if (yStartCoOrdinate < 1 || yStartCoOrdinate > 10)
				throw new YCoordOutOfBoundsException("The Y Coordinate must be within the bounds of the10 x 10 board");
		}

		public static void ValidateLength(this int length)
		{
			if (length < 1 || length > 10)
				throw new LengthOutOfBoundsException("You must add a ship length within the bounds of the 10 x 10 board");
		}

		public static void ValidateShipsDontOverlap(this ICell cell)
		{
			if (cell?.State.ReportState() == CellStateName.Occupied)
				throw new ShipsOverlapException("This cell range you are trying to place your ship on is occupied already");

			if (cell?.State.ReportState() == CellStateName.Hit)
				throw new ShipsOverlapException(
					"This cell range you are trying to place your ship on is occupied already and has been hit already");

			if (cell?.State.ReportState() == CellStateName.Sunk)
				throw new ShipsOverlapException(
					"This cell range you are trying to place your ship on is occupied already and has been sunk already");
		}

		public static void ValidateCellNotHitAlready(this ICell cell)
		{
			if (cell?.State.ReportState() == CellStateName.Hit)
				throw new CellHitAlreadyException("This cell has been hit already keep attacking");

			if (cell?.State.ReportState() == CellStateName.Sunk)
				throw new CellHitAlreadyException(
					"This cell is part of a ship that has been sunk already, keep attacking to find further ships!");
		}
	}
}
