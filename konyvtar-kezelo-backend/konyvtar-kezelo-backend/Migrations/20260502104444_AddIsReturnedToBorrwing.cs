using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace konyvtar_kezelo_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReturnedToBorrwing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "Borrowings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "Borrowings");
        }
    }
}
