using System;
using System.Collections.Generic;
using BattleShipStateTracker.Enums;
using BattleShipStateTracker.Interfaces;

namespace BattleShipStateTracker
{
	public class Ship : IShip
	{
		public int XStartCoordinate { get; set; }

		public int YStartCoordinate { get; set; }

		public int Length { get; set; }

		public ShipAlignment Alignment { get; set; }

		public int Hits { get; set; }

		public IList<Tuple<int, int>> ShipRange { get; set; }

		public Ship(int xStartCoordinate, int yStartCoordinate, int length, ShipAlignment alignment)
		{
			XStartCoordinate = xStartCoordinate;
			YStartCoordinate = yStartCoordinate;
			Length = length;
			Alignment = alignment;
			Hits = 0;
			AddShipRange();
		}

		public bool IsSunk()
		{
			return Hits >= Length;
		}

		private void AddShipRange()
		{
			ShipRange = new List<Tuple<int, int>>();

			int horizontalEndPosition = XStartCoordinate; //Represents the min length
			int verticalEndPosition = YStartCoordinate; //represents the min length

			//Works out length based on alignment
			if (Alignment == ShipAlignment.Horizontal)
				horizontalEndPosition = XStartCoordinate + Length - 1;

			if (Alignment == ShipAlignment.Vertical)
				verticalEndPosition = YStartCoordinate + Length - 1;

			for (int x = XStartCoordinate; x <= verticalEndPosition; x++)
			{
				for (int y = YStartCoordinate; y <= horizontalEndPosition; y++)
				{
					ShipRange.Add(new Tuple<int, int>(x, y));
				}
			}
		}
	}
}
