﻿using System.Threading.Tasks;
using Connected.Api.Comments.Commands;
using Connected.Api.Comments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Comments
{
    [ApiController]
    [Route("Groups/{groupId:int}/posts/{postId:int}/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int groupId, int postId)
        {
            var command = new GetComments {GroupId = groupId, PostId = postId};
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(int groupId, int postId, CreateComment createComment)
        {
            createComment.GroupId = groupId;
            createComment.PostId = postId;
            return Ok(await _mediator.Send(createComment));
        }

        [HttpGet("{commentId:int}")]
        public async Task<IActionResult> GetComment(int groupId, int postId, int commentId)
        {
            var command = new GetComment { GroupId = groupId, PostId = postId, CommentId = commentId };
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{commentId:int}")]
        public async Task<IActionResult> UpdateComment(int groupId, int postId, int commentId) => Ok();

        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> DeleteComment(int groupId, int postId, int commentId)
        {
            var command = new DeleteComment {GroupId = groupId, PostId = postId, CommentId = commentId};
            return Ok(await _mediator.Send(command));
        }
    }
}