using Microsoft.EntityFrameworkCore.Migrations;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class AddSecondsForRoutineSimple : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Seconds",
                table: "RoutineSimple",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seconds",
                table: "RoutineSimple");
        }
    }
}
