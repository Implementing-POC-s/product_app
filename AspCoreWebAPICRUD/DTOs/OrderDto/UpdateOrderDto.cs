namespace AspCoreWebAPICRUD.DTOs.OrderDto
{
    public class UpdateOrderDto
    {
        public int OrdId { get; set; }
        public DateOnly OrdDate { get; set; }
        public int CustId { get; set; }
    }
}
