namespace Api.Commands.Addresses
{
    public record CreateAddressRequest(string Street, string City, string State, string ZipCode);
}
