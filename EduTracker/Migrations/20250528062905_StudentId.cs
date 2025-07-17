using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduTracker.Migrations
{
    /// <inheritdoc />
    public partial class StudentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SID",
                table: "Students",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "SID");
        }
    }
}
