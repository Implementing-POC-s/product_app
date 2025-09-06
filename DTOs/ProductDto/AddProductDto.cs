using System.ComponentModel.DataAnnotations;

namespace AspCoreWebAPICRUD.DTOs.ProductDto

{
    public class AddProductDto
    {
        public string PName { get; set; }
        public decimal Price { get; set; }
        public int PId { get; set; }
    }
}
