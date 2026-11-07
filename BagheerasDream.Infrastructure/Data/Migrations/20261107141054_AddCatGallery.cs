using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BagheerasDream.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCatGallery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PedigreeImage",
                table: "Cats",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CatImage",
                columns: table => new
                {
                    CatImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatImage", x => x.CatImageId);
                    table.ForeignKey(
                        name: "FK_CatImage_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "CatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatImage_CatId_DisplayOrder",
                table: "CatImage",
                columns: new[] { "CatId", "DisplayOrder" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatImage");

            migrationBuilder.DropColumn(
                name: "PedigreeImage",
                table: "Cats");
        }
    }
}
