using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularEshop.Data.Migrations
{
    public partial class modify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProductComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductComments");
        }
    }
}
