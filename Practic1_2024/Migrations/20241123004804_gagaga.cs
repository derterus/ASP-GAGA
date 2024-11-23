using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practic1_2024.Migrations
{
    /// <inheritdoc />
    public partial class gagaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCharacteristics_Characteristics_Characteristicid",
                table: "ProductCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_ProductCharacteristics_Characteristicid",
                table: "ProductCharacteristics");

            migrationBuilder.DropColumn(
                name: "Characteristicid",
                table: "ProductCharacteristics");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCharacteristics_characteristic_id",
                table: "ProductCharacteristics",
                column: "characteristic_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCharacteristics_Characteristics_characteristic_id",
                table: "ProductCharacteristics",
                column: "characteristic_id",
                principalTable: "Characteristics",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCharacteristics_Characteristics_characteristic_id",
                table: "ProductCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_ProductCharacteristics_characteristic_id",
                table: "ProductCharacteristics");

            migrationBuilder.AddColumn<int>(
                name: "Characteristicid",
                table: "ProductCharacteristics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCharacteristics_Characteristicid",
                table: "ProductCharacteristics",
                column: "Characteristicid");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCharacteristics_Characteristics_Characteristicid",
                table: "ProductCharacteristics",
                column: "Characteristicid",
                principalTable: "Characteristics",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
