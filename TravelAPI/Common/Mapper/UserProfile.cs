using AutoMapper;
using TravelAPI.Core.Models;
using TravelAPI.ViewModels;
using TravelAPI.ViewModels.RequestModels;

namespace TravelAPI.Common.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequest, User>()
                .ForMember(set => set.UserName, opt => opt.MapFrom(src => src.Login))
                .ForMember(set => set.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(set => set.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(set => set.RegisteredOn, opt => opt.MapFrom(src => src.RegisteredOn))
                .ForMember(set => set.Email, opt => opt.MapFrom(src => src.Email))
                .IncludeAllDerived();

            CreateMap<User, UserViewModel>()
                .ForMember(set => set.Login, opt => opt.MapFrom(src => src.UserName))
                .ForMember(set => set.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(set => set.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(set => set.RegisterOn, opt => opt.MapFrom(src => src.RegisteredOn))
                .ForMember(set => set.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(set => set.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(set => set.Email, opt => opt.MapFrom(src => src.Email))
                .IncludeAllDerived();
        }
    }
}
