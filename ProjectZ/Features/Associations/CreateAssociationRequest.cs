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
using ProjectZ.Dtos;

namespace ProjectZ.Features.Associations
{
    public class CreateAssociationRequest : IRequest<GetAssociationDto>
    {
        public string Name { get; set; }
    }

    public class CreateAssociationRequestHandler :
        IRequestHandler<CreateAssociationRequest, GetAssociationDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CreateAssociationRequestHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAssociationDto> Handle(CreateAssociationRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Association>(request);

            _context.Set<Association>().Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetAssociationDto>(entity);
        }
    }

    public class CreateAssociationRequestValidator : AbstractValidator<CreateAssociationRequest>
    {
        private readonly DataContext _context;
        public CreateAssociationRequestValidator(DataContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .Must(BeUniqueName)
                .WithMessage(ErrorMessages.Association.NameAlreadyExists);
        }

        private bool BeUniqueName(string arg)
        {
            return !_context.Set<Association>().Any(x => x.Name == arg);
        }
    }
}
