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
    public class GetByIdUserAssociationRequest : IRequest<GetUserAssociationDto>
    {
        public int Id { get; set; }
    }

    public class GetByIdUserAssociationRequestHandler : IRequestHandler<GetByIdUserAssociationRequest, GetUserAssociationDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public GetByIdUserAssociationRequestHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetUserAssociationDto> Handle(GetByIdUserAssociationRequest request, CancellationToken cancellationToken)
        {
            var entity = _context.Set<UserAssociation>().Find(request.Id);
            var dto = _mapper.Map<GetUserAssociationDto>(entity);
            return dto;
        }
    }

    public class GetByIdUserAssociationRequestValidator : AbstractValidator<GetByIdUserAssociationRequest>
    {
        public GetByIdUserAssociationRequestValidator()
        {
            
        }
    }
}
