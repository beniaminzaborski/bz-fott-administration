using MediatR;
using Microsoft.Extensions.Logging;

namespace Bz.Fott.Administration.Domain.ManagingCompetition;

public class CompetitionApprovedHandler : INotificationHandler<CompetitionApproved>
{
    private readonly ILogger<CompetitionApprovedHandler> _logger;

    public CompetitionApprovedHandler(ILogger<CompetitionApprovedHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CompetitionApproved notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Competition approved!");

        // TODO: Publish integrate event to notify all potential servicess

        return Task.CompletedTask;
    }
}
