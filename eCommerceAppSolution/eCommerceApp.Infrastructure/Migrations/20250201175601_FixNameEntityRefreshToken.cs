using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNameEntityRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "448da773-0371-4a73-846f-f7f053944db7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55a720fa-63b1-4f01-9ad1-9b1be74d759a");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "RefreshToken",
                newName: "RefreshTokenHash");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8148ff94-4adf-4ed7-bb18-60baefec2ebf", null, "Admin", "ADMIN" },
                    { "d1f8e531-2ee8-4163-a631-ce6fb2b61046", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8148ff94-4adf-4ed7-bb18-60baefec2ebf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1f8e531-2ee8-4163-a631-ce6fb2b61046");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenHash",
                table: "RefreshToken",
                newName: "Token");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "448da773-0371-4a73-846f-f7f053944db7", null, "User", "USER" },
                    { "55a720fa-63b1-4f01-9ad1-9b1be74d759a", null, "Admin", "ADMIN" }
                });
        }
    }
}
