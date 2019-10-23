using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ProjectZ.Common.Responses;
using ProjectZ.Data;
using ProjectZ.Data.Entities;
using Vinformatix.Api.Responses;

namespace ProjectZ.Features.Associations
{
    public class DeleteAssociationRequest : IRequest<EmptyResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteAssociationRequestHandler :
        IRequestHandler<DeleteAssociationRequest,EmptyResponse>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeleteAssociationRequestHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmptyResponse> Handle(DeleteAssociationRequest request, CancellationToken cancellationToken)
        {
            var entity = _context.Set<Association>().Find(request.Id);
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new EmptyResponse();
        }
    }

    public class DeleteAssociationRequestValidator : AbstractValidator<DeleteAssociationRequest>
    {
        private readonly DataContext _context;
        public DeleteAssociationRequestValidator(DataContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .Must(ExistsInDatabase)
                .WithMessage(ErrorMessages.Association.IdDoesNotExist);
        }

        private bool ExistsInDatabase(int id)
        {
            return _context.Set<Association>().Any(x => x.Id == id);
        }
    }
}
