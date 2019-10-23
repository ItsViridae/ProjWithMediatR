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
    public class GetByIdAssociationRequest : IRequest<GetAssociationDto>
    {
        public int Id { get; set; }
    }

    public class GetByIdAssociationRequestHandler :
        IRequestHandler<GetByIdAssociationRequest, GetAssociationDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public GetByIdAssociationRequestHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAssociationDto> Handle(GetByIdAssociationRequest request, CancellationToken cancellationToken)
        {
            var entity = _context.Set<Association>().Find(request.Id);
            var dto = _mapper.Map<GetAssociationDto>(entity);
            return dto;
        }
    }

    public class GetByIdAssociationRequestValidator : AbstractValidator<GetByIdAssociationRequest>
    {
        private readonly DataContext _context;
        public GetByIdAssociationRequestValidator(DataContext context)
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
