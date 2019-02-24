using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class MoveAddPlanning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlanned",
                table: "CrossfitterWorkout");

            migrationBuilder.AddColumn<DateTime>(
                name: "PlanDate",
                table: "RoutineComplex",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanningLevel",
                table: "RoutineComplex",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanDate",
                table: "RoutineComplex");

            migrationBuilder.DropColumn(
                name: "PlanningLevel",
                table: "RoutineComplex");

            migrationBuilder.AddColumn<bool>(
                name: "IsPlanned",
                table: "CrossfitterWorkout",
                nullable: false,
                defaultValue: false);
        }
    }
}
