namespace CrossfitDiary.Model
{
    public class ExerciseMeasure : BaseModel
    {
        /// <summary>
        /// Exercise Measure type with description
        /// </summary>
        public virtual ExerciseMeasureType ExerciseMeasureType { get; set; }

        /// <summary>
        /// Foreign Key to ExerciseMeasureType
        /// </summary>
        public int ExerciseMeasureTypeId { get; set; }


        /// <summary>
        /// Exercise is a part of the simple routine
        /// </summary>
        public Exercise Exercise { get; set; }

        /// <summary>
        /// Foreign Key to Exercise
        /// </summary>
        public int ExerciseId { get; set; }
    }
}