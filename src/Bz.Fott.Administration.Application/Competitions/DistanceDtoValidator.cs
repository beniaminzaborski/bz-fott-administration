using FluentValidation;

namespace Bz.Fott.Administration.Application.Competitions;

public class DistanceDtoValidator : AbstractValidator<DistanceDto>
{
	public DistanceDtoValidator()
	{
		RuleFor(x => x.Amount)
			.GreaterThanOrEqualTo(0).WithMessage("Must be greater or equal to 0");

        RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("Must be not empty");
    }
}
