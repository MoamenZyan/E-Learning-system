using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning_Platform_API.Migrations
{
    /// <inheritdoc />
    public partial class examquestionrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Questions_ExamId",
                table: "ExamQuestions");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_QuestionId",
                table: "ExamQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_Questions_QuestionId",
                table: "ExamQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Questions_QuestionId",
                table: "ExamQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ExamQuestions_QuestionId",
                table: "ExamQuestions");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_Questions_ExamId",
                table: "ExamQuestions",
                column: "ExamId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}
