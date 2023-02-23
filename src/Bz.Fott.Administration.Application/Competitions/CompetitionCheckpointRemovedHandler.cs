using Bz.Fott.Administration.Domain.ManagingCompetition;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bz.Fott.Administration.Application.Competitions;

internal class CompetitionCheckpointRemovedHandler : INotificationHandler<CompetitionCheckpointRemoved>
{
    private readonly ILogger<CompetitionCheckpointRemovedHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public CompetitionCheckpointRemovedHandler(
        ILogger<CompetitionCheckpointRemovedHandler> logger,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(CompetitionCheckpointRemoved domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"<Application Layer> checkpoint {domainEvent.CheckpointId} from competition {domainEvent.CompetitionId} removed!");

        await _publishEndpoint.Publish(
            new CompetitionCheckpointRemovedIntegrationEvent(
                domainEvent.CompetitionId.Value,
                domainEvent.CheckpointId.Value,
                domainEvent.TrackPoint.Amount,
                domainEvent.TrackPoint.Unit.ToString()));
    }
}
