﻿// <auto-generated />
using System;
using DarkBox.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DarkBox.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250318142017_AddProjectRequestFK")]
    partial class AddProjectRequestFK
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DarkBox.Models.ActivityHistory", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ActivityID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityId"));

                    b.Property<DateTime?>("ActionDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ActionDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("ActivityId")
                        .HasName("PK__Activity__45F4A7F1477BA476");

                    b.HasIndex("UserId");

                    b.ToTable("ActivityHistory", (string)null);
                });

            modelBuilder.Entity("DarkBox.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MessageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectID");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int")
                        .HasColumnName("ReceiverID");

                    b.Property<int>("SenderId")
                        .HasColumnType("int")
                        .HasColumnName("SenderID");

                    b.Property<DateTime?>("SentAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("MessageId")
                        .HasName("PK__Messages__C87C037C15BBD094");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DarkBox.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("NotificationID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool?>("IsRead")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("NotificationText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("NotificationId")
                        .HasName("PK__Notifica__20CF2E32486489E0");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("DarkBox.Models.PasswordResets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UserID")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("Id")
                        .HasName("PK__Password__D3A3E3F1");

                    b.HasIndex("UserID");

                    b.ToTable("PasswordResets", (string)null);
                });

            modelBuilder.Entity("DarkBox.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateTime?>("PaymentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("PlanId")
                        .HasColumnType("int")
                        .HasColumnName("PlanID");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValue("pending");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("PaymentId")
                        .HasName("PK__Payments__9B556A58E6DCB0B6");

                    b.HasIndex("PlanId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("DarkBox.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProjectID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("ClientID");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("DeveloperId")
                        .HasColumnType("int")
                        .HasColumnName("DeveloperID");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValue("pending");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ProjectId")
                        .HasName("PK__Projects__761ABED0E4158867");

                    b.HasIndex("ClientId");

                    b.HasIndex("DeveloperId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("DarkBox.Models.ProjectCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId")
                        .HasName("PK__ProjectC__19093A2B46178F85");

                    b.HasIndex(new[] { "CategoryName" }, "UQ__ProjectC__8517B2E0592869B5")
                        .IsUnique();

                    b.ToTable("ProjectCategories");
                });

            modelBuilder.Entity("DarkBox.Models.ProjectComment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CommentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectID");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("CommentId")
                        .HasName("PK__ProjectC__C3B4DFAA011AE0EA");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectComments");
                });

            modelBuilder.Entity("DarkBox.Models.ProjectFile", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FileID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileId"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("FileSizeMb")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("FileSizeMB");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectID");

                    b.Property<string>("Readme")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("UploadedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("UploadedBy")
                        .HasColumnType("int");

                    b.HasKey("FileId")
                        .HasName("PK__ProjectF__6F0F989F01562FD2");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UploadedBy");

                    b.ToTable("ProjectFiles");
                });

            modelBuilder.Entity("DarkBox.Models.ProjectRequest", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RequestID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("ClientID");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("RequestedDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RequestedTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValue("pending");

                    b.HasKey("RequestId")
                        .HasName("PK__ProjectR__33A8519AEA532DBF");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectRequests");
                });

            modelBuilder.Entity("DarkBox.Models.ProjectUpdate", b =>
                {
                    b.Property<int>("UpdateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UpdateID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UpdateId"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("DeveloperId")
                        .HasColumnType("int")
                        .HasColumnName("DeveloperID");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectID");

                    b.Property<string>("UpdateMessage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UpdateId")
                        .HasName("PK__ProjectU__7A0CF325B25B7C5C");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectUpdates");
                });

            modelBuilder.Entity("DarkBox.Models.SubscriptionPlan", b =>
                {
                    b.Property<int>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PlanID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlanId"));

                    b.Property<int?>("MaxProjects")
                        .HasColumnType("int");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal>("StorageLimitMb")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("StorageLimitMB");

                    b.Property<string>("SupportLevel")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("PlanId")
                        .HasName("PK__Subscrip__755C22D701E5F796");

                    b.HasIndex(new[] { "PlanName" }, "UQ__Subscrip__46E12F9E665AB745")
                        .IsUnique();

                    b.ToTable("SubscriptionPlans");
                });

            modelBuilder.Entity("DarkBox.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Role")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValue("client");

                    b.Property<decimal?>("StorageUsedMb")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(10, 2)")
                        .HasDefaultValue(0m)
                        .HasColumnName("StorageUsedMB");

                    b.Property<int?>("SubscriptionPlanId")
                        .HasColumnType("int")
                        .HasColumnName("SubscriptionPlanID");

                    b.Property<string>("ThemePreference")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasDefaultValue("light");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CCAC92C9860A");

                    b.HasIndex("SubscriptionPlanId");

                    b.HasIndex(new[] { "Username" }, "UQ__Users__536C85E4F01319DD")
                        .IsUnique();

                    b.HasIndex(new[] { "Email" }, "UQ__Users__A9D105344E82C920")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectCategoryAssignment", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectID");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.HasKey("ProjectId", "CategoryId")
                        .HasName("PK__ProjectC__D78A2D72D95D496B");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProjectCategoryAssignments", (string)null);
                });

            modelBuilder.Entity("DarkBox.Models.ActivityHistory", b =>
                {
                    b.HasOne("DarkBox.Models.User", "User")
                        .WithMany("ActivityHistories")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__ActivityH__UserI__797309D9");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DarkBox.Models.Message", b =>
                {
                    b.HasOne("DarkBox.Models.Project", "Project")
                        .WithMany("Messages")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK__Messages__Projec__70DDC3D8");

                    b.HasOne("DarkBox.Models.User", "Receiver")
                        .WithMany("MessageReceivers")
                        .HasForeignKey("ReceiverId")
                        .IsRequired()
                        .HasConstraintName("FK__Messages__Receiv__6FE99F9F");

                    b.HasOne("DarkBox.Models.User", "Sender")
                        .WithMany("MessageSenders")
                        .HasForeignKey("SenderId")
                        .IsRequired()
                        .HasConstraintName("FK__Messages__Sender__6EF57B66");

                    b.Navigation("Project");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("DarkBox.Models.Notification", b =>
                {
                    b.HasOne("DarkBox.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__Notificat__UserI__75A278F5");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DarkBox.Models.PasswordResets", b =>
                {
                    b.HasOne("DarkBox.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DarkBox.Models.Payment", b =>
                {
                    b.HasOne("DarkBox.Models.SubscriptionPlan", "Plan")
                        .WithMany("Payments")
                        .HasForeignKey("PlanId")
                        .IsRequired()
                        .HasConstraintName("FK__Payments__PlanID__6B24EA82");

                    b.HasOne("DarkBox.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__Payments__UserID__6A30C649");

                    b.Navigation("Plan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DarkBox.Models.Project", b =>
                {
                    b.HasOne("DarkBox.Models.User", "Client")
                        .WithMany("ProjectClients")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("FK__Projects__Client__4AB81AF0");

                    b.HasOne("DarkBox.Models.User", "Developer")
                        .WithMany("ProjectDevelopers")
                        .HasForeignKey("DeveloperId")
                        .HasConstraintName("FK__Projects__Develo__4BAC3F29");

                    b.Navigation("Client");

                    b.Navigation("Developer");
                });

            modelBuilder.Entity("DarkBox.Models.ProjectComment", b =>
                {
                    b.HasOne("DarkBox.Models.Project", "Project")
                        .WithMany("ProjectComments")
                        .HasForeignKey("ProjectId")
                        .IsRequired()
                        .HasConstraintName("FK__ProjectCo__Proje__5EBF139D");

                    b.HasOne("DarkBox.Models.User", "User")
                        .WithMany("ProjectComments")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__ProjectCo__UserI__5FB337D6");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DarkBox.Models.ProjectFile", b =>
                {
                    b.HasOne("DarkBox.Models.Project", "Project")
                        .WithMany("ProjectFiles")
                        .HasForeignKey("ProjectId")
                        .IsRequired()
                        .HasConstraintName("FK__ProjectFi__Proje__59063A47");

                    b.HasOne("DarkBox.Models.User", "UploadedByNavigation")
                        .WithMany("ProjectFiles")
                        .HasForeignKey("UploadedBy")
                        .IsRequired()
                        .HasConstraintName("FK__ProjectFi__Uploa__59FA5E80");

                    b.Navigation("Project");

                    b.Navigation("UploadedByNavigation");
                });

            modelBuilder.Entity("DarkBox.Models.ProjectRequest", b =>
                {
                    b.HasOne("DarkBox.Models.User", "Client")
                        .WithMany("ProjectRequests")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("FK__ProjectRe__Clien__5165187F");

                    b.HasOne("DarkBox.Models.Project", "Projetos")
                        .WithMany("ProjectRequests")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Projetos");
                });

            modelBuilder.Entity("DarkBox.Models.ProjectUpdate", b =>
                {
                    b.HasOne("DarkBox.Models.User", "Developer")
                        .WithMany("ProjectUpdates")
                        .HasForeignKey("DeveloperId")
                        .IsRequired()
                        .HasConstraintName("FK__ProjectUp__Devel__6477ECF3");

                    b.HasOne("DarkBox.Models.Project", "Project")
                        .WithMany("ProjectUpdates")
                        .HasForeignKey("ProjectId")
                        .IsRequired()
                        .HasConstraintName("FK__ProjectUp__Proje__6383C8BA");

                    b.Navigation("Developer");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("DarkBox.Models.User", b =>
                {
                    b.HasOne("DarkBox.Models.SubscriptionPlan", "SubscriptionPlan")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionPlanId")
                        .HasConstraintName("FK__Users__Subscript__4222D4EF");

                    b.Navigation("SubscriptionPlan");
                });

            modelBuilder.Entity("ProjectCategoryAssignment", b =>
                {
                    b.HasOne("DarkBox.Models.ProjectCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK__ProjectCa__Categ__5535A963");

                    b.HasOne("DarkBox.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__ProjectCa__Proje__5441852A");
                });

            modelBuilder.Entity("DarkBox.Models.Project", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("ProjectComments");

                    b.Navigation("ProjectFiles");

                    b.Navigation("ProjectRequests");

                    b.Navigation("ProjectUpdates");
                });

            modelBuilder.Entity("DarkBox.Models.SubscriptionPlan", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("DarkBox.Models.User", b =>
                {
                    b.Navigation("ActivityHistories");

                    b.Navigation("MessageReceivers");

                    b.Navigation("MessageSenders");

                    b.Navigation("Notifications");

                    b.Navigation("Payments");

                    b.Navigation("ProjectClients");

                    b.Navigation("ProjectComments");

                    b.Navigation("ProjectDevelopers");

                    b.Navigation("ProjectFiles");

                    b.Navigation("ProjectRequests");

                    b.Navigation("ProjectUpdates");
                });
#pragma warning restore 612, 618
        }
    }
}
