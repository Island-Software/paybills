using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Paybills.API.Data.Migrations
{
    public partial class AddRelationShipTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Receivings_ReceivingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ReceivingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReceivingId",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReceivingDate",
                table: "Receivings",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.CreateTable(
                name: "AppUserReceiving",
                columns: table => new
                {
                    ReceivingsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserReceiving", x => new { x.ReceivingsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AppUserReceiving_Receivings_ReceivingsId",
                        column: x => x.ReceivingsId,
                        principalTable: "Receivings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserReceiving_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserReceiving_UsersId",
                table: "AppUserReceiving",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserReceiving");

            migrationBuilder.AddColumn<int>(
                name: "ReceivingId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReceivingDate",
                table: "Receivings",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReceivingId",
                table: "Users",
                column: "ReceivingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Receivings_ReceivingId",
                table: "Users",
                column: "ReceivingId",
                principalTable: "Receivings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
