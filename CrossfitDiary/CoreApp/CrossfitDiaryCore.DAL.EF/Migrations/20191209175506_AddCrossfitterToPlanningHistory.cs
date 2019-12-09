using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class AddCrossfitterToPlanningHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CrossfitterId",
                table: "PlanningHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanningHistory_CrossfitterId",
                table: "PlanningHistory",
                column: "CrossfitterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanningHistory_AspNetUsers_CrossfitterId",
                table: "PlanningHistory",
                column: "CrossfitterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanningHistory_AspNetUsers_CrossfitterId",
                table: "PlanningHistory");

            migrationBuilder.DropIndex(
                name: "IX_PlanningHistory_CrossfitterId",
                table: "PlanningHistory");

            migrationBuilder.DropColumn(
                name: "CrossfitterId",
                table: "PlanningHistory");
        }
    }
}
