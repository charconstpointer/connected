using System.Threading.Tasks;
using Connected.Api.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            return Ok(await _mediator.Send(login));
        }
    }
}