using System;
using BattleShipStateTracker.Enums;

namespace BattleShipStateTracker.Interfaces
{
	public interface IShip
	{
		Guid Name { get; set; }

		int XStartCoordinate { get; set; }

		int YStartCoordinate { get; set; }

		int Length { get; set; }

		ShipAlignment Alignment { get; set; }

		int Hits { get; set; }

		bool IsSunk();
	}
}
