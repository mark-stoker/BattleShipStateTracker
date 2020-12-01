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
}
