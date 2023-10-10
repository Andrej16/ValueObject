using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Common
{
    public sealed class EnvelopeResult<T> : ActionResult where T : class
    {
        private readonly Envelope<T> _envelope;
        private readonly int _statusCode;

        public EnvelopeResult(Envelope<T> envelope, HttpStatusCode statusCode)
        {
            _statusCode = (int)statusCode;
            _envelope = envelope;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_envelope)
            {
                StatusCode = _statusCode
            };

            return objectResult.ExecuteResultAsync(context);
        }
    }
}
