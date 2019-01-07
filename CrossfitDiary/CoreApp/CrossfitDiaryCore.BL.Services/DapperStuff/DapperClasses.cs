using CrossfitDiaryCore.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossfitDiaryCore.BL.Services.DapperStuff
{
    public class DapperResults {
        public IEnumerable<CrossfitterWorkout> CrossfitterWorkouts { get; }
        public IEnumerable<RoutineSimple> RoutineSimples { get; }
        public IEnumerable<RoutineComplex> ChildRoutines { get; }
        public IEnumerable<RoutineSimple> ChildRoutineSimples { get; }

        

        public DapperResults()
        {

        }

        public DapperResults(IEnumerable<CrossfitterWorkout> crossfitterWorkouts, IEnumerable<RoutineSimple> routineSimples, IEnumerable<RoutineComplex> routineComplex, IEnumerable<RoutineSimple> childRoutineSimples)
        {
            CrossfitterWorkouts = crossfitterWorkouts;
            RoutineSimples = routineSimples;
            ChildRoutines = routineComplex;
            ChildRoutineSimples = childRoutineSimples;
        }
    }


    public class RoutineComplexRepository
    {
        string _connectionString;
        public RoutineComplexRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ExerciseMeasure> GetAllExerciseMeasures()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT *
                            FROM [ExerciseMeasure]";
                return db.Query<ExerciseMeasure>(sql);
            }
        }

        

        //public Task<IEnumerable<RoutineSimple>> GetSimpleRoutines(List<int> ids)
        //{
        //    using (IDbConnection db = new SqlConnection(_connectionString))
        //    {
        //        string sql = @"SELECT [x.RoutineComplex.RoutineSimple].[Id], [x.RoutineComplex.RoutineSimple].[Calories], [x.RoutineComplex.RoutineSimple].[Centimeters], [x.RoutineComplex.RoutineSimple].[Count], [x.RoutineComplex.RoutineSimple].[CreatedUtc], [x.RoutineComplex.RoutineSimple].[Distance], [x.RoutineComplex.RoutineSimple].[ExerciseId], [x.RoutineComplex.RoutineSimple].[IsAlternative], [x.RoutineComplex.RoutineSimple].[IsDoUnbroken], [x.RoutineComplex.RoutineSimple].[Position], [x.RoutineComplex.RoutineSimple].[RoutineComplexId], [x.RoutineComplex.RoutineSimple].[TimeToWork], [x.RoutineComplex.RoutineSimple].[Weight], [r.Exercise].[Id], [r.Exercise].[Abbreviation], [r.Exercise].[CreatedUtc], [r.Exercise].[Title]
        //            FROM [RoutineSimple] AS [x.RoutineComplex.RoutineSimple]
        //            INNER JOIN [Exercise] AS [r.Exercise] ON [x.RoutineComplex.RoutineSimple].[ExerciseId] = [r.Exercise].[Id]
        //            INNER JOIN (
        //                SELECT DISTINCT [x.RoutineComplex0].[Id]
        //                FROM [CrossfitterWorkout] AS [x0]
        //                INNER JOIN [AspNetUsers] AS [x.Crossfitter0] ON [x0].[CrossfitterId] = [x.Crossfitter0].[Id]
        //                INNER JOIN [RoutineComplex] AS [x.RoutineComplex0] ON [x0].[RoutineComplexId] = [x.RoutineComplex0].[Id]
        //                WHERE [x0].[Id] IN @ids
        //            ) AS [t] ON [x.RoutineComplex.RoutineSimple].[RoutineComplexId] = [t].[Id]
        //            ORDER BY [t].[Id], [r.Exercise].[Id]";
        //        return db.QueryAsync<RoutineSimple, Exercise, RoutineSimple>(sql, (routineSimple, exercise) =>
        //        {
        //            routineSimple.Exercise = exercise;
        //            return routineSimple;
        //        }, new { ids });
        //    }
        //}


        //public Task<IEnumerable<RoutineSimple>> GetSimpleRoutinesFromChild(List<int> ids)
        //{
        //    using (IDbConnection db = new SqlConnection(_connectionString))
        //    {
        //        string sql = @"SELECT [x.RoutineComplex.Children.RoutineSimple].[Id], [x.RoutineComplex.Children.RoutineSimple].[Calories], [x.RoutineComplex.Children.RoutineSimple].[Centimeters], [x.RoutineComplex.Children.RoutineSimple].[Count], [x.RoutineComplex.Children.RoutineSimple].[CreatedUtc], [x.RoutineComplex.Children.RoutineSimple].[Distance], [x.RoutineComplex.Children.RoutineSimple].[ExerciseId], [x.RoutineComplex.Children.RoutineSimple].[IsAlternative], [x.RoutineComplex.Children.RoutineSimple].[IsDoUnbroken], [x.RoutineComplex.Children.RoutineSimple].[Position], [x.RoutineComplex.Children.RoutineSimple].[RoutineComplexId], [x.RoutineComplex.Children.RoutineSimple].[TimeToWork], [x.RoutineComplex.Children.RoutineSimple].[Weight], [r.Exercise1].[Id], [r.Exercise1].[Abbreviation], [r.Exercise1].[CreatedUtc], [r.Exercise1].[Title]
        //            FROM [RoutineSimple] AS [x.RoutineComplex.Children.RoutineSimple]
        //            INNER JOIN [Exercise] AS [r.Exercise1] ON [x.RoutineComplex.Children.RoutineSimple].[ExerciseId] = [r.Exercise1].[Id]
        //            INNER JOIN (
        //                SELECT DISTINCT [x.RoutineComplex.Children0].[Id], [t3].[Id] AS [Id0]
        //                FROM [RoutineComplex] AS [x.RoutineComplex.Children0]
        //                INNER JOIN (
        //                    SELECT DISTINCT [x.RoutineComplex3].[Id]
        //                    FROM [CrossfitterWorkout] AS [x3]
        //                    INNER JOIN [AspNetUsers] AS [x.Crossfitter3] ON [x3].[CrossfitterId] = [x.Crossfitter3].[Id]
        //                    INNER JOIN [RoutineComplex] AS [x.RoutineComplex3] ON [x3].[RoutineComplexId] = [x.RoutineComplex3].[Id]
        //                    WHERE [x3].[Id] IN @ids
        //                ) AS [t3] ON [x.RoutineComplex.Children0].[ParentId] = [t3].[Id]
        //            ) AS [t4] ON [x.RoutineComplex.Children.RoutineSimple].[RoutineComplexId] = [t4].[Id]
        //            ORDER BY [t4].[Id0], [t4].[Id], [r.Exercise1].[Id]";
        //        return db.QueryAsync<RoutineSimple, Exercise, RoutineSimple>(sql, (routineSimple, exercise) =>
        //        {
        //            routineSimple.Exercise = exercise;
        //            return routineSimple;
        //        }, new { ids });
        //    }
        //}
        //public Task<IEnumerable<RoutineComplex>> GetChildRoutineComplex(List<int> ids)
        //{
        //    using (IDbConnection db = new SqlConnection(_connectionString))
        //    {
        //        return db.QueryAsync<RoutineComplex>(@"SELECT [x.RoutineComplex.Children].[Id], [x.RoutineComplex.Children].[ComplexType], [x.RoutineComplex.Children].[CreatedById], [x.RoutineComplex.Children].[CreatedUtc], [x.RoutineComplex.Children].[ParentId], [x.RoutineComplex.Children].[Position], [x.RoutineComplex.Children].[RestBetweenExercises], [x.RoutineComplex.Children].[RestBetweenRounds], [x.RoutineComplex.Children].[RoundCount], [x.RoutineComplex.Children].[TimeCap], [x.RoutineComplex.Children].[TimeToWork], [x.RoutineComplex.Children].[Title]
        //            FROM [RoutineComplex] AS [x.RoutineComplex.Children]
        //            INNER JOIN (
        //                SELECT DISTINCT [x.RoutineComplex2].[Id]
        //                FROM [CrossfitterWorkout] AS [x2]
        //                INNER JOIN [AspNetUsers] AS [x.Crossfitter2] ON [x2].[CrossfitterId] = [x.Crossfitter2].[Id]
        //                INNER JOIN [RoutineComplex] AS [x.RoutineComplex2] ON [x2].[RoutineComplexId] = [x.RoutineComplex2].[Id]
        //                WHERE [x2].[Id] IN @ids
        //            ) AS [t2] ON [x.RoutineComplex.Children].[ParentId] = [t2].[Id]
        //            ORDER BY [t2].[Id], [x.RoutineComplex.Children].[Id]", new { ids});
        //    }
        //}

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

        public DapperResults GetMultiQuery(List<int> ids)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT [x].[Id], [x].[CreatedUtc], [x].[CrossfitterId], [x].[Date], [x].[Distance], [x].[IsModified], [x].[IsPlanned], [x].[IsRx], [x].[PartialRepsFinished], [x].[RepsToFinishOnCapTime], [x].[RoundsFinished], [x].[RoutineComplexId], [x].[TimePassed], [x].[WasFinished], [x.Crossfitter].[Id], [x.Crossfitter].[AccessFailedCount], [x.Crossfitter].[ConcurrencyStamp], [x.Crossfitter].[Email], [x.Crossfitter].[EmailConfirmed], [x.Crossfitter].[FirstName], [x.Crossfitter].[LastName], [x.Crossfitter].[LockoutEnabled], [x.Crossfitter].[LockoutEnd], [x.Crossfitter].[NormalizedEmail], [x.Crossfitter].[NormalizedUserName], [x.Crossfitter].[PasswordHash], [x.Crossfitter].[PhoneNumber], [x.Crossfitter].[PhoneNumberConfirmed], [x.Crossfitter].[SecurityStamp], [x.Crossfitter].[TwoFactorEnabled], [x.Crossfitter].[UserName], [x.RoutineComplex].[Id], [x.RoutineComplex].[ComplexType], [x.RoutineComplex].[CreatedById], [x.RoutineComplex].[CreatedUtc], [x.RoutineComplex].[ParentId], [x.RoutineComplex].[Position], [x.RoutineComplex].[RestBetweenExercises], [x.RoutineComplex].[RestBetweenRounds], [x.RoutineComplex].[RoundCount], [x.RoutineComplex].[TimeCap], [x.RoutineComplex].[TimeToWork], [x.RoutineComplex].[Title]
                    FROM [CrossfitterWorkout] AS [x]
                    INNER JOIN [AspNetUsers] AS [x.Crossfitter] ON [x].[CrossfitterId] = [x.Crossfitter].[Id]
                    INNER JOIN [RoutineComplex] AS [x.RoutineComplex] ON [x].[RoutineComplexId] = [x.RoutineComplex].[Id]
                    WHERE [x].[Id] IN @ids
                    ORDER BY [x.RoutineComplex].[Id]; 

                    SELECT [x.RoutineComplex.RoutineSimple].[Id], [x.RoutineComplex.RoutineSimple].[Calories], [x.RoutineComplex.RoutineSimple].[Centimeters], [x.RoutineComplex.RoutineSimple].[Count], [x.RoutineComplex.RoutineSimple].[CreatedUtc], [x.RoutineComplex.RoutineSimple].[Distance], [x.RoutineComplex.RoutineSimple].[ExerciseId], [x.RoutineComplex.RoutineSimple].[IsAlternative], [x.RoutineComplex.RoutineSimple].[IsDoUnbroken], [x.RoutineComplex.RoutineSimple].[Position], [x.RoutineComplex.RoutineSimple].[RoutineComplexId], [x.RoutineComplex.RoutineSimple].[TimeToWork], [x.RoutineComplex.RoutineSimple].[Weight], [r.Exercise].[Id], [r.Exercise].[Abbreviation], [r.Exercise].[CreatedUtc], [r.Exercise].[Title]
                    FROM [RoutineSimple] AS [x.RoutineComplex.RoutineSimple]
                    INNER JOIN [Exercise] AS [r.Exercise] ON [x.RoutineComplex.RoutineSimple].[ExerciseId] = [r.Exercise].[Id]
                    INNER JOIN (
                        SELECT DISTINCT [x.RoutineComplex0].[Id]
                        FROM [CrossfitterWorkout] AS [x0]
                        INNER JOIN [AspNetUsers] AS [x.Crossfitter0] ON [x0].[CrossfitterId] = [x.Crossfitter0].[Id]
                        INNER JOIN [RoutineComplex] AS [x.RoutineComplex0] ON [x0].[RoutineComplexId] = [x.RoutineComplex0].[Id]
                        WHERE [x0].[Id] IN @ids
                    ) AS [t] ON [x.RoutineComplex.RoutineSimple].[RoutineComplexId] = [t].[Id]
                    ORDER BY [t].[Id], [r.Exercise].[Id];

                    SELECT [x.RoutineComplex.Children].[Id], [x.RoutineComplex.Children].[ComplexType], [x.RoutineComplex.Children].[CreatedById], [x.RoutineComplex.Children].[CreatedUtc], [x.RoutineComplex.Children].[ParentId], [x.RoutineComplex.Children].[Position], [x.RoutineComplex.Children].[RestBetweenExercises], [x.RoutineComplex.Children].[RestBetweenRounds], [x.RoutineComplex.Children].[RoundCount], [x.RoutineComplex.Children].[TimeCap], [x.RoutineComplex.Children].[TimeToWork], [x.RoutineComplex.Children].[Title]
                    FROM [RoutineComplex] AS [x.RoutineComplex.Children]
                    INNER JOIN (
                        SELECT DISTINCT [x.RoutineComplex2].[Id]
                        FROM [CrossfitterWorkout] AS [x2]
                        INNER JOIN [AspNetUsers] AS [x.Crossfitter2] ON [x2].[CrossfitterId] = [x.Crossfitter2].[Id]
                        INNER JOIN [RoutineComplex] AS [x.RoutineComplex2] ON [x2].[RoutineComplexId] = [x.RoutineComplex2].[Id]
                        WHERE [x2].[Id] IN @ids
                    ) AS [t2] ON [x.RoutineComplex.Children].[ParentId] = [t2].[Id]
                    ORDER BY [t2].[Id], [x.RoutineComplex.Children].[Id];       

                    SELECT [x.RoutineComplex.Children.RoutineSimple].[Id], [x.RoutineComplex.Children.RoutineSimple].[Calories], [x.RoutineComplex.Children.RoutineSimple].[Centimeters], [x.RoutineComplex.Children.RoutineSimple].[Count], [x.RoutineComplex.Children.RoutineSimple].[CreatedUtc], [x.RoutineComplex.Children.RoutineSimple].[Distance], [x.RoutineComplex.Children.RoutineSimple].[ExerciseId], [x.RoutineComplex.Children.RoutineSimple].[IsAlternative], [x.RoutineComplex.Children.RoutineSimple].[IsDoUnbroken], [x.RoutineComplex.Children.RoutineSimple].[Position], [x.RoutineComplex.Children.RoutineSimple].[RoutineComplexId], [x.RoutineComplex.Children.RoutineSimple].[TimeToWork], [x.RoutineComplex.Children.RoutineSimple].[Weight], [r.Exercise1].[Id], [r.Exercise1].[Abbreviation], [r.Exercise1].[CreatedUtc], [r.Exercise1].[Title]
                    FROM [RoutineSimple] AS [x.RoutineComplex.Children.RoutineSimple]
                    INNER JOIN [Exercise] AS [r.Exercise1] ON [x.RoutineComplex.Children.RoutineSimple].[ExerciseId] = [r.Exercise1].[Id]
                    INNER JOIN (
                        SELECT DISTINCT [x.RoutineComplex.Children0].[Id], [t3].[Id] AS [Id0]
                        FROM [RoutineComplex] AS [x.RoutineComplex.Children0]
                        INNER JOIN (
                            SELECT DISTINCT [x.RoutineComplex3].[Id]
                            FROM [CrossfitterWorkout] AS [x3]
                            INNER JOIN [AspNetUsers] AS [x.Crossfitter3] ON [x3].[CrossfitterId] = [x.Crossfitter3].[Id]
                            INNER JOIN [RoutineComplex] AS [x.RoutineComplex3] ON [x3].[RoutineComplexId] = [x.RoutineComplex3].[Id]
                            WHERE [x3].[Id] IN @ids
                        ) AS [t3] ON [x.RoutineComplex.Children0].[ParentId] = [t3].[Id]
                    ) AS [t4] ON [x.RoutineComplex.Children.RoutineSimple].[RoutineComplexId] = [t4].[Id]
                    ORDER BY [t4].[Id0], [t4].[Id], [r.Exercise1].[Id]                    
