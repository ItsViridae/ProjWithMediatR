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
using ProjectZ.Common.Responses;
using ProjectZ.Data;
using ProjectZ.Data.Entities;
using ProjectZ.Dtos;

namespace ProjectZ.Features.UserAssociations
{
    public class GetAllUsersForAssociationRequest :
        IRequest<List<GetUserDto>>
    {
        public int AssociationId { get; set; }
    }

    public class GetAllUsersForAssociationRequestHandler :
        IRequestHandler<GetAllUsersForAssociationRequest, List<GetUserDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public GetAllUsersForAssociationRequestHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<GetUserDto>> Handle(GetAllUsersForAssociationRequest request, CancellationToken cancellationToken)
        {
            var entity = _context.Set<UserAssociation>()
                .Where(x => x.AssociationId == request.AssociationId)
                .Select(x => x.UserId);

            return await _context.Set<User>().Where(x => entity.Contains(x.Id))
                .ProjectTo<GetUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }

    public class GetAllUsersForAssociationRequestValidator : AbstractValidator<GetAllUsersForAssociationRequest>
    {
        private readonly DataContext _context;
        public GetAllUsersForAssociationRequestValidator(DataContext context)
        {
            _context = context;

            RuleFor(x => x.AssociationId)
                .Must(ExistInDatabase)
                .WithMessage(ErrorMessages.Association.IdDoesNotExist);
        }

        private bool ExistInDatabase(int id)
        {
            return _context.Set<UserAssociation>().Any(x => x.AssociationId == id);
        }
    }

    
}
