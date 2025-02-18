using System;
using System.Collections.Generic;

namespace DarkBox.Models;

public partial class ProjectComment
{
    public int CommentId { get; set; }

    public int ProjectId { get; set; }

    public int UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public int? Rating { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
