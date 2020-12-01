using BattleShipStateTracker;
using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.GameStatus;
using NUnit.Framework;

namespace BattleShipTrackerTests
{
	public class BoardTests
	{
		[Test]
		public void CreateBoard_BoardIsCreatedWithNoShipsHitState()
		{
			//Arrange
			IBoard gameBoard = new Board();

			//Act
			var gameState = gameBoard.GameState.GameStateName;

			//Assert
			Assert.AreEqual(GameStateName.NoShipsHit, gameState);
		}

		[Test]
		public void AddShipToBoard_HorizontalAlignment_ShipOccupiesThoseCells()
		{
			//Arrange
			IBoard gameBoard = new Board();
			int x = 3;
			int y = 3;
			int length = 4;

			//Act
			gameBoard.AddShipToBoard(x, y, length, Alignment.Horizontal);
			var gameState = gameBoard.GameState.GameStateName;

			//Assert
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x, y));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x, y += 1));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x, y += 1));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x, y += 1));
			Assert.AreEqual(GameStateName.NoShipsHit, gameState);
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
			gameBoard.AddShipToBoard(x, y, length, Alignment.Vertical);
			var gameState = gameBoard.GameState.GameStateName;

			//Assert
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x, y));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x += 1, y));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x += 1, y));
			Assert.AreEqual(CellStateName.Occupied, gameBoard.FindCellStateOnBoard(x += 1, y));
			Assert.AreEqual(GameStateName.NoShipsHit, gameState);
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
		//	gameBoard.AddShipToBoard(x, y, length, Alignment.Horizontal);
		//	gameBoard.AddShipToBoard(x, y, length, Alignment.Horizontal);

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
		//	gameBoard.AddShipToBoard(x, y, length, Alignment.Horizontal);

		//	//Assert
		//	Assert.Catch<System.IndexOutOfRangeException>(() => gameBoard.AddShipToBoard(x, y, length, Alignment.Horizontal));
		//}

		[Test]
		public void AttackCellOnBoard_CellStateIsWater_AttackMisses()
		{
			//Arrange
			IBoard gameBoard = new Board();

			int x = 3;
			int y = 3;
			int length = 4;
			gameBoard.AddShipToBoard(x, y, length, Alignment.Vertical);
			
			////Act
			var cellState = gameBoard.AttackCellOnBoard(9, 9);
			var gameState = gameBoard.GameState.GameStateName;

			//Assert
			Assert.AreEqual(CellStateName.Water, cellState);
			Assert.AreEqual(GameStateName.NoShipsHit, gameState);
		}

		[Test]
		public void AttackCellOnBoard_CellStateIsHit_AttackConfirmsThisCellHitAlready()
		{
			//Arrange
			IBoard gameBoard = new Board();

			int x = 3;
			int y = 3;
			int length = 4;
			gameBoard.AddShipToBoard(x, y, length, Alignment.Vertical);

			//Act
			gameBoard.AttackCellOnBoard(x, y);
			var cellState = gameBoard.AttackCellOnBoard(x, y);
			var gameState = gameBoard.GameState.GameStateName;

			//Assert
			Assert.AreEqual(CellStateName.Hit, cellState);
			Assert.AreEqual(GameStateName.PartialShipHits, gameState);
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
			gameBoard.AddShipToBoard(x, y, length, Alignment.Vertical);

			gameBoard.AttackCellOnBoard(6, 6);
			gameBoard.AttackCellOnBoard(7, 6);

			var cellState = gameBoard.AttackCellOnBoard(8, 6);
			var gameState = gameBoard.GameState.GameStateName;

			//Assert
			Assert.AreEqual(CellStateName.Sunk, cellState);
			Assert.AreEqual(GameStateName.AllShipsSunk, gameState);
		}

		//[Test]
		//public void AttackCellOffBoard_CellIsOutOfBounds_OutOfBoundsExceptionIsThrown()
		//{
		//	//Arrange
		//	IBoard gameBoard = new Board();

		//	int x = 7;
		//	int y = 7;
		//	int length = 4;

		//	gameBoard.AddShipToBoard(x, y, length, Alignment.Vertical);

		//	//Act
		//	var result = gameBoard.AttackCellOnBoard(12, 12);

		//	//Assert
		//	Assert.AreEqual("You must attack a ship within the bounds of the 10 x 10 board", result.ToString());
		//}
	}
}