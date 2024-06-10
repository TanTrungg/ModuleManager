using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModuleManager.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "IsActive", "IsDeleted", "PasswordHash", "UserName" },
                values: new object[] { 1, new DateTime(2024, 6, 3, 11, 5, 21, 557, DateTimeKind.Local).AddTicks(749), "admin@example.com", true, false, "201bce2458f00a54130c695ca8d1658319b32206d495adf175847b57bd4a4151", "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");
        }
    }
}
