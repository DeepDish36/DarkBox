using System;
using System.Collections.Generic;

namespace DarkBox.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public int ClientId { get; set; }

    public int? DeveloperId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Client { get; set; } = null!;

    public virtual User? Developer { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<ProjectComment> ProjectComments { get; set; } = new List<ProjectComment>();

    public virtual ICollection<ProjectFile> ProjectFiles { get; set; } = new List<ProjectFile>();

    public virtual ICollection<ProjectUpdate> ProjectUpdates { get; set; } = new List<ProjectUpdate>();

    public virtual ICollection<ProjectCategory> Categories { get; set; } = new List<ProjectCategory>();
}
