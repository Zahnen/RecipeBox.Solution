using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBox.Migrations
{
    public partial class Secondary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_AspNetUsers_UserId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientId",
                table: "RecipeIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_UserId",
                table: "Ingredients",
                newName: "IX_Ingredients_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_AspNetUsers_UserId",
                table: "Ingredients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Ingredients_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_AspNetUsers_UserId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Ingredients_IngredientId",
                table: "RecipeIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_UserId",
                table: "Ingredient",
                newName: "IX_Ingredient_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_AspNetUsers_UserId",
                table: "Ingredient",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
