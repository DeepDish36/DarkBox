using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DarkBox.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityHistory> ActivityHistories { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectCategory> ProjectCategories { get; set; }

    public virtual DbSet<ProjectComment> ProjectComments { get; set; }

    public virtual DbSet<ProjectFile> ProjectFiles { get; set; }

    public virtual DbSet<ProjectRequest> ProjectRequests { get; set; }

    public virtual DbSet<ProjectUpdate> ProjectUpdates { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityHistory>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__Activity__45F4A7F1477BA476");

            entity.ToTable("ActivityHistory");

            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.ActionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ActionDescription).HasColumnType("text");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ActivityH__UserI__797309D9");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C037C15BBD094");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.MessageText).HasColumnType("text");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Projec__70DDC3D8");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Receiv__6FE99F9F");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Sender__6EF57B66");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32486489E0");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.NotificationText).HasColumnType("text");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__75A278F5");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58E6DCB0B6");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Plan).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__PlanID__6B24EA82");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__UserID__6A30C649");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABED0E4158867");

            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.DeveloperId).HasColumnName("DeveloperID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending");
            entity.Property(e => e.Title).HasMaxLength(255);

            // Relacionamento Project -> Client
            entity.HasOne(d => d.Client).WithMany(p => p.ProjectClients)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Projects__Client__4AB81AF0");

            // Relacionamento Project -> Developer
            entity.HasOne(d => d.Developer).WithMany(p => p.ProjectDevelopers)
                .HasForeignKey(d => d.DeveloperId)
                .HasConstraintName("FK__Projects__Develo__4BAC3F29");

            // Relacionamento de muitos para muitos entre Project e Category
            entity.HasMany(d => d.Categories).WithMany(p => p.Projects)
                .UsingEntity<Dictionary<string, object>>(
                    "ProjectCategoryAssignment",
                    r => r.HasOne<ProjectCategory>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProjectCa__Categ__5535A963"),
                    l => l.HasOne<Project>().WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ProjectCa__Proje__5441852A"),
                    j =>
                    {
                        j.HasKey("ProjectId", "CategoryId").HasName("PK__ProjectC__D78A2D72D95D496B");
                        j.ToTable("ProjectCategoryAssignments");
                        j.IndexerProperty<int>("ProjectId").HasColumnName("ProjectID");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("CategoryID");
                    });

            // Relacionamento de exclusão em cascata entre Project e ProjectFile
            entity.HasMany(d => d.ProjectFiles)
                .WithOne(pf => pf.Project)
                .HasForeignKey(pf => pf.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);  // Adicionando a exclusão em cascata para os arquivos
        });


        modelBuilder.Entity<ProjectCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__ProjectC__19093A2B46178F85");

            entity.HasIndex(e => e.CategoryName, "UQ__ProjectC__8517B2E0592869B5").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<ProjectComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__ProjectC__C3B4DFAA011AE0EA");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CommentText).HasColumnType("text");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectComments)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectCo__Proje__5EBF139D");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectCo__UserI__5FB337D6");
        });

        modelBuilder.Entity<ProjectFile>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PK__ProjectF__6F0F989F01562FD2");

            entity.Property(e => e.FileId).HasColumnName("FileID");
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.FileSizeMb)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("FileSizeMB");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectFiles)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectFi__Proje__59063A47");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.ProjectFiles)
                .HasForeignKey(d => d.UploadedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectFi__Uploa__59FA5E80");
            entity.Property(e => e.Readme).HasMaxLength(500);
        });

        modelBuilder.Entity<ProjectRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__ProjectR__33A8519AEA532DBF");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RequestedDescription).HasColumnType("text");
            entity.Property(e => e.RequestedTitle).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending");

            entity.HasOne(d => d.Client).WithMany(p => p.ProjectRequests)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectRe__Clien__5165187F");
        });

        modelBuilder.Entity<ProjectUpdate>(entity =>
        {
            entity.HasKey(e => e.UpdateId).HasName("PK__ProjectU__7A0CF325B25B7C5C");

            entity.Property(e => e.UpdateId).HasColumnName("UpdateID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeveloperId).HasColumnName("DeveloperID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.UpdateMessage).HasColumnType("text");

            entity.HasOne(d => d.Developer).WithMany(p => p.ProjectUpdates)
                .HasForeignKey(d => d.DeveloperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectUp__Devel__6477ECF3");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectUpdates)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectUp__Proje__6383C8BA");
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Subscrip__755C22D701E5F796");

            entity.HasIndex(e => e.PlanName, "UQ__Subscrip__46E12F9E665AB745").IsUnique();

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.PlanName).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StorageLimitMb)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("StorageLimitMB");
            entity.Property(e => e.SupportLevel).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC92C9860A");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4F01319DD").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105344E82C920").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("client");
            entity.Property(e => e.StorageUsedMb)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("StorageUsedMB");
            entity.Property(e => e.SubscriptionPlanId).HasColumnName("SubscriptionPlanID");
            entity.Property(e => e.ThemePreference)
                .HasMaxLength(10)
                .HasDefaultValue("light");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.Users)
                .HasForeignKey(d => d.SubscriptionPlanId)
                .HasConstraintName("FK__Users__Subscript__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
