using AutoMapper;
using TravelAPI.Core.Models;
using TravelAPI.ViewModels.RequestModels;

namespace TravelAPI.Common.Mapper
{
    public class TravelProfile : Profile
    {
        public TravelProfile()
        {
            CreateMap<CreateTravelRequest, Travel>()
                .ForMember(set => set.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(set => set.Region, opt => opt.MapFrom(src => src.Region))
                .ForMember(set => set.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(set => set.CompanyOrganizer, opt => opt.MapFrom(src => src.CompanyOrganizer))
                .IncludeAllDerived();

            CreateMap<UpdateTravelRequest, Travel>()
                .ForMember(set => set.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(set => set.Description, opt => opt.MapFrom(src => src.Description))
                .IncludeAllDerived();
        }
    }
}
