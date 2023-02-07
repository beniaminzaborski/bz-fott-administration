using Bz.Fott.Administration.Domain.ManagingCompetition;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bz.Fott.Administration.Application.Competitions;

public class CompetitionMaxCompetitorsDecreasedHandler : INotificationHandler<CompetitionMaxCompetitorsDecreased>
{
    private readonly ILogger<CompetitionMaxCompetitorsDecreasedHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public CompetitionMaxCompetitorsDecreasedHandler(
        ILogger<CompetitionMaxCompetitorsDecreasedHandler> logger,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(CompetitionMaxCompetitorsDecreased domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("<Application Layer> Maximum numbers of competitors has been decreased!");

        await _publishEndpoint.Publish(new CompetitionMaxCompetitorsDecreasedIntegrationEvent(domainEvent.Id.Value, domainEvent.MaxCompetitors));
    }
}
