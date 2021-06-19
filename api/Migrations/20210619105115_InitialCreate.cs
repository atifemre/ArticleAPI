using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "Reviews",
                newName: "ArticlesId");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Articles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ArticlesId",
                table: "Reviews",
                column: "ArticlesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Articles_ArticlesId",
                table: "Reviews",
                column: "ArticlesId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Articles_ArticlesId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ArticlesId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "ArticlesId",
                table: "Reviews",
                newName: "ArticleId");
        }
    }
}
