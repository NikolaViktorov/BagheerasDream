using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BagheerasDream.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoOfLitterToLitter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoOfLitter",
                table: "Litters",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoOfLitter",
                table: "Litters");
        }
    }
}
