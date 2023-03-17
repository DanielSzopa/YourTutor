using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourTutor.Infrastructure.Dal.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrimaryKeyForTutor_For_TheSame_Like_UserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Tutor_TutorId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Tutor_TutorId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_Tutor_Users_UserId",
                table: "Tutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tutor",
                table: "Tutor");

            migrationBuilder.DropIndex(
                name: "IX_Tutor_UserId",
                table: "Tutor");

            migrationBuilder.DropIndex(
                name: "IX_Experience_TutorId",
                table: "Experience");

            migrationBuilder.DropIndex(
                name: "IX_Course_TutorId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "Tutor");

            migrationBuilder.RenameColumn(
                name: "TutorId",
                table: "Experience",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TutorId",
                table: "Course",
                newName: "UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Tutor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TutorUserId",
                table: "Experience",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TutorUserId",
                table: "Course",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tutor",
                table: "Tutor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_TutorUserId",
                table: "Experience",
                column: "TutorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_TutorUserId",
                table: "Course",
                column: "TutorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Tutor_TutorUserId",
                table: "Course",
                column: "TutorUserId",
                principalTable: "Tutor",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Tutor_TutorUserId",
                table: "Experience",
                column: "TutorUserId",
                principalTable: "Tutor",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutor_Users_UserId",
                table: "Tutor",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Tutor_TutorUserId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Tutor_TutorUserId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_Tutor_Users_UserId",
                table: "Tutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tutor",
                table: "Tutor");

            migrationBuilder.DropIndex(
                name: "IX_Experience_TutorUserId",
                table: "Experience");

            migrationBuilder.DropIndex(
                name: "IX_Course_TutorUserId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "TutorUserId",
                table: "Experience");

            migrationBuilder.DropColumn(
                name: "TutorUserId",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Experience",
                newName: "TutorId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Course",
                newName: "TutorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Tutor",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "TutorId",
                table: "Tutor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tutor",
                table: "Tutor",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tutor_UserId",
                table: "Tutor",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_TutorId",
                table: "Experience",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_TutorId",
                table: "Course",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Tutor_TutorId",
                table: "Course",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Tutor_TutorId",
                table: "Experience",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutor_Users_UserId",
                table: "Tutor",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
