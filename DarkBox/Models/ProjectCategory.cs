using System;
using System.Collections.Generic;

namespace DarkBox.Models;

public partial class ProjectCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
