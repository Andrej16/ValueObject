using Api.Abstractions;
using Api.Common;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        protected ActionResult<Envelope<T>> Ok2<T>(T? result = null) where T : class
        {
            return new EnvelopeResult<T>(Envelope<T>.Ok(result), HttpStatusCode.OK);
        }

        protected ActionResult<Envelope<T>> NotFound<T>(Error error) where T : class
        {
            return new EnvelopeResult<T>(Envelope<T>.Error(error), HttpStatusCode.NotFound);
        }

        protected ActionResult<Envelope<T>> Error<T>(Error error) where T : class
        {
            return new EnvelopeResult<T>(Envelope<T>.Error(error), HttpStatusCode.BadRequest);
        }

        protected ActionResult<Envelope<T>> NoContent<T>(Result<T, Error> result) 
            where T : class, INoContentViewModel
        {
            if (result.IsFailure)
                return Error<T>(result.Error);

            return StatusCode(result.Value.StatusCode);
        }

        protected ActionResult<Envelope<T>> FromResult<T>(Result<T, Error> result) 
            where T : class
        {
            if (result.IsSuccess)
                return Ok2(result.Value);

            return Error<T>(result.Error);
        }
    }
}
