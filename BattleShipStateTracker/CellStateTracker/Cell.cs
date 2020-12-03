using System.ComponentModel.DataAnnotations;
using BattleShipStateTracker.CellStateTracker.Enums;
using BattleShipStateTracker.CellStateTracker.Interfaces;

namespace BattleShipStateTracker.CellStateTracker
{
	public class Cell : ICell
	{
		private readonly int _xCoordinate;
		private readonly int _yCoordinate;

		public ICellState State { get; set; }

		public Cell(int x, int y)
		{
			_xCoordinate = x;
			_yCoordinate = y;
			State = new WaterState();
		}

		public void ChangeState()
		{
			this.State.ChangeState(this);
		}

		public CellStateName ReportState()
		{
			return this.State.ReportState();
		}
	}
}
