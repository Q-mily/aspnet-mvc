using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETDemo3.Migrations
{
    public partial class delete_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Lobbies_lobbyId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Lobbies_lobbyId",
                table: "Orders",
                column: "lobbyId",
                principalTable: "Lobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Lobbies_lobbyId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Lobbies_lobbyId",
                table: "Orders",
                column: "lobbyId",
                principalTable: "Lobbies",
                principalColumn: "Id");
        }
    }
}
