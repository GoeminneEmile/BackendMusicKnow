using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Backend.Models.Migrations
{
    public partial class UpdatedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ApiLogs",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ApiLogs",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Questions",
                table: "ApiLogs",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedDescription",
                table: "ApiDeletes",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedDifficulty",
                table: "ApiDeletes",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedName",
                table: "ApiDeletes",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedQuestionCount",
                table: "ApiDeletes",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BonusPoints",
                columns: table => new
                {
                    BonusId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusPoints", x => x.BonusId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusPoints");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ApiLogs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ApiLogs");

            migrationBuilder.DropColumn(
                name: "Questions",
                table: "ApiLogs");

            migrationBuilder.DropColumn(
                name: "DeletedDescription",
                table: "ApiDeletes");

            migrationBuilder.DropColumn(
                name: "DeletedDifficulty",
                table: "ApiDeletes");

            migrationBuilder.DropColumn(
                name: "DeletedName",
                table: "ApiDeletes");

            migrationBuilder.DropColumn(
                name: "DeletedQuestionCount",
                table: "ApiDeletes");
        }
    }
}
