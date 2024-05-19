using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "productgroups");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "productgroups",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
