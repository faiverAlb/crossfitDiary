using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.DAL.EF.Repositories;
using CrossfitDiary.Model;

namespace CrossfitDiary.Service
{
    public class ManageWorkoutsService
    {
        private readonly ReadWorkoutsService _readWorkoutsService;
        private readonly IRoutineComplexRepository _routineComplexRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrossfitterWorkoutRepository _crossfitterWorkoutRepository;

        public ManageWorkoutsService(ReadWorkoutsService readWorkoutsService, IRoutineComplexRepository routineComplexRepository, ICrossfitterWorkoutRepository crossfitterWorkoutRepository, IUnitOfWork unitOfWork)
        {
            _readWorkoutsService = readWorkoutsService;
            _routineComplexRepository = routineComplexRepository;
            _unitOfWork = unitOfWork;
            _crossfitterWorkoutRepository = crossfitterWorkoutRepository;
        }
        public void CreateAndLogNewWorkout(RoutineComplex newWorkoutRoutine, CrossfitterWorkout logWorkoutModel, bool isEditMode)
        {
            int newWorkoutId = CreateWorkout(newWorkoutRoutine);
            logWorkoutModel.RoutineComplexId = newWorkoutId;
            LogWorkout(logWorkoutModel, isEditMode);
        }


        /// <summary>
        /// Log workout: delete first old if <param name="isEditMode">equals true</param> otherwise just log
        /// </summary>
        /// <param name="workoutToLog">
        /// The workout to log.
        /// </param>
        /// <param name="isEditMode">
        /// The is edit mode.
        /// </param>
        public void LogWorkout(CrossfitterWorkout workoutToLog, bool isEditMode)
        {
            if (isEditMode)
            {
                _crossfitterWorkoutRepository.Delete(x => x.Id == workoutToLog.Id);
                _unitOfWork.Commit();
            }
            _crossfitterWorkoutRepository.AddOrUpdate(workoutToLog);
            _unitOfWork.Commit();
        }


        public int CreateWorkout(RoutineComplex routineComplexToSave)
        {
            int workoutId = _readWorkoutsService.FindDefaultOrExistingWorkout(routineComplexToSave);
            if (workoutId != 0)
            {
                return workoutId;
            }
            _routineComplexRepository.Add(routineComplexToSave);
            _unitOfWork.Commit();
            return routineComplexToSave.Id;
        }

    }
}