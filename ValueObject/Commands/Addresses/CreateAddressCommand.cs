using Api.Abstractions;
using Domain.Aggregates;
using Domain.Common;

namespace Api.Commands.Addresses
{
    public record CreateAddressCommand(
        string Street,
        string City,
        string State,
        string ZipCode) : ICommand<Result<CreateAddressResponse, Error>>;

    public class CreateAddressCommandHandler
        : ICommandHandler<CreateAddressCommand, Result<CreateAddressResponse, Error>>
    {
        public async Task<Result<CreateAddressResponse, Error>> Handle(
            CreateAddressCommand command,
            CancellationToken cancellation)
        {
            string[] allStates = { "AA", "BB", "CC" };
            Result<Address, Error> addressResult = Address.Create(
                command.Street,
                command.City,
                command.State,
                command.ZipCode,
                allStates);

            if (addressResult.IsFailure)
            {
                return addressResult.Error;
            }

            return new CreateAddressResponse(addressResult.Value.Id, addressResult.Value.ToString());
        }
    }
}
