using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Budget.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataWhenBuildingModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "Expenses", "Income", "Name" },
                values: new object[,]
                {
                    { 1, 850m, 650m, 1500.00m, "Test Wallet" },
                    { 2, 1500m, 0m, 1500.00m, "Main Wallet" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "WalletId" },
                values: new object[,]
                {
                    { 1, "Gadgets", 1 },
                    { 2, "Groceries", 1 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "CategoryId", "Cost", "Date", "Description", "Month", "Name" },
                values: new object[,]
                {
                    { 1, 1, 500.00m, new DateTime(2023, 3, 18, 9, 27, 56, 888, DateTimeKind.Local).AddTicks(6420), "I bought a new laptop, external keyboard and mouse", null, "Computer Accessories" },
                    { 2, 2, 150.00m, new DateTime(2023, 3, 18, 9, 27, 56, 888, DateTimeKind.Local).AddTicks(6468), "I bought a bunch of bananas, grapes and 7 oranges", null, "Weekly fruit stocking" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
