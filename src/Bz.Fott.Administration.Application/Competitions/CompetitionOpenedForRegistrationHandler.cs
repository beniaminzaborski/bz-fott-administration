using AutoMapper;
using Bz.Fott.Administration.Domain.ManagingCompetition;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bz.Fott.Administration.Application.Competitions;

public class CompetitionOpenedForRegistrationHandler : INotificationHandler<CompetitionOpenedForRegistration>
{
    private readonly ILogger<CompetitionOpenedForRegistrationHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public CompetitionOpenedForRegistrationHandler(
        ILogger<CompetitionOpenedForRegistrationHandler> logger,
        IPublishEndpoint publishEndpoint,
        IMapper mapper)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }

    public async Task Handle(CompetitionOpenedForRegistration domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("<Application Layer> Competition opened to registration by competitors!");

        await _publishEndpoint.Publish(new CompetitionOpenedForRegistrationIntegrationEvent(
            domainEvent.Id.Value,
            _mapper.Map<CompetitionPlaceDto>(domainEvent.Place),
            _mapper.Map<DistanceDto>(domainEvent.Distance),
            domainEvent.StartAt,
            domainEvent.MaxCompetitors,
            _mapper.Map<IEnumerable<Checkpoint>, IEnumerable<CheckpointDto>>(domainEvent.Checkpoints)));
    }
}
