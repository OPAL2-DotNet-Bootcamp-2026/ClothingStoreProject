using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    brandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brandName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    logoUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    websiteUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.brandId);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    imageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    parentCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.categoryId);
                    table.ForeignKey(
                        name: "FK_categories_categories_parentCategoryId",
                        column: x => x.parentCategoryId,
                        principalTable: "categories",
                        principalColumn: "categoryId");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    registrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    basePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    material = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    clothingStyle = table.Column<int>(type: "int", nullable: false),
                    season = table.Column<int>(type: "int", nullable: true),
                    careInstructions = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.productId);
                    table.ForeignKey(
                        name: "FK_products_brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "brands",
                        principalColumn: "brandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.cartId);
                    table.ForeignKey(
                        name: "FK_carts_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    totalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    shippingAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderId);
                    table.ForeignKey(
                        name: "FK_orders_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productsVariant",
                columns: table => new
                {
                    variantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    sku = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    size = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stockQuantity = table.Column<int>(type: "int", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productsVariant", x => x.variantId);
                    table.ForeignKey(
                        name: "FK_productsVariant_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    reviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rating = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    reviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.reviewId);
                    table.ForeignKey(
                        name: "FK_reviews_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reviews_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cartItems",
                columns: table => new
                {
                    cartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartId = table.Column<int>(type: "int", nullable: false),
                    variantId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    addedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartItems", x => x.cartItemId);
                    table.ForeignKey(
                        name: "FK_cartItems_carts_cartId",
                        column: x => x.cartId,
                        principalTable: "carts",
                        principalColumn: "cartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cartItems_productsVariant_variantId",
                        column: x => x.variantId,
                        principalTable: "productsVariant",
                        principalColumn: "variantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderItems",
                columns: table => new
                {
                    orderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderId = table.Column<int>(type: "int", nullable: false),
                    variantId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems", x => x.orderItemId);
                    table.ForeignKey(
                        name: "FK_orderItems_orders_orderId",
                        column: x => x.orderId,
                        principalTable: "orders",
                        principalColumn: "orderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderItems_productsVariant_variantId",
                        column: x => x.variantId,
                        principalTable: "productsVariant",
                        principalColumn: "variantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_brands_brandName",
                table: "brands",
                column: "brandName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_cartId",
                table: "cartItems",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_variantId",
                table: "cartItems",
                column: "variantId");

            migrationBuilder.CreateIndex(
                name: "IX_carts_userId",
                table: "carts",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_categoryName",
                table: "categories",
                column: "categoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_parentCategoryId",
                table: "categories",
                column: "parentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_orderId",
                table: "orderItems",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_variantId",
                table: "orderItems",
                column: "variantId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_userId",
                table: "orders",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_products_BrandId",
                table: "products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryId",
                table: "products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_products_productName",
                table: "products",
                column: "productName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_productsVariant_ProductId",
                table: "productsVariant",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_productId",
                table: "reviews",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_userId",
                table: "reviews",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_users_userName_email",
                table: "users",
                columns: new[] { "userName", "email" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartItems");

            migrationBuilder.DropTable(
                name: "orderItems");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "productsVariant");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
