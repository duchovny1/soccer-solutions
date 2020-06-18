using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerSolutionsApp.Data.Migrations
{
    public partial class AlterStatisticsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropColumn(
                name: "GoasAgainstAsAway",
                table: "Statistics");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Statistics",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GoalsAgainstAsAway",
                table: "Statistics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "GoalsAgainstAvgAsAway",
                table: "Statistics",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GoalsAgainstAvgAsHome",
                table: "Statistics",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GoalsAgainstAvgTotal",
                table: "Statistics",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GoalsForAvgAsAway",
                table: "Statistics",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GoalsForAvgAsHome",
                table: "Statistics",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GoalsForAvgTotal",
                table: "Statistics",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Statistics",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Statistics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_IsDeleted",
                table: "Statistics",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_LeagueId",
                table: "Statistics",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Leagues_LeagueId",
                table: "Statistics",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Leagues_LeagueId",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_IsDeleted",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_LeagueId",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "GoalsAgainstAsAway",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "GoalsAgainstAvgAsAway",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "GoalsAgainstAvgAsHome",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "GoalsAgainstAvgTotal",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "GoalsForAvgAsAway",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "GoalsForAvgAsHome",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "GoalsForAvgTotal",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Statistics");

            migrationBuilder.AddColumn<int>(
                name: "GoasAgainstAsAway",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_IsDeleted",
                table: "Settings",
                column: "IsDeleted");
        }
    }
}
