using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagerModel.Migrations
{
    /// <inheritdoc />
    public partial class _20241206_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e53ad554-d26e-40a7-893d-c07b595375e5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e53ad554-d26e-40a7-893d-c07b595375e5", 0, "f82d5109-5dc6-430c-926c-5fdb22e183b4", "admin@test.localhost", true, "admin", "admin", false, null, "ADMIN@TEST.LOCALHOST", "ADMIN", "$2a$11$xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "063020", false, "e53ad554-d26e-40a7-893d-c07b595375e5", false, "admin" });
        }
    }
}
