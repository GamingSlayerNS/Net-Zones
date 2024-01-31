using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Forums.Data.Migrations
{
    public partial class addingCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Zone",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Zone");
        }
    }
}
