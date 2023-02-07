using FluentValidation;
using System.Drawing;

namespace Bz.Fott.Administration.Application.Competitions;

public class CompetitionPlaceDtoValidator : AbstractValidator<CompetitionPlaceDto>
{
	public CompetitionPlaceDtoValidator()
	{
		RuleFor(x => x.Latitude)
		  .InclusiveBetween(-90.0000000m, 90.0000000m).WithMessage("Must be between -90.0000000 and 90.0000000");

        RuleFor(x => x.Longitute)
          .InclusiveBetween(-180.0000000m, 180.0000000m).WithMessage("Must be between -180.0000000 and 180.0000000");

		RuleFor(x => x.City)
			.NotEmpty().WithMessage("Must be not empty")
			.MaximumLength(100).WithMessage("Lenght must be less than or equal to 100 characters");
    }
}
