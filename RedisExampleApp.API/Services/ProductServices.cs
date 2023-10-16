using RedisExampleApp.API.Models;
using RedisExampleApp.API.Repository;

namespace RedisExampleApp.API.Services
{
    public class ProductServices : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<Product> CreateAsync(Product product)
        {
           return _productRepository.CreateAsync(product);
        }

        public Task<List<Product>> GetAllAsync()
        {
            return _productRepository.GetAllAsync();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return _productRepository.GetByIdAsync(id);
        }
    }
}
