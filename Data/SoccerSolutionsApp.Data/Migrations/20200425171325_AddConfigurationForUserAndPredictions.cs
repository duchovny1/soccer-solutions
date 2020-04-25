using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerSolutionsApp.Data.Migrations
{
    public partial class AddConfigurationForUserAndPredictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Followings_IsDeleted",
                table: "Followings",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Followings_IsDeleted",
                table: "Followings");
        }
    }
}
