using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarkBox.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDeleteIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectC__19093A2B46178F85", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    PlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MaxProjects = table.Column<int>(type: "int", nullable: true),
                    StorageLimitMB = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SupportLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subscrip__755C22D701E5F796", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "client"),
                    ThemePreference = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, defaultValue: "light"),
                    SubscriptionPlanID = table.Column<int>(type: "int", nullable: true),
                    StorageUsedMB = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0m),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC92C9860A", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__Users__Subscript__4222D4EF",
                        column: x => x.SubscriptionPlanID,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "PlanID");
                });

            migrationBuilder.CreateTable(
                name: "ActivityHistory",
                columns: table => new
                {
                    ActivityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ActionDescription = table.Column<string>(type: "text", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Activity__45F4A7F1477BA476", x => x.ActivityID);
                    table.ForeignKey(
                        name: "FK__ActivityH__UserI__797309D9",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    NotificationText = table.Column<string>(type: "text", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__20CF2E32486489E0", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK__Notificat__UserI__75A278F5",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PlanID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__9B556A58E6DCB0B6", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK__Payments__PlanID__6B24EA82",
                        column: x => x.PlanID,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "PlanID");
                    table.ForeignKey(
                        name: "FK__Payments__UserID__6A30C649",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ProjectRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    RequestedTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RequestedDescription = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "pending"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectR__33A8519AEA532DBF", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK__ProjectRe__Clien__5165187F",
                        column: x => x.ClientID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    DeveloperID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "pending"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Projects__761ABED0E4158867", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK__Projects__Client__4AB81AF0",
                        column: x => x.ClientID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Projects__Develo__4BAC3F29",
                        column: x => x.DeveloperID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    ReceiverID = table.Column<int>(type: "int", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    MessageText = table.Column<string>(type: "text", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__C87C037C15BBD094", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK__Messages__Projec__70DDC3D8",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK__Messages__Receiv__6FE99F9F",
                        column: x => x.ReceiverID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Messages__Sender__6EF57B66",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ProjectCategoryAssignments",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectC__D78A2D72D95D496B", x => new { x.ProjectID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK__ProjectCa__Categ__5535A963",
                        column: x => x.CategoryID,
                        principalTable: "ProjectCategories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK__ProjectCa__Proje__5441852A",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectComments",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectC__C3B4DFAA011AE0EA", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK__ProjectCo__Proje__5EBF139D",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK__ProjectCo__UserI__5FB337D6",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ProjectFiles",
                columns: table => new
                {
                    FileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    UploadedBy = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileSizeMB = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Readme = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectF__6F0F989F01562FD2", x => x.FileID);
                    table.ForeignKey(
                        name: "FK__ProjectFi__Proje__59063A47",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK__ProjectFi__Uploa__59FA5E80",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ProjectUpdates",
                columns: table => new
                {
                    UpdateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    DeveloperID = table.Column<int>(type: "int", nullable: false),
                    UpdateMessage = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectU__7A0CF325B25B7C5C", x => x.UpdateID);
                    table.ForeignKey(
                        name: "FK__ProjectUp__Devel__6477ECF3",
                        column: x => x.DeveloperID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__ProjectUp__Proje__6383C8BA",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistory_UserID",
                table: "ActivityHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ProjectID",
                table: "Messages",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverID",
                table: "Messages",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderID",
                table: "Messages",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PlanID",
                table: "Payments",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserID",
                table: "Payments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__ProjectC__8517B2E0592869B5",
                table: "ProjectCategories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategoryAssignments_CategoryID",
                table: "ProjectCategoryAssignments",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_ProjectID",
                table: "ProjectComments",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_UserID",
                table: "ProjectComments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFiles_ProjectID",
                table: "ProjectFiles",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFiles_UploadedBy",
                table: "ProjectFiles",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequests_ClientID",
                table: "ProjectRequests",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientID",
                table: "Projects",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DeveloperID",
                table: "Projects",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUpdates_DeveloperID",
                table: "ProjectUpdates",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUpdates_ProjectID",
                table: "ProjectUpdates",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "UQ__Subscrip__46E12F9E665AB745",
                table: "SubscriptionPlans",
                column: "PlanName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SubscriptionPlanID",
                table: "Users",
                column: "SubscriptionPlanID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E4F01319DD",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D105344E82C920",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityHistory");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProjectCategoryAssignments");

            migrationBuilder.DropTable(
                name: "ProjectComments");

            migrationBuilder.DropTable(
                name: "ProjectFiles");

            migrationBuilder.DropTable(
                name: "ProjectRequests");

            migrationBuilder.DropTable(
                name: "ProjectUpdates");

            migrationBuilder.DropTable(
                name: "ProjectCategories");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
