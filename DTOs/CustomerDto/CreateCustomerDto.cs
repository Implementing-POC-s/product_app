namespace AspCoreWebAPICRUD.DTOs.CustomerDto
{
    public class CreateCustomerDto
    {
        public int CustId { get; set; }
        public string CustName { get; set; } = null!;
    }
}
