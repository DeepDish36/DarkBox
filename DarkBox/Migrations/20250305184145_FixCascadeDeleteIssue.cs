using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarkBox.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDeleteIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ProjectCategories' and xtype='U')
            BEGIN
                CREATE TABLE [ProjectCategories] (
                    [CategoryID] int NOT NULL IDENTITY,
                    [CategoryName] nvarchar(50) NOT NULL,
                    CONSTRAINT [PK__ProjectC__19093A2B46178F85] PRIMARY KEY ([CategoryID])
                );
            END
        ");

            migrationBuilder.Sql(@"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PasswordResets' and xtype='U')
            BEGIN
                CREATE TABLE [PasswordResets] (
                    [Id] int NOT NULL IDENTITY,
                    [UserID] int NOT NULL,
                    [Token] nvarchar(450) NULL,
                    [Expiration] datetime2 NOT NULL,
                    CONSTRAINT [PK_PasswordResets] PRIMARY KEY ([Id]),
                    CONSTRAINT [FK_PasswordResets_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
                );
            END
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectCategories");

            migrationBuilder.DropTable(
                name: "PasswordResets");
        }
    }
}
