using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossfitDiaryCore.Model
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
        public virtual Exercise Exercise { get; set; }

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
        public decimal? Count { get; set; }

        /// <summary>
        /// Possible distance (meters) to pass
        /// </summary>
        public decimal? Distance { get; set; }

        /// <summary>
        /// Possible weight in kgs
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// Calories to make
        /// </summary>
        public decimal? Calories { get; set; }

        /// <summary>
        /// Centimeters to jump
        /// </summary>
        public decimal? Centimeters { get; set; }

        /// <summary>
        /// Specify does this exercise is alternative exercise (primarily used in EMOM/E2MOM)
        /// </summary>
        public bool IsAlternative { get; set; }


        /// <summary>
        ///     Calculated property from service to identify as new maximum
        /// </summary>
        [NotMapped]
        public bool IsNewWeightMaximum { get; set; }
        
        /// <summary>
        ///     Substraction between maximum and previous maximum weights
        /// </summary>
        [NotMapped]
        public decimal? AddedToMaxWeight { get; set; }

        /// <summary>
        ///     Should be done without break
        /// </summary>
        public bool IsDoUnbroken { get; set; }

    }
}