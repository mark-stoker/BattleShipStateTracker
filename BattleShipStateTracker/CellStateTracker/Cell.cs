using System.ComponentModel.DataAnnotations;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.CellStateTracker.Interfaces;

namespace BattleShipStateTracker.CellStateTracker
{
	public class Cell : ICell
	{
		public int XCoordinate { get; set; }
		public int YCoordinate { get; set; }
		public ICellState State { get; set; }

		public Cell(int x, int y)
		{
			XCoordinate = x;
			YCoordinate = y;
			State = new WaterState();
		}

		public void ChangeState(ICell cell)
		{
			this.State.ChangeState(this);
		}

		public CellStateName ReportState()
		{
			return this.State.ReportState();
		}
	}
}
