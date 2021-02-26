using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieShop.Infrastructure.Migrations
{
    public partial class ChangeColumnNamesInUserRoleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_UserId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_RoleId",
                table: "UserRole");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
