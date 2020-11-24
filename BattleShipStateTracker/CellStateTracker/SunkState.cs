using System;

namespace BattleShipStateTracker.CellStateTracker
{
	public class SunkState : ICellState
	{
		public CellStateName IncomingAttack(Cell cell, Cell[,] boardCells)
		{
			throw new NotImplementedException();
		}

		public void ChangeState(Cell cell)
		{
			throw new NotImplementedException();
		}

		public CellStateName ReportState()
		{
			return CellStateName.Sunk;
		}
	}
}
