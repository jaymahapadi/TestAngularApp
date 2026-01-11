using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestAngularApp.Server.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class InitialMigrationforauth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "edc267ec-d43c-4e3b-8108-a1a1f819906d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4b544728-36eb-468a-a28d-7b3471a18fb6", "0909a374-2068-4c20-ad33-9bee2c3a2673" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "edc267ec-d43c-4e3b-8108-a1a1f819906d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a958b4c4-3585-4428-8b19-40bb08a0691f", "7cb12ef9-8c25-4b64-a8fc-42bcacf3fa1b" });
        }
    }
}
