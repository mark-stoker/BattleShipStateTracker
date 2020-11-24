using BattleShipStateTracker.CellStateTracker;
using NUnit.Framework;

namespace BattleShipTrackerTests
{
	public class CellTests
	{
		private Cell _cell;

		[Test, Order(1)]
		public void CellInCorrectState_InitialCreation_InWaterState()
		{
			//Arrange
			_cell = new Cell(3, 3); //Constructor will put this into WaterState()
			
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