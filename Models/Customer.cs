using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AspCoreWebAPICRUD.Models;

public partial class Customer
{
    public int CustId { get; set; }

    public string CustName { get; set; } = null!;
    
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
