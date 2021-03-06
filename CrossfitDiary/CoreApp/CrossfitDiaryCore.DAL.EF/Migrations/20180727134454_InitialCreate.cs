﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrossfitDiaryCore.DAL.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Abbreviation = table.Column<string>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseMeasureType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    MeasureType = table.Column<int>(nullable: false),
                    ShortMeasureDescription = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseMeasureType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoutineComplex",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComplexType = table.Column<int>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    RestBetweenExercises = table.Column<TimeSpan>(nullable: true),
                    RestBetweenRounds = table.Column<TimeSpan>(nullable: true),
                    RoundCount = table.Column<int>(nullable: true),
                    TimeCap = table.Column<TimeSpan>(nullable: true),
                    TimeToWork = table.Column<TimeSpan>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineComplex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutineComplex_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoutineComplex_RoutineComplex_ParentId",
                        column: x => x.ParentId,
                        principalTable: "RoutineComplex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseMeasure",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false),
                    ExerciseMeasureTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseMeasure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseMeasure_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseMeasure_ExerciseMeasureType_ExerciseMeasureTypeId",
                        column: x => x.ExerciseMeasureTypeId,
                        principalTable: "ExerciseMeasureType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrossfitterWorkout",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CrossfitterId = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Distance = table.Column<int>(nullable: true),
                    IsModified = table.Column<bool>(nullable: false),
                    IsPlanned = table.Column<bool>(nullable: false),
                    IsRx = table.Column<bool>(nullable: false),
                    PartialRepsFinished = table.Column<int>(nullable: true),
                    RepsToFinishOnCapTime = table.Column<int>(nullable: true),
                    RoundsFinished = table.Column<int>(nullable: true),
                    RoutineComplexId = table.Column<int>(nullable: false),
                    TimePassed = table.Column<TimeSpan>(nullable: true),
                    WasFinished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrossfitterWorkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrossfitterWorkout_AspNetUsers_CrossfitterId",
                        column: x => x.CrossfitterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrossfitterWorkout_RoutineComplex_RoutineComplexId",
                        column: x => x.RoutineComplexId,
                        principalTable: "RoutineComplex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoutineSimple",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Calories = table.Column<decimal>(nullable: true),
                    Centimeters = table.Column<decimal>(nullable: true),
                    Count = table.Column<decimal>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    Distance = table.Column<decimal>(nullable: true),
                    ExerciseId = table.Column<int>(nullable: false),
                    IsAlternative = table.Column<bool>(nullable: false),
                    IsDoUnbroken = table.Column<bool>(nullable: false),
                    RoutineComplexId = table.Column<int>(nullable: false),
                    TimeToWork = table.Column<TimeSpan>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineSimple", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutineSimple_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutineSimple_RoutineComplex_RoutineComplexId",
                        column: x => x.RoutineComplexId,
                        principalTable: "RoutineComplex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CrossfitterWorkout_CrossfitterId",
                table: "CrossfitterWorkout",
                column: "CrossfitterId");

            migrationBuilder.CreateIndex(
                name: "IX_CrossfitterWorkout_RoutineComplexId",
                table: "CrossfitterWorkout",
                column: "RoutineComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMeasure_ExerciseId",
                table: "ExerciseMeasure",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMeasure_ExerciseMeasureTypeId",
                table: "ExerciseMeasure",
                column: "ExerciseMeasureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineComplex_CreatedById",
                table: "RoutineComplex",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineComplex_ParentId",
                table: "RoutineComplex",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineSimple_ExerciseId",
                table: "RoutineSimple",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineSimple_RoutineComplexId",
                table: "RoutineSimple",
                column: "RoutineComplexId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CrossfitterWorkout");

            migrationBuilder.DropTable(
                name: "ExerciseMeasure");

            migrationBuilder.DropTable(
                name: "RoutineSimple");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ExerciseMeasureType");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "RoutineComplex");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
