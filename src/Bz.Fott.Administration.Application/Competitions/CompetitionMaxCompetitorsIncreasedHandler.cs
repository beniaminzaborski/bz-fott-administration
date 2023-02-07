using Bz.Fott.Administration.Domain.ManagingCompetition;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bz.Fott.Administration.Application.Competitions;

public class CompetitionMaxCompetitorsIncreasedHandler : INotificationHandler<CompetitionMaxCompetitorsIncreased>
{
    private readonly ILogger<CompetitionMaxCompetitorsIncreasedHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public CompetitionMaxCompetitorsIncreasedHandler(
        ILogger<CompetitionMaxCompetitorsIncreasedHandler> logger,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(CompetitionMaxCompetitorsIncreased domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("<Application Layer> Maximum numbers of competitors has been increased!");

        await _publishEndpoint.Publish(new CompetitionMaxCompetitorsIncreasedIntegrationEvent(domainEvent.Id.Value, domainEvent.MaxCompetitors));
    }
}
