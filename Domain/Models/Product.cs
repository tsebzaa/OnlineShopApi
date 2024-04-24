using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Product
{
    
    public int ProductId { get;internal set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public int? InventoryId { get; set; }

    public string? Description { get; set; }

    public int? ProductCategoryId { get; set; }
    [JsonIgnore]
    public virtual Inventory? Inventory { get; set; }
    [JsonIgnore]

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    [JsonIgnore]
    public virtual ProductCategory? ProductCategory { get; set; }
}
