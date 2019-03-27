using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommentSold.WebTest.Migrations
{
    public partial class inventory_and_orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Sku = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    PriceCents = table.Column<long>(nullable: false),
                    SalePriceCents = table.Column<long>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Weight = table.Column<long>(nullable: false),
                    Length = table.Column<long>(nullable: false),
                    Width = table.Column<long>(nullable: false),
                    Height = table.Column<long>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetAddress = table.Column<string>(nullable: true),
                    Apartment = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    PaymentAmountCents = table.Column<int>(nullable: false),
                    ShipChargedCents = table.Column<long>(nullable: true),
                    ShipCostCents = table.Column<long>(nullable: true),
                    SubTotalCents = table.Column<long>(nullable: false),
                    TotalCents = table.Column<long>(nullable: false),
                    ShipperName = table.Column<string>(nullable: true),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    ShippedDate = table.Column<DateTime>(nullable: true),
                    TrackingNumber = table.Column<string>(nullable: true),
                    TaxTotalCents = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    InventoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_InventoryId",
                table: "Order",
                column: "InventoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Inventory");
        }
    }
}
