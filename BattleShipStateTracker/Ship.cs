namespace BattleShipStateTracker
{
	public enum Alignment
	{
		Vertical = 0,
		Horizontal = 1
	};

	public class Ship
	{
		protected int _xAxis;
		protected int _yAxis;
		protected int _length;
		protected Alignment _alignment;
		

		public Ship(int x, int y, int length, Alignment alignment)
		{
			_xAxis = x;
			_yAxis = y;
			_length = length;
			_alignment = alignment;
		}
	}
}
