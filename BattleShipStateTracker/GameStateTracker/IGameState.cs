namespace BattleShipStateTracker.GameStateTracker
{
	public interface IGameState
	{
		void ChangeState(Game game);

		GameStateName ReportState();
	}
}
