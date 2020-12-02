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
		private readonly int _xCoordinate;
		private readonly int _yCoordinate;

		public ICellState State { get; set; }

		public Cell(int x, int y)
		{
			_xCoordinate = x;
			_yCoordinate = y;
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
	}
}
