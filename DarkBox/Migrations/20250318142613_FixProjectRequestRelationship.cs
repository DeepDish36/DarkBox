using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarkBox.Migrations
{
    /// <inheritdoc />
    public partial class FixProjectRequestRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Projects_ProjectId",
                table: "ProjectRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRequests_ProjectId",
                table: "ProjectRequests");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ProjectRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequests_ProjectId",
                table: "ProjectRequests",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Projects_ProjectId",
                table: "ProjectRequests",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
