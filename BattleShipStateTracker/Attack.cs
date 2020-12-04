using BattleShipStateTracker.Interfaces;

namespace BattleShipStateTracker
{
	public class Attack : IAttack
	{
		public int XCoordinate { get; set; }

		public int YCoordinate { get; set; }

		public bool SuccessfulAttack { get; set; }
	}
}