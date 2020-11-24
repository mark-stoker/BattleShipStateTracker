namespace BattleShipStateTracker.CellStateTracker
{
	public class WaterState : ICellState
	{
		public CellStateName IncomingAttack(Cell cell, Cell[,] boardCells)
		{
			return ReportState();
		}

		public void ChangeState(Cell cell)
		{
			cell.State = new OccupiedState();
		}	

		public CellStateName ReportState()
		{
			return CellStateName.Water;
		}
	}
}
