using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Discount.gRPC.Migrations
{
    /// <inheritdoc />
    public partial class dataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Amount", "Description", "ProductName" },
                values: new object[,]
                {
                    { 3, 120, "Google Pixel Discount", "Google Pixel 6" },
                    { 4, 90, "OnePlus Discount", "OnePlus 9" },
                    { 5, 80, "Sony Discount", "Sony Xperia 5" },
                    { 6, 70, "LG Discount", "LG Velvet" },
                    { 7, 60, "Motorola Discount", "Motorola Edge" },
                    { 8, 110, "Huawei Discount", "Huawei P40" },
                    { 9, 50, "Nokia Discount", "Nokia 8.3" },
                    { 10, 85, "Xiaomi Discount", "Xiaomi Mi 11" },
                    { 11, 95, "Oppo Discount", "Oppo Find X3" },
                    { 12, 65, "Vivo Discount", "Vivo X60" },
                    { 13, 75, "GT Discount", "GT" },
                    { 14, 130, "Asus Discount", "Asus ROG Phone 5" },
                    { 15, 115, "Lenovo Discount", "Lenovo Legion Duel" },
                    { 16, 105, "Black Shark Discount", "Black Shark 4" },
                    { 17, 55, "ZTE Discount", "ZTE Axon 30" },
                    { 18, 200, "MacBook Discount", "Apple MacBook Pro" },
                    { 19, 180, "Dell Discount", "Dell XPS 13" },
                    { 20, 170, "HP x360 Discount", "HP x360" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ProductName",
                table: "Coupons",
                column: "ProductName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupons_ProductName",
                table: "Coupons");

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
