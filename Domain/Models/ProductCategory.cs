using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class ProductCategory
{
    public int ProductCategoryId { get;internal set; }

    public string? Name { get; set; }
    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
