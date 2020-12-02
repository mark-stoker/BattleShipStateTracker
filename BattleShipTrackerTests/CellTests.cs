using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.CellStateTracker.Enums;
using NUnit.Framework;

namespace BattleShipTrackerTests
{
	public class CellTests
	{
		private int _xCoordinate;
		private int _yCoordinate;
		private Cell _cell;

		[Test, Order(1)]
		public void CellInCorrectState_InitialCreation_InWaterState()
		{
			//Arrange
			_xCoordinate = 3;
			_yCoordinate = 3;
			_cell = new Cell(_xCoordinate, _yCoordinate); //Constructor will put this into WaterState()
			
			//Act
			var result = _cell.ReportState();

			//Assert
			Assert.AreEqual(CellStateName.Water, result);
		}

		[Test, Order(2)]
		public void CellInCorrectState_ChangeFromWaterState_InOccupiedState()
		{
			//Arrange
			_cell.ChangeState();

			//Act
			var result = _cell.ReportState();

			//Assert
			Assert.AreEqual(CellStateName.Occupied, result);
		}

		[Test, Order(3)]
		public void CellInCorrectState_ChangeFromOccupiedState_InHitState()
		{
			//Arrange
			_cell.ChangeState();

			//Act
			var result = _cell.ReportState();

			//Assert
			Assert.AreEqual(CellStateName.Hit, result);
		}

		[Test, Order(4)]
		public void CellInCorrectState_ChangeFromHitState_InToSunkState()
		{
			//Arrange
			_cell.ChangeState();
			
			//Act
			var result = _cell.ReportState();

			//Assert
			Assert.AreEqual(CellStateName.Sunk, result);
		}
	}
}