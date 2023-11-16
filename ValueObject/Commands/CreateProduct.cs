using Domain.Aggregates;
using MediatR;
using Persistence.Context;

namespace Api.Commands
{
    public class CreateProduct
    {
        public sealed class Command : IRequest<int>
        {
            //public string Name { get; set; } = string.Empty;

            //public string Category { get; set; } = string.Empty;

            //public decimal Price { get; set; }
        }

        internal sealed class Handler : IRequestHandler<Command, int>
        {
            private readonly ApplicationDbContext _dbContext;

            public Handler(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = Product.CreateDefault();

                _dbContext.Add(product);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return product.Id;
            }
        }
    }
}
