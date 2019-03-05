using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        [NotMapped]
        public virtual ICollection<ExerciseMeasure> OrderedExerciseMeasures {
            get
            {
                var returnCollection = new List<ExerciseMeasure>();
                AddInOrder(MeasureType.Count, returnCollection);
                AddInOrder(MeasureType.Weight, returnCollection);
                AddInOrder(MeasureType.AlternativeWeight, returnCollection);
                AddInOrder(MeasureType.Calories, returnCollection);
                AddInOrder(MeasureType.Distance, returnCollection);
                AddInOrder(MeasureType.Height, returnCollection);
                
                return returnCollection;
            }

        }

        private void AddInOrder(MeasureType measure, List<ExerciseMeasure> returnCollection)
        {
            if (ExerciseMeasures.Any(x => x.ExerciseMeasureTypeId == measure))
            {
                var exerciseMeasure = ExerciseMeasures.Single(x => x.ExerciseMeasureTypeId == measure);
                returnCollection.Add(exerciseMeasure);
            }
        }
    }
}