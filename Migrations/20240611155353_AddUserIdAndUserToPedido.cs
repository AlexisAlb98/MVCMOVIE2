using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdAndUserToPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pedido",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_UserId",
                table: "Pedido",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Users_UserId",
                table: "Pedido",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Users_UserId",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_UserId",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pedido");
        }
    }
}
