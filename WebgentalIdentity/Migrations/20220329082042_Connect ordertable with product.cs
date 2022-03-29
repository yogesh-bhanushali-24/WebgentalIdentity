using Microsoft.EntityFrameworkCore.Migrations;

namespace WebgentalIdentity.Migrations
{
    public partial class Connectordertablewithproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orderss_Pid",
                table: "Orderss",
                column: "Pid");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderss_products_Pid",
                table: "Orderss",
                column: "Pid",
                principalTable: "products",
                principalColumn: "Pid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderss_products_Pid",
                table: "Orderss");

            migrationBuilder.DropIndex(
                name: "IX_Orderss_Pid",
                table: "Orderss");
        }
    }
}
