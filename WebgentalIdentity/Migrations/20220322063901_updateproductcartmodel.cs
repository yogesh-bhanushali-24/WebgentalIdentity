using Microsoft.EntityFrameworkCore.Migrations;

namespace WebgentalIdentity.Migrations
{
    public partial class updateproductcartmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qauntity",
                table: "carts",
                newName: "Qauntity_Cart");

            migrationBuilder.CreateIndex(
                name: "IX_carts_Pid",
                table: "carts",
                column: "Pid");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_products_Pid",
                table: "carts",
                column: "Pid",
                principalTable: "products",
                principalColumn: "Pid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_products_Pid",
                table: "carts");

            migrationBuilder.DropIndex(
                name: "IX_carts_Pid",
                table: "carts");

            migrationBuilder.RenameColumn(
                name: "Qauntity_Cart",
                table: "carts",
                newName: "Qauntity");
        }
    }
}
