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

			var attack = new Mock<IAttack>();
			attack.Setup(x => x.XCoordinate).Returns(9);
			attack.Setup(x => x.YCoordinate).Returns(9);

			//Act
			var expected = CellStateName.Water;
			var result = board.AttackCellOnBoard(attack.Object);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AttackCellOnBoard_CellStateIsOccupiedByVerticalShip_AttackConfirmsThisCellHit()
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

			var attack = new Mock<IAttack>();
			attack.Setup(x => x.XCoordinate).Returns(2);
			attack.Setup(x => x.YCoordinate).Returns(1);

			//Act
			var expected = CellStateName.Hit;
			var result = board.AttackCellOnBoard(attack.Object);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AttackCellOnBoard_CellStateIsOccupiedByHorizontalShip_AttackConfirmsThisCellHit()
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

			var attack = new Mock<IAttack>();
			attack.Setup(x => x.XCoordinate).Returns(1);
			attack.Setup(x => x.YCoordinate).Returns(3);

			//Act
			var expected = CellStateName.Hit;
			var result = board.AttackCellOnBoard(attack.Object);

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

			var attack1 = new Mock<IAttack>();
			attack1.Setup(x => x.XCoordinate).Returns(6);
			attack1.Setup(x => x.YCoordinate).Returns(6);

			var attack2 = new Mock<IAttack>();
			attack2.Setup(x => x.XCoordinate).Returns(7);
			attack2.Setup(x => x.YCoordinate).Returns(6);

			var attack3 = new Mock<IAttack>();
			attack3.Setup(x => x.XCoordinate).Returns(8);
			attack3.Setup(x => x.YCoordinate).Returns(6);

			board.AttackCellOnBoard(attack1.Object);
			board.AttackCellOnBoard(attack2.Object);

			var expected = CellStateName.Sunk;
			var result = board.AttackCellOnBoard(attack3.Object);

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

			var attack1 = new Mock<IAttack>();
			attack1.Setup(x => x.XCoordinate).Returns(6);
			attack1.Setup(x => x.YCoordinate).Returns(6);

			var attack2 = new Mock<IAttack>();
			attack2.Setup(x => x.XCoordinate).Returns(6);
			attack2.Setup(x => x.YCoordinate).Returns(7);

			var attack3 = new Mock<IAttack>();
			attack3.Setup(x => x.XCoordinate).Returns(6);
			attack3.Setup(x => x.YCoordinate).Returns(8);

			board.AttackCellOnBoard(attack1.Object);
			board.AttackCellOnBoard(attack2.Object);

			var expected = CellStateName.Sunk;
			var result = board.AttackCellOnBoard(attack3.Object);

			//Assert
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AttackCellOnBoard_ShipAlreadyHitInGivenPosition_CellHitAlreadyExceptionIsCaught()
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

			var attack = new Mock<IAttack>();
			attack.Setup(x => x.XCoordinate).Returns(9); 
			attack.Setup(x => x.YCoordinate).Returns(1);

			board.AttackCellOnBoard(attack.Object); //attack one

			//Act and Assert
			Assert.DoesNotThrow(() => board.AttackCellOnBoard(attack.Object));
		}

		[Test]
		public void AttackCellOnBoard_AttackIsOutOfBound_XCoordOutOfBoundsExceptionIsCaught()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(1); //Out of bounds
			ship.Setup(x => x.YStartCoordinate).Returns(1);
			ship.Setup(x => x.Length).Returns(5);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Vertical);

			var attack = new Mock<IAttack>();
			attack.Setup(x => x.XCoordinate).Returns(100); //Out of bounds
			attack.Setup(x => x.YCoordinate).Returns(1);

			board.AddShipToBoard(ship.Object);

			//Act and Assert
			Assert.DoesNotThrow(() => board.AttackCellOnBoard(attack.Object));
		}

		[Test]
		public void AttackCellOnBoard_AttackIsOutOfBound_YCoordOutOfBoundsExceptionIsCaught()
		{
			//Arrange
			var board = new Board();
			board.CreateBoard();

			var ship = new Mock<IShip>();
			ship.Setup(x => x.XStartCoordinate).Returns(1);
			ship.Setup(x => x.YStartCoordinate).Returns(1);
			ship.Setup(x => x.Length).Returns(5);
			ship.Setup(x => x.Alignment).Returns(ShipAlignment.Vertical);

			var attack = new Mock<IAttack>();
			attack.Setup(x => x.XCoordinate).Returns(1);
			attack.Setup(x => x.YCoordinate).Returns(100);  //Out of bounds

			board.AddShipToBoard(ship.Object);

			//Act and Assert
			Assert.DoesNotThrow(() => board.AttackCellOnBoard(attack.Object));
		}
	}
}