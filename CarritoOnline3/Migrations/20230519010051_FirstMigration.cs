using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarritoOnline3.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    BrandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.BrandID);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Producto_Marca_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Marca",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producto_Pedido_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Pedido",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producto_BrandID",
                table: "Producto",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_OrderID",
                table: "Producto",
                column: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Pedido");
        }
    }
}
