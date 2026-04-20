using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.Infrastructure.Migrations
{
    public partial class AddProductSoftDeleteAndUserLock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_locked",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "product");

            migrationBuilder.DropColumn(
                name: "is_locked",
                table: "user");
        }
    }
}