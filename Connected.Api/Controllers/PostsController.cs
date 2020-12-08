using System.Threading.Tasks;
using Connected.Api.Posts.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Controllers
{
    [ApiController]
    [Route("Groups/{groupId:int}/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts(int groupId) => Ok(groupId);
        [HttpGet("{postId:int}")]
        public async Task<IActionResult> GetPost(int groupId, int postId) => Ok();
        
        [HttpPost]
        public async Task<IActionResult> CreatePost(int groupId, CreatePost createPost)
        {
            createPost.GroupId = groupId;
            return Ok(await _mediator.Send(createPost));
        }
        [HttpPut("{postId:int}")]
        public async Task<IActionResult> UpdatePost() => Ok();
        [HttpDelete("{postId:int}")]
        public async Task<IActionResult> DeletePost() => Ok();
    }
}