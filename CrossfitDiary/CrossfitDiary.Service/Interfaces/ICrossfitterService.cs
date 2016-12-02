using CrossfitDiary.Model;

namespace CrossfitDiary.Service.Interfaces
{
    public interface ICrossfitterService
    {
        void CreateWorkout(RoutineComplex map);
        void LogWorkout(CrossfitterWorkout map);
    }
}