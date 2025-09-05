namespace AspCoreWebAPICRUD.DTOs.BillingDto
{
    public class CreateBillingDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
    }
}
