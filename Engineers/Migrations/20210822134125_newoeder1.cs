using Microsoft.EntityFrameworkCore.Migrations;

namespace Engineers.Migrations
{
    public partial class newoeder1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersInWorks_AspNetUsers_UserId",
                table: "OrdersInWorks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OrdersInWorks",
                newName: "ExecutorId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersInWorks_UserId",
                table: "OrdersInWorks",
                newName: "IX_OrdersInWorks_ExecutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersInWorks_AspNetUsers_ExecutorId",
                table: "OrdersInWorks",
                column: "ExecutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersInWorks_AspNetUsers_ExecutorId",
                table: "OrdersInWorks");

            migrationBuilder.RenameColumn(
                name: "ExecutorId",
                table: "OrdersInWorks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersInWorks_ExecutorId",
                table: "OrdersInWorks",
                newName: "IX_OrdersInWorks_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersInWorks_AspNetUsers_UserId",
                table: "OrdersInWorks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
