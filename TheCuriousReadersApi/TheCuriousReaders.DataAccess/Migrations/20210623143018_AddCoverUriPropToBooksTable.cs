using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCuriousReaders.DataAccess.Migrations
{
    public partial class AddCoverUriPropToBooksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "CoverUri",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUri",
                table: "Books");

            migrationBuilder.AddColumn<byte[]>(
                name: "Cover",
                table: "Books",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
