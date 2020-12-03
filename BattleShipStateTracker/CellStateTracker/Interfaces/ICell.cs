using BattleShipStateTracker.CellStateTracker.Enums;

namespace BattleShipStateTracker.CellStateTracker.Interfaces
{
	public interface ICell
	{
		int XCoordinate { get; set; }

		int YCoordinate { get; set; }

		ICellState State { get; set; }

		void ChangeState(ICell cell);

		CellStateName ReportState();
	}
}
