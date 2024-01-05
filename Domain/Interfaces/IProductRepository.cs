using Athena.Core.Entities;

namespace Athena.Core.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    void UpdateProduct(Product product);
}
