using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETDemo3.Migrations
{
    public partial class update_order_invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPrice",
                table: "Orders",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");
        }
    }
}
