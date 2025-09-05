namespace AspCoreWebAPICRUD.DTOs.BillingDto
{
    public class UpdateBillingDto
    {
        public int BId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
    }
}
