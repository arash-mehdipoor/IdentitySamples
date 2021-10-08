using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentitySamples.Migrations
{
    public partial class ApplicationUserForNewProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeMeli",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeMeli",
                table: "AspNetUsers");
        }
    }
}
