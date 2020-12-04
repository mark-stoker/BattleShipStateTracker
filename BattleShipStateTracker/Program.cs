using System;
using BattleShipStateTracker.Enums;

namespace BattleShipStateTracker
{
	class Program
	{
		static void Main(string[] args)
		{
			Game game = new Game();
			
			MainMenu();

			bool quitNow = false;

			while (!quitNow)
			{
				switch (Console.ReadLine())
				{
					case "1":
						CreateGameBoard(game);
						MainMenu();
						break;
					case "2":
						AddShipToBoard(game);
						MainMenu();
						break;
					case "3":
						AttackCellOnBoard(game);
						MainMenu();
						break;
					case "4":
						GameStatus(game);
						MainMenu();
						break;
					case "5":
						Console.WriteLine($"You have quit the gameState");
						quitNow = true;
						break;
				}
			}
		}

		private static void MainMenu()
		{
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Choose an option from the following list:");
			Console.WriteLine("\t1 - Create board");
			Console.WriteLine("\t2 - Add Battleship to the Board");
			Console.WriteLine("\t3 - Take an attack at a given position");
			Console.WriteLine("\t4 - Find out status of gameState");
			Console.WriteLine("\t5 - Quit gameState");
			Console.WriteLine();
			Console.WriteLine();
		}

		private static void CreateGameBoard(Game game)
		{
			if (!game.Board.BoardCreated())
			{
				game.Board.CreateBoard();
				Console.WriteLine();
				Console.WriteLine($"Battleships 10 x 10 board created");
				Console.WriteLine();
				Console.WriteLine();
			}

			else
			{
				Console.WriteLine();
				Console.WriteLine($"Board is already created");
				Console.WriteLine();
				Console.WriteLine();
			}
		}

		private static void AddShipToBoard(Game game)
		{
			if (!game.Board.BoardCreated())
			{
				Console.WriteLine();
				Console.WriteLine($"Please create a board before adding a ship");
				Console.WriteLine();
				Console.WriteLine();
			}
			else
			{
				Console.WriteLine($"State ship position in the format: X, Y, Length, Orientation e.g. 3, 3, 4, Horizontal");
				int xStartingPosition = 0;
				int yStartingPosition = 0;

				Console.WriteLine("Please enter the starting x coordinate of the ship:");
				xStartingPosition = Convert.ToInt32(Console.ReadLine());

				Console.WriteLine("Please enter the starting y coordinate of the ship:");
				yStartingPosition = Convert.ToInt32(Console.ReadLine());

				Console.WriteLine("Please enter the length of the ship:");
				var length = Convert.ToInt32(Console.ReadLine());

				Console.WriteLine("Please enter the orientation of the ship:");
				var alignment = Convert.ToString(Console.ReadLine());
				Console.WriteLine();
				Console.WriteLine();

				game?.AddShipToBoard(xStartingPosition, yStartingPosition, length,
					(ShipAlignment)Enum.Parse(typeof(ShipAlignment), alignment));

				Console.WriteLine();
				Console.WriteLine("Battleship added to Board");
				Console.WriteLine();
				Console.WriteLine();
			}
		}

		private static void AttackCellOnBoard(Game game)
		{
			if (!game.Board.BoardCreated())
			{
				Console.WriteLine($"Please create a board and add a ship before attacking");
			}
			else if (game.Ships.Count == 0)
			{
				Console.WriteLine($"Please add a ship before attacking");
				Console.WriteLine();
				Console.WriteLine();
			}
			else
			{
				Console.WriteLine($"Take an attack at a given position");
				int xAttackPosition = 0;
				int yAttackPosition = 0;

				Console.WriteLine("Please enter the x coordinate of the ship:");
				xAttackPosition = Convert.ToInt32(Console.ReadLine());

				Console.WriteLine("Please enter the y coordinate of the ship:");
				yAttackPosition = Convert.ToInt32(Console.ReadLine());

				Console.WriteLine();
				Console.WriteLine();
				Console.WriteLine(game.AttackCellOnBoard(xAttackPosition, yAttackPosition) + "!");
				Console.WriteLine();
				
				Console.WriteLine();
				
			}
		}

		private static void GameStatus(Game game)
		{
			if (!game.Board.BoardCreated())
			{
				Console.WriteLine();
				Console.WriteLine();
				Console.WriteLine($"To start the game, please create a board");
				Console.WriteLine();
				Console.WriteLine();
			}
			else
			{
				Console.WriteLine();
				Console.WriteLine();
				var test = game.GetGameState();

				if (game.GameStateName == GameStateName.AllShipsSunk)
				{
					Console.WriteLine("The game state is: " + game.GetGameState() + ". All your ships are sunk, game over.");
				}

				if (game.GameStateName == GameStateName.ShipsPartiallyHit)
				{
					Console.WriteLine("The game state is: " + game.GetGameState() + ". Some damage has been received, keep playing.");
				}

				if (game.GameStateName == GameStateName.NoShipsHit)
				{
					Console.WriteLine("The game state is: " + game.GetGameState() + ". No damage received, keep playing.");
				}
				Console.WriteLine("The number of ships on the board was " + game.Ships.Count);
				Console.WriteLine();
				Console.WriteLine();
			}
		}

	}
}
