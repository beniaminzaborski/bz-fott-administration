using Bz.Fott.Administration.Domain.ManagingCompetition;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bz.Fott.Administration.Application.Competitions;

public class CompetitionOpenedForRegistrationHandler : INotificationHandler<CompetitionOpenedForRegistration>
{
    private readonly ILogger<CompetitionOpenedForRegistrationHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public CompetitionOpenedForRegistrationHandler(
        ILogger<CompetitionOpenedForRegistrationHandler> logger,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(CompetitionOpenedForRegistration notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("<Application Layer> Competition opened to registration by competitors!");

        await _publishEndpoint.Publish(new CompetitionOpenedForRegistrationIntegrationEvent 
        { 

        });
    }
}
