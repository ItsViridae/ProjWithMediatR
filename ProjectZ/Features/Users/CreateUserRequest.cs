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
using Vinformatix.Security;

namespace ProjectZ.Features.Users
{
    public class CreateUserRequest : IRequest<GetUserDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, GetUserDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CreateUserRequestHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUserDto> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var passwordHash = new PasswordHash(request.Password);

            var entity = _mapper.Map<User>(request);
            entity.PasswordHash = passwordHash.Hash;
            entity.PasswordSalt = passwordHash.Salt;

            _context.Set<User>().Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetUserDto>(entity);
        }
    }

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        private readonly DataContext _context;

        public CreateUserRequestValidator(DataContext context)
        {
            _context = context;

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
                .Must(BeUniqueEmail)
                .WithMessage(ErrorMessages.User.EmailAlreadyExists);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();
        }

        private bool BeUniqueEmail(string arg)
        {
            return !_context.Set<User>().Any(x => x.Email == arg);
        }
    }
}
