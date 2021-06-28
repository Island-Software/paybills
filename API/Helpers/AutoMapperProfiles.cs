using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<Bill, BillDto>()
                .ForMember(dest => dest.UserName, 
                    opt => opt.MapFrom(src => (from user in src.Users 
                                               select user.UserName)));
            CreateMap<BillType, BillTypeDto>();
        }
    }
}