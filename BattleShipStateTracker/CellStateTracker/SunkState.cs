using System;

namespace BattleShipStateTracker.CellStateTracker
{
	public class SunkState : ICellState
	{
		public CellStateName IncomingAttack(ICell cell, ICell[,] boardCells)
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
