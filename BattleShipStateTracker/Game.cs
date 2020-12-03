using System;
using System.Collections.Generic;
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

		public Game()
		{
			GameStateName = GameStateName.NoShipsHit;
			Board = new Board();
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
