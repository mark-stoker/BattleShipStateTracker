using BattleShipStateTracker.Enums;

namespace BattleShipStateTracker.Interfaces
{
	public interface IGame
	{
		GameStateName GameStateName { get; set; }

		IBoard Board { get; set; }

		GameStateName GetGameState();

		//void CreateBoard();

		//void AddShipToBoard(int xStartCoordinate, int yStartCoordinate, int length, ShipAlignment alignment);

		//CellStateName? IncomingAttack(int xCoordinate, int yCoordinate);

		//int NumberOfShipsOnBoard();

		//bool BoardCreated();
	}
}
