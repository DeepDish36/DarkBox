using System;
using System.Collections.Generic;

namespace DarkBox.Models;

public partial class ProjectUpdate
{
    public int UpdateId { get; set; }

    public int ProjectId { get; set; }

    public int DeveloperId { get; set; }

    public string UpdateMessage { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual User Developer { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
