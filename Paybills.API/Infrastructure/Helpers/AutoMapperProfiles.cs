using System.Linq;
using Paybills.API.DTOs;
using Paybills.API.Entities;
using AutoMapper;

namespace Paybills.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<AppUser, UserEditDto>();
            CreateMap<Bill, BillDto>()
                .ForMember(dest => dest.UserName, 
                    opt => opt.MapFrom(src => (from user in src.Users 
                                               select user.UserName)));
            CreateMap<BillType, BillTypeDto>();
        }
    }
}