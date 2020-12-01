namespace BattleShipStateTracker.CellStateTracker
{
	public interface ICellState
	{
		CellStateName IncomingAttack(ICell cell, ICell[,] boardCells);

		void ChangeState(ICell cell);

		CellStateName ReportState();
	}
}
