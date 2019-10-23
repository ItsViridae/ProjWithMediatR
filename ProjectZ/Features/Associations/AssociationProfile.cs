using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProjectZ.Data.Entities;
using ProjectZ.Dtos;

namespace ProjectZ.Features.Associations
{
    public class AssociationProfile : Profile
    {
        public AssociationProfile()
        {
            CreateMap<Association, GetAssociationDto>();
            CreateMap<CreateAssociationRequest, Association>();
            CreateMap<EditAssociationRequest, Association>();
        }
    }
}
