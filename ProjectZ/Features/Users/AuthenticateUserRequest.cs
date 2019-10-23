using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectZ.Common.Responses;
using ProjectZ.Data;
using ProjectZ.Data.Entities;
using ProjectZ.Dtos;
using ProjectZ.Features.Users;
using Vinformatix.Authentication;
using Vinformatix.Security;

namespace ProjectZ.Features.Users
{
    public class AuthenticateUserRequest : IRequest<GetUserAuthenticationDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserRequestHandler : IRequestHandler<AuthenticateUserRequest, GetUserAuthenticationDto>
    {
        private readonly DataContext _context;
        private readonly AppSettings _appSettings;

        public AuthenticateUserRequestHandler(
            DataContext context,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<GetUserAuthenticationDto> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _context.Set<User>().FirstOrDefault(x => x.Email == request.Email);
            
            await _context.SaveChangesAsync(cancellationToken);

            var userToReturn = new GetUserAuthenticationDto
            {
                Id = user.Id,
                Token = Authentication.GenerateJwtToken(_appSettings, user.Id.ToString(), DateTime.UtcNow.AddDays(7))
            };

            return userToReturn;
        }
    }

    public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
    {
        private readonly DataContext _context;
        private bool _emailPassed = true;
        private bool _passwordPassed = true;

        public AuthenticateUserRequestValidator(
            DataContext context)
        {
            _context = context;

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .OnFailure(x => _emailPassed = false);

            RuleFor(x => x.Password)
                .NotEmpty()
                .OnFailure(x => _passwordPassed = false);

            RuleFor(x => x)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(ExistInDatabase)
                .When(x => _emailPassed && _passwordPassed)
                .WithMessage(ErrorMessages.User.EmailOrPasswordIsIncorrect)
                .Must(BeCorrectEmailAndPassword)
                .When(x => _emailPassed && _passwordPassed)
                .WithMessage(ErrorMessages.User.EmailOrPasswordIsIncorrect);
        }

        private bool ExistInDatabase(AuthenticateUserRequest arg)
        {
            return _context.Set<User>().Any(x => x.Email == arg.Email);
        }

        private bool BeCorrectEmailAndPassword(AuthenticateUserRequest arg)
        {
            var user = _context.Set<User>().Single(x => x.Email == arg.Email);
            var passwordHash = new PasswordHash(user.PasswordSalt, user.PasswordHash);
            return passwordHash.Verify(arg.Password);
        }
    }
}
