using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProjectZ.Data.Entities;
using ProjectZ.Dtos;

namespace ProjectZ.Features.UserAssociations
{
    public class UserAssociationProfile : Profile
    {
        public UserAssociationProfile()
        {
            CreateMap<GetUserAssociationDto, UserAssociation>()
                .ReverseMap();
            CreateMap<CreateUserAssociationRequest, UserAssociation>();
        }
    }
}
