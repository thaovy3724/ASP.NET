using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVoucherAndOrderDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "discount_amount",
                table: "order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "voucher_code",
                table: "order",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "voucher_id",
                table: "order",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "voucher",
                columns: table => new
                {
                    voucher_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    discount_percent = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    max_discount_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voucher", x => x.voucher_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_voucher_id",
                table: "order",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "IX_voucher_code",
                table: "voucher",
                column: "code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_order_voucher_voucher_id",
                table: "order",
                column: "voucher_id",
                principalTable: "voucher",
                principalColumn: "voucher_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_voucher_voucher_id",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_voucher_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "discount_amount",
                table: "order");

            migrationBuilder.DropColumn(
                name: "voucher_code",
                table: "order");

            migrationBuilder.DropColumn(
                name: "voucher_id",
                table: "order");

            migrationBuilder.DropTable(
                name: "voucher");
        }
    }
}