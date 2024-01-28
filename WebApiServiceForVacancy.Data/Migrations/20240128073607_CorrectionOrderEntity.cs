using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiServiceForVacancy.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreaDateTime",
                table: "Orders",
                newName: "CreateDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDateTime",
                table: "Orders",
                newName: "CreaDateTime");
        }
    }
}
