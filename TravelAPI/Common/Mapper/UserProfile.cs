using AutoMapper;
using TravelAPI.Core.Models;
using TravelAPI.ViewModels.RequestModels;

namespace TravelAPI.Common.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequest, User>()
                .ForMember(set => set.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(set => set.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(set => set.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(set => set.RegisteredOn, opt => opt.MapFrom(src => src.RegisteredOn))
                .IncludeAllDerived();
        }
    }
}
