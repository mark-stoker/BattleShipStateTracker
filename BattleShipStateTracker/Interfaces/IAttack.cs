namespace BattleShipStateTracker.Interfaces
{
	public interface IAttack
	{
		int XCoordinate { get; set; }

		int YCoordinate { get; set; }

		bool SuccessfulAttack { get; set; }
	}
}