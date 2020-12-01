namespace BattleShipStateTracker.CellStateTracker
{
	public class OccupiedState : ICellState
	{
		public CellStateName IncomingAttack(ICell cell, ICell[,] boardCells)
		{
			cell.State = new HitState();

			if (AllOccupiedCellsHit(boardCells))
			{
				cell.State = new SunkState();
				return CellStateName.Sunk;
			}
		
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

		private bool AllOccupiedCellsHit(ICell[,] boardCells)
		{
			for (int col = 0; col < boardCells.GetLength(1); col++)
			{
				for (int row = 0; row < boardCells.GetLength(0); row++)
				{
					if (boardCells[row, col].State.ReportState().ToString() == CellStateName.Occupied.ToString())
						return false;
				}
			}

			return true;
		}
	}
}
