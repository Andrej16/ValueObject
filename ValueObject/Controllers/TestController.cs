﻿using Api.Commands.Addresses;
using Api.Commands.TestChannelHostedService;
using Api.Common;
using Domain.Common;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ApplicationController
    {
        private readonly ISender _sender;
        private readonly ILogger<TestController> _logger;

        public TestController(ISender sender, ILogger<TestController> logger)
        {
            this._sender = sender;
            _logger = logger;
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

        [HttpPut("test-warning")]
        public async Task<IActionResult> CreateState2Async(
            string name, CancellationToken cancellation)
        {
            string[] allStates = { "AA", "BB", "CC" };
            Result<State, Error> state = State.Create(name, allStates);

            if (state.IsFailure)
            {
                return BadRequest(state.Error);
            }

            if (state.HasWarning)
            {
                return Ok(new { State = state.Value, state.Warning });
            }

            return Ok(state.Value);
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

        [HttpGet("async-enumerable")]
        public IAsyncEnumerable<int> FetchItemsAsync()
        {
            return FetchItems();
        }

        [HttpPost]
        public async Task<ActionResult<Envelope<CreateAddressResponse>>> PostAddressAsync(
            [FromQuery] CreateAddressRequest request,
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

        [HttpPost("work-items")]
        public async Task<ActionResult<Envelope<TestChannelHostedServiceResponse>>> PostWorkItems(
            CancellationToken cancellationToken)
        {
            var command = new TestChannelHostedServiceCommand("Create work items", "Send notifications");

            var responseResult = await _sender.Send(command, cancellationToken);

            return NoContent(responseResult);
        }

        [HttpGet("logging-overview")]
        public ActionResult<IEnumerable<string>> LoggingOverview()
        {
            var error = Errors.General.InternalServerError("Test Logging Overview");

            _logger.LogError(LogEventConstants.ClearAttachments, error.ToString());

            return new string[] { "value1", "value2" };
        }

        private static async IAsyncEnumerable<int> FetchItems()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);

                yield return i;
            }
        }
    }
}
