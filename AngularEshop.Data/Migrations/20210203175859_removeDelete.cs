using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularEshop.Data.Migrations
{
    public partial class removeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ProductVisits");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ProductSelectedCategories");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ProductGalleries");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "OrderDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ProductVisits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ProductSelectedCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ProductGalleries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ProductComments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ProductCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "OrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
