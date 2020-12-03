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
	}
}
