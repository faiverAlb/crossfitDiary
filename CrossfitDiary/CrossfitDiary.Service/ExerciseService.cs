using System.Collections.Generic;
using System.Linq;
using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.DAL.EF.Repositories;
using CrossfitDiary.Model;
using CrossfitDiary.Service.Interfaces;

namespace CrossfitDiary.Service
{
    public class ExerciseService: IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ExerciseService(IExerciseRepository exerciseRepository, IUnitOfWork unitOfWork)
        {
            _exerciseRepository = exerciseRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Exercise> GetExercises(string title = null)
        {
            var exercises = string.IsNullOrEmpty(title) ? _exerciseRepository.GetAll() : _exerciseRepository.GetMany(x => x.Title == title);
            return exercises.OrderBy(x => x.Title);
        }

        public Exercise GetExercise(int id)
        {
            return _exerciseRepository.GetById(id);
        }

        public void CreateExercise(Exercise exercise)
        {
            _exerciseRepository.Add(exercise);
        }

        public void SaveExercise()
        {
            _unitOfWork.Commit();
        }
    }
}
