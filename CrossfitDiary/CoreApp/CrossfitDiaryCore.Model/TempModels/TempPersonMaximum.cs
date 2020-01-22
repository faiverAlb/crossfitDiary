using System;

namespace CrossfitDiaryCore.Model.TempModels
{
    /// <summary>
    ///     Represents interim class for person maximum weight/alternative weight for an exercise
    /// </summary>
    public class TempPersonMaximum
    {
        public int ExerciseId { get; set; }

        public int RoutineSimpleId { get; set; }

        public int CrossfitterWorkoutId { get; set; }
        public int RoutineComplexId { get; set; }
        
        public decimal? RoutineSimpleWeight { get; set; }

        public decimal? CrossfitWodWeight { get; set; }

        public decimal? AddedToMaxWeight { get; set; }

        public DateTime Date { get; set; }

        public decimal CalculatedMaximumWeight
        {
            get { return Math.Max(CrossfitWodWeight ?? 0, RoutineSimpleWeight ?? 0); }
        }

        public void CalculatedAddedWeight(TempPersonMaximum previousMaxValue)
        {
            if (previousMaxValue == null)
            {
                AddedToMaxWeight = CalculatedMaximumWeight;
            }
            else
            {
                AddedToMaxWeight = CalculatedMaximumWeight - previousMaxValue.CalculatedMaximumWeight;
            }
        }
    }
}