using DataAccess.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApiCatalog.DTO;


namespace WebApiCatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogApiController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public CatalogApiController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _productRepository.GetProduct();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(long id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(new { message = $"Продукт с ID {id} не найден" });
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        if (string.IsNullOrEmpty(productDto.Name))
        {
            return BadRequest(new { message = "Название продукта обязательно" });
        }

        if (productDto.Price <= 0)
        {
            return BadRequest(new { message = "Цена должна быть больше 0" });
        }

        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price
        };

        _productRepository.AddProduct(product);
        await _productRepository.SaveChangesAsync();

        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(long id, [FromBody] UpdateProductDto productDto)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(new { message = $"Продукт с ID {id} не найден" });
        }

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;

        _productRepository.UpdateProduct(product);
        await _productRepository.SaveChangesAsync();

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(long id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(new { message = $"Продукт с ID {id} не найден" });
        }

        _productRepository.DeleteProduct(product);
        await _productRepository.SaveChangesAsync();

        return Ok(new { message = "Продукт успешно удален" });
    }

    [HttpGet("search")]
    public IActionResult SearchProducts([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest(new { message = "Введите название для поиска" });
        }

        var products = _productRepository.GetProduct()
            .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(products);
    }
}