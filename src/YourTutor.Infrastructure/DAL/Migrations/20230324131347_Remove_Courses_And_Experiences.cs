using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourTutor.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Courses_And_Experiences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Experience");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Course_Tutor_TutorUserId",
                        column: x => x.TutorUserId,
                        principalTable: "Tutor",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    ExperienceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.ExperienceId);
                    table.ForeignKey(
                        name: "FK_Experience_Tutor_TutorUserId",
                        column: x => x.TutorUserId,
                        principalTable: "Tutor",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_TutorUserId",
                table: "Course",
                column: "TutorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_TutorUserId",
                table: "Experience",
                column: "TutorUserId");
        }
    }
}
