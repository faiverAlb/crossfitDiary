using CrossfitDiaryCore.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CrossfitDiaryCore.BL.Services.DapperStuff
{
    

    public class RoutineComplexRepository
    {
        string _connectionString;
        public RoutineComplexRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ExerciseMeasure> GetExerciseMeasures(List<int> ids)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT [r.Exercise.ExerciseMeasures].[Id], [r.Exercise.ExerciseMeasures].[CreatedUtc], [r.Exercise.ExerciseMeasures].[ExerciseId], [r.Exercise.ExerciseMeasures].[ExerciseMeasureTypeId]
                            FROM [ExerciseMeasure] AS [r.Exercise.ExerciseMeasures]
                            INNER JOIN (
                                SELECT DISTINCT [r.Exercise0].[Id], [t0].[Id] AS [Id0]
                                FROM [RoutineSimple] AS [x.RoutineComplex.RoutineSimple0]
                                INNER JOIN [Exercise] AS [r.Exercise0] ON [x.RoutineComplex.RoutineSimple0].[ExerciseId] = [r.Exercise0].[Id]
                                INNER JOIN (
                                    SELECT DISTINCT [x.RoutineComplex1].[Id]
                                    FROM [CrossfitterWorkout] AS [x1]
                                    INNER JOIN [AspNetUsers] AS [x.Crossfitter1] ON [x1].[CrossfitterId] = [x.Crossfitter1].[Id]
                                    INNER JOIN [RoutineComplex] AS [x.RoutineComplex1] ON [x1].[RoutineComplexId] = [x.RoutineComplex1].[Id]
                                    WHERE [x1].[Id] IN @ids
                                ) AS [t0] ON [x.RoutineComplex.RoutineSimple0].[RoutineComplexId] = [t0].[Id]
                            ) AS [t1] ON [r.Exercise.ExerciseMeasures].[ExerciseId] = [t1].[Id]
                            ORDER BY [t1].[Id0], [t1].[Id]";
                return db.Query<ExerciseMeasure>(sql, new { ids })
                .ToList();
            }
        }
        public List<RoutineSimple> GetSimpleRoutines(List<int> ids)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT [x.RoutineComplex.RoutineSimple].[Id], [x.RoutineComplex.RoutineSimple].[Calories], [x.RoutineComplex.RoutineSimple].[Centimeters], [x.RoutineComplex.RoutineSimple].[Count], [x.RoutineComplex.RoutineSimple].[CreatedUtc], [x.RoutineComplex.RoutineSimple].[Distance], [x.RoutineComplex.RoutineSimple].[ExerciseId], [x.RoutineComplex.RoutineSimple].[IsAlternative], [x.RoutineComplex.RoutineSimple].[IsDoUnbroken], [x.RoutineComplex.RoutineSimple].[Position], [x.RoutineComplex.RoutineSimple].[RoutineComplexId], [x.RoutineComplex.RoutineSimple].[TimeToWork], [x.RoutineComplex.RoutineSimple].[Weight], [r.Exercise].[Id], [r.Exercise].[Abbreviation], [r.Exercise].[CreatedUtc], [r.Exercise].[Title]
                    FROM [RoutineSimple] AS [x.RoutineComplex.RoutineSimple]
                    INNER JOIN [Exercise] AS [r.Exercise] ON [x.RoutineComplex.RoutineSimple].[ExerciseId] = [r.Exercise].[Id]
                    INNER JOIN (
                        SELECT DISTINCT [x.RoutineComplex0].[Id]
                        FROM [CrossfitterWorkout] AS [x0]
                        INNER JOIN [AspNetUsers] AS [x.Crossfitter0] ON [x0].[CrossfitterId] = [x.Crossfitter0].[Id]
                        INNER JOIN [RoutineComplex] AS [x.RoutineComplex0] ON [x0].[RoutineComplexId] = [x.RoutineComplex0].[Id]
                        WHERE [x0].[Id] IN @ids
                    ) AS [t] ON [x.RoutineComplex.RoutineSimple].[RoutineComplexId] = [t].[Id]
                    ORDER BY [t].[Id], [r.Exercise].[Id]";
                return db.Query<RoutineSimple, Exercise, RoutineSimple>(sql, (routineSimple, exercise) =>
                {
                    routineSimple.Exercise = exercise;
                    return routineSimple;
                }, new { ids })
                .ToList();
            }
        }

        public List<int> GetIds(int offset,int count)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<int>(@"SELECT [x].[Id]
                                        FROM [CrossfitterWorkout] AS[x]
                                        ORDER BY[x].[Date] DESC, [x].[CreatedUtc]
                                                DESC
                                        OFFSET @offset ROWS FETCH NEXT @count ROWS ONLY", new { offset, count }).ToList();
            }

            
        }

        public List<CrossfitterWorkout> GetCrossfitterRoutines(List<int> ids)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT [x].[Id], [x].[CreatedUtc], [x].[CrossfitterId], [x].[Date], [x].[Distance], [x].[IsModified], [x].[IsPlanned], [x].[IsRx], [x].[PartialRepsFinished], [x].[RepsToFinishOnCapTime], [x].[RoundsFinished], [x].[RoutineComplexId], [x].[TimePassed], [x].[WasFinished], [x.Crossfitter].[Id], [x.Crossfitter].[AccessFailedCount], [x.Crossfitter].[ConcurrencyStamp], [x.Crossfitter].[Email], [x.Crossfitter].[EmailConfirmed], [x.Crossfitter].[FirstName], [x.Crossfitter].[LastName], [x.Crossfitter].[LockoutEnabled], [x.Crossfitter].[LockoutEnd], [x.Crossfitter].[NormalizedEmail], [x.Crossfitter].[NormalizedUserName], [x.Crossfitter].[PasswordHash], [x.Crossfitter].[PhoneNumber], [x.Crossfitter].[PhoneNumberConfirmed], [x.Crossfitter].[SecurityStamp], [x.Crossfitter].[TwoFactorEnabled], [x.Crossfitter].[UserName], [x.RoutineComplex].[Id], [x.RoutineComplex].[ComplexType], [x.RoutineComplex].[CreatedById], [x.RoutineComplex].[CreatedUtc], [x.RoutineComplex].[ParentId], [x.RoutineComplex].[Position], [x.RoutineComplex].[RestBetweenExercises], [x.RoutineComplex].[RestBetweenRounds], [x.RoutineComplex].[RoundCount], [x.RoutineComplex].[TimeCap], [x.RoutineComplex].[TimeToWork], [x.RoutineComplex].[Title]
                    FROM [CrossfitterWorkout] AS [x]
                    INNER JOIN [AspNetUsers] AS [x.Crossfitter] ON [x].[CrossfitterId] = [x.Crossfitter].[Id]
                    INNER JOIN [RoutineComplex] AS [x.RoutineComplex] ON [x].[RoutineComplexId] = [x.RoutineComplex].[Id]
                    WHERE [x].[Id] IN @ids
                    ORDER BY [x.RoutineComplex].[Id]";
                return db.Query<CrossfitterWorkout, ApplicationUser, RoutineComplex, CrossfitterWorkout>(sql, (crossfiterWorkout, user, routine) =>
                {
                    crossfiterWorkout.RoutineComplex = routine;
                    crossfiterWorkout.Crossfitter = user;
                    return crossfiterWorkout;
                }, new { ids }).ToList();
            }


        }


    }
}
