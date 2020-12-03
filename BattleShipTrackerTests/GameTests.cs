using BattleShipStateTracker;
using BattleShipStateTracker.Enums;
using BattleShipStateTracker.Interfaces;
using Moq;
using NUnit.Framework;

namespace BattleShipTrackerTests
{
	public class GameTests
	{
		[Test]
		public void GameCreatedInCorrectState_NoActionsTaken_GameIsInNoShipsHitState()
		{
			//Arrange
			var board = new Mock<IBoard>();
			var game = new Game(board.Object);

			//Act
			var expectedResult = GameStateName.NoShipsHit;
			var result = game.GetGameState();

			//Assert
			Assert.AreEqual(expectedResult, result);
		}

		[Test]
		public void GameInProgress_SomeShipCellsHaveBeenHit_GameIsInShipsPartiallyHitState()
		{
			//Arrange
			var board = new Mock<IBoard>();
			board.Setup(x => x.AllOccupiedBoardCellsHit()).Returns(false);
			board.Setup(x => x.BoardCellsPartiallyHit()).Returns(true);

			var game = new Game(board.Object);

			//Act
			var expectedResult = GameStateName.ShipsPartiallyHit;
			var result = game.GetGameState();
			
			//Assert
			Assert.AreEqual(expectedResult, result);
		}

		[Test]
		public void GameInProgress_AllShipCellsHaveBeenHit_GameIsInAllShipsSunkState()
		{
			//Arrange
			var board = new Mock<IBoard>();
			board.Setup(x => x.AllOccupiedBoardCellsHit()).Returns(true);
			board.Setup(x => x.BoardCellsPartiallyHit()).Returns(false);

			var game = new Game(board.Object);

			//Act
			var expectedResult = GameStateName.AllShipsSunk;
			var result = game.GetGameState();

			//Assert
			Assert.AreEqual(expectedResult, result);
		}

		[Test]
		public void AddShipToBoard_XCoordinateOutOfBounds_XCoordOutOfBoundsExceptionIsCaught()
		{
			//Arrange
			var game = new Game();

			var x = 100; //Out of bounds
			var y = 3;
			var length = 4;
			var alignment = ShipAlignment.Horizontal;

			//Act and Assert
			Assert.DoesNotThrow(() => game.AddShipToBoard(x, y, length, alignment));
		}

		[Test]
		public void AddShipToBoard_YCoordinateOutOfBounds_YCoordOutOfBoundsExceptionIsCaught()
		{
			//Arrange
			var game = new Game();

			var x = 3;
			var y = 100; //Out of bounds
			var length = 4;
			var alignment = ShipAlignment.Horizontal;

			//Act and Assert
			Assert.DoesNotThrow(() => game.AddShipToBoard(x, y, length, alignment));
		}

		[Test]
		public void AddShipToBoard_LengthOutOfBounds_LengthOutOfBoundsExceptionIsCaught()
		{
			//Arrange
			var game = new Game();

			var x = 3;
			var y = 3;
			var length = 100;  //Out of bounds
			var alignment = ShipAlignment.Horizontal;

			//Act and Assert
			Assert.DoesNotThrow(() => game.AddShipToBoard(x, y, length, alignment));
		}

		[Test]
		public void NumberOfShipsOnBoard_AddTwoShipsToBoard_TwoShipsReturned()
		{
			//Arrange
			var game = new Game();
			game.Board.CreateBoard();

			var xFirst = 1;
			var yFirst = 1;
			var lengthFirst = 3;
			var alignmentFirst = ShipAlignment.Vertical;

			var xSecond = 6;
			var ySecond = 6;
			var lengthSecond = 3;
			var alignmentSecond = ShipAlignment.Horizontal;

			//Act
			game.AddShipToBoard(xFirst, yFirst, lengthFirst, alignmentFirst);
			game.AddShipToBoard(xSecond, ySecond, lengthSecond, alignmentSecond);

			var expected = 2;
			var result = game.Ships.Count;

			//Assert
			Assert.AreEqual(expected, result);
		}
	}
}