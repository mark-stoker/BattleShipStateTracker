namespace BattleShipStateTracker.CellStateTracker
{
	public interface ICellState
	{
		CellStateName IncomingAttack(Cell cell, Cell[,] boardCells);

		void ChangeState(Cell cell);

		CellStateName ReportState();
	}
}
