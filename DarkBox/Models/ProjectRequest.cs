using System;
using System.Collections.Generic;

namespace DarkBox.Models;

public partial class ProjectRequest
{
    public int RequestId { get; set; }

    public int ClientId { get; set; }

    public string RequestedTitle { get; set; } = null!;

    public string RequestedDescription { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Client { get; set; } = null!;
}
