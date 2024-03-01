using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackYourSpendings.Web.Migrations;

/// <inheritdoc />
public partial class AddActiveWalletColumn : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "Active",
            table: "Wallets",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "Transactions",
            type: "character varying(128)",
            maxLength: 128,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Active",
            table: "Wallets");

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "Transactions",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(128)",
            oldMaxLength: 128,
            oldNullable: true);
    }
}