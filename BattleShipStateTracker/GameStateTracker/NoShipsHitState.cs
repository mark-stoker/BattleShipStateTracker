using System;

namespace BattleShipStateTracker.GameStateTracker
{
	public class NoShipsHitState : IGameState
	{
		public void ChangeState(Game game)
		{
			game.State = new PartialShipHitsState();
		}

		public GameStateName ReportState()
		{
			DetailedStateMessage();
			return GameStateName.NoShipsHit;
		}

		private static void DetailedStateMessage()
		{
			Console.WriteLine("No ships have been hit, keep attacking.");
		}
	}
}
