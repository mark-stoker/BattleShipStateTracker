using BattleShipStateTracker.Enums;
using FluentValidation;

namespace BattleShipStateTracker
{
	class GameValidator : AbstractValidator<Game>
	{
		public GameValidator()
		{
			
			//RuleFor(x => x.AddShipToBoard(xStartCoordinate: 2, yStartCoordinate: 3, length: 3,
			//	alignment: ShipAlignment.Horizontal)).Length(1, 10);
		}
	}
}

