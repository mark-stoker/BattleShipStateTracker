namespace BattleShipStateTracker.CellStateTracker
{
	public interface ICell
	{
		ICellState State { get; set; }

		void ChangeState();

		CellStateName ReportState();

		CellStateName SpecificCellStatus(int horizontalCoOrdinate, int verticalCoOrdinate, ICell[,] boardCells);
	}
}
