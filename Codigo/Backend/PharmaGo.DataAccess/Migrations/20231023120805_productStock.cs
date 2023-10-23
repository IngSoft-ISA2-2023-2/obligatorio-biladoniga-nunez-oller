using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaGo.DataAccess.Migrations
{
    public partial class productStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "PurchaseDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_ProductId",
                table: "PurchaseDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_Products_ProductId",
                table: "PurchaseDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_Products_ProductId",
                table: "PurchaseDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseDetails_ProductId",
                table: "PurchaseDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "PurchaseDetails");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");
        }
    }
}
