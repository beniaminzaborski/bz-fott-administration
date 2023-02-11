using AutoMapper;
using Bz.Fott.Administration.Domain.ManagingCompetition;

namespace Bz.Fott.Administration.Application.Competitions;

internal class CompetitionMappings : Profile
{
    public CompetitionMappings()
    {
        CreateMap<Competition, CompetitionDto>()
            .DisableCtorValidation()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<Distance, DistanceDto>()
            .DisableCtorValidation()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ToString()));

        CreateMap<CompetitionPlace, CompetitionPlaceDto>()
            .DisableCtorValidation()
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Localization.Latitude))
            .ForMember(dest => dest.Longitute, opt => opt.MapFrom(src => src.Localization.Longitude));

        CreateMap<Checkpoint, CheckpointDto>()
            .DisableCtorValidation()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.TrackPointAmount, opt => opt.MapFrom(src => src.TrackPoint.Amount))
            .ForMember(dest => dest.TrackPointUnit, opt => opt.MapFrom(src => src.TrackPoint.Unit.ToString()));
    }
}
