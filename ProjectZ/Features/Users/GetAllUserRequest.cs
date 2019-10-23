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
using Vinformatix.Authentication;

namespace ProjectZ.Features.Users
{
    public class GetAllUserRequest : IRequest<List<GetUserDto>>
    {
    }

    public class GetAllUserRequestHandler : IRequestHandler<GetAllUserRequest, List<GetUserDto>>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public GetAllUserRequestHandler(
            DataContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<GetUserDto>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            return  await _context
                .Set<User>()
                .ProjectTo<GetUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }

    public class GetAllUserRequestValidator : AbstractValidator<GetAllUserRequest>
    {
        public GetAllUserRequestValidator()
        {
            
        }
    }
}
