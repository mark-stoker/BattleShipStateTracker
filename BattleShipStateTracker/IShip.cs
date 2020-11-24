namespace BattleShipStateTracker
{
	public interface IShip
	{
		Ship Ship(int x, int y, int length, Alignment alignment);
	}
}
