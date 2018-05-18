using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossfitDiary.Model
{
    /// <summary>
    ///     Сrossfitter workout entity
    /// </summary>
    public class CrossfitterWorkout : BaseModel
    {
        #region constructors

        public CrossfitterWorkout()
        {
            Date = DateTime.Now;
        }

        #endregion

        #region properties

        /// <summary>
        ///     Gets or sets the crossfitter.
        /// </summary>
        public virtual ApplicationUser Crossfitter { get; set; }

        /// <summary>
        ///     Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Gets or sets the distance.
        /// </summary>
        public int? Distance { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is modified.
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is workout is planned.
        ///     TODO: To be removed
        /// </summary>
        public bool IsPlanned { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether workout was done in Rx mode
        /// </summary>
        public bool IsRx { get; set; }

        /// <summary>
        ///     Gets or sets the partial reps finished of not finished round
        /// </summary>
        public int? PartialRepsFinished { get; set; }

        /// <summary>
        ///     Gets or sets the reps to finish on cap time.
        /// </summary>
        public int? RepsToFinishOnCapTime { get; set; }

        /// <summary>
        ///     Gets or sets the rounds finished
        /// </summary>
        public int? RoundsFinished { get; set; }

        /// <summary>
        ///     Gets or sets the routine complex.
        /// </summary>
        public virtual RoutineComplex RoutineComplex { get; set; }

        /// <summary>
        ///     Gets or sets the routine complex id.
        /// </summary>
        public virtual int RoutineComplexId { get; set; }

        /// <summary>
        ///     Gets or sets the time passed.
        /// </summary>
        public TimeSpan? TimePassed { get; set; }


        /// <summary>
        ///     Gets or sets a value indicating whether was finished.
        /// </summary>
        public bool WasFinished { get; set; }

        #endregion
    }
}