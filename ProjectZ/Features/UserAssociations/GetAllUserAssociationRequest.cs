using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectZ.Data;
using ProjectZ.Data.Entities;
using ProjectZ.Dtos;

namespace ProjectZ.Features.UserAssociations
{
    public class GetAllUserAssociationRequest : IRequest<List<GetUserAssociationDto>>
    {
    }

    public class GetAllUserAssociationRequestHandler :
        IRequestHandler<GetAllUserAssociationRequest,
            List<GetUserAssociationDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetAllUserAssociationRequestHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetUserAssociationDto>> Handle(
            GetAllUserAssociationRequest request,
            CancellationToken cancellationToken)
        {
            return await _context.Set<UserAssociation>()
                .ProjectTo<GetUserAssociationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }

    public class GetAllUserAssociationRequestValidator : AbstractValidator<GetAllUserAssociationRequest>
    {
        public GetAllUserAssociationRequestValidator()
        {
            
        }
    }
}
