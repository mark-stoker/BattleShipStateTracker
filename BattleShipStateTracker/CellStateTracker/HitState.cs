namespace BattleShipStateTracker.CellStateTracker
{
	public class HitState : ICellState
	{
		public CellStateName IncomingAttack(ICell cell, ICell[,] boardCells)
		{
			return ReportState();
		}

		public void ChangeState(ICell cell)
		{
			cell.State = new SunkState();
		}

		public CellStateName ReportState()
		{
			return CellStateName.Hit;
		}
	}
}
