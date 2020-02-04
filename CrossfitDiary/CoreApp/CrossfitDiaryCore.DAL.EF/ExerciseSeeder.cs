using System;
using System.Collections.Generic;
using CrossfitDiaryCore.Model;
using Microsoft.EntityFrameworkCore;

namespace CrossfitDiaryCore.DAL.EF
{
    public static class ExerciseSeeder
    {
        private static readonly List<Exercise> _exercises = new List<Exercise>()
        {
            new Exercise {Id = 130, Abbreviation = "HSC", Title = "Hang Squat Clean"},
            new Exercise {Id = 131, Abbreviation = "HCP", Title = "Hang Clean Pull"},
            new Exercise {Id = 132, Abbreviation = "DDL", Title = "Deficit Deadlift"},
            new Exercise {Id = 133, Abbreviation = "PSBH", Title = "Pistol squat bottom hold"},
            new Exercise {Id = 134, Abbreviation = "Pegboard", Title = "Pegboard"},
            new Exercise {Id = 135, Abbreviation = "Ring PshU", Title = "Ring Push-Up"},
            new Exercise {Id = 136, Abbreviation = "Burpee D-ball over box", Title = "Burpee D-ball over box"},
            new Exercise {Id = 137, Abbreviation = "Hip extension hold (GHD)", Title = "Hip extension hold (GHD)"},
            new Exercise {Id = 138, Abbreviation = "Glute bridge", Title = "Glute bridge"},
            new Exercise {Id = 139, Abbreviation = "Lat flys", Title = "Lat flys"},
            new Exercise {Id = 140, Abbreviation = "C2B hold", Title = "Chest to bar hold"},
            new Exercise {Id = 141, Abbreviation = "COvB hold", Title = "Chin over bar hold"},
            new Exercise {Id = 142, Abbreviation = "Bottom up OHS", Title = "Bottom up overhead squat"},
            new Exercise {Id = 143, Abbreviation = "Paused BS", Title = "Paused Back Squat"},
            new Exercise {Id = 144, Abbreviation = "Single leg step-up on box", Title = "Single leg step-up on box"},
        };

        private static readonly List<ExerciseMeasure[]> _measures = new List<ExerciseMeasure[]>()
        {
            new[]
            {
                new ExerciseMeasure {Id = 311, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 130},
                new ExerciseMeasure {Id = 312, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 130},
                new ExerciseMeasure {Id = 313, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 130}
            },
            new[]
            {
                new ExerciseMeasure {Id = 314, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 131},
                new ExerciseMeasure {Id = 315, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 131},
                new ExerciseMeasure {Id = 316, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 131}
            },
            new[]
            {
                new ExerciseMeasure {Id = 317, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 132},
                new ExerciseMeasure {Id = 318, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 132},
                new ExerciseMeasure {Id = 319, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 132}
            },
            new[]
            {
                new ExerciseMeasure {Id = 320, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 133},
                new ExerciseMeasure {Id = 321, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = 133},
            },
            new[]
            {
                new ExerciseMeasure {Id = 322, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 134},
                new ExerciseMeasure {Id = 323, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 134},
                new ExerciseMeasure {Id = 324, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 134}
            },
            new[]
            {
                new ExerciseMeasure {Id = 325, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 135},
                new ExerciseMeasure {Id = 326, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 135},
                new ExerciseMeasure {Id = 327, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 135}
            },
            new[]
            {
                new ExerciseMeasure {Id = 328, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 136},
                new ExerciseMeasure {Id = 329, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 136},
                new ExerciseMeasure {Id = 330, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 136}
            },
            new[]
            {
                new ExerciseMeasure {Id = 331, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 137},
                new ExerciseMeasure {Id = 332, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = 137},
            },
            new[]
            {
                new ExerciseMeasure {Id = 333, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 138},
                new ExerciseMeasure {Id = 334, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 138},
                new ExerciseMeasure {Id = 335, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 138}
            },
            new[]
            {
                new ExerciseMeasure {Id = 336, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 139},
            },
            new[]
            {
                new ExerciseMeasure {Id = 337, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 140},
                new ExerciseMeasure {Id = 338, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = 140},
            },
            new[]
            {
                new ExerciseMeasure {Id = 339, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 141},
                new ExerciseMeasure {Id = 340, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = 141},
            },
            new[]
            {
                new ExerciseMeasure {Id = 341, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 142},
                new ExerciseMeasure {Id = 342, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 142},
                new ExerciseMeasure {Id = 343, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 142}
            },
            new[] {new ExerciseMeasure {Id = 344, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = 38}},
            new[] {new ExerciseMeasure {Id = 345, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = 114}},
            new[] {new ExerciseMeasure {Id = 346, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = 46}},
            new[] {new ExerciseMeasure {Id = 347, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = 47}},
            new[]
            {
                new ExerciseMeasure {Id = 348, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 143},
                new ExerciseMeasure {Id = 349, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 143},
                new ExerciseMeasure {Id = 350, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 143},
                new ExerciseMeasure {Id = 351, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = 143}
            },
            new[]
            {
                new ExerciseMeasure {Id = 352, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 144},
                new ExerciseMeasure {Id = 353, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 144},
                new ExerciseMeasure {Id = 354, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 144}
            },

        };


        public static void SeedData(ModelBuilder builder)
        {
            foreach (Exercise exercise in _exercises)
            {
                builder.Entity<Exercise>(b => { b.HasData(exercise); });
            }

            foreach (ExerciseMeasure[] exerciseMeasures in _measures)
            {
                builder.Entity<ExerciseMeasure>(b => { b.HasData(exerciseMeasures); });
            }
        }
    }
}