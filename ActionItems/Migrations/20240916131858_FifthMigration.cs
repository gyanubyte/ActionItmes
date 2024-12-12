using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActionItems.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tasks",
                newName: "Subject");

            migrationBuilder.AddColumn<string>(
                name: "requester",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requester",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Tasks",
                newName: "Title");
        }
    }
}
