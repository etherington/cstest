using Microsoft.EntityFrameworkCore.Migrations;

namespace CommentSold.WebTest.Migrations
{
    public partial class inventory_cost_cents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CostCents",
                table: "Inventory",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCents",
                table: "Inventory");
        }
    }
}
