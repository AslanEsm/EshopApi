using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularEshop.Data.Migrations
{
    public partial class addfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastLoginDate",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "AspNetUsers");
        }
    }
}
