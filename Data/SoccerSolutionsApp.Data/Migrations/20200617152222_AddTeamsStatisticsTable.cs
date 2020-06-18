using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerSolutionsApp.Data.Migrations
{
    public partial class AddTeamsStatisticsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(nullable: false),
                    MatchPlayedAsHomeTeam = table.Column<int>(nullable: false),
                    MatchPlayedAsAwayTeam = table.Column<int>(nullable: false),
                    MatchPlayedTotal = table.Column<int>(nullable: false),
                    MatchesWinsAsHome = table.Column<int>(nullable: false),
                    MatchesWinsAsAway = table.Column<int>(nullable: false),
                    MatchesWinsTotal = table.Column<int>(nullable: false),
                    MatchesDrawsAsHome = table.Column<int>(nullable: false),
                    MatchesDrawsAsAway = table.Column<int>(nullable: false),
                    MatchesDrawsTotal = table.Column<int>(nullable: false),
                    MatchesLosesAsHome = table.Column<int>(nullable: false),
                    MatchesLosesAsAway = table.Column<int>(nullable: false),
                    MatchesLosesTotal = table.Column<int>(nullable: false),
                    GoalsForAsHome = table.Column<int>(nullable: false),
                    GoalsForAsAway = table.Column<int>(nullable: false),
                    GoalsForTotal = table.Column<int>(nullable: false),
                    GoalsAgainstAsHome = table.Column<int>(nullable: false),
                    GoasAgainstAsAway = table.Column<int>(nullable: false),
                    GoalsAgainstTotal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statistics_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_TeamId",
                table: "Statistics",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");
        }
    }
}
