using System;

namespace BattleShipStateTracker
{
	class Program
	{
		static void Main(string[] args)
		{
			Board gameBoard = null; 
			
			MainMenu();

			bool quitNow = false;

			while (!quitNow)
			{
				switch (Console.ReadLine())
				{
					case "1":
						gameBoard = CreateGameBoard(gameBoard);
						MainMenu();
						break;
					case "2":
						AddShipToBoard(gameBoard);
						MainMenu();
						break;
					case "3":
						AttackCellOnBoard(gameBoard);
						MainMenu();
						break;
					case "4":
						GameStatus(gameBoard);
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

		private static Board CreateGameBoard(Board gameBoard)
		{
			if (gameBoard == null)
			{
				gameBoard = new Board();
				Console.WriteLine($"Battleships 10 x 10 board created");
			}
			else
			{
				Console.WriteLine($"Board is already created");
			}

			return gameBoard;
		}

		private static void AddShipToBoard(Board gameBoard)
		{
			if (gameBoard == null)
			{
				Console.WriteLine($"Please create a board before adding a ship");
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

				gameBoard?.AddShipToBoard(xStartingPosition, yStartingPosition, length,
					(ShipAlignment)Enum.Parse(typeof(ShipAlignment), alignment));

				Console.WriteLine("Battleship added to Board");
				Console.WriteLine();
			}
		}

		private static void AttackCellOnBoard(Board gameBoard)
		{
			if (gameBoard == null)
			{
				Console.WriteLine($"Please create a board and add a ship before attacking");
			}
			else if (gameBoard.NumberOfShipsOnBoard() == 0)
			{
				Console.WriteLine($"Please add a ship before attacking");
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

				Console.WriteLine(gameBoard.AttackCellOnBoard(xAttackPosition, yAttackPosition));
				Console.WriteLine();
			}
		}

		private static void GameStatus(Board gameBoard)
		{
			if (gameBoard == null)
			{
				Console.WriteLine($"To start the gameState, please create a board");
			}
			else
			{
				Console.WriteLine(gameBoard.GetGameState());
				Console.WriteLine("The number of ships on the board was " + gameBoard.NumberOfShipsOnBoard());
				Console.WriteLine("The position of the ships were " + gameBoard.NumberOfShipsOnBoard());
			}
		}
	}
}
