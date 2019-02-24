

using System.Collections.Generic;
using System.Linq;
using CrossfitDiaryCore.DAL.EF;
using CrossfitDiaryCore.Model;
using Microsoft.EntityFrameworkCore;

namespace CrossfitDiaryCore.BL.Services
{
    public class ReadExercisesService
    {
        private readonly WorkouterContext _context;

        public ReadExercisesService(WorkouterContext context)
        {
            _context = context;
        }


        public List<Exercise> GetExercises()
        {
            List<Exercise> exercises = _context.Exercises.Include(x => x.ExerciseMeasures).ToList();
            return exercises;
        }
    }
}