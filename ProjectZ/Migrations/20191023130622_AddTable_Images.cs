using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectZ.Migrations
{
    public partial class AddTable_Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    ImageBytes = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 181, 129, 198, 149, 55, 71, 67, 227, 131, 71, 123, 80, 67, 179, 119, 90, 43, 126, 183, 197 }, new byte[] { 79, 250, 250, 108, 57, 23, 126, 79, 62, 0, 161, 112, 210, 254, 60, 8 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 171, 19, 244, 249, 114, 132, 58, 64, 140, 25, 188, 167, 67, 179, 245, 157, 235, 123, 44, 50 }, new byte[] { 185, 165, 185, 133, 19, 43, 137, 79, 77, 68, 216, 129, 228, 70, 161, 229 } });
        }
    }
}
