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
using Vinformatix.Infrastructure.Mediatr;

namespace ProjectZ.Features.Associations
{
    public class GetAllAssociationRequest : IRequest<List<GetAssociationDto>>
    {

    }

    public class GetAllAssociationRequestHandler :
        IRequestHandler<GetAllAssociationRequest,
            List<GetAssociationDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetAllAssociationRequestHandler(
            DataContext context, 
            IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<List<GetAssociationDto>> Handle(GetAllAssociationRequest request, CancellationToken cancellationToken)
        {
            return await _context.Set<Association>()
                .ProjectTo<GetAssociationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }

    public class GetAllAssociationRequestValidator : AbstractValidator<GetAllAssociationRequest>
    {
        public GetAllAssociationRequestValidator()
        {
            
        }
    }
}
