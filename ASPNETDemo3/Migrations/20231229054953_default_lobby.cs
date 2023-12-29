using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETDemo3.Migrations
{
    public partial class default_lobby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Lobbies",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "./",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Lobbies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "./");
        }
    }
}
