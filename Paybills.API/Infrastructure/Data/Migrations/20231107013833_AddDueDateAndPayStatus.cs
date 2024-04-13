using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Paybills.API.Data.Migrations
{
    public partial class AddDueDateAndPayStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Bills",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Bills",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Bills");
        }
    }
}
