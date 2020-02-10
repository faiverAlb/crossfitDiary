using Microsoft.EntityFrameworkCore.Migrations;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class SeedData10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "Abbreviation", "Title" },
                values: new object[] { 157, "BTN Jerk", "Behind the Neck Jerk" });

            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "Abbreviation", "Title" },
                values: new object[] { 158, "TU", "Tuck Ups" });

            migrationBuilder.InsertData(
                table: "ExerciseMeasure",
                columns: new[] { "Id", "ExerciseId", "ExerciseMeasureTypeId" },
                values: new object[,]
                {
                    { 383, 157, 2 },
                    { 384, 157, 3 },
                    { 385, 157, 8 },
                    { 386, 158, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 158);
        }
    }
}
