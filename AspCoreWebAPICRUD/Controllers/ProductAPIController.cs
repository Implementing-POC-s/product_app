using AspCoreWebAPICRUD.DTOs;
using AspCoreWebAPICRUD.DTOs.ProductDto;
using AspCoreWebAPICRUD.Models;
using AspCoreWebAPICRUD.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductAPIController(IProductRepository repo)
        {
            _repo = repo;
        }

        // GET: api/ProductAPI
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();
            var dtoList = products.Select(p => new ProductDTO
            {
                Id = p.PId,
                Name = p.PName,
                Price = p.Price
            }).ToList();

            return Ok(dtoList);
        }

        // GET: api/ProductAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetProductById(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            var dto = new GetProductDto
            {
                pId = product.PId,
                PName = product.PName,
                Price = product.Price
            };

            return Ok(dto);
        }

        // POST: api/ProductAPI
        [HttpPost]
        public async Task<ActionResult<GetProductDto>> CreateProduct([FromBody] AddProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new Product
            {
                PName = dto.PName,
                Price = dto.Price
            };

            await _repo.AddProductAsync(entity);

            var resultDto = new GetProductDto
            {
                pId = entity.PId,
                PName = entity.PName,
                Price = entity.Price
            };

            return CreatedAtAction(nameof(GetProductById), new { id = resultDto.pId }, resultDto);
        }

        // PUT: api/ProductAPI/5
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto dto)
        {
            if (dto == null)
                return BadRequest("Product data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _repo.GetProductByIdAsync(dto.pId);
            if (entity == null)
                return NotFound($"No product found with ID = {dto.pId}");

            entity.PName = dto.PName;
            entity.Price = dto.Price;

            await _repo.UpdateProductAsync(entity);

            return Ok();
        }

        // DELETE: api/ProductAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _repo.DeleteProductAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}
