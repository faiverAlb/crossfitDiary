using System;
using System.Collections.Generic;

namespace CrossfitDiary.Model
{
    /// <summary>
    ///     High level container for <see cref="RoutineSimple" />
    /// </summary>
    /// <example>
    ///     Wod: Cindy.
    ///     5 pull-ups, 10 push-ups, 15 air squats.
    ///     The entire description - is Complext routine
    /// </example>
    public class RoutineComplex : BaseModel
    {
        #region properties

        /// <summary>
        ///     Gets or sets the complex type.
        /// </summary>
        public RoutineComplexType ComplexType { get; set; }


        /// <summary>
        ///     Gets or sets the created by.
        /// </summary>
        public virtual ApplicationUser CreatedBy { get; set; }

        /// <summary>
        ///     Time to rest between exercises
        /// </summary>
        public TimeSpan? RestBetweenExercises { get; set; }


        /// <summary>
        ///     Time to rest between rounds
        /// </summary>
        public TimeSpan? RestBetweenRounds { get; set; }

        /// <summary>
        ///     Possible rounds to do
        /// </summary>
        public int? RoundCount { get; set; }

        /// <summary>
        ///     List of Simple Routines.
        /// </summary>
        public virtual ICollection<RoutineSimple> RoutineSimple { get; set; }


        /// <summary>
        ///     Possible time to work
        /// </summary>
        public TimeSpan? TimeToWork { get; set; }

        /// <summary>
        ///     Title of the Workout (Routing Complex == Workout)
        /// </summary>
        public string Title { get; set; }

        #endregion
    }
}