using AutoMapper;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterDto, User>().ForMember(dest => dest.Status, opt => opt.MapFrom(x => true));
        }
    }
}
