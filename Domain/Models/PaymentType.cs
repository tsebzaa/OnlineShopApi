using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class PaymentType
{
    public int PaymentId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
