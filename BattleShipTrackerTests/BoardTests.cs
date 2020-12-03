using BattleShipStateTracker;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.Enums;
using BattleShipStateTracker.Interfaces;
using Moq;
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

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(3);
			ship.Setup(x => x.YStartCoordinate).Returns(3);
			ship.Setup(x => x.Length).Returns(4);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Horizontal);

			//Act
			board.AddShipToBoard(ship.Object);

			var expected = CellStateName.Occupied;

			//Assert
			Assert.AreEqual(expected, board.FindCellStateOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate += 1));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate += 1));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate += 1));
		}

		[Test]
		public void AddShipToBoard_VerticalAlignment_ShipOccupiesThoseCells()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(3);
			ship.Setup(x => x.YStartCoordinate).Returns(3);
			ship.Setup(x => x.Length).Returns(4);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Vertical);

			//Act
			board.AddShipToBoard(ship.Object);

			var expected = CellStateName.Occupied;

			//Assert
			Assert.AreEqual(expected, board.FindCellStateOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(ship.Object.XStartCoordinate += 1, ship.Object.YStartCoordinate));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(ship.Object.XStartCoordinate += 1, ship.Object.YStartCoordinate));
			Assert.AreEqual(expected, board.FindCellStateOnBoard(ship.Object.XStartCoordinate += 1, ship.Object.YStartCoordinate));
		}

		[Test]
		public void AddShipToBoard_WhereAShipAlreadyExists_ShipsOverlapExceptionIsCaught()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(6);
			ship.Setup(x => x.YStartCoordinate).Returns(6);
			ship.Setup(x => x.Length).Returns(2);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Horizontal);

			board.AddShipToBoard(ship.Object);

			//Act and Assert
			Assert.DoesNotThrow(() => board.AddShipToBoard(ship.Object));
		}

		[Test]
		public void AttackCellOnBoard_CellStateIsWater_AttackMisses()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();
			
			//Act
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

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(1);
			ship.Setup(x => x.YStartCoordinate).Returns(1);
			ship.Setup(x => x.Length).Returns(4);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Vertical);

			board.AddShipToBoard(ship.Object);

			//Act
			var expected = CellStateName.Hit;
			var result = board.AttackCellOnBoard(ship.Object.XStartCoordinate += 1, ship.Object.YStartCoordinate);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AttackHorizontalShipOnBoard_CellStateIsOccupied_AttackConfirmsThisCellHit()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(1);
			ship.Setup(x => x.YStartCoordinate).Returns(1);
			ship.Setup(x => x.Length).Returns(4);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Horizontal);

			board.AddShipToBoard(ship.Object);

			//Act
			var expected = CellStateName.Hit;
			var result = board.AttackCellOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate += 1);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AttackCellOnBoard_CellIsLastCellOfVerticalShip_ShipConfirmedAsSunk()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(6);
			ship.Setup(x => x.YStartCoordinate).Returns(6);
			ship.Setup(x => x.Length).Returns(3);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Vertical);

			//Act
			board.AddShipToBoard(ship.Object);

			var test = board.AttackCellOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate);
			var test2 = board.AttackCellOnBoard(7, ship.Object.YStartCoordinate);

			var expected = CellStateName.Sunk;
			var result = board.AttackCellOnBoard(8, ship.Object.YStartCoordinate);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AttackCellOnBoard_CellIsLastCellOfHorizontalShip_ShipConfirmedAsSunk()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(6);
			ship.Setup(x => x.YStartCoordinate).Returns(6);
			ship.Setup(x => x.Length).Returns(3);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Horizontal);

			//Act
			board.AddShipToBoard(ship.Object);

			board.AttackCellOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate);
			board.AttackCellOnBoard(ship.Object.XStartCoordinate, 7);

			var expected = CellStateName.Sunk;
			var result = board.AttackCellOnBoard(ship.Object.XStartCoordinate, 8);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void ShipAlreadyHitInGivenPosition_ThatPositionIsHitAgain_CellHitAlreadyExceptionIsCaught()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(9);
			ship.Setup(x => x.YStartCoordinate).Returns(1);
			ship.Setup(x => x.Length).Returns(5);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Vertical);

			board.AddShipToBoard(ship.Object);
			board.AttackCellOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate); //attack one

			//Act and Assert
			Assert.DoesNotThrow(() => board.AttackCellOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate));
		}

		[Test]
		public void AttackShip_AttackIsOutOfBound_XCoordOutOfBoundsExceptionIsCaught()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(100); //Out of bounds
			ship.Setup(x => x.YStartCoordinate).Returns(1);
			ship.Setup(x => x.Length).Returns(5);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Vertical);

			board.AddShipToBoard(ship.Object);

			//Act and Assert
			Assert.DoesNotThrow(() => board.AttackCellOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate));
		}

		[Test]
		public void AttackShip_AttackIsOutOfBound_YCoordOutOfBoundsExceptionIsCaught()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(1);
			ship.Setup(x => x.YStartCoordinate).Returns(100); //Out of bounds
			ship.Setup(x => x.Length).Returns(5);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Vertical);

			board.AddShipToBoard(ship.Object);

			//Act and Assert
			Assert.DoesNotThrow(() => board.AttackCellOnBoard(ship.Object.XStartCoordinate, ship.Object.YStartCoordinate));
		}
	}
}