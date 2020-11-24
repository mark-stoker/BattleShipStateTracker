using System;

namespace BattleShipStateTracker.GameStateTracker
{
	public class AllShipsSunkState : IGameState
	{
		public void ChangeState(Game game)
		{
			ReportState();
		}

		public GameStateName ReportState()
		{
			DetailedStateMessage();
			return GameStateName.AllShipsSunk;
		}

		private static void DetailedStateMessage()
		{
			Console.WriteLine("All ships are sunk! Thanks for playing.");
		}
	}
}
