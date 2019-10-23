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
    public class EditUserRequest : IRequest<GetUserDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class EditUserRequestHandler : IRequestHandler<EditUserRequest, GetUserDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EditUserRequestHandler(DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUserDto> Handle(
            EditUserRequest request,
            CancellationToken cancellationToken)
        {
            var userToEdit = _context.Set<User>().Find(request.Id);

            _mapper.Map(request, userToEdit);

            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<GetUserDto>(userToEdit);
        }
    }

    public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
    {
        private readonly DataContext _context;
        public EditUserRequestValidator(DataContext context)
        {
            _context = context;

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
                .Must(BeUniqueEmail)
                .WithMessage(ErrorMessages.User.EmailAlreadyExists);

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
