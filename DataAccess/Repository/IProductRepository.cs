using Domain;

namespace DataAccess.Interfaces;

public interface IProductRepository
{
    List<Product> GetProduct();

    void AddProduct(Product product);
    Task<Product> GetByIdAsync(long id);

}

