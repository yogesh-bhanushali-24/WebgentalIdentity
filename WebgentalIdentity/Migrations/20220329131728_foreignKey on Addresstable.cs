using Microsoft.EntityFrameworkCore.Migrations;

namespace WebgentalIdentity.Migrations
{
    public partial class foreignKeyonAddresstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "addressModelsAddressId",
                table: "Orderss",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orderss_addressModelsAddressId",
                table: "Orderss",
                column: "addressModelsAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderss_addressModels_addressModelsAddressId",
                table: "Orderss",
                column: "addressModelsAddressId",
                principalTable: "addressModels",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderss_addressModels_addressModelsAddressId",
                table: "Orderss");

            migrationBuilder.DropIndex(
                name: "IX_Orderss_addressModelsAddressId",
                table: "Orderss");

            migrationBuilder.DropColumn(
                name: "addressModelsAddressId",
                table: "Orderss");
        }
    }
}
