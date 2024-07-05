using AutoMapper;
using Redarbor.Api.Application.Entities;
using Redarbor.Api.Domain.Dtos;

namespace Redarbor.Api.Common.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Redarbors, RedarborDto>().ReverseMap();
            CreateMap<Redarbors, RedarborResponseDto>().ReverseMap();
        }
    }
}
