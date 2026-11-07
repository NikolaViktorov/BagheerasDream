using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BagheerasDream.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CheckPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatImage_Cats_CatId",
                table: "CatImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatImage",
                table: "CatImage");

            migrationBuilder.RenameTable(
                name: "CatImage",
                newName: "CatImages");

            migrationBuilder.RenameIndex(
                name: "IX_CatImage_CatId_DisplayOrder",
                table: "CatImages",
                newName: "IX_CatImages_CatId_DisplayOrder");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatImages_Cats_CatId",
                table: "CatImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatImages",
                table: "CatImages");

            migrationBuilder.RenameTable(
                name: "CatImages",
                newName: "CatImage");

            migrationBuilder.RenameIndex(
                name: "IX_CatImages_CatId_DisplayOrder",
                table: "CatImage",
                newName: "IX_CatImage_CatId_DisplayOrder");

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
        }
    }
}
