using BattleShipStateTracker.CellStateTracker.Enums;

namespace BattleShipStateTracker.CellStateTracker.Interfaces
{
	public interface ICellState
	{
		CellStateName IncomingAttack(ICell cell);

		void ChangeState(ICell cell);

		CellStateName ReportState();
	}
}
