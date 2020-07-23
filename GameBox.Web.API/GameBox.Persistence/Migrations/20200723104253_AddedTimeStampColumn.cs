using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameBox.Persistence.Migrations
{
    public partial class AddedTimeStampColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "Users",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "Roles",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "TimeStamp",
                table: "Orders",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "Messages",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "Games",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "TimeStamp",
                table: "Comments",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "Categories",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Categories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldRowVersion: true,
                oldNullable: true);
        }
    }
}
