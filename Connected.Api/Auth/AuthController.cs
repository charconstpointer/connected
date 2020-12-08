using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login() => Ok();
    }
}