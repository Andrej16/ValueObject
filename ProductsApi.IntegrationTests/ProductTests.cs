using Api.Commands;
using ProductsApi.IntegrationTests.Core;

namespace ProductsApi.IntegrationTests
{
    public class ProductTests : BaseIntegrationTest
    {
        public ProductTests(IntegrationTestWebAppFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Create_ShouldCreateProduct()
        {
            // Arrange
            var command = new CreateProduct.Command();

            // Act
            var productId = await Sender.Send(command);

            // Assert
            var product = DbContext
                .Products
                .FirstOrDefault(p => p.Id == productId);

            Assert.NotNull(product);
        }
    }
}