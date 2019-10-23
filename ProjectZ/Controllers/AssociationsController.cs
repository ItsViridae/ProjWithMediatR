using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectZ.Data;
using ProjectZ.Dtos;
using ProjectZ.Features.Associations;
using Vinformatix.Api.Responses;
using Vinformatix.Infrastructure.Mediatr;

namespace ProjectZ.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssociationsController : MediatorControllerBase
    {
        private readonly IMediatorService _mediatorService;
        public AssociationsController(IMediatorService mediatorService)
        {
            _mediatorService = mediatorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<List<GetAssociationDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllAssociationRequest();
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<GetAssociationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var request = new GetByIdAssociationRequest {Id = id};
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<GetAssociationDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateAssociationRequest request)
        {
            var result = await _mediatorService.Send(request);
            return Created("api/Associations/Id-Here", result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Response<GetAssociationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, EditAssociationRequest request)
        {
            request.Id = id;
            var result = await _mediatorService.Send(request);
            return Created("api/Associations/Id-Here", result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<GetAssociationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id)
        {
            var request = new DeleteAssociationRequest {Id = id};
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }
    }
}