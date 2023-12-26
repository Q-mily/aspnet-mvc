using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETDemo3.Migrations
{
    public partial class add_mapping_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodOrderTable",
                columns: table => new
                {
                    FoodsId = table.Column<int>(type: "int", nullable: false),
                    OrderTablesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOrderTable", x => new { x.FoodsId, x.OrderTablesId });
                    table.ForeignKey(
                        name: "FK_FoodOrderTable_Foods_FoodsId",
                        column: x => x.FoodsId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodOrderTable_OrderTables_OrderTablesId",
                        column: x => x.OrderTablesId,
                        principalTable: "OrderTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodOrderTable_OrderTablesId",
                table: "FoodOrderTable",
                column: "OrderTablesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodOrderTable");
        }
    }
}
