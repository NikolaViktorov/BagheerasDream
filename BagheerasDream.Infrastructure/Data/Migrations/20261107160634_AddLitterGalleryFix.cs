using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BagheerasDream.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLitterGalleryFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatImage_Cats_CatId",
                table: "CatImage");

            migrationBuilder.DropForeignKey(
                name: "FK_LitterImage_Litters_LitterId",
                table: "LitterImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LitterImage",
                table: "LitterImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatImage",
                table: "CatImage");

            migrationBuilder.RenameTable(
                name: "LitterImage",
                newName: "LitterImages");

            migrationBuilder.RenameTable(
                name: "CatImage",
                newName: "CatImages");

            migrationBuilder.RenameIndex(
                name: "IX_LitterImage_LitterId_DisplayOrder",
                table: "LitterImages",
                newName: "IX_LitterImages_LitterId_DisplayOrder");

            migrationBuilder.RenameIndex(
                name: "IX_CatImage_CatId_DisplayOrder",
                table: "CatImages",
                newName: "IX_CatImages_CatId_DisplayOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LitterImages",
                table: "LitterImages",
                column: "LitterImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatImages",
                table: "CatImages",
                column: "CatImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatImages_Cats_CatId",
                table: "CatImages",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LitterImages_Litters_LitterId",
                table: "LitterImages",
                column: "LitterId",
                principalTable: "Litters",
                principalColumn: "LitterId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatImages_Cats_CatId",
                table: "CatImages");

            migrationBuilder.DropForeignKey(
                name: "FK_LitterImages_Litters_LitterId",
                table: "LitterImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LitterImages",
                table: "LitterImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatImages",
                table: "CatImages");

            migrationBuilder.RenameTable(
                name: "LitterImages",
                newName: "LitterImage");

            migrationBuilder.RenameTable(
                name: "CatImages",
                newName: "CatImage");

            migrationBuilder.RenameIndex(
                name: "IX_LitterImages_LitterId_DisplayOrder",
                table: "LitterImage",
                newName: "IX_LitterImage_LitterId_DisplayOrder");

            migrationBuilder.RenameIndex(
                name: "IX_CatImages_CatId_DisplayOrder",
                table: "CatImage",
                newName: "IX_CatImage_CatId_DisplayOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LitterImage",
                table: "LitterImage",
                column: "LitterImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatImage",
                table: "CatImage",
                column: "CatImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatImage_Cats_CatId",
                table: "CatImage",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LitterImage_Litters_LitterId",
                table: "LitterImage",
                column: "LitterId",
                principalTable: "Litters",
                principalColumn: "LitterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
