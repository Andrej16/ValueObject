using Domain.Common;

namespace Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public string? Info { get; set; }

        public static Product CreateDefault()
        {
            var product = new Product()
            {
                Name = "Default Product",
                Category = "Default Category",
                Price = 0.00m,
                CreatedOnUtc = DateTime.UtcNow
            };

            return product;
        }
    }
}
