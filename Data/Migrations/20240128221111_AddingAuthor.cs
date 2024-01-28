using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Forums.Data.Migrations
{
    public partial class AddingAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Zone",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Zone");
        }
    }
}
