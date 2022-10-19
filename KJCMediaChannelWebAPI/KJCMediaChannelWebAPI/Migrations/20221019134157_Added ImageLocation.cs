using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KJCMediaChannelWebAPI.Migrations
{
    public partial class AddedImageLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLocation",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageLocation",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageLocation",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLocation",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageLocation",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ImageLocation",
                table: "Events");
        }
    }
}