";
                using (var multi = db.QueryMultiple(sql, new { ids }))
                {
                    IEnumerable<CrossfitterWorkout> crossfitterWorkouts = multi.Read<CrossfitterWorkout, ApplicationUser, RoutineComplex, CrossfitterWorkout>((crossfiterWorkout, user, routine) =>
                    {
                        crossfiterWorkout.RoutineComplex = routine;
                        crossfiterWorkout.Crossfitter = user;
                        return crossfiterWorkout;
                    });

                    IEnumerable<RoutineSimple> routineSimples = multi.Read<RoutineSimple, Exercise, RoutineSimple>((routineSimple, exercise) =>
                    {
                        routineSimple.Exercise = exercise;
                        return routineSimple;
                    });

                    IEnumerable<RoutineComplex> childRoutineComplex = multi.Read<RoutineComplex>();

                    IEnumerable<RoutineSimple> childRoutineSimples = multi.Read<RoutineSimple, Exercise, RoutineSimple>((routineSimple, exercise) =>
                    {
                        routineSimple.Exercise = exercise;
                        return routineSimple;
                    });
                    return new DapperResults(crossfitterWorkouts,routineSimples, childRoutineComplex, childRoutineSimples);
                }
            }
        }
        //public Task<IEnumerable<CrossfitterWorkout>> GetCrossfitterRoutines(List<int> ids)
        //{
        //    using (IDbConnection db = new SqlConnection(_connectionString))
        //    {
        //        string sql = @"SELECT [x].[Id], [x].[CreatedUtc], [x].[CrossfitterId], [x].[Date], [x].[Distance], [x].[IsModified], [x].[IsPlanned], [x].[IsRx], [x].[PartialRepsFinished], [x].[RepsToFinishOnCapTime], [x].[RoundsFinished], [x].[RoutineComplexId], [x].[TimePassed], [x].[WasFinished], [x.Crossfitter].[Id], [x.Crossfitter].[AccessFailedCount], [x.Crossfitter].[ConcurrencyStamp], [x.Crossfitter].[Email], [x.Crossfitter].[EmailConfirmed], [x.Crossfitter].[FirstName], [x.Crossfitter].[LastName], [x.Crossfitter].[LockoutEnabled], [x.Crossfitter].[LockoutEnd], [x.Crossfitter].[NormalizedEmail], [x.Crossfitter].[NormalizedUserName], [x.Crossfitter].[PasswordHash], [x.Crossfitter].[PhoneNumber], [x.Crossfitter].[PhoneNumberConfirmed], [x.Crossfitter].[SecurityStamp], [x.Crossfitter].[TwoFactorEnabled], [x.Crossfitter].[UserName], [x.RoutineComplex].[Id], [x.RoutineComplex].[ComplexType], [x.RoutineComplex].[CreatedById], [x.RoutineComplex].[CreatedUtc], [x.RoutineComplex].[ParentId], [x.RoutineComplex].[Position], [x.RoutineComplex].[RestBetweenExercises], [x.RoutineComplex].[RestBetweenRounds], [x.RoutineComplex].[RoundCount], [x.RoutineComplex].[TimeCap], [x.RoutineComplex].[TimeToWork], [x.RoutineComplex].[Title]
        //            FROM [CrossfitterWorkout] AS [x]
        //            INNER JOIN [AspNetUsers] AS [x.Crossfitter] ON [x].[CrossfitterId] = [x.Crossfitter].[Id]
        //            INNER JOIN [RoutineComplex] AS [x.RoutineComplex] ON [x].[RoutineComplexId] = [x.RoutineComplex].[Id]
        //            WHERE [x].[Id] IN @ids
        //            ORDER BY [x.RoutineComplex].[Id]";
        //        return db.QueryAsync<CrossfitterWorkout, ApplicationUser, RoutineComplex, CrossfitterWorkout>(sql, (crossfiterWorkout, user, routine) =>
        //        {
        //            crossfiterWorkout.RoutineComplex = routine;
        //            crossfiterWorkout.Crossfitter = user;
        //            return crossfiterWorkout;
        //        }, new { ids });
        //    }


        //}


    }
}
