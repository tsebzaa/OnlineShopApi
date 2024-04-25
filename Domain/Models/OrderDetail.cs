using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get;internal set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public int? OrderId { get; set; }
    [JsonIgnore]

    public virtual Order? Order { get; set; }
    [JsonIgnore]

    public virtual Product? Product { get; set; }
}
