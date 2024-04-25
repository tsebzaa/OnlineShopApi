using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Inventory
{
    public int InventoryId { get;internal set; }

    public int? Amount { get; set; }
    [JsonIgnore]
    public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
}
