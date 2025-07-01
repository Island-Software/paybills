using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Paybills.API.Data.Migrations
{
    public partial class AddReceivings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceivingId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReceivingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receivings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceivingTypeId = table.Column<int>(type: "integer", nullable: true),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    ReceivingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Received = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receivings_ReceivingTypes_ReceivingTypeId",
                        column: x => x.ReceivingTypeId,
                        principalTable: "ReceivingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReceivingId",
                table: "Users",
                column: "ReceivingId");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_ReceivingTypeId",
                table: "Receivings",
                column: "ReceivingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Receivings_ReceivingId",
                table: "Users",
                column: "ReceivingId",
                principalTable: "Receivings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Receivings_ReceivingId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Receivings");

            migrationBuilder.DropTable(
                name: "ReceivingTypes");

            migrationBuilder.DropIndex(
                name: "IX_Users_ReceivingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReceivingId",
                table: "Users");
        }
    }
}
