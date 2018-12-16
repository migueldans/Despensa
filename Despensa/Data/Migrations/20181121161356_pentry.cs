using Microsoft.EntityFrameworkCore.Migrations;

namespace Despensa.Data.Migrations
{
    public partial class pentry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "despensa",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "despensa");
        }
    }
}
