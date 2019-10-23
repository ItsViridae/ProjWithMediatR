using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectZ.Data;
using ProjectZ.Data.Entities;
using ProjectZ.Dtos;
using ProjectZ.Features.UserAssociations;
using ProjectZ.Features.Users;
using Vinformatix.Api.Responses;
using Vinformatix.Infrastructure.Mediatr;

namespace ProjectZ.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class UsersController : MediatorControllerBase
    {
        private readonly IMediatorService _mediatorService;
        private readonly DataContext _context;

        public UsersController(
            IMediatorService mediatorService,
            DataContext context)
        {
            _mediatorService = mediatorService;
            _context = context;
        }

        // POST: api/Image
        [AllowAnonymous]
        [HttpPost("image")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            //string extension = Path.GetExtension(file.FileName);

            var uploads = Path.Combine(@"C:\Uploads");
            if (file.Length > 0)
            {
                // Saving it to a file structure
                using (var fileStream = new FileStream(Path.Combine(uploads, $"{file.FileName}"), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Saving it to a database
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();

                    var image = new Image
                    {
                        FileName = file.FileName,
                        ImageBytes = fileBytes
                    };

                    await _context.AddAsync(image);
                    await _context.SaveChangesAsync();

                    return Created(@"api/GetFile/", image.Id);
                }
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var image = await _context.Set<Image>().FindAsync(id);

            var ms = new MemoryStream(image.ImageBytes);
            var response = File(ms, "application/octet-stream", image.FileName);

            return response;
        }


        [HttpGet("[controller]")]
        [ProducesResponseType(typeof(Response<List<GetUserDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllUserRequest();
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }


        [HttpGet("[controller]/{id}")]
        [ProducesResponseType(typeof(Response<GetUserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var request = new GetByIdUserRequest {Id = id};
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }


        [AllowAnonymous]
        [HttpPost("[controller]/authenticate")]
        [ProducesResponseType(typeof(Response<GetUserAuthenticationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Authenticate(AuthenticateUserRequest request)
        {
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("[controller]")]
        [ProducesResponseType(typeof(Response<GetUserDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var result = await _mediatorService.Send(request);
            return Created("User/id", result);
        }

        [HttpPut("[controller]/{id}")] 
        [ProducesResponseType(typeof(Response<GetUserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, EditUserRequest request)
        {
            request.Id = id;
            var result = await _mediatorService.Send(request);
            return Created("User/id", result);
        }

        [HttpDelete("[controller]/{id}")]
        [ProducesResponseType(typeof(Response<EmptyResponse>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteUserRequest {Id = id};
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }
    }
}
