using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiServiceForVacancy.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
