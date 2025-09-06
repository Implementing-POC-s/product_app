using System.ComponentModel.DataAnnotations;

namespace AspCoreWebAPICRUD.DTOs.ProductDto
{
    public class GetProductDto
    {
        public int pId { get; set; }
        public string PName { get; set; }
        public decimal Price { get; set; }

    }
}
