using Microsoft.EntityFrameworkCore.Migrations;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class SeedData4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExerciseMeasure",
                columns: new[] { "Id", "ExerciseId", "ExerciseMeasureTypeId" },
                values: new object[] { 345, 114, 11 });

            migrationBuilder.InsertData(
                table: "ExerciseMeasure",
                columns: new[] { "Id", "ExerciseId", "ExerciseMeasureTypeId" },
                values: new object[] { 346, 46, 11 });

            migrationBuilder.InsertData(
                table: "ExerciseMeasure",
                columns: new[] { "Id", "ExerciseId", "ExerciseMeasureTypeId" },
                values: new object[] { 347, 47, 11 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 347);
        }
    }
}
