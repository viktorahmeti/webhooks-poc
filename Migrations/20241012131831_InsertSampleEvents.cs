using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webhooks_poc.Migrations
{
    /// <inheritdoc />
    public partial class InsertSampleEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: ["Name", "Description"],
                values: ["MARKET_UP", "When S&P500 goes up in a 1 hour interval"]
            );

            migrationBuilder.InsertData(
                table: "Events",
                columns: ["Name", "Description"],
                values: ["MARKET_DOWN", "When S&P500 goes down in a 1 hour interval"]
            );

            migrationBuilder.InsertData(
                table: "Events",
                columns: ["Name", "Description"],
                values: ["CRASH", "When the market crashes"]
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Name",
                keyValue: "MARKET_UP"
            );

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Name",
                keyValue: "MARKET_DOWN"
            );

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Name",
                keyValue: "CRASH"
            );
        }
    }
}
