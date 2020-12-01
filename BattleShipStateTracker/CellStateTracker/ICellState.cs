namespace BattleShipStateTracker.CellStateTracker
{
	public interface ICellState
	{
		CellStateName IncomingAttack(ICell cell);

		void ChangeState(ICell cell);

		CellStateName ReportState();
	}
}
