using Infrastructure.Data;

namespace Athena.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
    }
}
