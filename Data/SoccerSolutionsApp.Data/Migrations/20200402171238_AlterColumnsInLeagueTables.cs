using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerSolutionsApp.Data.Migrations
{
    public partial class AlterColumnsInLeagueTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeagueType",
                table: "Leagues");

            migrationBuilder.AlterColumn<string>(
                name: "StartYear",
                table: "Seasons",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Leagues",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Leagues",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SeasonEnd",
                table: "Leagues",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SeasonStart",
                table: "Leagues",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Leagues",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "SeasonEnd",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "SeasonStart",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Leagues");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartYear",
                table: "Seasons",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndYear",
                table: "Seasons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "LeagueType",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
