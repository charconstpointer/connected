using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetGroups() => Ok();
        [HttpPost]
        public async Task<IActionResult> CreateGroup() => Ok();
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGroup() => Ok();

        [HttpDelete("{id:int}")] public async Task<IActionResult> DeleteGroup() => Ok();
    }
}