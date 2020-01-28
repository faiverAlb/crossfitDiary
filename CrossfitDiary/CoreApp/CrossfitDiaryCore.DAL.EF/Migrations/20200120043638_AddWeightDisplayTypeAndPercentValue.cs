using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class AddWeightDisplayTypeAndPercentValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeightDisplayType",
                table: "RoutineSimple",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "WeightPercentValue",
                table: "RoutineSimple",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeightDisplayType",
                table: "RoutineSimple");

            migrationBuilder.DropColumn(
                name: "WeightPercentValue",
                table: "RoutineSimple");
        }
    }
}
