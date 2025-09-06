namespace AspCoreWebAPICRUD.DTOs.OrderDto
{
    public class CreateOrderDto
    {
        public DateOnly OrdDate { get; set; }
        public int CustId { get; set; }
    }
}
