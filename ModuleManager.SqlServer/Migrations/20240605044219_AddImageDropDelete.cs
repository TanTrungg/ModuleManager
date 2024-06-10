using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModuleManager.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddImageDropDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageUrl", "PasswordHash" },
                values: new object[] { new DateTime(2024, 6, 5, 11, 42, 17, 505, DateTimeKind.Local).AddTicks(8108), "https://res.cloudinary.com/demo/image/upload/v1600123456/sample.jpg", "3b612c75a7b5048a435fb6ec81e52ff92d6d795a8b5a9c17070f6a63c97a53b2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsDeleted", "PasswordHash" },
                values: new object[] { new DateTime(2024, 6, 3, 11, 5, 21, 557, DateTimeKind.Local).AddTicks(749), false, "201bce2458f00a54130c695ca8d1658319b32206d495adf175847b57bd4a4151" });
        }
    }
}
