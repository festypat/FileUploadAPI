using Microsoft.EntityFrameworkCore.Migrations;

namespace FileUpload.Persistence.Migrations
{
    public partial class updatedstatisticstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Result",
                table: "NumberStatistics",
                newName: "ResultTwo");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "NumberStatistics",
                newName: "ResultOne");

            migrationBuilder.AddColumn<string>(
                name: "CategoryOne",
                table: "NumberStatistics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryTwo",
                table: "NumberStatistics",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryOne",
                table: "NumberStatistics");

            migrationBuilder.DropColumn(
                name: "CategoryTwo",
                table: "NumberStatistics");

            migrationBuilder.RenameColumn(
                name: "ResultTwo",
                table: "NumberStatistics",
                newName: "Result");

            migrationBuilder.RenameColumn(
                name: "ResultOne",
                table: "NumberStatistics",
                newName: "Category");
        }
    }
}
