using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using ProjectZ.Common.Responses;
using ProjectZ.Data;
using ProjectZ.Data.Entities;
using ProjectZ.Dtos;

namespace ProjectZ.Features.Associations
{
    public class EditAssociationRequest : IRequest<GetAssociationDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditAssociationRequestHandler : IRequestHandler<EditAssociationRequest, GetAssociationDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EditAssociationRequestHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetAssociationDto> Handle(EditAssociationRequest request, CancellationToken cancellationToken)
        {
            var editAssociation = _context.Set<Association>().Find(request.Id);

            _mapper.Map(request, editAssociation);

            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<GetAssociationDto>(editAssociation);
        }
    }

    public class EditAssociationRequestValidator : AbstractValidator<EditAssociationRequest>
    {
        private readonly DataContext _context;
        public EditAssociationRequestValidator(DataContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .Must(ExistsInDatabase)
                .WithMessage(ErrorMessages.Association.IdDoesNotExist);

            RuleFor(x => x.Name)
                .Must(BeUniqueName)
                .WithMessage(ErrorMessages.Association.NameAlreadyExists);
        }
        private bool ExistsInDatabase(int id)
        {
            return _context.Set<Association>().Any(x => x.Id == id);
        }
        private bool BeUniqueName(string arg)
        {
            return !_context.Set<Association>().Any(x => x.Name == arg);
        }
    }
}
