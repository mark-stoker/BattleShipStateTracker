using BattleShipStateTracker.GameStateTracker;
using NUnit.Framework;

namespace BattleShipTrackerTests
{
	public class GameTests
	{
		private Game _game;

		[Test, Order(1)]
		public void GameInCorrectState_InitialCreation_InNoShipsHitState()
		{
			//Arrange
			_game = new Game(); //Constructor puts this into starting state of NoShipsHitState

			//Act
			var result = _game.ReportState();

			//Assert
			Assert.AreEqual(GameStateName.NoShipsHit, result);
		}

		[Test, Order(2)]
		public void GameInCorrectState_FromPartialHitState_ChangeToOccupiedState()
		{
			//Arrange
			_game.ChangeState();

			//Act
			var result = _game.ReportState();

			//Assert
			Assert.AreEqual(GameStateName.PartialShipHits, result);
		}

		[Test, Order(3)]
		public void GameInCorrectState_PartialShipHitsState_AllShipsSunkState()
		{
			//Arrange
			_game.ChangeState();

			//Act
			var result = _game.ReportState();

			//Assert
			Assert.AreEqual(GameStateName.AllShipsSunk, result);
		}
	}
}