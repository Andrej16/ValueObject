using Api.Commands.Addresses;
using Api.Common;
using Domain.Aggregates;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ApplicationController
    {
        private readonly ISender _sender;

        public TestController(ISender sender)
        {
            this._sender = sender;
        }

        [HttpPut]
        public async Task<ActionResult<Envelope<State>>> CreateStateAsync(
            string name, CancellationToken cancellation)
        {
            string[] allStates = { "AA", "BB", "CC" };
            Result<State, Error> state = State.Create(name, allStates);

            if (state.IsFailure)
            {
                return BadRequest(state.Error);
            }

            return Ok2(state.Value);
        }

        [HttpGet]
        public async Task<ActionResult<Envelope<State>>> GetErrorAsync(
            string name, CancellationToken cancellation)
        {
            Error error = Errors.General.InternalServerError($"Testing return errors: {name}");

            return Error<State>(error);
        }

        [HttpGet("from-result")]
        public async Task<ActionResult<Envelope<State>>> GetFromResultAsync(
            string name, CancellationToken cancellation)
        {
            string[] allStates = { "AA", "BB", "CC" };
            Result<State, Error> state = State.Create(name, allStates);

            if (state.IsFailure)
            {
                Error error = Errors.General.InternalServerError($"Testing return errors: {name}");

                //return FromResult<State>(error);

                Result<State, Error> errorResult = Result.Failure<State, Error>(error);
                return FromResult(errorResult);
            }

            Result<State, Error> okjResult = Result.Success<State, Error>(state.Value);
            return FromResult(okjResult);
        }

        [HttpPost]
        public async Task<ActionResult<Envelope<CreateAddressResponse>>> CreateAddressAsync(
            CreateAddressRequest request,
            CancellationToken cancellation)
        {
            var command = new CreateAddressCommand(
                request.Street,
                request.City,
                request.State,
                request.ZipCode);

            var responseResult = await _sender.Send(command, cancellation);

            return FromResult(responseResult);
        }
    }
}
