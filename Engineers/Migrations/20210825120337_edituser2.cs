using Microsoft.EntityFrameworkCore.Migrations;

namespace Engineers.Migrations
{
    public partial class edituser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinDescription",
                table: "AspNetUsers",
                newName: "Qualification");

            migrationBuilder.RenameColumn(
                name: "Fulldescription",
                table: "AspNetUsers",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Education",
                table: "AspNetUsers",
                newName: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qualification",
                table: "AspNetUsers",
                newName: "MinDescription");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AspNetUsers",
                newName: "Fulldescription");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "AspNetUsers",
                newName: "Education");
        }
    }
}
