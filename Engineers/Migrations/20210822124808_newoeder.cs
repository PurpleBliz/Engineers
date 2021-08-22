using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Engineers.Migrations
{
    public partial class newoeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersInWorks",
                table: "OrdersInWorks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrdersInWorks");

            migrationBuilder.DropColumn(
                name: "Order_Id",
                table: "OrdersInWorks");

            migrationBuilder.DropColumn(
                name: "ExecutorId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "OrdersInWorks",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_OwnerId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Finished_at",
                table: "OrdersInWorks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Started_at",
                table: "OrdersInWorks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrdersInWorks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InWorkId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersInWorks",
                table: "OrdersInWorks",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersInWorks_UserId",
                table: "OrdersInWorks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_OwnerId",
                table: "Orders",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersInWorks_AspNetUsers_UserId",
                table: "OrdersInWorks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersInWorks_Orders_OrderId",
                table: "OrdersInWorks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_OwnerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersInWorks_AspNetUsers_UserId",
                table: "OrdersInWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersInWorks_Orders_OrderId",
                table: "OrdersInWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersInWorks",
                table: "OrdersInWorks");

            migrationBuilder.DropIndex(
                name: "IX_OrdersInWorks_UserId",
                table: "OrdersInWorks");

            migrationBuilder.DropColumn(
                name: "Finished_at",
                table: "OrdersInWorks");

            migrationBuilder.DropColumn(
                name: "Started_at",
                table: "OrdersInWorks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrdersInWorks");

            migrationBuilder.DropColumn(
                name: "InWorkId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrdersInWorks",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OwnerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrdersInWorks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Order_Id",
                table: "OrdersInWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExecutorId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersInWorks",
                table: "OrdersInWorks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
