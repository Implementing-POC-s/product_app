namespace AspCoreWebAPICRUD.DTOs.BillingDto
{
    public class BillingDto
    {
        public int BId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
    }
}
