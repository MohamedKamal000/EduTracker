using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduTracker.Migrations
{
    /// <inheritdoc />
    public partial class IndexingForPerformance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentId",
                table: "Students",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentName",
                table: "Students",
                column: "StudentName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_StudentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentName",
                table: "Students");
        }
    }
}
