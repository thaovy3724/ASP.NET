using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerAddressAndOrderAddressId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "address_id",
                table: "order",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "customer_address",
                columns: table => new
                {
                    customer_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    receiver_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    address_line = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    is_default = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_address", x => x.customer_address_id);

                    table.ForeignKey(
                        name: "FK_customer_address_user_customer_id",
                        column: x => x.customer_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_address_id",
                table: "order",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_address_customer_id",
                table: "customer_address",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_address_customer_id_is_default",
                table: "customer_address",
                columns: new[] { "customer_id", "is_default" },
                unique: true,
                filter: "[is_default] = 1");

            migrationBuilder.AddForeignKey(
                name: "FK_order_customer_address_address_id",
                table: "order",
                column: "address_id",
                principalTable: "customer_address",
                principalColumn: "customer_address_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_customer_address_address_id",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_address_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "address_id",
                table: "order");

            migrationBuilder.DropTable(
                name: "customer_address");
        }
    }
}