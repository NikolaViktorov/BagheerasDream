using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BagheerasDream.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLitterGallery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "LitterImage",
                columns: table => new
                {
                    LitterImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LitterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LitterImage", x => x.LitterImageId);
                    table.ForeignKey(
                        name: "FK_LitterImage_Litters_LitterId",
                        column: x => x.LitterId,
                        principalTable: "Litters",
                        principalColumn: "LitterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LitterImage_LitterId_DisplayOrder",
                table: "LitterImage",
                columns: new[] { "LitterId", "DisplayOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_CatImage_Cats_CatId",
                table: "CatImage",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatImage_Cats_CatId",
                table: "CatImage");

            migrationBuilder.DropTable(
                name: "LitterImage");

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
    }
}
