using Microsoft.EntityFrameworkCore.Migrations;

namespace WebgentalIdentity.Migrations
{
    public partial class DirectOrdertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "directOrders",
                columns: table => new
                {
                    DirectOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pid = table.Column<int>(type: "int", nullable: false),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qauntity_Cart = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directOrders", x => x.DirectOrderId);
                    table.ForeignKey(
                        name: "FK_directOrders_products_Pid",
                        column: x => x.Pid,
                        principalTable: "products",
                        principalColumn: "Pid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orderss_AddressId",
                table: "Orderss",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_directOrders_Pid",
                table: "directOrders",
                column: "Pid");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderss_addressModels_AddressId",
                table: "Orderss",
                column: "AddressId",
                principalTable: "addressModels",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderss_addressModels_AddressId",
                table: "Orderss");

            migrationBuilder.DropTable(
                name: "directOrders");

            migrationBuilder.DropIndex(
                name: "IX_Orderss_AddressId",
                table: "Orderss");

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
    }
}
