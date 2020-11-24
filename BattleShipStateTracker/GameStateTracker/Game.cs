namespace BattleShipStateTracker.GameStateTracker
{
	public enum GameStateName
	{
		NoShipsHit = 1,
		PartialShipHits = 2,
		AllShipsSunk = 3,
	}

	public class Game
	{
		public IGameState State { get; set; }

		public Game()
		{
			State = new NoShipsHitState();
		}

		public void ChangeState()
		{
			this.State.ChangeState(this);
		}

		public GameStateName ReportState()
		{
			return this.State.ReportState();
		}
	}
}
