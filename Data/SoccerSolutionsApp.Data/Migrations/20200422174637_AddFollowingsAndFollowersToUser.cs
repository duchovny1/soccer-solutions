using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerSolutionsApp.Data.Migrations
{
    public partial class AddFollowingsAndFollowersToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Following_AspNetUsers_UserFollowingId",
                table: "Following");

            migrationBuilder.DropForeignKey(
                name: "FK_Following_AspNetUsers_UserToFollowId",
                table: "Following");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Following",
                table: "Following");

            migrationBuilder.RenameTable(
                name: "Following",
                newName: "Followings");

            migrationBuilder.RenameIndex(
                name: "IX_Following_UserToFollowId",
                table: "Followings",
                newName: "IX_Followings_UserToFollowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Followings",
                table: "Followings",
                columns: new[] { "UserFollowingId", "UserToFollowId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_AspNetUsers_UserFollowingId",
                table: "Followings",
                column: "UserFollowingId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_AspNetUsers_UserToFollowId",
                table: "Followings",
                column: "UserToFollowId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followings_AspNetUsers_UserFollowingId",
                table: "Followings");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_AspNetUsers_UserToFollowId",
                table: "Followings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Followings",
                table: "Followings");

            migrationBuilder.RenameTable(
                name: "Followings",
                newName: "Following");

            migrationBuilder.RenameIndex(
                name: "IX_Followings_UserToFollowId",
                table: "Following",
                newName: "IX_Following_UserToFollowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Following",
                table: "Following",
                columns: new[] { "UserFollowingId", "UserToFollowId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Following_AspNetUsers_UserFollowingId",
                table: "Following",
                column: "UserFollowingId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Following_AspNetUsers_UserToFollowId",
                table: "Following",
                column: "UserToFollowId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
