using Microsoft.EntityFrameworkCore.Migrations;

namespace SD.MvcWebUI.Migrations.SD
{
    public partial class SeedSystemParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SystemParameters",
                columns: new[] { "ParameterId", "Description", "IsReadOnly", "ParameterName", "ParameterValue" },
                values: new object[] { 1, "Sistemin açık olup olmadığı durumu", true, "SYSTEMSTATUS", "ACTIVE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemParameters",
                keyColumn: "ParameterId",
                keyValue: 1);
        }
    }
}
