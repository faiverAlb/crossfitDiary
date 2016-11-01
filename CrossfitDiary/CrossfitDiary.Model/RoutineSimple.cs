using System;

namespace CrossfitDiary.Model
{
    /// <summary>
    /// Part of <see cref="RoutineComplex"/>
    /// </summary>
    /// <example>Wod: Cindy. 
    /// 5 pull-ups, 10 push-ups, 15 air squats.
    /// "5 pull-ups" - Simple Routine
    /// </example>
    public class RoutineSimple : BaseModel
    {

        /// <summary>
        /// Foreign Key to Exercise
        /// </summary>
        public int ExerciseId { get; set; }
        /// <summary>
        /// Exercise is a part of the simple routine
        /// </summary>
        public Exercise Exercise { get; set; }

        /// <summary>
        /// Foreign Key to Routine Complex
        /// </summary>
        public int RoutineComplexId { get; set; }
        
        /// <summary>
        /// Link to Routine Complex
        /// </summary>
        public RoutineComplex RoutineComplex { get; set; }

        /// <summary>
        /// Possible time to work of this routine
        /// </summary>
        public TimeSpan? TimeToWork { get; set; }

        /// <summary>
        /// Possible count to do for this routine
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// Possible distance (meters) to pass
        /// </summary>
        public int? Distance { get; set; }

        /// <summary>
        /// Possible weight in lbs
        /// </summary>
        public int? Weight { get; set; }
    }
}