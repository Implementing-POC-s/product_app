using AspCoreWebAPICRUD.Models;
using Microsoft.EntityFrameworkCore;
namespace AspCoreWebAPICRUD.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly ProjectDbContext _context;
        public ProductRepository(ProjectDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var existing = await _context.Products.FindAsync(product.PId);
            if (existing == null)
            {
                return null;
            }
            existing.PName = product.PName;
            existing.Price = product.Price;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

    }
}

