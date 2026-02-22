
using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class ProductRepository: IProductRepository
{
    private readonly WebApplicationDBContext _dbContext;

    public ProductRepository(WebApplicationDBContext dBContext)
    {
        _dbContext = dBContext;
    }
    public void AddProduct(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }

    public List<Product> GetProduct()
    {
        return _dbContext.Products.ToList();
    }
    public async Task<Product> GetByIdAsync(long id)
    {
        return await _dbContext.Products.FindAsync(id);
    }
}
