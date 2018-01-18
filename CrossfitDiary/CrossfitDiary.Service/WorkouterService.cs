using System;
using CrossfitDiary.Model;

namespace CrossfitDiary.Service
{
    public class WorkouterService
    {
        /// <summary>
        /// The get person maximum for exercise.
        /// </summary>
        /// <param name="exerciseId">
        /// The exercise id.
        /// </param>
        /// <returns>
        /// The <see cref="PersonMaximum"/>.
        /// </returns>
        public PersonMaximum GetPersonMaximumForExercise(int exerciseId)
        {
            return new PersonMaximum()
            {
                Date = DateTime.Now,
                MaximumWeight = exerciseId,
                PersonName = "test"
            };
        }

    }
}