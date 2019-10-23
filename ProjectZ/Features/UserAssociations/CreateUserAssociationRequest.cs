using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ProjectZ.Data;
using ProjectZ.Data.Entities;
using ProjectZ.Dtos;

namespace ProjectZ.Features.UserAssociations
{
    public class CreateUserAssociationRequest :
        IRequest<GetUserAssociationDto>
    {
        public int UserId { get; set; }
        public int AssociationId { get; set; }
    }

    public class CreateUserAssociationRequestHandler :
        IRequestHandler<CreateUserAssociationRequest, GetUserAssociationDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CreateUserAssociationRequestHandler(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUserAssociationDto> Handle(
            CreateUserAssociationRequest request,
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<UserAssociation>(request);

            _context.Set<UserAssociation>().Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetUserAssociationDto>(entity);
        }
    }

    public class CreateUserAssociationRequestValidator :
        AbstractValidator<CreateUserAssociationRequest>
    {
        public CreateUserAssociationRequestValidator()
        {
            
        }
    }
}
