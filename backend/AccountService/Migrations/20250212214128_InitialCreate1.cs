using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AccountTrades_AccountId",
                table: "AccountTrades",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTrades_Accounts_AccountId",
                table: "AccountTrades",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTrades_Accounts_AccountId",
                table: "AccountTrades");

            migrationBuilder.DropIndex(
                name: "IX_AccountTrades_AccountId",
                table: "AccountTrades");
        }
    }
}
