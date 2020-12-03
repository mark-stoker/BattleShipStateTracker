using System;
using BattleShipStateTracker.Enums;
using BattleShipStateTracker.Interfaces;

namespace BattleShipStateTracker
{
	public class Game : IGame
	{
		public GameStateName GameStateName { get; set; }

		public IBoard Board { get; set; }

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

		public GameStateName GetGameState()
		{
			if (Board.AllOccupiedBoardCellsHit())
				GameStateName = GameStateName.AllShipsSunk;

			if (Board.BoardCellsPartiallyHit())
				GameStateName = GameStateName.ShipsPartiallyHit;

			return GameStateName;
		}

		public string OutputGameState()
		{
			string result = null;
			GetGameState();

			if (GameStateName == GameStateName.AllShipsSunk)
			{
				result = "The game state is: " + GameStateName + ". All your ships are sunk, game over.";
				Console.WriteLine(result);
			}

			if (GameStateName == GameStateName.ShipsPartiallyHit)
			{
				result = "The game state is: " + GameStateName + ". Some damage has been received, keep playing.";
				Console.WriteLine(result);
			}

			if (GameStateName == GameStateName.NoShipsHit)
			{
				result = "The game state is: " + GameStateName + ". No damage received, keep playing.";
				Console.WriteLine(result);
			}

			return result;
		}

		public void test()
		{

		}
	}
}
