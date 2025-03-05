using System;
using System.Collections.Generic;

namespace DarkBox.Models;

public partial class ProjectFile
{
    public int FileId { get; set; }

    public int ProjectId { get; set; }

    public int UploadedBy { get; set; }

    public string FileName { get; set; } = null!;

    public decimal FileSizeMb { get; set; }

    public string FilePath { get; set; } = null!;

    public string? Readme { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual User UploadedByNavigation { get; set; } = null!;
}
