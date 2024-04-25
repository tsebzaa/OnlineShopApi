using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Order
{
    public int OrderId { get;internal set; }

    public int? PaymentId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? OrderDate { get; set; }
    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    [JsonIgnore]

    public virtual PaymentType? Payment { get; set; }
    [JsonIgnore]

    public virtual User? User { get; set; }
}
