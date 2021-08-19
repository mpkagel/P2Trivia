using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P2.DAL.Migrations.Project2
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "P2");

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "P2",
                columns: table => new
                {
                    QId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QCategory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('Multiple')"),
                    QRating = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    QAverageReview = table.Column<decimal>(type: "decimal(6,2)", nullable: true, defaultValueSql: "((0))"),
                    QString = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__CAB1462B3334A5BB", x => x.QId);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                schema: "P2",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizMaxScore = table.Column<int>(type: "int", nullable: false),
                    QuizDifficulty = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    QuizCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.QuizId);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                schema: "P2",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    QId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuizQues__27E9BAEC17616440", x => new { x.QuizId, x.QId });
                });

            migrationBuilder.CreateTable(
                name: "TUsers",
                schema: "P2",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PW = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreditCardNumber = table.Column<long>(type: "bigint", nullable: true),
                    PointTotal = table.Column<int>(type: "int", nullable: false),
                    AccountType = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TUsers__1788CC4CA51CD972", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                schema: "P2",
                columns: table => new
                {
                    AId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QId = table.Column<int>(type: "int", nullable: false),
                    AAnswer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Correct = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Answers__C6900628A24242C6", x => x.AId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QId",
                        column: x => x.QId,
                        principalSchema: "P2",
                        principalTable: "Questions",
                        principalColumn: "QId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "P2",
                columns: table => new
                {
                    RId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QId = table.Column<int>(type: "int", nullable: true),
                    QuizId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RRatings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__CAFF40D29D101EF8", x => x.RId);
                    table.ForeignKey(
                        name: "FK_Reviews_Question_QId",
                        column: x => x.QId,
                        principalSchema: "P2",
                        principalTable: "Questions",
                        principalColumn: "QId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "P2",
                        principalTable: "TUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserQuizzes",
                schema: "P2",
                columns: table => new
                {
                    UserQuizId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    QuizMaxScore = table.Column<int>(type: "int", nullable: false),
                    QuizActualScore = table.Column<int>(type: "int", nullable: false),
                    QuizDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuizzes", x => x.UserQuizId);
                    table.ForeignKey(
                        name: "FK_UserQuizes_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalSchema: "P2",
                        principalTable: "Quiz",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserQuizes_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "P2",
                        principalTable: "TUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                schema: "P2",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserQuizId = table.Column<int>(type: "int", nullable: false),
                    QId = table.Column<int>(type: "int", nullable: false),
                    UserAnswer = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Correct = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Results_Questions_QId",
                        column: x => x.QId,
                        principalSchema: "P2",
                        principalTable: "Questions",
                        principalColumn: "QId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_UserQuizzes_UserQuizId",
                        column: x => x.UserQuizId,
                        principalSchema: "P2",
                        principalTable: "UserQuizzes",
                        principalColumn: "UserQuizId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QId",
                schema: "P2",
                table: "Answers",
                column: "QId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_QId",
                schema: "P2",
                table: "Results",
                column: "QId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_UserQuizId",
                schema: "P2",
                table: "Results",
                column: "UserQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_QId",
                schema: "P2",
                table: "Reviews",
                column: "QId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                schema: "P2",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__TUsers__315DB9256B962BB8",
                schema: "P2",
                table: "TUsers",
                column: "CreditCardNumber",
                unique: true,
                filter: "[CreditCardNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__TUsers__536C85E4A3387B7C",
                schema: "P2",
                table: "TUsers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserQuizzes_QuizId",
                schema: "P2",
                table: "UserQuizzes",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuizzes_UserId",
                schema: "P2",
                table: "UserQuizzes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers",
                schema: "P2");

            migrationBuilder.DropTable(
                name: "QuizQuestions",
                schema: "P2");

            migrationBuilder.DropTable(
                name: "Results",
                schema: "P2");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "P2");

            migrationBuilder.DropTable(
                name: "UserQuizzes",
                schema: "P2");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "P2");

            migrationBuilder.DropTable(
                name: "Quiz",
                schema: "P2");

            migrationBuilder.DropTable(
                name: "TUsers",
                schema: "P2");
        }
    }
}
