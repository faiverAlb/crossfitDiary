using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class AddAsNonBreakingSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AsNonBreakingSet",
                table: "RoutineComplex",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsNonBreakingSet",
                table: "RoutineComplex");
        }
    }
}
