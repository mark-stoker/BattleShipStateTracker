namespace BattleShipStateTracker.CellStateTracker
{
	public class HitState : ICellState
	{
		public CellStateName IncomingAttack(Cell cell, Cell[,] boardCells)
		{
			return ReportState();
		}

		public void ChangeState(Cell cell)
		{
			cell.State = new SunkState();
		}

		public CellStateName ReportState()
		{
			return CellStateName.Hit;
		}
	}
}
