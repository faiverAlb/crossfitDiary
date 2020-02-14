using Microsoft.EntityFrameworkCore.Migrations;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class SeedData13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "Abbreviation", "Title" },
                values: new object[,]
                {
                    { 164, "BTN Push press", "BTN Push press" },
                    { 165, "DB hang squat clean", "DB hang squat clean" },
                    { 166, "Burpee pull-up", "Burpee pull-up" },
                    { 167, "Deficit push-up", "Deficit push-up" },
                    { 168, "Sumo deadlift", "Sumo deadlift" },
                    { 169, "Single arm overhead walking (DB/KB)", "Single arm overhead walking (DB/KB)" },
                    { 170, "GHD barbell back ext", "GHD barbell back extension" }
                });

            migrationBuilder.InsertData(
                table: "ExerciseMeasure",
                columns: new[] { "Id", "ExerciseId", "ExerciseMeasureTypeId" },
                values: new object[,]
                {
                    { 400, 164, 2 },
                    { 414, 170, 2 },
                    { 413, 169, 8 },
                    { 412, 169, 3 },
                    { 411, 169, 2 },
                    { 410, 168, 8 },
                    { 409, 168, 3 },
                    { 415, 170, 3 },
                    { 408, 168, 2 },
                    { 406, 166, 2 },
                    { 405, 165, 8 },
                    { 404, 165, 3 },
                    { 403, 165, 2 },
                    { 402, 164, 8 },
                    { 401, 164, 3 },
                    { 407, 167, 2 },
                    { 416, 170, 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 409);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 413);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "ExerciseMeasure",
                keyColumn: "Id",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 170);
        }
    }
}
