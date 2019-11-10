using Microsoft.EntityFrameworkCore.Migrations;

namespace MyApp.Web.Migrations
{
    public partial class correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompaniesId",
                table: "QuestionTypes",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdSubject",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTypes_CompaniesId",
                table: "QuestionTypes",
                column: "CompaniesId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTypes_Companies_CompaniesId",
                table: "QuestionTypes",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTypes_Companies_CompaniesId",
                table: "QuestionTypes");

            migrationBuilder.DropIndex(
                name: "IX_QuestionTypes_CompaniesId",
                table: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "CompaniesId",
                table: "QuestionTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "IdSubject",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
