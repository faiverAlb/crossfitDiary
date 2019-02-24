using System.Collections.Generic;

namespace CrossfitDiaryCore.Model
{
    public class Exercise : BaseModel
    {
        /// <summary>
        /// Title of the exercise
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Abbreviation of the exercise
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// List of Exercise Measures 
        /// </summary>
        public virtual ICollection<ExerciseMeasure> ExerciseMeasures { get; set; }
    }
}