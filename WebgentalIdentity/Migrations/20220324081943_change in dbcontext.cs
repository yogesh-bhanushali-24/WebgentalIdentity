using Microsoft.EntityFrameworkCore.Migrations;

namespace WebgentalIdentity.Migrations
{
    public partial class changeindbcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressModel",
                table: "AddressModel");

            migrationBuilder.RenameTable(
                name: "AddressModel",
                newName: "addressModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_addressModels",
                table: "addressModels",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_addressModels",
                table: "addressModels");

            migrationBuilder.RenameTable(
                name: "addressModels",
                newName: "AddressModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressModel",
                table: "AddressModel",
                column: "AddressId");
        }
    }
}
