using System.ComponentModel.DataAnnotations;

namespace AspCoreWebAPICRUD.DTOs.ProductDto

{
    public class UpdateProductDto
    {
        public int pId { get; set; }
        public string PName { get; set; }
        public string Price { get; set; }
    }
}
