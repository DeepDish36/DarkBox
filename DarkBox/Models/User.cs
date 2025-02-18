using System;
using System.Collections.Generic;

namespace DarkBox.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Role { get; set; }

    public string? ThemePreference { get; set; }

    public int? SubscriptionPlanId { get; set; }

    public decimal? StorageUsedMb { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<ActivityHistory> ActivityHistories { get; set; } = new List<ActivityHistory>();

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Project> ProjectClients { get; set; } = new List<Project>();

    public virtual ICollection<ProjectComment> ProjectComments { get; set; } = new List<ProjectComment>();

    public virtual ICollection<Project> ProjectDevelopers { get; set; } = new List<Project>();

    public virtual ICollection<ProjectFile> ProjectFiles { get; set; } = new List<ProjectFile>();

    public virtual ICollection<ProjectRequest> ProjectRequests { get; set; } = new List<ProjectRequest>();

    public virtual ICollection<ProjectUpdate> ProjectUpdates { get; set; } = new List<ProjectUpdate>();

    public virtual SubscriptionPlan? SubscriptionPlan { get; set; }
}
