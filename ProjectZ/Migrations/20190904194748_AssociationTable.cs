using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectZ.Migrations
{
    public partial class AssociationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Association",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Association", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 53, 9, 192, 168, 162, 189, 42, 26, 110, 6, 220, 187, 7, 94, 235, 165, 0, 55, 148, 78 }, new byte[] { 193, 178, 234, 1, 20, 211, 81, 181, 237, 161, 185, 122, 151, 30, 140, 120 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Association");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 21, 159, 212, 211, 210, 144, 104, 169, 133, 10, 28, 198, 189, 202, 141, 135, 185, 157, 114, 41 }, new byte[] { 148, 50, 67, 227, 85, 143, 156, 23, 66, 28, 114, 195, 167, 160, 79, 240 } });
        }
    }
}
