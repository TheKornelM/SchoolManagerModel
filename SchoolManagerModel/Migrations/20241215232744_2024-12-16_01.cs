using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagerModel.Migrations
{
    /// <inheritdoc />
    public partial class _20241216_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Classes",
                newName: "SchoolClass");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "SchoolClass",
                table: "Classes",
                newName: "Name");
        }
    }
}
