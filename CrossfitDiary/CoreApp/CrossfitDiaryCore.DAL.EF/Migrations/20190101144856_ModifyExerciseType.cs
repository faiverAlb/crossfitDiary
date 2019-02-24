using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class ModifyExerciseType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseMeasure_ExerciseMeasureType_ExerciseMeasureTypeId",
                table: "ExerciseMeasure");

            migrationBuilder.DropTable(
                name: "ExerciseMeasureType");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseMeasure_ExerciseMeasureTypeId",
                table: "ExerciseMeasure");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciseMeasureType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedUtc = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Description = table.Column<string>(nullable: false),
                    MeasureType = table.Column<int>(nullable: false),
                    ShortMeasureDescription = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseMeasureType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMeasure_ExerciseMeasureTypeId",
                table: "ExerciseMeasure",
                column: "ExerciseMeasureTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseMeasure_ExerciseMeasureType_ExerciseMeasureTypeId",
                table: "ExerciseMeasure",
                column: "ExerciseMeasureTypeId",
                principalTable: "ExerciseMeasureType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
