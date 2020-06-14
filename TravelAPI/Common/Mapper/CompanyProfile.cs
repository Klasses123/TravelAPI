using AutoMapper;
using TravelAPI.Core.Models;
using TravelAPI.ViewModels;
using TravelAPI.ViewModels.RequestModels;

namespace TravelAPI.Common.Mapper
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CreateCompanyRequest, Company>()
                .ForMember(set => set.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(set => set.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(set => set.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(set => set.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .IncludeAllDerived();

            CreateMap<Company, CompanyViewModel>()
                .ForMember(set => set.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(set => set.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(set => set.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(set => set.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(set => set.Id, opt => opt.MapFrom(src => src.Id))
                .IncludeAllDerived();
        }
    }
}
