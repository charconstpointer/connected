using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Controllers
{
    [ApiController]
    [Route("Groups/{groupId:int}/[controller]")]
    public class PostsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetPosts(int groupId) => Ok(groupId);
        [HttpGet("{postId:int}")]
        public async Task<IActionResult> GetPost() => Ok();
        [HttpPost("{postId:int}")]
        public async Task<IActionResult> CreatePost() => Ok();
        [HttpPut("{postId:int}")]
        public async Task<IActionResult> UpdatePost() => Ok();
        [HttpDelete("{postId:int}")]
        public async Task<IActionResult> DeletePost() => Ok();
    }
}