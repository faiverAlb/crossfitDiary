

using System.Collections.Generic;
using System.Linq;
using CrossfitDiaryCore.DAL.EF;
using CrossfitDiaryCore.Model;

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
            return _context.Exercises.ToList();
        }
    }
}