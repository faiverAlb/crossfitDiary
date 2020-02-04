using Microsoft.EntityFrameworkCore.Migrations;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class SeedData6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "Abbreviation", "Title" },
                values: new object[,]
                {
                    { 145, "Jump to hollow", "Jump to hollow" },
                    { 146, "Kipping PU", "Kipping pull-up" },
                    { 147, "Butterfly PU", "Butterfly pull-up" },
                    { 148, "Butterfly PU rhythm drill", "Butterfly pull-up rhythm drill" }
                });

            migrationBuilder.InsertData(
                table: "ExerciseMeasure",
                columns: new[] { "Id", "ExerciseId", "ExerciseMeasureTypeId" },
                values: new object[,]
                {
                    { 355, 145, 2 },
                    { 356, 146, 2 },
                    { 357, 147, 2 },
                    { 358, 148, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 148);
        }
    }
}
