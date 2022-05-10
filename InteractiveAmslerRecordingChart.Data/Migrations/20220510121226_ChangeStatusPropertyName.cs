using Microsoft.EntityFrameworkCore.Migrations;

namespace InteractiveAmslerRecordingChart.Data.Migrations
{
    public partial class ChangeStatusPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Coordinates",
                newName: "VisualFieldStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VisualFieldStatus",
                table: "Coordinates",
                newName: "Status");
        }
    }
}
