namespace BattleShipStateTracker.CellStateTracker
{
	public interface ICell
	{
		ICellState State { get; set; }

		void ChangeState();

		CellStateName ReportState();
	}
}
