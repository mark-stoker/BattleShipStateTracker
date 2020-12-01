namespace BattleShipStateTracker.CellStateTracker
{
	public class WaterState : ICellState
	{
		public CellStateName IncomingAttack(ICell cell, ICell[,] boardCells)
		{
			return ReportState();
		}

		public void ChangeState(ICell cell)
		{
			cell.State = new OccupiedState();
		}	

		public CellStateName ReportState()
		{
			return CellStateName.Water;
		}
	}
}
