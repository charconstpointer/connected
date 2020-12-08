using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers() => Ok();
        [HttpPost]
        public async Task<IActionResult> CreateUser() => Ok();
        [HttpGet("userId:int")]
        public async Task<IActionResult> GetUser() => Ok();
        [HttpPut("userId:int")]
        public async Task<IActionResult> UpdateUser() => Ok();
        [HttpDelete("userId:int")]
        public async Task<IActionResult> DeleteUser() => Ok();
    }
}