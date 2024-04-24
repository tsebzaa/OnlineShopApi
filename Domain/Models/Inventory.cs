using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int? Amount { get; set; }

    public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
}
