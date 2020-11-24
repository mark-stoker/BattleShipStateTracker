using System;

namespace BattleShipStateTracker.GameStateTracker
{
	public class PartialShipHitsState : IGameState
	{
		public void ChangeState(Game game)
		{
			game.State = new AllShipsSunkState();
		}

		public GameStateName ReportState()
		{
			DetailedStateMessage();
			return GameStateName.PartialShipHits;
		}

		private static void DetailedStateMessage()
		{
			Console.WriteLine("You have partially hit ships on the board, keep attacking.");
		}
	}
}
