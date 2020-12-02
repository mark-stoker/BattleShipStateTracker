using BattleShipStateTracker.CellStateTracker.Enums;

namespace BattleShipStateTracker.CellStateTracker.Interfaces
{
	public interface ICell
	{
		ICellState State { get; set; }

		void ChangeState();

		CellStateName ReportState();
	}
}
