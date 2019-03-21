namespace CrossfitDiaryCore.Model.TempModels
{
    /// <summary>
    ///     Represents interim class for person maximum weight/alternative weight for an exercise
    /// </summary>
    public class TempPersonMaximum
    {
        public int ExerciseId { get; set; }

        public int? RoutineSimpleId { get; set; }
        
        public decimal? MaximumWeight { get; set; }

        public decimal? MaximumAlternativeWeight { get; set; }
    }
}