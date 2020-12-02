using System;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.CellStateTracker.Interfaces;

namespace BattleShipStateTracker.CellStateTracker
{
	public class SunkState : ICellState
	{
		public CellStateName IncomingAttack(ICell cell)
		{
			throw new NotImplementedException();
		}

		public void ChangeState(ICell cell)
		{
			throw new NotImplementedException();
		}

		public CellStateName ReportState()
		{
			return CellStateName.Sunk;
		}
	}
}
