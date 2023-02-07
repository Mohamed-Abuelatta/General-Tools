using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tools.Migrations
{
    public partial class xxxx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustAge",
                table: "customers");

            migrationBuilder.AddColumn<int>(
                name: "AgeId",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Age",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Age", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_AgeId",
                table: "customers",
                column: "AgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_Age_AgeId",
                table: "customers",
                column: "AgeId",
                principalTable: "Age",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_Age_AgeId",
                table: "customers");

            migrationBuilder.DropTable(
                name: "Age");

            migrationBuilder.DropIndex(
                name: "IX_customers_AgeId",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "AgeId",
                table: "customers");

            migrationBuilder.AddColumn<string>(
                name: "CustAge",
                table: "customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
