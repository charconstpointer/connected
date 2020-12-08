using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Controllers
{
    [ApiController]
    [Route("Groups/{groupId:int}/posts/{postId:int}/[controller]")]
    public class CommentsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetComments(int groupId, int postId) => Ok();

        [HttpPost]
        public async Task<IActionResult> CreateComment(int groupId, int postId) => Ok();

        [HttpGet("{commentId:int}")]
        public async Task<IActionResult> GetComment(int groupId, int postId, int commentId) => Ok();

        [HttpPut("{commentId:int}")]
        public async Task<IActionResult> UpdateComment(int groupId, int postId, int commentId) => Ok();

        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> DeleteComment(int groupId, int postId, int commentId) => Ok();
    }
}