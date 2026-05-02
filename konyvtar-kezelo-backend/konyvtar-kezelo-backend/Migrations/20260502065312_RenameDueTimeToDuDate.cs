using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace konyvtar_kezelo_backend.Migrations
{
    /// <inheritdoc />
    public partial class RenameDueTimeToDuDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueTime",
                table: "Borrowings",
                newName: "DueDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Borrowings",
                newName: "DueTime");
        }
    }
}
