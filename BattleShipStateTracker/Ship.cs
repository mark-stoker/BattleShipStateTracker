using System;
using BattleShipStateTracker.Enums;
using BattleShipStateTracker.Interfaces;

namespace BattleShipStateTracker
{
	public class Ship : IShip
	{
		public Guid Name { get; set; }

		public int XStartCoordinate { get; set; }

		public int YStartCoordinate { get; set; }

		public int Length { get; set; }

		public ShipAlignment Alignment { get; set; }

		public int Hits { get; set; }

		public Ship(int xStartCoordinate, int yStartCoordinate, int length, ShipAlignment alignment)
		{
			Name = Guid.NewGuid();
			XStartCoordinate = xStartCoordinate;
			YStartCoordinate = yStartCoordinate;
			Length = length;
			Alignment = alignment;
		}

		public bool IsSunk()
		{
			return Hits >= Length;
		}
	}
}
