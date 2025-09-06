using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AspCoreWebAPICRUD.Models;

public partial class Order
{
    public int OrdId { get; set; }

    public DateOnly OrdDate { get; set; }

    public int CustId { get; set; }
    
    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();
    
    public virtual Customer Cust { get; set; } = null!;
}
