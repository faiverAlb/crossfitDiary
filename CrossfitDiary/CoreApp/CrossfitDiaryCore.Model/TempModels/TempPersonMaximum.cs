namespace CrossfitDiaryCore.Model.TempModels
{
    /// <summary>
    ///     Represents interim class for person maximum weight/alternative weight for an exercise
    /// </summary>
    public class TempPersonMaximum
    {
        public int ExerciseId { get; set; }

        public string ExerciseTitle { get; set; }

        public int RoutineSimpleId { get; set; }

        public int CrossfitterWorkoutId { get; set; }
        
        public decimal? MaximumWeight { get; set; }

        public decimal? MaximumAlternativeWeight { get; set; }

        public decimal? AddedToMaxWeight { get; set; }
    }
}