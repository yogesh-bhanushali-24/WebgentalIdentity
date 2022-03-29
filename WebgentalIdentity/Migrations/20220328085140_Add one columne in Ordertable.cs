using Microsoft.EntityFrameworkCore.Migrations;

namespace WebgentalIdentity.Migrations
{
    public partial class AddonecolumneinOrdertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressId",
                table: "Orderss",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Orderss");
        }
    }
}
