using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CrossfitDiaryCore.Model
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
        public virtual ICollection<RoutineSimple> RoutineSimple { get; set; } = new List<RoutineSimple>();


        /// <summary>
        ///     Time capacity for workout
        /// </summary>
        public TimeSpan? TimeCap { get; set; }


        /// <summary>
        ///     Possible time to work
        /// </summary>
        public TimeSpan? TimeToWork { get; set; }

        /// <summary>
        ///     Title of the Workout (Routing Complex == Workout)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Optional link to parent workout if any
        /// </summary>
        public int? ParentId { get; set; }


        /// <summary>
        ///     Optional parent workout
        /// </summary>
        public virtual RoutineComplex Parent { get; set; }


        /// <summary>
        ///      Children workouts
        /// </summary>
        public virtual ICollection<RoutineComplex> Children { get; set; } = new List<RoutineComplex>();

        [NotMapped]
        public List<RoutineComplex> OrderedChildren
        {
            get
            {
                return Children.OrderBy(x => x.Position).ToList();
            }
        }


        /// <summary>
        ///     Specify order which is important in Children collection
        /// </summary>
        public int Position { get; set; }
        //
        // /// <summary>
        // ///     Possible planning date
        // /// </summary>
        // [NotMapped]
        // public DateTime? PlanDate { get; set; } = DateTime.Now;

        // /// <summary>
        // ///     Possible Planning Level
        // /// </summary>
        // [NotMapped]
        // public PlanningLevel? PlanningLevel { get; set; }


        // /// <summary>
        // ///     Gets or sets Workout Sub type
        // /// </summary>
        // [NotMapped]
        // public WodSubType WodSubType { get; set; }


        /// <summary>
        ///     General comment regarding workout
        /// </summary>
        public string Comment { get; set; }


        [NotMapped]
        public int ResultsCount { get; set; }


        public bool AsNonBreakingSet { get; set; }
    }
}