using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class User
{
    public int UserId { get;internal set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
