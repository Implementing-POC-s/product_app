using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AspCoreWebAPICRUD.Models;

public partial class Product
{
    public int PId { get; set; }

    public string? PName { get; set; } 

    public decimal Price { get; set; }
    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();
}
