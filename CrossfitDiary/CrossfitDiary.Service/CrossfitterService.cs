using System.Collections.Generic;
using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.DAL.EF.Repositories;
using CrossfitDiary.Model;
using CrossfitDiary.Service.Interfaces;

namespace CrossfitDiary.Service
{
    public class CrossfitterService : ICrossfitterService
    {
        private readonly IRoutineComplexRepository _routineComplexRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExerciseRepository _exerciseRepository;

        public CrossfitterService(IRoutineComplexRepository routineComplexRepository, IUnitOfWork unitOfWork, IExerciseRepository exerciseRepository)
        {
            _routineComplexRepository = routineComplexRepository;
            _unitOfWork = unitOfWork;
            _exerciseRepository = exerciseRepository;
        }

        public void CreateWorkout(RoutineComplex routineComplexToSave)
        {
            SetRoutineComplexTitle(routineComplexToSave);

            _routineComplexRepository.Add(routineComplexToSave);
            _unitOfWork.Commit();
        }

        private void SetRoutineComplexTitle(RoutineComplex routineComplexToSave)
        {
            if (!string.IsNullOrEmpty(routineComplexToSave.Title))
                return;

            List<string> exerciseNames = new List<string>();
            foreach (var routineSimple in routineComplexToSave.RoutineSimple)
            {
                Exercise exercise = _exerciseRepository.GetById(routineSimple.ExerciseId);
                exerciseNames.Add(exercise.Title);
            }
            routineComplexToSave.Title = $"{routineComplexToSave.ComplexType}: {string.Join(", ", exerciseNames)}";
        }
    }
}