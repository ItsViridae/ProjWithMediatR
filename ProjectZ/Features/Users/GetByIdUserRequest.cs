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

namespace ProjectZ.Features.Users
{
    public class GetByIdUserRequest : IRequest<GetUserDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }

    public class GetByIdUserRequestHandler : IRequestHandler<GetByIdUserRequest, GetUserDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetByIdUserRequestHandler(DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetUserDto> Handle(GetByIdUserRequest request, CancellationToken cancellationToken)
        {
            var entity = _context.Set<User>().Find(request.Id);
            var userDto = _mapper.Map<GetUserDto>(entity);
            return userDto;
        }
    }

    public class GetByIdUserRequestValidator : AbstractValidator<GetByIdUserRequest>
    {
        private readonly DataContext _context;
        public GetByIdUserRequestValidator(DataContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .Must(ExistInDatabase)
                .WithMessage(ErrorMessages.User.IdDoesNotExist);
        }

        private bool ExistInDatabase(int id)
        {
            return _context.Set<User>().Any(x => x.Id == id);
        }
    }
}
