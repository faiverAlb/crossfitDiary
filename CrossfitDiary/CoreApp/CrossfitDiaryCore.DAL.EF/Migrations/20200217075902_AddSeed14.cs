using Microsoft.EntityFrameworkCore.Migrations;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class AddSeed14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "Abbreviation", "Title" },
                values: new object[,]
                {
                    { 171, "Pegboard alt PU", "Pegboard alt pull-up" },
                    { 172, "Pegboard toes to peg", "Pegboard toes to peg" },
                    { 173, "Rope anchors", "Rope anchors" },
                    { 174, "Clapping push-ups", "Clapping push-ups" },
                    { 175, "Barbell cuban rotation", "Barbell cuban rotation" },
                    { 176, "German hang", "German hang" },
                    { 177, "Prowler push", "Prowler push" }
                });

            migrationBuilder.InsertData(
                table: "ExerciseMeasure",
                columns: new[] { "Id", "ExerciseId", "ExerciseMeasureTypeId" },
                values: new object[,]
                {
                    { 417, 171, 2 },
                    { 418, 172, 2 },
                    { 419, 173, 2 },
                    { 420, 174, 2 },
                    { 421, 175, 2 },
                    { 422, 176, 2 },
                    { 423, 176, 11 },
                    { 424, 177, 2 },
                    { 425, 177, 11 },
                    { 426, 177, 3 },
                    { 427, 177, 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 419);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 421);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 424);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 425);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 426);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 177);
        }
    }
}
