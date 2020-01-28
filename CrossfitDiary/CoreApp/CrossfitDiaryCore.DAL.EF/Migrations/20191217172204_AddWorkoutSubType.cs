using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class AddWorkoutSubType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WodSubType",
                table: "PlanningHistory",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "WodSubType",
                table: "CrossfitterWorkout",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WodSubType",
                table: "PlanningHistory");

            migrationBuilder.DropColumn(
                name: "WodSubType",
                table: "CrossfitterWorkout");
        }
    }
}
