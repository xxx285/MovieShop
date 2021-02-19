using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieShop.Infrastructure.Migrations
{
    public partial class AddConstraintsToPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Purchase_UserId",
                table: "Purchase");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseNumber",
                table: "Purchase",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_UserId_MovieId",
                table: "Purchase",
                columns: new[] { "UserId", "MovieId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Purchase_UserId_MovieId",
                table: "Purchase");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseNumber",
                table: "Purchase",
                type: "UniqueIdentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_UserId",
                table: "Purchase",
                column: "UserId");
        }
    }
}
