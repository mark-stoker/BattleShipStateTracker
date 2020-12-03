using System;

namespace BattleShipStateTracker.Exceptions
{
	public class XCoordOutOfBoundsException : Exception
	{
		public XCoordOutOfBoundsException(string message) : 
			base("The X Coordinate must be within the bounds of the 10 x 10 board") { }
		
	}

	public class YCoordOutOfBoundsException : Exception
	{
		public YCoordOutOfBoundsException(string message) : 
			base("The Y Coordinate must be within the bounds of the10 x 10 board") { }
	}

	public class LengthOutOfBoundsException : Exception
	{
		public LengthOutOfBoundsException(string message) : 
			base("You must add a ship length within the bounds of the 10 x 10 board") { }
	}

	public class ShipsOverlapException : Exception
	{
		public ShipsOverlapException(string message) : 
			base("The position of your ship overlaps with another ship, please try again") { }
	}

	public class CellHitAlreadyException : Exception
	{
		public CellHitAlreadyException(string message) : 
			base("The cell you have attacked has been hit already") { }
	}

}
