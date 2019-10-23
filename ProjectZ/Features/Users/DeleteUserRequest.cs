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
using Vinformatix.Api.Responses;

namespace ProjectZ.Features.Users
{
    public class DeleteUserRequest : IRequest<EmptyResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, EmptyResponse>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeleteUserRequestHandler(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmptyResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var entity = _context.Set<User>().Find(request.Id);
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new EmptyResponse();
        }
    }

    public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
    {
        private readonly DataContext _context;
        public DeleteUserRequestValidator(DataContext context)
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
