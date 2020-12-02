namespace BattleShipStateTracker.GameStatus
{
	public enum GameStateName
	{
		NoShipsHit = 1,
		PartialShipHits = 2,
		AllShipsSunk = 3,
	}

	public class GameState : IGameState
	{
		public GameStateName GameStateName { get; set; }

		public GameState()
		{
			GameStateName = GameStateName.NoShipsHit;
		}
	}

	//Create Board
	//Add Battleship to Board
		//Validate if ship off board
		//Validate if ship overlaps current ship
	//Take attack at given coordinate and state whether hit or miss
		//Validate if attack off Board
		//Validate if cell already hit
		//If all ships hit/sunk update game state
	//Return whether player has lost game
	//Validation
}
