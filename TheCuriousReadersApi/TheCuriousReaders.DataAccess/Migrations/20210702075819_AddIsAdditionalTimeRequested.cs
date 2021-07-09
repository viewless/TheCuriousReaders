using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCuriousReaders.DataAccess.Migrations
{
    public partial class AddIsAdditionalTimeRequested : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdditionalTimeRequested",
                table: "UserSubscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdditionalTimeRequested",
                table: "UserSubscriptions");
        }
    }
}
