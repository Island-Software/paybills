using System.Linq;
using Paybills.API.DTOs;
using Paybills.API.Entities;
using AutoMapper;
using Paybills.API.Domain.Entities;
using Paybills.API.Application.DTOs.ReceivingType;
using Paybills.API.Application.DTOs.Receiving;

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
            CreateMap<Receiving, ReceivingDto>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => (from user in src.Users
                                               select user.UserName)));
            CreateMap<ReceivingType, ReceivingTypeDto>();
            CreateMap<ReceivingTypeDto, ReceivingType>();
        }
    }
}