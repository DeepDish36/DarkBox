using System;
using System.Collections.Generic;

namespace DarkBox.Models;

public partial class SubscriptionPlan
{
    public int PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public decimal Price { get; set; }

    public int? MaxProjects { get; set; }

    public decimal StorageLimitMb { get; set; }

    public string SupportLevel { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
