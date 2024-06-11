using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionDeModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_PedidoId",
                table: "CartItems",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Pedido_PedidoId",
                table: "CartItems",
                column: "PedidoId",
                principalTable: "Pedido",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Pedido_PedidoId",
                table: "CartItems");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_PedidoId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "CartItems");
        }
    }
}
