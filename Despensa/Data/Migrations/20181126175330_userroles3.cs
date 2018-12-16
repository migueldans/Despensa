using Microsoft.EntityFrameworkCore.Migrations;

namespace Despensa.Data.Migrations
{
    public partial class userroles3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_despensa_AspNetUsers_AppUserId",
                table: "despensa");

            migrationBuilder.DropIndex(
                name: "IX_despensa_AppUserId",
                table: "despensa");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "despensa");

            migrationBuilder.AddColumn<int>(
                name: "despensaId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_despensaId",
                table: "AspNetUsers",
                column: "despensaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_despensa_despensaId",
                table: "AspNetUsers",
                column: "despensaId",
                principalTable: "despensa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_despensa_despensaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_despensaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "despensaId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "despensa",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_despensa_AppUserId",
                table: "despensa",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_despensa_AspNetUsers_AppUserId",
                table: "despensa",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
