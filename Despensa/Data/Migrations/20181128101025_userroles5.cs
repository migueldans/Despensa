using Microsoft.EntityFrameworkCore.Migrations;

namespace Despensa.Data.Migrations
{
    public partial class userroles5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name_Product",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name_Product",
                table: "Product");
        }
    }
}
