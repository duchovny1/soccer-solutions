using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerSolutionsApp.Data.Migrations
{
    public partial class AddTwoBoolColumnsInPredictionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMatchFinished",
                table: "Predictions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPredictionTrue",
                table: "Predictions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMatchFinished",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "IsPredictionTrue",
                table: "Predictions");
        }
    }
}
