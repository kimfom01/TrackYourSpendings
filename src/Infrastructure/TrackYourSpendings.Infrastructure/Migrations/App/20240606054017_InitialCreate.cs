using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrackYourSpendings.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Currency = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Income = table.Column<decimal>(type: "numeric", nullable: false),
                    Expenses = table.Column<decimal>(type: "numeric", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "app",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "app",
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "app",
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("227afb2b-7601-4171-ab24-7d5de3796351"), new DateTime(2024, 6, 6, 8, 40, 17, 215, DateTimeKind.Local).AddTicks(5355), "Recreation & Entertainment", null, "" },
                    { new Guid("405eb333-07dc-4af0-86e3-fe3183e85c93"), new DateTime(2024, 6, 6, 8, 40, 17, 215, DateTimeKind.Local).AddTicks(5365), "Miscellaneous", null, "" },
                    { new Guid("4d328b83-997f-4719-98d5-9713e9d5fcdb"), new DateTime(2024, 6, 6, 8, 40, 17, 215, DateTimeKind.Local).AddTicks(5328), "Saving, Investing & Dept Payments", null, "" },
                    { new Guid("504becb7-60f9-41db-ae6a-ae98f7e6ccbc"), new DateTime(2024, 6, 6, 8, 40, 17, 215, DateTimeKind.Local).AddTicks(5299), "Insurance", null, "" },
                    { new Guid("50d0672e-5a9f-41f2-9912-6eaccec3b09b"), new DateTime(2024, 6, 6, 8, 40, 17, 215, DateTimeKind.Local).AddTicks(5196), "Housing", null, "" },
                    { new Guid("5cda7116-a260-4c68-b9db-2a596714346f"), new DateTime(2024, 6, 6, 8, 40, 17, 215, DateTimeKind.Local).AddTicks(5251), "Transportation", null, "" },
                    { new Guid("74140cff-4752-4a7d-8f86-699edc23f4a8"), new DateTime(2024, 6, 6, 8, 40, 17, 215, DateTimeKind.Local).AddTicks(5341), "Personal Spending", null, "" },
                    { new Guid("a0ae659f-78e2-467e-a639-12a286f3cd74"), new DateTime(2024, 6, 6, 8, 40, 17, 215, DateTimeKind.Local).AddTicks(5270), "Food", null, "" },
                    { new Guid("b91f92c3-1849-444c-915b-57ee11ecf757"), new DateTime(2024, 6, 6, 8, 40, 17, 215, DateTimeKind.Local).AddTicks(5285), "Utilities", null, "" },
                    { new Guid("ec241f35-3d76-4f8c-9fc5-5af3f5a5872a"), new DateTime(2024, 6, 6, 8, 40, 17, 215, DateTimeKind.Local).AddTicks(5313), "Medical & Healthcare", null, "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                schema: "app",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                schema: "app",
                table: "Transactions",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Wallets",
                schema: "app");
        }
    }
}
