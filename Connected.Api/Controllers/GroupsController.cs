using System.Threading.Tasks;
using Connected.Api.Groups;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups() 
            => Ok(await _mediator.Send(new GetGroups()));
        [HttpPost]
        public async Task<IActionResult> CreateGroup(CreateGroup createGroup) 
            => Created("", await _mediator.Send(createGroup));
        [HttpPut("{groupId:int}")]
        public async Task<IActionResult> UpdateGroup(int groupId,UpdateGroup updateGroup)
        {
            updateGroup.GroupId = groupId;
            return Ok(await _mediator.Send(updateGroup));
        }

        [HttpDelete("{groupId:int}")] public async Task<IActionResult> DeleteGroup(int groupId)
            => Ok(await _mediator.Send(new DeleteGroup(groupId)));
    }
}