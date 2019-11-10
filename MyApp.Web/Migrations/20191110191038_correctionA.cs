using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyApp.Web.Migrations
{
    public partial class correctionA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTypes_Companies_CompaniesId",
                table: "QuestionTypes");

            migrationBuilder.DropTable(
                name: "CompanyQuestionType");

            migrationBuilder.DropIndex(
                name: "IX_QuestionTypes_CompaniesId",
                table: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "CompaniesId",
                table: "QuestionTypes");

            migrationBuilder.CreateTable(
                name: "CQTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<int>(nullable: true),
                    QuestionTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CQTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CQTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CQTypes_QuestionTypes_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CQTypes_CompanyId",
                table: "CQTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CQTypes_QuestionTypeId",
                table: "CQTypes",
                column: "QuestionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CQTypes");

            migrationBuilder.AddColumn<int>(
                name: "CompaniesId",
                table: "QuestionTypes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyQuestionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<int>(nullable: false),
                    QuestionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyQuestionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyQuestionType_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyQuestionType_QuestionTypes_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTypes_CompaniesId",
                table: "QuestionTypes",
                column: "CompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyQuestionType_CompanyId",
                table: "CompanyQuestionType",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyQuestionType_QuestionTypeId",
                table: "CompanyQuestionType",
                column: "QuestionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTypes_Companies_CompaniesId",
                table: "QuestionTypes",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
