using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    /// <inheritdoc />
    public partial class migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCategory_Category_CategoryId",
                table: "SupplierCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCategory_Supplier_SupplierId",
                table: "SupplierCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierInventory_Products_ProductId",
                table: "SupplierInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierInventory_Supplier_SupplierId",
                table: "SupplierInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierInventory",
                table: "SupplierInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierCategory",
                table: "SupplierCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "SupplierInventory",
                newName: "SupplierInventories");

            migrationBuilder.RenameTable(
                name: "SupplierCategory",
                newName: "SupplierCategories");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierInventory_SupplierId",
                table: "SupplierInventories",
                newName: "IX_SupplierInventories_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierInventory_ProductId",
                table: "SupplierInventories",
                newName: "IX_SupplierInventories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierCategory_SupplierId",
                table: "SupplierCategories",
                newName: "IX_SupplierCategories_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierCategory_CategoryId",
                table: "SupplierCategories",
                newName: "IX_SupplierCategories_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierInventories",
                table: "SupplierInventories",
                column: "SupplierInventoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierCategories",
                table: "SupplierCategories",
                column: "SupplierCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCategories_Categories_CategoryId",
                table: "SupplierCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCategories_Suppliers_SupplierId",
                table: "SupplierCategories",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierInventories_Products_ProductId",
                table: "SupplierInventories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierInventories_Suppliers_SupplierId",
                table: "SupplierInventories",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCategories_Categories_CategoryId",
                table: "SupplierCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCategories_Suppliers_SupplierId",
                table: "SupplierCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierInventories_Products_ProductId",
                table: "SupplierInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierInventories_Suppliers_SupplierId",
                table: "SupplierInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierInventories",
                table: "SupplierInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierCategories",
                table: "SupplierCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supplier");

            migrationBuilder.RenameTable(
                name: "SupplierInventories",
                newName: "SupplierInventory");

            migrationBuilder.RenameTable(
                name: "SupplierCategories",
                newName: "SupplierCategory");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierInventories_SupplierId",
                table: "SupplierInventory",
                newName: "IX_SupplierInventory_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierInventories_ProductId",
                table: "SupplierInventory",
                newName: "IX_SupplierInventory_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierCategories_SupplierId",
                table: "SupplierCategory",
                newName: "IX_SupplierCategory_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierCategories_CategoryId",
                table: "SupplierCategory",
                newName: "IX_SupplierCategory_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierInventory",
                table: "SupplierInventory",
                column: "SupplierInventoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierCategory",
                table: "SupplierCategory",
                column: "SupplierCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCategory_Category_CategoryId",
                table: "SupplierCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCategory_Supplier_SupplierId",
                table: "SupplierCategory",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierInventory_Products_ProductId",
                table: "SupplierInventory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierInventory_Supplier_SupplierId",
                table: "SupplierInventory",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId");
        }
    }
}
