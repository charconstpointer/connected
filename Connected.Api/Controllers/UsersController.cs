using System.Threading.Tasks;
using Connected.Api.Persistence;
using Connected.Api.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _mediator.Send(new GetUsers()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUser createUser)
        {
            return Created("", await _mediator.Send(createUser));
        }

        [HttpGet("userId:int")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var command = new GetUser {UserId = userId};
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("userId:int")]
        public async Task<IActionResult> UpdateUser(int userId, UpdateUser updateUser)
        {
            updateUser.UserId = userId;
            return Ok(await _mediator.Send(updateUser));
        }

        [HttpDelete("userId:int")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var command = new DeleteUser {UserId = userId};
            return Ok(await _mediator.Send(command));
        }
    }
}