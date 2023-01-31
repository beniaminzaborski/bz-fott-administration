using Bz.Fott.Administration.Domain.ManagingCompetition;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bz.Fott.Administration.Application.Competitions;

public class CompetitionRegistrationCompletedHandler : INotificationHandler<CompetitionRegistrationCompleted>
{
    private readonly ILogger<CompetitionRegistrationCompletedHandler> _logger;

    public CompetitionRegistrationCompletedHandler(ILogger<CompetitionRegistrationCompletedHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CompetitionRegistrationCompleted notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("<Application Layer> Competition registration completed!");

        // TODO: Publish integrate event to notify all potential services

        return Task.CompletedTask;
    }
}
