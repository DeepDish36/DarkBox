using System;
using System.Collections.Generic;

namespace DarkBox.Models;

public partial class ActivityHistory
{
    public int ActivityId { get; set; }

    public int UserId { get; set; }

    public string ActionDescription { get; set; } = null!;

    public DateTime? ActionDate { get; set; }

    public virtual User User { get; set; } = null!;
}
