using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETDemo3.Migrations
{
    public partial class add_ordertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    table_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTables_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderTables_OrderId",
                table: "OrderTables",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTables");
        }
    }
}
