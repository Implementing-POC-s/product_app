using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AspCoreWebAPICRUD.Models;

public partial class Billing
{
    public int ItemId { get; set; }

    public int OrdId { get; set; }

    public int PId { get; set; }

    public int Quantity { get; set; }
    
    public virtual Order Ord { get; set; } = null!;
    
    public virtual Product PIdNavigation { get; set; } = null!;
}
