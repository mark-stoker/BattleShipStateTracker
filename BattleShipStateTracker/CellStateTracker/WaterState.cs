using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.CellStateTracker.Interfaces;

namespace BattleShipStateTracker.CellStateTracker
{
	public class WaterState : ICellState
	{
		public CellStateName IncomingAttack(ICell cell)
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
