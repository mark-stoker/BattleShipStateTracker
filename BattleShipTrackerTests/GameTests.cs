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
			var game = new Game();
			game.Board.CreateBoard();

			int xStartCoordinate = 3;
			int yStartCoordinate = 3;
			int length = 3;
			ShipAlignment alignment = ShipAlignment.Horizontal;

			game.Board.AddShipToBoard(xStartCoordinate, yStartCoordinate, length, alignment);

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
	}
}