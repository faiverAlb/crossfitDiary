﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CrossfitDiaryCore.Model.TempModels;

namespace CrossfitDiaryCore.Model
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


        /// <summary>
        ///     Comment regarding workout from crossfitter
        /// </summary>
        public string Comment { get; set; }


        /// <summary>
        ///     Gets or sets Workout Sub type
        /// </summary>
        public WodSubType WodSubType { get; set; }


        /// <summary>
        ///     
        /// </summary>
        [NotMapped]
        public List<TempPersonMaximum> PersonalRecords { get; set; } = new List<TempPersonMaximum>();

        /// <summary>
        ///     Possible max weight done during the workouts
        /// </summary>
        public decimal? Weight { get; set; }

        public void AddToPersonRecord(TempPersonMaximum newMax)
        {
            PersonalRecords.Add(newMax);
        }
    }
}