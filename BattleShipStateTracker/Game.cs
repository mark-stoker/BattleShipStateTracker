using System;
using System.Collections.Generic;
using System.Linq;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.Enums;
using BattleShipStateTracker.Exceptions;
using BattleShipStateTracker.Interfaces;

namespace BattleShipStateTracker
{
	public class Game : IGame
	{
		public GameStateName GameStateName { get; set; }
		public IBoard Board { get; set; }
		public IList<IShip> Ships { get; set; }
		public IAttack Attack { get; set; }

		public Game()
		{
			GameStateName = GameStateName.NoShipsHit;
			Board = new Board();
			Ships = new List<IShip>();
		}

		public Game(IBoard board)
		{
			GameStateName = GameStateName.NoShipsHit;
			Board = board;
		}

		public void AddShipToBoard(int xStartCoordinate, int yStartCoordinate, int length, ShipAlignment alignment)
		{
			try
			{
				xStartCoordinate.ValidateXStartCoordinate();
				yStartCoordinate.ValidateYStartCoordinate();
				length.ValidateLength();

				var ship = new Ship(xStartCoordinate, yStartCoordinate, length, alignment);
				Ships.Add(ship);

				Board.AddShipToBoard(ship);
			}
			catch (XCoordOutOfBoundsException exception)
			{
				Console.WriteLine(exception.Message);
			}
			catch (YCoordOutOfBoundsException exception)
			{
				Console.WriteLine(exception.Message);
			}
			catch (LengthOutOfBoundsException exception)
			{
				Console.WriteLine(exception.Message);
			}
		}

		public CellStateName? AttackCellOnBoard(int xCoordinate, int yCoordinate)
		{
			Attack = new Attack
			{
				XCoordinate = xCoordinate,
				YCoordinate = yCoordinate
			};

			var result = Board.AttackCellOnBoard(Attack);

			if (result == CellStateName.Hit)
			{
				var ship = Ships.FirstOrDefault(x => x.ShipRange.Contains(new Tuple<int, int>(xCoordinate, yCoordinate)));
				if (ship != null) ship.Hits += 1;

				Attack.SuccessfulAttack = true;
			}

			return result;
		}

		public GameStateName GetGameState()
		{
			if (Board.AllOccupiedBoardCellsHit())
				GameStateName = GameStateName.AllShipsSunk;

			if (Board.BoardCellsPartiallyHit())
				GameStateName = GameStateName.ShipsPartiallyHit;

			return GameStateName;
		}
	}
}
