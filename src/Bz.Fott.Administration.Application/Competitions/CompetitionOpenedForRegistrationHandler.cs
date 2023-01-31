using Bz.Fott.Administration.Domain.ManagingCompetition;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bz.Fott.Administration.Application.Competitions;

public class CompetitionOpenedForRegistrationHandler : INotificationHandler<CompetitionOpenedForRegistration>
{
    private readonly ILogger<CompetitionOpenedForRegistrationHandler> _logger;

    public CompetitionOpenedForRegistrationHandler(ILogger<CompetitionOpenedForRegistrationHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CompetitionOpenedForRegistration notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("<Application Layer> Competition opened to registration by competitors!");

        // TODO: Publish integrate event to notify all potential services

        return Task.CompletedTask;
    }
}
