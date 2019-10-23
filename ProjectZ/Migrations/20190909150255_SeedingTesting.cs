using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectZ.Migrations
{
    public partial class SeedingTesting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Association",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Association",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "Seven" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 171, 19, 244, 249, 114, 132, 58, 64, 140, 25, 188, 167, 67, 179, 245, 157, 235, 123, 44, 50 }, new byte[] { 185, 165, 185, 133, 19, 43, 137, 79, 77, 68, 216, 129, 228, 70, 161, 229 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Association",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Association",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 195, 20, 62, 133, 41, 157, 161, 77, 155, 3, 104, 160, 138, 8, 115, 181, 204, 61, 25, 96 }, new byte[] { 165, 251, 17, 114, 27, 114, 0, 74, 81, 106, 36, 222, 69, 163, 241, 52 } });
        }
    }
}
