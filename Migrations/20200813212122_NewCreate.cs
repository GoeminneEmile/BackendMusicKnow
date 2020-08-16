using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Backend.Data.Migrations
{
    public partial class NewCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScoreId",
                table: "Scores",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Scores",
                table: "Scores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuizId",
                table: "Quizzes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuizName",
                table: "Quizzes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Quizzes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionId",
                table: "Questions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AnswerId",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuizId",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerId",
                table: "Answer",
                type: "NVARCHAR(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answers",
                table: "Answer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScoreId",
                table: "Answer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scores",
                table: "Scores",
                column: "ScoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes",
                column: "QuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "AnswerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Scores",
                table: "Scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "ScoreId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Scores",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuizName",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Answers",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "ScoreId",
                table: "Answer");
        }
    }
}
