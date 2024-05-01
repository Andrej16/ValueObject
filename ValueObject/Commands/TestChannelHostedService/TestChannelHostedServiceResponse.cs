using Api.Abstractions;

namespace Api.Commands.TestChannelHostedService;

public record TestChannelHostedServiceResponse(
    int StatusCode = StatusCodes.Status204NoContent) : INoContent;
