using BattleShipStateTracker;
using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.Enums;
using BattleShipStateTracker.Interfaces;
using Moq;
using NUnit.Framework;

namespace BattleShipTrackerTests
{
	public class GameStatusTests
	{
		[Test]
		public void CreateBoard_UpdatesPrivateField_boardIsNotNull()
		{
			//Arrange
			var game = new Game();

			//Act
			game.CreateBoard();

			//Assert
			Assert.NotNull(game.BoardCreated());
		}

		[Test]
		public void CreateBoard_GameStatusCreated_ReturnsNoS()
		{
			//Arrange
			var game = new Game();

			//Act
			game.CreateBoard();

			//Assert
			Assert.NotNull(game.BoardCreated());
		}

		//TODO a variation of this test should be in GameTests
		//[Test]
		//public void CreateBoard_BoardIsCreatedWithNoShipsHitState()
		//{
		//	//Arrange
		//	IBoard gameBoard = new Board();

		//	//Act
		//	var gameState = gameBoard.GetGameState();

		//	//Assert
		//	Assert.AreEqual(GameStateName.NoShipsHit, gameState);
		//}

		[Test]
		public void AddOneShipToBoard_WithinBoundsOfBoard_BoardIsUpdatedWithShip()
		{
			//Arrange
			Mock<IBoard> board = new Mock<IBoard>();
			board.Setup(x => x.NumberOfShipsOnBoard()).Returns(1);

			var game = new Game();
			game.CreateBoard();

			//Act

			int xStartCoordinate = 3;
			int yStartCoordinate = 3;
			int length = 3;
			var alignment = ShipAlignment.Horizontal;
			game.AddShipToBoard(xStartCoordinate, yStartCoordinate, length, alignment);

			var result = game.NumberOfShipsOnBoard();
			var expectedResult = 1;

			//Assert
			Assert.AreEqual(result, expectedResult);
		}

		//[Test]
		//public void AddOneShipToBoard_OutsideBoundsOfBoard_ExceptionIsThrown()
		//{
		//	//Arrange
		//	var game = new Game();
		//	game.CreateBoard();

		//	int xStartCoordinate = 3;
		//	int yStartCoordinate = 3;
		//	int length = 20; //Outside 10 x 10 board
		//	var alignment = ShipAlignment.Horizontal;
		//	game.AddShipToBoard(xStartCoordinate, yStartCoordinate, length, alignment);

		//	//Act and Assert
		//	Assert.Throws<System.IndexOutOfRangeException>(() => game.AddShipToBoard(xStartCoordinate, yStartCoordinate, length, alignment));
		//}

		//[Test]
		//public void AddTwoShipsToBoard_ShipsOverlap_ExceptionIsThrown()
		//{
		//	//Arrange
		//	var game = new Game();
		//	game.CreateBoard();

		//	int xFirstStartCoordinate = 3;
		//	int yFirstStartCoordinate = 3;
		//	int firstLength = 3; //Outside 10 x 10 board
		//	var firstAlignment = ShipAlignment.Horizontal;
		//	game.AddShipToBoard(xFirstStartCoordinate, yFirstStartCoordinate, firstLength, firstAlignment);

		//	int xSecondStartCoordinate = 3;
		//	int ySecondStartCoordinate = 3;
		//	int secondLength = 3; //Outside 10 x 10 board
		//	var secondAlignment = ShipAlignment.Vertical;
		//	game.AddShipToBoard(xSecondStartCoordinate, ySecondStartCoordinate, secondLength, secondAlignment);

		//	//Act and Assert
		//	Assert.Throws<System.IndexOutOfRangeException>(() => game.AddShipToBoard(xSecondStartCoordinate, ySecondStartCoordinate, secondLength, secondAlignment));
		//}

		[Test]
		public void AttackCellOnBoard_BoardIsOccupiedWithShip_ResponseReceivedAttackHitTarget()
		{
			//Arrange
			Mock<IBoard> board = new Mock<IBoard>();
			board.Setup(x => x.AttackCellOnBoard(3, 3)).Returns(CellStateName.Hit);

			var game = new Game();
			game.CreateBoard();

			int xStartCoordinate = 3;
			int yStartCoordinate = 3;
			int length = 3;
			var alignment = ShipAlignment.Horizontal;
			game.AddShipToBoard(xStartCoordinate, yStartCoordinate, length, alignment);

			//Act
			var result = game.IncomingAttack(xStartCoordinate, yStartCoordinate);
			var expectedResult = CellStateName.Hit;

			//Assert
			Assert.AreEqual(result, expectedResult);
		}

		[Test]
		public void AttackCellOnBoard_BoardIsNotOccupiedWithShip_ResponseReceivedAttackHitWater()
		{
			//Arrange
			Mock<IBoard> board = new Mock<IBoard>();
			board.Setup(x => x.AttackCellOnBoard(3, 3)).Returns(CellStateName.Water);

			var game = new Game();
			game.CreateBoard();

			int xStartCoordinate = 3;
			int yStartCoordinate = 3;
			int length = 3;
			var alignment = ShipAlignment.Horizontal;
			game.AddShipToBoard(xStartCoordinate, yStartCoordinate, length, alignment);

			//Act
			var result = game.IncomingAttack(xStartCoordinate, yStartCoordinate);
			var expectedResult = CellStateName.Hit;

			//Assert
			Assert.AreEqual(result, expectedResult);
		}

		[Test]
		public void AttackCellOnBoard_HitsTheLastRemainingCellOccupiedByShip_ResponseReceivedShipSunk()
		{
			//Arrange
			Mock<IBoard> board = new Mock<IBoard>();
			board.Setup(x => x.AttackCellOnBoard(3, 3)).Returns(CellStateName.Sunk);

			var game = new Game();
			game.CreateBoard();

			int xStartCoordinate = 3;
			int yStartCoordinate = 3;
			int length = 3;
			var alignment = ShipAlignment.Horizontal;
			game.AddShipToBoard(xStartCoordinate, yStartCoordinate, length, alignment);

			//Act
			game.IncomingAttack(xStartCoordinate, yStartCoordinate);
			game.IncomingAttack(xStartCoordinate, yStartCoordinate += 1);
			var result = game.IncomingAttack(xStartCoordinate, yStartCoordinate += 1);
			var expectedResult = CellStateName.Sunk;

			//Assert
			Assert.AreEqual(result, expectedResult);
		}

		[Test]
		public void AttackCellOffBoard_AttackIsOutOfBoardBounds_ExceptionThrown()
		{
			//Arrange
			var game = new Game();
			game.CreateBoard();

			int xStartCoordinate = 3;
			int yStartCoordinate = 3;
			int length = 3;
			var alignment = ShipAlignment.Horizontal;
			game.AddShipToBoard(xStartCoordinate, yStartCoordinate, length, alignment);
			

			//Act and Assert
			int xCoordinate = 15;
			int yCoordinate = 15;
			Assert.Throws<System.IndexOutOfRangeException>(() => game.IncomingAttack(xCoordinate, yCoordinate));
		}



	}
}