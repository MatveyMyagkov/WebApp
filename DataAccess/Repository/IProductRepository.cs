using Domain;

namespace DataAccess.Interfaces;

public interface IProductRepository
{
    List<Product> GetProduct();

    void AddProduct(Product product);
    Task<Product> GetByIdAsync(long id);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    Task SaveChangesAsync();

}

