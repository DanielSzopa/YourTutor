using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourTutor.Infrastructure.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Change_DefaultSchema_And_MigrationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "yt");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "yt");

            migrationBuilder.RenameTable(
                name: "Tutor",
                newName: "Tutor",
                newSchema: "yt");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "Offers",
                newSchema: "yt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "yt",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Tutor",
                schema: "yt",
                newName: "Tutor");

            migrationBuilder.RenameTable(
                name: "Offers",
                schema: "yt",
                newName: "Offers");
        }
    }
}
