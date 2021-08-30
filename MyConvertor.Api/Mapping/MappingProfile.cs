
using AutoMapper;
using MyConvertor.Api.Resources;
using MyConvertor.Core.Models;

namespace MyConvertor.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Currency, ConvertorResource>();            
            // Resource to Domain
            CreateMap<ConvertorResource, Currency>();

            CreateMap<ConvertorResource, Currency>();
        }
    }
}