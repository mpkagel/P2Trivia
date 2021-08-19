using Microsoft.EntityFrameworkCore.Migrations;

namespace P2.DAL.Migrations.Project2
{
    public partial class Removecreditcardnumbercolumnfromuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__TUsers__315DB9256B962BB8",
                schema: "P2",
                table: "TUsers");

            migrationBuilder.DropColumn(
                name: "CreditCardNumber",
                schema: "P2",
                table: "TUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreditCardNumber",
                schema: "P2",
                table: "TUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ__TUsers__315DB9256B962BB8",
                schema: "P2",
                table: "TUsers",
                column: "CreditCardNumber",
                unique: true,
                filter: "[CreditCardNumber] IS NOT NULL");
        }
    }
}
