using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.CellStateTracker.Interfaces;

namespace BattleShipStateTracker.CellStateTracker
{
	public class OccupiedState : ICellState
	{
		public CellStateName IncomingAttack(ICell cell)
		{
			cell.State = new HitState();
			return CellStateName.Hit;
		}

		public void ChangeState(ICell cell)
		{
			cell.State = new HitState();
		}

		public CellStateName ReportState()
		{
			return CellStateName.Occupied;
		}
	}
}
