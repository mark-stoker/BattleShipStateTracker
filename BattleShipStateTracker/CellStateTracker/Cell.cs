namespace BattleShipStateTracker.CellStateTracker
{
	public enum CellStateName
	{
		Water = 1,
		Occupied = 2,
		Hit = 3,
		Sunk = 4
	}

	public class Cell : ICell
	{
		public ICellState State { get; set; }

		public Cell(int x, int y)
		{
			State = new WaterState();
		}

		public void ChangeState()
		{
			this.State.ChangeState(this);
		}

		public CellStateName ReportState()
		{
			return this.State.ReportState();
		}

		public CellStateName SpecificCellStatus(int horizontalCoOrdinate, int verticalCoOrdinate, ICell[,] boardCells)
		{
			return boardCells[horizontalCoOrdinate - 1, verticalCoOrdinate - 1].State.ReportState();
		}
	}
}
