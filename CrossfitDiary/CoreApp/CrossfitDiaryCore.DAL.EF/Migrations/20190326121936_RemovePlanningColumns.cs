using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class RemovePlanningColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanDate",
                table: "RoutineComplex");

            migrationBuilder.DropColumn(
                name: "PlanningLevel",
                table: "RoutineComplex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PlanDate",
                table: "RoutineComplex",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanningLevel",
                table: "RoutineComplex",
                nullable: true);
        }
    }
}
