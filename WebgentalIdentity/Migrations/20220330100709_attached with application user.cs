using Microsoft.EntityFrameworkCore.Migrations;

namespace WebgentalIdentity.Migrations
{
    public partial class attachedwithapplicationuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orderss",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orderss_UserId",
                table: "Orderss",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderss_AspNetUsers_UserId",
                table: "Orderss",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderss_AspNetUsers_UserId",
                table: "Orderss");

            migrationBuilder.DropIndex(
                name: "IX_Orderss_UserId",
                table: "Orderss");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orderss",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
