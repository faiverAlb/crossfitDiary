using System;

namespace CrossfitDiary.Model
{
    public class PersonMaximum
    {
        #region properties

        /// <summary>
        ///     Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Gets or sets the exercise abbreviation.
        /// </summary>
        public string ExerciseDisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the workout exercise Id.
        /// </summary>
        public int ExerciseId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is it me.
        /// </summary>
        public bool IsItMe { get; set; }

        /// <summary>
        ///     Gets or sets the maximum weight.
        /// </summary>
        public decimal? MaximumWeight { get; set; }

        /// <summary>
        ///     Previous maximum weight
        /// </summary>
        public decimal? PreviousMaximumWeight { get; set; }

        /// <summary>
        ///     Gets or sets the person id.
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        ///     Gets or sets the person name.
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        ///     Gets or sets the position between others.
        /// </summary>
        public int PositionBetweenOthers { get; set; }

        /// <summary>
        ///     Gets or sets the workout title for found maximum in exercise
        /// </summary>
        public string WorkoutTitle { get; set; }

        /// <summary>
        /// Gets or sets the workout id for found maximum in exercise
        /// </summary>
        public int CrossfitWorkoutId { get; set; }

        #endregion
    }
}