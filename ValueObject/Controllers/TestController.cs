using Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> CreateStateAsync(string name, CancellationToken cancellation)
        {
            string[] allStates = { "AA", "BB", "CC" };
            Domain.Common.Result<State, Domain.Common.Error> state = State.Create(name, allStates);

            if (state.IsFailure)
            {
                return BadRequest(state.Error);
            }

            return Ok(state.Value);
        }
    }
}
