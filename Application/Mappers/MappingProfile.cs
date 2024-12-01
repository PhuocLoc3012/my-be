using Application.Dtos.AuthDto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationDto, ApplicationUser>();
                //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                //.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                //.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                //.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                //.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                //.ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar));
            //   .ForAllOtherMembers(opt => opt.Ignore());


        }
    }
}
