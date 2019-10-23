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
    public class GetAllAssociationsForUserRequest :
        IRequest<List<GetAssociationDto>>
    {
        public int UserId { get; set; }
    }

    public class GetAllAssociationsForUserRequestHandler :
        IRequestHandler<GetAllAssociationsForUserRequest,
            List<GetAssociationDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetAllAssociationsForUserRequestHandler(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetAssociationDto>> Handle(
            GetAllAssociationsForUserRequest request,
            CancellationToken cancellationToken)
        {
            var entity = _context.Set<UserAssociation>()
                .Where(x => x.UserId == request.UserId)
                .Select(x => x.AssociationId);

            return await _context.Set<Association>().Where(x => entity.Contains(x.Id))
                .ProjectTo<GetAssociationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }

    public class GetAllAssociationsForUserRequestValidator :
        AbstractValidator<GetAllAssociationsForUserRequest>
    {
        public GetAllAssociationsForUserRequestValidator()
        {

        }
    }
}
