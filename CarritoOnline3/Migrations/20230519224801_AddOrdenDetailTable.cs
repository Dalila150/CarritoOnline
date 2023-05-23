using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarritoOnline3.Migrations
{
    public partial class AddOrdenDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Marca_BrandID",
                table: "Producto");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Pedido_OrderID",
                table: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Producto",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Producto_OrderID",
                table: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marca",
                table: "Marca");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Producto");

            migrationBuilder.RenameTable(
                name: "Producto",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Pedido",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Marca",
                newName: "Brand");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_BrandID",
                table: "Product",
                newName: "IX_Product_BrandID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brand",
                table: "Brand",
                column: "BrandID");

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderID",
                table: "OrderDetail",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductID",
                table: "OrderDetail",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandID",
                table: "Product",
                column: "BrandID",
                principalTable: "Brand",
                principalColumn: "BrandID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandID",
                table: "Product");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brand",
                table: "Brand");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Producto");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Pedido");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Marca");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BrandID",
                table: "Producto",
                newName: "IX_Producto_BrandID");

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "Producto",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Producto",
                table: "Producto",
                column: "ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido",
                column: "OrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marca",
                table: "Marca",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_OrderID",
                table: "Producto",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Marca_BrandID",
                table: "Producto",
                column: "BrandID",
                principalTable: "Marca",
                principalColumn: "BrandID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Pedido_OrderID",
                table: "Producto",
                column: "OrderID",
                principalTable: "Pedido",
                principalColumn: "OrderID");
        }
    }
}
