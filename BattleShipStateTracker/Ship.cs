namespace BattleShipStateTracker
{
	public enum Alignment
	{
		Vertical = 0,
		Horizontal = 1
	};

	public class Ship : IShip
	{
		private int _xAxis;
		private int _yAxis;
		private int _length;
		private Alignment _alignment;

		public Ship(int x, int y, int length, Alignment alignment)
		{
			_xAxis = x;
			_yAxis = y;
			_length = length;
			_alignment = alignment;
		}
	}
}
