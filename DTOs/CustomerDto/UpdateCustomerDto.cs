namespace AspCoreWebAPICRUD.DTOs.CustomerDto
{
    public class UpdateCustomerDto
    {
        public int CustId { get; set; }  
        public string CustName { get; set; } = null!;
    }
}
