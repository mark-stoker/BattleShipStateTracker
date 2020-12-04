using System;
using System.Collections.Generic;
using BattleShipStateTracker.Enums;

namespace BattleShipStateTracker.Interfaces
{
	public interface IShip
	{
		int XStartCoordinate { get; set; }

		int YStartCoordinate { get; set; }

		int Length { get; set; }

		ShipAlignment Alignment { get; set; }

		int Hits { get; set; }

		IList<Tuple<int, int>> ShipRange { get; set; }

		bool IsSunk();
	}
}
