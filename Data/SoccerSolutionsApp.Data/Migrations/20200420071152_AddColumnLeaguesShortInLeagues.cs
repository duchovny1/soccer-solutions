using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerSolutionsApp.Data.Migrations
{
    public partial class AddColumnLeaguesShortInLeagues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeagueShort",
                table: "Leagues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeagueShort",
                table: "Leagues");
        }
    }
}
