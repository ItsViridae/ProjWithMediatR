using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectZ.Migrations
{
    public partial class AddingUserAssociationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAssociation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    AssociationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssociation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAssociation_Association_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Association",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAssociation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 195, 20, 62, 133, 41, 157, 161, 77, 155, 3, 104, 160, 138, 8, 115, 181, 204, 61, 25, 96 }, new byte[] { 165, 251, 17, 114, 27, 114, 0, 74, 81, 106, 36, 222, 69, 163, 241, 52 } });

            migrationBuilder.CreateIndex(
                name: "IX_UserAssociation_AssociationId",
                table: "UserAssociation",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssociation_UserId",
                table: "UserAssociation",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAssociation");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 53, 9, 192, 168, 162, 189, 42, 26, 110, 6, 220, 187, 7, 94, 235, 165, 0, 55, 148, 78 }, new byte[] { 193, 178, 234, 1, 20, 211, 81, 181, 237, 161, 185, 122, 151, 30, 140, 120 } });
        }
    }
}
