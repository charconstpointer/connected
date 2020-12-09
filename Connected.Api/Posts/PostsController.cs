using System.Threading.Tasks;
using Connected.Api.Posts.Commands;
using Connected.Api.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Posts
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
        public async Task<IActionResult> GetPosts(int groupId)
        {
            var command = new GetPosts {GroupId = groupId};
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{postId:int}")]
        public async Task<IActionResult> GetPost(int groupId, int postId)
        {
            var command = new GetPost {GroupId = groupId, PostId = postId};
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(int groupId, CreatePost createPost)
        {
            createPost.GroupId = groupId;
            return Ok(await _mediator.Send(createPost));
        }

        [HttpPut("{postId:int}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(int groupId, int postId, UpdatePost updatePost)
        {
            updatePost.GroupId = groupId;
            updatePost.PostId = postId;
            return Ok(await _mediator.Send(updatePost));
        }

        [HttpDelete("{postId:int}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int groupId, int postId)
        {
            var command = new DeletePost {GroupId = groupId, PostId = postId};
            return Ok(await _mediator.Send(command));
        }
    }
}