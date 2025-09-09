using System.ComponentModel.DataAnnotations;

namespace AspCoreWebAPICRUD.DTOs.ProductDto

{
    public class UpdateProductDto
    {
        public int pId { get; set; }
        public string? PName { get; set; }
        public decimal Price { get; set; }
       
        public string? prod { get; set; }
    }
}
