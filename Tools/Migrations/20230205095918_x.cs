using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tools.Migrations
{
    public partial class x : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustCity",
                table: "customers");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_CityId",
                table: "customers",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_City_CityId",
                table: "customers",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_City_CityId",
                table: "customers");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropIndex(
                name: "IX_customers_CityId",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "customers");

            migrationBuilder.AddColumn<string>(
                name: "CustCity",
                table: "customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
