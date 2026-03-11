using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class indexingdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_product_SupplierId",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_order_detail_order_id",
                table: "order_detail");

            migrationBuilder.DropIndex(
                name: "IX_GRN_detail_GRN_id",
                table: "GRN_detail");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "user",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.CreateIndex(
                name: "IX_user_refresh_token",
                table: "user",
                column: "refresh_token");

            migrationBuilder.CreateIndex(
                name: "IX_user_username",
                table: "user",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_supplier_Email",
                table: "supplier",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_supplier_Name",
                table: "supplier",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_supplier_Phone",
                table: "supplier",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_SupplierId_product_name",
                table: "product",
                columns: new[] { "SupplierId", "product_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_order_id_product_id",
                table: "order_detail",
                columns: new[] { "order_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GRN_detail_GRN_id_product_id",
                table: "GRN_detail",
                columns: new[] { "GRN_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_name",
                table: "category",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_refresh_token",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_username",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_supplier_Email",
                table: "supplier");

            migrationBuilder.DropIndex(
                name: "IX_supplier_Name",
                table: "supplier");

            migrationBuilder.DropIndex(
                name: "IX_supplier_Phone",
                table: "supplier");

            migrationBuilder.DropIndex(
                name: "IX_product_SupplierId_product_name",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_order_detail_order_id_product_id",
                table: "order_detail");

            migrationBuilder.DropIndex(
                name: "IX_GRN_detail_GRN_id_product_id",
                table: "GRN_detail");

            migrationBuilder.DropIndex(
                name: "IX_category_name",
                table: "category");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "user",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.CreateIndex(
                name: "IX_product_SupplierId",
                table: "product",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_order_id",
                table: "order_detail",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_GRN_detail_GRN_id",
                table: "GRN_detail",
                column: "GRN_id");
        }
    }
}
