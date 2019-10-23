using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectZ.Dtos;
using ProjectZ.Features.UserAssociations;
using Vinformatix.Api.Responses;
using Vinformatix.Infrastructure.Mediatr;

namespace ProjectZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAssociationsController : MediatorControllerBase
    {
        private readonly IMediatorService _mediatorService;
        public UserAssociationsController(IMediatorService mediatorService)
        {
            _mediatorService = mediatorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<List<GetUserAssociationDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllUserAssociationRequest();
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<List<GetAssociationDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var request = new GetByIdUserAssociationRequest{ Id = id };
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }

        [HttpGet("User/{id}")]
        [ProducesResponseType(typeof(Response<List<GetAssociationDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAssociations(int id)
        {
            var request = new GetAllAssociationsForUserRequest { UserId = id };
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }

        [HttpGet("Association/{id}")]
        [ProducesResponseType(typeof(Response<List<GetUserDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllUsers(int id)
        {
            var request = new GetAllUsersForAssociationRequest{ AssociationId = id};
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<GetUserAssociationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(int userId, int associationId)
        {
            var request = new CreateUserAssociationRequest
            {
                UserId = userId,
                AssociationId = associationId
            };
            var result = await _mediatorService.Send(request);
            return Created("UserAssociation/Id-here", result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<EmptyResponse>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteUserAssociationRequest{ Id = id };
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }
    }
}