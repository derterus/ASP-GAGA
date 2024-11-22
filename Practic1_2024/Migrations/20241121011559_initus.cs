using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practic1_2024.Migrations
{
    /// <inheritdoc />
    public partial class initus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCharacteristics_Characteristics_CharacteristicId",
                table: "ProductCharacteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCharacteristics_Products_ProductId",
                table: "ProductCharacteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Manufacturers_ManufacturerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reviews",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reviews",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Reviews",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                newName: "IX_Reviews_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                newName: "IX_Reviews_product_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ManufacturerId",
                table: "Products",
                newName: "manufacturer_id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products",
                newName: "IX_Products_manufacturer_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_category_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductImages",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductImages",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_product_id");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ProductCharacteristics",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "CharacteristicId",
                table: "ProductCharacteristics",
                newName: "Characteristicid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductCharacteristics",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductCharacteristics",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCharacteristics_CharacteristicId",
                table: "ProductCharacteristics",
                newName: "IX_ProductCharacteristics_Characteristicid");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCharacteristics_ProductId",
                table: "ProductCharacteristics",
                newName: "IX_ProductCharacteristics_product_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "Сумма");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "Статус");

            migrationBuilder.RenameColumn(
                name: "ShippingDate",
                table: "Orders",
                newName: "Дата_обновления");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "Дата_создания");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_user_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderItems",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItems",
                newName: "Количество");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderItems",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItems",
                newName: "Цена_за_единицу");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderItems",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                newName: "IX_OrderItems_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_order_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Manufacturers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Characteristics",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "characteristic_id",
                table: "ProductCharacteristics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_order_id",
                table: "OrderItems",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_product_id",
                table: "OrderItems",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_user_id",
                table: "Orders",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCharacteristics_Characteristics_Characteristicid",
                table: "ProductCharacteristics",
                column: "Characteristicid",
                principalTable: "Characteristics",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCharacteristics_Products_product_id",
                table: "ProductCharacteristics",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_product_id",
                table: "ProductImages",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_category_id",
                table: "Products",
                column: "category_id",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Manufacturers_manufacturer_id",
                table: "Products",
                column: "manufacturer_id",
                principalTable: "Manufacturers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_product_id",
                table: "Reviews",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_user_id",
                table: "Reviews",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_order_id",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_product_id",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_user_id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCharacteristics_Characteristics_Characteristicid",
                table: "ProductCharacteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCharacteristics_Products_product_id",
                table: "ProductCharacteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_product_id",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_category_id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Manufacturers_manufacturer_id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_product_id",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_user_id",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "characteristic_id",
                table: "ProductCharacteristics");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Reviews",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Reviews",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "Reviews",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_user_id",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_product_id",
                table: "Reviews",
                newName: "IX_Reviews_ProductId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "manufacturer_id",
                table: "Products",
                newName: "ManufacturerId");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_manufacturer_id",
                table: "Products",
                newName: "IX_Products_ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_category_id",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProductImages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "ProductImages",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_product_id",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "ProductCharacteristics",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Characteristicid",
                table: "ProductCharacteristics",
                newName: "CharacteristicId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProductCharacteristics",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "ProductCharacteristics",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCharacteristics_Characteristicid",
                table: "ProductCharacteristics",
                newName: "IX_ProductCharacteristics_CharacteristicId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCharacteristics_product_id",
                table: "ProductCharacteristics",
                newName: "IX_ProductCharacteristics_ProductId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Сумма",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "Статус",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Дата_создания",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "Дата_обновления",
                table: "Orders",
                newName: "ShippingDate");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_user_id",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OrderItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Цена_за_единицу",
                table: "OrderItems",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Количество",
                table: "OrderItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "OrderItems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "OrderItems",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_product_id",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_order_id",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Manufacturers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Characteristics",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categories",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCharacteristics_Characteristics_CharacteristicId",
                table: "ProductCharacteristics",
                column: "CharacteristicId",
                principalTable: "Characteristics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCharacteristics_Products_ProductId",
                table: "ProductCharacteristics",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Manufacturers_ManufacturerId",
                table: "Products",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
