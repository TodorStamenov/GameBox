using Microsoft.EntityFrameworkCore.Migrations;

namespace GameBox.Data.Migrations
{
    public partial class AddedOrderCountColumnInOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderCount",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCount",
                table: "Games");
        }
    }
}
