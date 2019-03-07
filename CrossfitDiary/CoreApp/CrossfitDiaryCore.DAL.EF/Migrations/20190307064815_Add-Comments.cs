using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class AddComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "RoutineComplex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "CrossfitterWorkout",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "RoutineComplex");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "CrossfitterWorkout");
        }
    }
}
