using AspCoreWebAPICRUD.DTOs;
using AspCoreWebAPICRUD.DTOs.ProductDto;
using AspCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspCoreWebAPICRUD.Repository;



namespace AspCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductAPIController(IProductRepository repo)
        {
            _repo=repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();

                var dto=products.Select(p=> new ProductDTO
                {
                    Id=p.PId,
                    Name=p.PName,
                    Price=p.Price
                }).ToList();
 
            return Ok(dto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AddProductDto>> GetProductById(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            if (product== null)
            {
                return NotFound();
            }
            var dto = new GetProductDto

            {
                pId = product.PId,
                PName = product.PName,
                Price = product.Price
            };
                
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
            await _repo.AddProductAsync(entity);
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
            var entity = await _repo.UpdateProductAsync(prod);
            if (entity == null)
            {
                return NotFound();
            }

            entity.PName = prod.PName;
            entity.Price = prod.Price;


            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var prod = await _repo.DeleteProductAsync(id);
            if (prod == null)
            {
                return NotFound();
            }
           
            var dto = new Product
            {
                PId= prod.PId,
                PName= prod.PName,
            };
            return Ok(dto);
        }

    }
}