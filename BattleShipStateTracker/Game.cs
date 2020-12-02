using BattleShipStateTracker.CellStateTracker;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.Enums;
using BattleShipStateTracker.Interfaces;

namespace BattleShipStateTracker
{
	public class Game : IGame
	{
		private IBoard _board;
		public GameStateName GameStateName { get; set; }

		public Game()
		{
			GameStateName = GameStateName.NoShipsHit;
		}

		//Validate if already created
		public void CreateBoard()
		{
			_board = new Board();
		}

		//Validate if ship off board
		//Validate if ship overlaps current ship
		//Validate board is already created
		public void AddShipToBoard(int xStartCoordinate, int yStartCoordinate, int length, ShipAlignment alignment)
		{
			_board.AddShipToBoard(xStartCoordinate, yStartCoordinate, length, alignment);
		}

		//Validate if attack off Board
		//Validate if cell already hit
		//If all ships hit/sunk update game state
		public CellStateName? IncomingAttack(int xCoordinate, int yCoordinate)
		{
			var result = _board.AttackCellOnBoard(xCoordinate, yCoordinate);

			if (result == CellStateName.Hit)
				GameStateName = GameStateName.PartialShipHits;

			if (result == CellStateName.Sunk)
				GameStateName = GameStateName.AllShipsSunk;

			return result;
		}

		public int NumberOfShipsOnBoard()
		{
			return _board.NumberOfShipsOnBoard();
		}

		public bool BoardCreated()
		{
			if (_board == null)
				return false;

			return true;
		}

	}
}
