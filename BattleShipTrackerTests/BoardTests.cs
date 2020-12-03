using BattleShipStateTracker;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.Enums;
using NUnit.Framework;

namespace BattleShipTrackerTests
{
	public class BoardTests
	{
		[Test]
		public void CreateBoard_UpdatesPrivateField_boardIsNotNull()
		{
			//Arrange
			var board = new Board();

			//Act
			board.CreateBoard();

			//Assert
			Assert.NotNull(board.BoardCreated());
		}

		[Test]
		public void AddShipToBoard_HorizontalAlignment_ShipOccupiesThoseCells()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 3;
			var y = 3;
			var length = 4;
			var alignment = ShipAlignment.Horizontal;

			//Act
			board.AddShipToBoard(x, y, length, ShipAlignment.Horizontal);

			var expected = CellStateName.Occupied;

			//Assert
			Assert.AreEqual(expected, board.FindCellStateOnBoard(x, y));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(x, y += 1));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(x, y += 1));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(x, y += 1));
		}

		[Test]
		public void AddShipToBoard_VerticalAlignment_ShipOccupiesThoseCells()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 3;
			var y = 3;
			var length = 4;
			var alignment = ShipAlignment.Vertical;

			//Act
			board.AddShipToBoard(x, y, length, alignment);

			var expected = CellStateName.Occupied;

			//Assert
			Assert.AreEqual(expected, board.FindCellStateOnBoard(x, y));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(x += 1, y));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(x += 1, y));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(x += 1, y));
		}

		[Test]
		public void AddShipToBoard_WhereAShipAlreadyExists_ShipsOverlapExceptionIsCaught()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 3;
			var y = 3;
			var length = 4;
			var alignment = ShipAlignment.Horizontal;

			board.AddShipToBoard(x, y, length, alignment);

			//Act and Assert
			Assert.DoesNotThrow(() => board.AddShipToBoard(x, y, length, alignment));
		}

		[Test]
		public void AddShipToBoard_XCoordinateOutOfBounds_XCoordOutOfBoundsExceptionIsCaught()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 100; //Out of bounds
			var y = 3;
			var length = 4;
			var alignment = ShipAlignment.Horizontal;

			//Act and Assert
			Assert.DoesNotThrow(() => board.AddShipToBoard(x, y, length, alignment));
		}

		[Test]
		public void AddShipToBoard_YCoordinateOutOfBounds_YCoordOutOfBoundsExceptionIsCaught()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 3;
			var y = 100; //Out of bounds
			var length = 4;
			var alignment = ShipAlignment.Horizontal;

			//Act and Assert
			Assert.DoesNotThrow(() => board.AddShipToBoard(x, y, length, alignment));
		}

		[Test]
		public void AddShipToBoard_LengthOutOfBounds_LengthOutOfBoundsExceptionIsCaught()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 3;
			var y = 3;
			var length = 100;  //Out of bounds
			var alignment = ShipAlignment.Horizontal;

			//Act and Assert
			Assert.DoesNotThrow(() => board.AddShipToBoard(x, y, length, alignment));
		}

		[Test]
		public void AttackCellOnBoard_CellStateIsWater_AttackMisses()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 3;
			var y = 3;
			var length = 4;
			var alignment = ShipAlignment.Vertical;

			board.AddShipToBoard(x, y, length, alignment);
			
			////Act
			var expected = CellStateName.Water;
			var result = board.AttackCellOnBoard(9, 9);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AttackVerticalShipOnBoard_CellStateIsOccupied_AttackConfirmsThisCellHit()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 3;
			var y = 3;
			var length = 4;
			var alignment = ShipAlignment.Vertical;

			board.AddShipToBoard(x, y, length, alignment);

			//Act
			var expected = CellStateName.Hit;
			var result = board.AttackCellOnBoard(x += 1, y);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AttackHorizontalShipOnBoard_CellStateIsOccupied_AttackConfirmsThisCellHit()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 3;
			var y = 3;
			var length = 4;
			var alignment = ShipAlignment.Vertical;

			board.AddShipToBoard(x, y, length, alignment);

			//Act
			var expected = CellStateName.Hit;
			var result = board.AttackCellOnBoard(x += 1, y);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AttackCellOnBoard_CellIsLastCellOfVerticalShip_ShipConfirmedAsSunk()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 6;
			var y = 6;
			var length = 3;
			var alignment = ShipAlignment.Vertical;

			//Act
			board.AddShipToBoard(x, y, length, alignment);

			board.AttackCellOnBoard(x, y);
			board.AttackCellOnBoard(x += 1, y);

			var expected = CellStateName.Sunk;
			var result = board.AttackCellOnBoard(x += 1, y);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AttackCellOnBoard_CellIsLastCellOfHorizontalShip_ShipConfirmedAsSunk()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 6;
			var y = 6;
			var length = 3;
			var alignment = ShipAlignment.Horizontal;

			//Act
			board.AddShipToBoard(x, y, length, alignment);

			board.AttackCellOnBoard(x, y);
			board.AttackCellOnBoard(x, y += 1);

			var expected = CellStateName.Sunk;
			var result = board.AttackCellOnBoard(x, y += 1);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void ShipAlreadyHitInGivenPosition_ThatPositionIsHitAgain_CellHitAlreadyExceptionIsCaught()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var x = 9;
			var y = 1;
			var length = 5;
			var alignment = ShipAlignment.Vertical;

			board.AddShipToBoard(x, y, length, alignment);
			board.AttackCellOnBoard(x, y); //attack one

			//Act and Assert
			Assert.DoesNotThrow(() => board.AttackCellOnBoard(x, y));
		}

		[Test]
		public void NumberOfShipsOnBoard_AddTwoShipsToBoard_TwoShipsReturned()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var xFirst = 1;
			var yFirst = 1;
			var lengthFirst = 3;
			var alignmentFirst = ShipAlignment.Vertical;

			var xSecond = 6;
			var ySecond = 6;
			var lengthSecond = 3;
			var alignmentSecond = ShipAlignment.Horizontal;

			//Act
			board.AddShipToBoard(xFirst, yFirst, lengthFirst, alignmentFirst);
			board.AddShipToBoard(xSecond, ySecond, lengthSecond, alignmentSecond);

			var expected = 2;
			var result = board.NumberOfShipsOnBoard();

			//Assert
			Assert.AreEqual(expected, result);
		}
	}
}