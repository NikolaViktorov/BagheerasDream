using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BagheerasDream.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHappyFamilies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HappyFamilies",
                columns: table => new
                {
                    HappyFamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HappyFamilies", x => x.HappyFamilyId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HappyFamilies");
        }
    }
}
