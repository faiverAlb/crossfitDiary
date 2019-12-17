using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CrossfitDiaryCore.Model
{
    public class PlanningHistory
    {
        public virtual int Id { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        ///     WOD level to do
        /// </summary>
        public PlanningLevel PlanningLevel { get; set; }


        /// <summary>
        ///     Gets or sets the routine complex.
        /// </summary>
        public virtual RoutineComplex RoutineComplex { get; set; }

        /// <summary>
        ///     Gets or sets the routine complex id.
        /// </summary>
        public virtual int RoutineComplexId { get; set; }

        /// <summary>
        ///     Date when WOD should be done
        /// </summary>
        public DateTime PlanningDate { get; set; }

        /// <summary>
        ///     Gets or sets the crossfitter.
        /// </summary>
        public virtual ApplicationUser Crossfitter { get; set; }

        /// <summary>
        ///     Gets or sets Workout Sub type
        /// </summary>
        public WodSubType WodSubType { get; set; } = WodSubType.Wod;

    }
}
