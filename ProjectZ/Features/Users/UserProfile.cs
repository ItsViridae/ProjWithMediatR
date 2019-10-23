using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProjectZ.Data.Entities;
using ProjectZ.Dtos;

namespace ProjectZ.Features.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetUserDto>();

            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.PasswordSalt, opts => opts.Ignore())
                .ForMember(dest => dest.PasswordHash, opts => opts.Ignore());

            CreateMap<EditUserRequest, User>();
        }
    }
}
