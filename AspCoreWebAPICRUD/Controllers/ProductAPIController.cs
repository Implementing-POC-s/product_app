using AspCoreWebAPICRUD.DTOs;
using AspCoreWebAPICRUD.DTOs.ProductDto;
using AspCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AspCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly ProjectDbContext context;

        public ProductAPIController(ProjectDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        {
            var dto = await context.Products
                .Select(p=> new ProductDTO
                {
                    Id=p.PId,
                    Name=p.PName,
                    Price=p.Price
                })
                .ToListAsync();
            return Ok(dto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AddProductDto>> GetProductById(int id)
        {
            var data1 = await context.Products.FindAsync(id);
            if (data1 == null)
            {
                return NotFound();
            }
            var dto= await context.Products
                .Where(p => p.PId == id)
                .Select(p => new GetProductDto
                {
                    pId = p.PId,
                    PName = p.PName,
                    Price = p.Price
                })
                .FirstOrDefaultAsync();


            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(AddProductDto prod)
        {
           
            var entity = new Product
            {

                PName = prod.PName,
                Price = prod.Price
            };
            await context.Products.AddAsync(entity);
            await context.SaveChangesAsync();
            var dto = new AddProductDto
            {
                PId = entity.PId,
                PName = entity.PName,
                Price = entity.Price
            };
            return Ok(dto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product prod)
        {
            var entity = await context.Products.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.PName = prod.PName;
            entity.Price = prod.Price;

            await context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var prod = await context.Products.FindAsync(id);
            if (prod == null)
            {
                return NotFound();
            }
            context.Products.Remove(prod);
            await context.SaveChangesAsync();
            var dto = new Product
            {
                PId= prod.PId,
                PName= prod.PName,
            };
            return Ok(dto);
        }

    }
}