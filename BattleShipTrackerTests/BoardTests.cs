using BattleShipStateTracker;
using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.Enums;
using BattleShipStateTracker.Interfaces;
using NUnit.Framework;

namespace BattleShipTrackerTests
{
	public class BoardTests
	{
		[Test]
		public void AddShipToBoard_HorizontalAlignment_ShipOccupiesThoseCells()
		{
			//Arrange
			IBoard gameBoard = new Board();
			int x = 3;
			int y = 3;
			int length = 4;

			//Act
			gameBoard.AddShipToBoard(x, y, length, ShipAlignment.Horizontal);

			//Assert
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x, y));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x, y += 1));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x, y += 1));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x, y += 1));
		}

		[Test]
		public void AddShipToBoard_VerticalAlignment_ShipOccupiesThoseCells()
		{
			//Arrange
			IBoard gameBoard = new Board();
			int x = 3;
			int y = 3;
			int length = 4;

			//Act
			gameBoard.AddShipToBoard(x, y, length, ShipAlignment.Vertical);

			//Assert
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x, y));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x += 1, y));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x += 1, y));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x += 1, y));
		}

		//[Test]
		//public void AddShipToBoard_WhereAShipAlreadyExists_ErrorThatShipAlreadyInThatPosition()
		//{
		//	//Arrange
		//	IBoard gameBoard = new Board();
		//	int x = 3;
		//	int y = 3;
		//	int length = 4;

		//	//Act
		//	gameBoard.AddShipToBoard(x, y, length, ShipAlignment.Horizontal);
		//	gameBoard.AddShipToBoard(x, y, length, ShipAlignment.Horizontal);

		//	//Assert
		//	
		//}

		//[Test]
		//public void AddShipToBoard_WithCoordinatesOutOfBounds_OutOfBoundsExceptionIsCaught()
		//{
		//	//Arrange
		//	IBoard gameBoard = new Board();
		//	int x = 15;
		//	int y = 15;
		//	int length = 4;

		//	//Act
		//	gameBoard.AddShipToBoard(x, y, length, ShipAlignment.Horizontal);

		//	//Assert
		//	Assert.Catch<System.IndexOutOfRangeException>(() => gameBoard.AddShipToBoard(x, y, length, ShipAlignment.Horizontal));
		//}

		[Test]
		public void AttackCellOnBoard_CellStateIsWater_AttackMisses()
		{
			//Arrange
			IBoard gameBoard = new Board();

			int x = 3;
			int y = 3;
			int length = 4;
			gameBoard.AddShipToBoard(x, y, length, ShipAlignment.Vertical);
			
			////Act
			var cellState = gameBoard.AttackCellOnBoard(9, 9);

			//Assert
			Assert.AreEqual(CellStateName.Water, cellState);
		}

		[Test]
		public void AttackCellOnBoard_CellStateIsHit_AttackConfirmsThisCellHitAlready()
		{
			//Arrange
			IBoard gameBoard = new Board();

			int x = 3;
			int y = 3;
			int length = 4;
			gameBoard.AddShipToBoard(x, y, length, ShipAlignment.Vertical);

			//Act
			gameBoard.AttackCellOnBoard(x, y);
			var cellState = gameBoard.AttackCellOnBoard(x, y);

			//Assert
			Assert.AreEqual(CellStateName.Hit, cellState);
		}

		[Test]
		public void AttackCellOnBoard_CellIsLastCellOfShip_ShipConfirmedAsSunk()
		{
			//Arrange
			IBoard gameBoard = new Board();

			int x = 6;
			int y = 6;
			int length = 3;

			//Act
			gameBoard.AddShipToBoard(x, y, length, ShipAlignment.Vertical);

			gameBoard.AttackCellOnBoard(6, 6);
			gameBoard.AttackCellOnBoard(7, 6);

			var cellState = gameBoard.AttackCellOnBoard(8, 6);

			//Assert
			Assert.AreEqual(CellStateName.Sunk, cellState);
		}

		//[Test]
		//public void AttackCellOffBoard_CellIsOutOfBounds_OutOfBoundsExceptionIsThrown()
		//{
		//	//Arrange
		//	IBoard gameBoard = new Board();

		//	int x = 7;
		//	int y = 7;
		//	int length = 4;

		//	gameBoard.AddShipToBoard(x, y, length, ShipAlignment.Vertical);

		//	//Act
		//	var result = gameBoard.AttackCellOnBoard(12, 12);

		//	//Assert
		//	Assert.AreEqual("You must attack a ship within the bounds of the 10 x 10 board", result.ToString());
		//}
	}
}