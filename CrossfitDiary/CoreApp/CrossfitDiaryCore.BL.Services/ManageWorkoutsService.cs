//using CrossfitDiary.DAL.EF.Infrastructure;
//using CrossfitDiary.DAL.EF.Repositories;
//using CrossfitDiary.Model;

using System.Collections.Generic;
using System.Linq;
using CrossfitDiaryCore.DAL.EF;
using CrossfitDiaryCore.Model;

namespace CrossfitDiaryCore.BL.Services
{
    public class ManageWorkoutsService
    {
        private readonly WorkouterContext _context;

        public ManageWorkoutsService(WorkouterContext context)
        {
            _context = context;
        }

        public void RemoveWorkout(int crossfitterWorkoutId, string userId)
        {
            CrossfitterWorkout toRemove = _context.CrossfitterWorkouts.Single(x => x.Id == crossfitterWorkoutId && x.Crossfitter.Id == userId);
            _context.CrossfitterWorkouts.Remove(toRemove);
            _context.SaveChanges();
        }
    }
}