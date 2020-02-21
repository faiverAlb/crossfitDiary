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
            new Exercise {Id = 145, Abbreviation = "Jump to hollow", Title = "Jump to hollow"},
            new Exercise {Id = 146, Abbreviation = "Kipping PU", Title = "Kipping pull-up"},
            new Exercise {Id = 147, Abbreviation = "Butterfly PU", Title = "Butterfly pull-up"},
            new Exercise {Id = 148, Abbreviation = "Butterfly PU rhythm drill", Title = "Butterfly pull-up rhythm drill"},
            new Exercise {Id = 149, Abbreviation = "Glute ham raises", Title = "Glute ham raises"},
            new Exercise {Id = 150, Abbreviation = "Straight legs D-Ball clean", Title = "Straight leg D-Ball clean"},
            new Exercise {Id = 151, Abbreviation = "D-ball Squat", Title = "D-ball Squat"},
            new Exercise {Id = 152, Abbreviation = "D-ball Front hold good morning", Title = "D-ball Front hold good morning"},
            new Exercise {Id = 153, Abbreviation = "Straight legs sumo deadlift", Title = "Straight leg sumo deadlift"},
            new Exercise {Id = 154, Abbreviation = "Hip thrust", Title = "Hip thrust"},
            new Exercise {Id = 155, Abbreviation = "Straight legs romanian deadlift", Title = "Straight leg romanian deadlift"},
            new Exercise {Id = 156, Abbreviation = "Jumping BS", Title = "Jumping back squat"},
            new Exercise {Id = 157, Abbreviation = "BTN Jerk", Title = "Behind the Neck Jerk"},
            new Exercise {Id = 158, Abbreviation = "TU", Title = "Tuck Ups"},
            new Exercise {Id = 159, Abbreviation = "BTN Split Jerk", Title = "Behind the Neck Split Jerk"},
            new Exercise {Id = 160, Abbreviation = "Single leg DL(2xDB)", Title = "Single leg Deadlift(2xDB)"},
            new Exercise {Id = 161, Abbreviation = "Bulgarian split squat(2xDB)", Title = "Bulgarian split squat(2xDB)"},
            new Exercise {Id = 162, Abbreviation = "Banded hamstring curls", Title = "Banded hamstring curls"},
            new Exercise {Id = 163, Abbreviation = "Arch rock", Title = "Arch rock"},
            new Exercise {Id = 164, Abbreviation = "BTN Push press", Title = "BTN Push press"},
            new Exercise {Id = 165, Abbreviation = "DB hang squat clean", Title = "DB hang squat clean"},
            new Exercise {Id = 166, Abbreviation = "Burpee pull-up", Title = "Burpee pull-up"},
            new Exercise {Id = 167, Abbreviation = "Deficit push-up", Title = "Deficit push-up"},
            new Exercise {Id = 168, Abbreviation = "Sumo deadlift", Title = "Sumo deadlift"},
            new Exercise {Id = 169, Abbreviation = "Single arm overhead walking (DB/KB)", Title = "Single arm overhead walking (DB/KB)"},
            new Exercise {Id = 170, Abbreviation = "GHD barbell back ext", Title = "GHD barbell back extension"},
            new Exercise {Id = 171, Abbreviation = "Pegboard alt PU", Title = "Pegboard alt pull-up"},
            new Exercise {Id = 172, Abbreviation = "Pegboard toes to peg", Title = "Pegboard toes to peg"},
            new Exercise {Id = 173, Abbreviation = "Rope anchors", Title = "Rope anchors"},
            new Exercise {Id = 174, Abbreviation = "Clapping push-ups", Title = "Clapping push-ups"},
            new Exercise {Id = 175, Abbreviation = "Barbell cuban rotation", Title = "Barbell cuban rotation"},
            new Exercise {Id = 176, Abbreviation = "German hang", Title = "German hang"},
            new Exercise {Id = 177, Abbreviation = "Prowler push", Title = "Prowler push"},
            new Exercise {Id = 178, Abbreviation = "Power jerk", Title = "Power jerk"},
            new Exercise {Id = 179, Abbreviation = "Front rack lunge", Title = "Front rack lunge"},
            new Exercise {Id = 180, Abbreviation = "Skin the cat", Title = "Skin the cat"},
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
            new[]
            {
                new ExerciseMeasure {Id = 355, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 145},
            },
            new[]
            {
                new ExerciseMeasure {Id = 356, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 146},
            },
            new[]
            {
                new ExerciseMeasure {Id = 357, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 147},
            },
            new[]
            {
                new ExerciseMeasure {Id = 358, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 148},
            },
            new[]
            {
                new ExerciseMeasure {Id = 359, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 149},
                new ExerciseMeasure {Id = 360, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 149},
                new ExerciseMeasure {Id = 361, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 149}
            },
            new[]
            {
                new ExerciseMeasure {Id = 362, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 150},
                new ExerciseMeasure {Id = 363, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 150},
                new ExerciseMeasure {Id = 364, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 150}
            },
            new[]
            {
                new ExerciseMeasure {Id = 365, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 151},
                new ExerciseMeasure {Id = 366, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 151},
                new ExerciseMeasure {Id = 367, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 151}
            },
            new[]
            {
                new ExerciseMeasure {Id = 368, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 152},
                new ExerciseMeasure {Id = 369, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 152},
                new ExerciseMeasure {Id = 370, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 152}
            },
            new[]
            {
                new ExerciseMeasure {Id = 371, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 153},
                new ExerciseMeasure {Id = 372, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 153},
                new ExerciseMeasure {Id = 373, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 153}
            },
            new[]
            {
                new ExerciseMeasure {Id = 374, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 154},
                new ExerciseMeasure {Id = 375, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 154},
                new ExerciseMeasure {Id = 376, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 154}
            },
            new[]
            {
                new ExerciseMeasure {Id = 377, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 155},
                new ExerciseMeasure {Id = 378, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 155},
                new ExerciseMeasure {Id = 379, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 155}
            },
            new[]
            {
                new ExerciseMeasure {Id = 380, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 156},
                new ExerciseMeasure {Id = 381, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 156},
                new ExerciseMeasure {Id = 382, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 156}
            },
            new[]
            {
                new ExerciseMeasure {Id = 383, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 157},
                new ExerciseMeasure {Id = 384, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 157},
                new ExerciseMeasure {Id = 385, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 157}
            },
            new[]
            {
                new ExerciseMeasure {Id = 386, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 158},
            },
            new[]
            {
                new ExerciseMeasure {Id = 387, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 159},
                new ExerciseMeasure {Id = 388, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 159},
                new ExerciseMeasure {Id = 389, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 159}
            },
            new[]
            {
                new ExerciseMeasure {Id = 390, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 160},
                new ExerciseMeasure {Id = 391, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 160},
                new ExerciseMeasure {Id = 392, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 160}
            },
            new[]
            {
                new ExerciseMeasure {Id = 393, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 161},
                new ExerciseMeasure {Id = 394, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 161},
                new ExerciseMeasure {Id = 395, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 161}
            },
            new[]
            {
                new ExerciseMeasure {Id = 396, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 162},
                new ExerciseMeasure {Id = 397, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 162},
                new ExerciseMeasure {Id = 398, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 162}
            },
            new[]
            {
                new ExerciseMeasure {Id = 399, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 163},
            },
            new[]
            {
                new ExerciseMeasure {Id = 400, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 164},
                new ExerciseMeasure {Id = 401, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 164},
                new ExerciseMeasure {Id = 402, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 164}
            },
            new[]
            {
                new ExerciseMeasure {Id = 403, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 165},
                new ExerciseMeasure {Id = 404, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 165},
                new ExerciseMeasure {Id = 405, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 165}
            },
            new[]
            {
                new ExerciseMeasure {Id = 406, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 166},
            },
            new[]
            {
                new ExerciseMeasure {Id = 407, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 167},
            },
            new[]
            {
                new ExerciseMeasure {Id = 408, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 168},
                new ExerciseMeasure {Id = 409, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 168},
                new ExerciseMeasure {Id = 410, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 168}
            },
            new[]
            {
                new ExerciseMeasure {Id = 411, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 169},
                new ExerciseMeasure {Id = 412, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 169},
                new ExerciseMeasure {Id = 413, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 169}
            },
            new[]
            {
                new ExerciseMeasure {Id = 414, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 170},
                new ExerciseMeasure {Id = 415, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 170},
                new ExerciseMeasure {Id = 416, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 170}
            },
            new[]
            {
                new ExerciseMeasure {Id = 417, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 171},
            },
            new[]
            {
                new ExerciseMeasure {Id = 418, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 172},
            },
            new[]
            {
                new ExerciseMeasure {Id = 419, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 173},
            },
            new[]
            {
                new ExerciseMeasure {Id = 420, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 174},
            },
            new[]
            {
                new ExerciseMeasure {Id = 421, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 175},
            },
            new[]
            {
                new ExerciseMeasure {Id = 422, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 176},
                new ExerciseMeasure {Id = 423, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = 176},
            },
            new[]
            {
                new ExerciseMeasure {Id = 424, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 177},
                new ExerciseMeasure {Id = 425, ExerciseMeasureTypeId = MeasureType.Time, ExerciseId = 177},
                new ExerciseMeasure {Id = 426, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 177},
                new ExerciseMeasure {Id = 427, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 177},
            },
            new[]
            {
                new ExerciseMeasure {Id = 428, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 178},
                new ExerciseMeasure {Id = 429, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 178},
                new ExerciseMeasure {Id = 430, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 178},
            },
            new[]
            {
                new ExerciseMeasure {Id = 431, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 179},
                new ExerciseMeasure {Id = 432, ExerciseMeasureTypeId = MeasureType.Weight, ExerciseId = 179},
                new ExerciseMeasure {Id = 433, ExerciseMeasureTypeId = MeasureType.AlternativeWeight, ExerciseId = 179},
            },
            new[]
            {
                new ExerciseMeasure {Id = 434, ExerciseMeasureTypeId = MeasureType.Count, ExerciseId = 180},
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