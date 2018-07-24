using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class FixIdentityColumnsFromMVC_EF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
//
//            migrationBuilder.AlterColumn<string>(
//                table: "AspNetUserLogins",
//                name: "LoginProvider",
//                maxLength: 450);
//
//            migrationBuilder.AlterColumn<string>(
//                table: "AspNetUserLogins",
//                name: "ProviderKey",
//                maxLength: 450);


            migrationBuilder.AddColumn<string>(
                table: "AspNetRoles",
                name: "ConcurrencyStamp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                table: "AspNetRoles",
                name: "NormalizedName",
                nullable: true);


            migrationBuilder.AddColumn<string>(
                table: "AspNetUserLogins",
                name: "ProviderDisplayName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                table: "AspNetUsers",
                name: "ConcurrencyStamp",
                nullable: true);


            migrationBuilder.AddColumn<DateTimeOffset>(
                table: "AspNetUsers",
                name: "LockoutEnd",
                nullable: true);


            migrationBuilder.AddColumn<string>(
                table: "AspNetUsers",
                name: "NormalizedEmail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                table: "AspNetUsers",
                name: "NormalizedUserName",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                table: "AspNetRoles",
                name: "ConcurrencyStamp");

            migrationBuilder.DropColumn(
                table: "AspNetRoles",
                name: "NormalizedName");


            migrationBuilder.DropColumn(
                table: "AspNetUserLogins",
                name: "ProviderDisplayName");

            migrationBuilder.DropColumn(
                table: "AspNetUsers",
                name: "ConcurrencyStamp");

            migrationBuilder.DropColumn(
                table: "AspNetUsers",
                name: "LockoutEnd");

            migrationBuilder.DropColumn(
                table: "AspNetUsers",
                name: "NormalizedEmail");

            migrationBuilder.DropColumn(
                table: "AspNetUsers",
                name: "NormalizedUserName");
        }
    }
}
