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
using Vinformatix.Api.Responses;

namespace ProjectZ.Features.UserAssociations
{
    public class DeleteUserAssociationRequest : IRequest<EmptyResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteUserAssociationRequestHandler :
        IRequestHandler<DeleteUserAssociationRequest, EmptyResponse>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DeleteUserAssociationRequestHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<EmptyResponse> Handle(DeleteUserAssociationRequest request, CancellationToken cancellationToken)
        {
            var entity = _context
                .Set<UserAssociation>()
                .Find(request.Id);

            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new EmptyResponse();
        }
    }

    public class DeleteUserAssociationRequestValidator : AbstractValidator<DeleteUserAssociationRequest>
    {
        public DeleteUserAssociationRequestValidator()
        {
            
        }
    }
}
