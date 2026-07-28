using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Migrations
{
    /// <inheritdoc />
    public partial class RenamingTablesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_carts_cartId",
                table: "cartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_productsVariant_variantId",
                table: "cartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_carts_users_userId",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_categories_categories_parentCategoryId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_orders_orderId",
                table: "orderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_productsVariant_variantId",
                table: "orderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_userId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_products_brands_BrandId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_productsVariant_products_ProductId",
                table: "productsVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_products_productId",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_userId",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reviews",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productsVariant",
                table: "productsVariant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderItems",
                table: "orderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_carts",
                table: "carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartItems",
                table: "cartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_brands",
                table: "brands");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "reviews",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "productsVariant",
                newName: "ProductsVariant");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "orderItems",
                newName: "OrderItems");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "carts",
                newName: "Carts");

            migrationBuilder.RenameTable(
                name: "cartItems",
                newName: "CartItems");

            migrationBuilder.RenameTable(
                name: "brands",
                newName: "Brands");

            migrationBuilder.RenameIndex(
                name: "IX_users_userName_email",
                table: "Users",
                newName: "IX_Users_userName_email");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_userId",
                table: "Reviews",
                newName: "IX_Reviews_userId");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_productId",
                table: "Reviews",
                newName: "IX_Reviews_productId");

            migrationBuilder.RenameIndex(
                name: "IX_productsVariant_ProductId",
                table: "ProductsVariant",
                newName: "IX_ProductsVariant_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_products_productName",
                table: "Products",
                newName: "IX_Products_productName");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_BrandId",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_userId",
                table: "Orders",
                newName: "IX_Orders_userId");

            migrationBuilder.RenameIndex(
                name: "IX_orderItems_variantId",
                table: "OrderItems",
                newName: "IX_OrderItems_variantId");

            migrationBuilder.RenameIndex(
                name: "IX_orderItems_orderId",
                table: "OrderItems",
                newName: "IX_OrderItems_orderId");

            migrationBuilder.RenameIndex(
                name: "IX_categories_parentCategoryId",
                table: "Categories",
                newName: "IX_Categories_parentCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_categories_categoryName",
                table: "Categories",
                newName: "IX_Categories_categoryName");

            migrationBuilder.RenameIndex(
                name: "IX_carts_userId",
                table: "Carts",
                newName: "IX_Carts_userId");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_variantId",
                table: "CartItems",
                newName: "IX_CartItems_variantId");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_cartId",
                table: "CartItems",
                newName: "IX_CartItems_cartId");

            migrationBuilder.RenameIndex(
                name: "IX_brands_brandName",
                table: "Brands",
                newName: "IX_Brands_brandName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "reviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsVariant",
                table: "ProductsVariant",
                column: "variantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "productId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "orderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "orderItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "categoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "cartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "cartItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "brandId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_cartId",
                table: "CartItems",
                column: "cartId",
                principalTable: "Carts",
                principalColumn: "cartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductsVariant_variantId",
                table: "CartItems",
                column: "variantId",
                principalTable: "ProductsVariant",
                principalColumn: "variantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_userId",
                table: "Carts",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_parentCategoryId",
                table: "Categories",
                column: "parentCategoryId",
                principalTable: "Categories",
                principalColumn: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_orderId",
                table: "OrderItems",
                column: "orderId",
                principalTable: "Orders",
                principalColumn: "orderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductsVariant_variantId",
                table: "OrderItems",
                column: "variantId",
                principalTable: "ProductsVariant",
                principalColumn: "variantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_userId",
                table: "Orders",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "brandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "categoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsVariant_Products_ProductId",
                table: "ProductsVariant",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_productId",
                table: "Reviews",
                column: "productId",
                principalTable: "Products",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_userId",
                table: "Reviews",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_cartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductsVariant_variantId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_userId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_parentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_orderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductsVariant_variantId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_userId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsVariant_Products_ProductId",
                table: "ProductsVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_productId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_userId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsVariant",
                table: "ProductsVariant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "reviews");

            migrationBuilder.RenameTable(
                name: "ProductsVariant",
                newName: "productsVariant");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "orders");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "orderItems");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "carts");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "cartItems");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "brands");

            migrationBuilder.RenameIndex(
                name: "IX_Users_userName_email",
                table: "users",
                newName: "IX_users_userName_email");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_userId",
                table: "reviews",
                newName: "IX_reviews_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_productId",
                table: "reviews",
                newName: "IX_reviews_productId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsVariant_ProductId",
                table: "productsVariant",
                newName: "IX_productsVariant_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_productName",
                table: "products",
                newName: "IX_products_productName");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "products",
                newName: "IX_products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "products",
                newName: "IX_products_BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_userId",
                table: "orders",
                newName: "IX_orders_userId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_variantId",
                table: "orderItems",
                newName: "IX_orderItems_variantId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_orderId",
                table: "orderItems",
                newName: "IX_orderItems_orderId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_parentCategoryId",
                table: "categories",
                newName: "IX_categories_parentCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_categoryName",
                table: "categories",
                newName: "IX_categories_categoryName");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_userId",
                table: "carts",
                newName: "IX_carts_userId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_variantId",
                table: "cartItems",
                newName: "IX_cartItems_variantId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_cartId",
                table: "cartItems",
                newName: "IX_cartItems_cartId");

            migrationBuilder.RenameIndex(
                name: "IX_Brands_brandName",
                table: "brands",
                newName: "IX_brands_brandName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviews",
                table: "reviews",
                column: "reviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productsVariant",
                table: "productsVariant",
                column: "variantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "productId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "orderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderItems",
                table: "orderItems",
                column: "orderItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "categoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_carts",
                table: "carts",
                column: "cartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartItems",
                table: "cartItems",
                column: "cartItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_brands",
                table: "brands",
                column: "brandId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_carts_cartId",
                table: "cartItems",
                column: "cartId",
                principalTable: "carts",
                principalColumn: "cartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_productsVariant_variantId",
                table: "cartItems",
                column: "variantId",
                principalTable: "productsVariant",
                principalColumn: "variantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carts_users_userId",
                table: "carts",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_categories_categories_parentCategoryId",
                table: "categories",
                column: "parentCategoryId",
                principalTable: "categories",
                principalColumn: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_orders_orderId",
                table: "orderItems",
                column: "orderId",
                principalTable: "orders",
                principalColumn: "orderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_productsVariant_variantId",
                table: "orderItems",
                column: "variantId",
                principalTable: "productsVariant",
                principalColumn: "variantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_userId",
                table: "orders",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_brands_BrandId",
                table: "products",
                column: "BrandId",
                principalTable: "brands",
                principalColumn: "brandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "categoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productsVariant_products_ProductId",
                table: "productsVariant",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_products_productId",
                table: "reviews",
                column: "productId",
                principalTable: "products",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_userId",
                table: "reviews",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
