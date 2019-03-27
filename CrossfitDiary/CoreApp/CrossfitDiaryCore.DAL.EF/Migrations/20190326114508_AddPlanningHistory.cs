using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class AddPlanningHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanningHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedUtc = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    PlanningDate = table.Column<DateTime>(nullable: false),
                    PlanningLevel = table.Column<int>(nullable: false),
                    RoutineComplexId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanningHistory_RoutineComplex_RoutineComplexId",
                        column: x => x.RoutineComplexId,
                        principalTable: "RoutineComplex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanningHistory_RoutineComplexId",
                table: "PlanningHistory",
                column: "RoutineComplexId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanningHistory");
        }
    }
}
