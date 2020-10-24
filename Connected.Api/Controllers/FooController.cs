using System.Collections;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Connected.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FooController : ControllerBase
    {
        private readonly ConnectedContext _connectedContext;

        public FooController(ConnectedContext connectedContext)
        {
            _connectedContext = connectedContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(_connectedContext.Groups);
    }
}