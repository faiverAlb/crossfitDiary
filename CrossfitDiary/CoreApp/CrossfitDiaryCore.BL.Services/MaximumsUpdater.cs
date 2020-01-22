using System;
using System.Collections.Generic;
using System.Linq;
using CrossfitDiaryCore.BL.Services.DapperStuff;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Model.TempModels;

namespace CrossfitDiaryCore.BL.Services
{
    public class MaximumsUpdater
    {
        private readonly DapperRepository _dapperRepository;

        public MaximumsUpdater(DapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public void UpdateWorkoutsWithRecords(List<CrossfitterWorkout> crossfitterWorkouts)
        {
            List<string> distinctUsers = crossfitterWorkouts.GroupBy(x => x.Crossfitter.Id).Select(x => x.Key).ToList();
            foreach (string userId in distinctUsers)
            {
                IEnumerable<TempPersonMaximum> allExercisesMaximums = _dapperRepository.GetAllMaximums(userId, DateTime.Now);
                IEnumerable<IGrouping<int, TempPersonMaximum>> groupedByExercises = allExercisesMaximums.GroupBy(x => x.ExerciseId);
                foreach (IGrouping<int, TempPersonMaximum> exerciseMaxGroup in groupedByExercises)
                {
                    IOrderedEnumerable<TempPersonMaximum> orderedByWeight = exerciseMaxGroup.OrderByDescending(x => x.CalculatedMaximumWeight).ThenBy(x => x.Date);
                    TempPersonMaximum firstMax = orderedByWeight.FirstOrDefault();
                    CrossfitterWorkout wodWithNewMax = crossfitterWorkouts.SingleOrDefault(x => x.Id == firstMax.CrossfitterWorkoutId);
                    if (wodWithNewMax == null)
                    {
                        continue;
                    }

                    TempPersonMaximum previousMaxValue = orderedByWeight.FirstOrDefault(x => x.CalculatedMaximumWeight != firstMax.CalculatedMaximumWeight);
                    firstMax.CalculatedAddedWeight(previousMaxValue);
                    wodWithNewMax.AddToPersonRecord(firstMax);
                }

            }
        }

        public void UpdateWorkoutsWithPercentWeightCalculations(List<CrossfitterWorkout> crossfitterWorkouts)
        {
            var wodsWithCalculationNeeded = crossfitterWorkouts.Where(x => x.RoutineComplex.RoutineSimple.Any(y => y.WeightDisplayType != WeightDisplayType.Default)).ToList();
            List<string> distinctUsers = wodsWithCalculationNeeded.GroupBy(x => x.Crossfitter.Id).Select(x => x.Key).ToList();
            foreach (string userId in distinctUsers)
            {
                IEnumerable<TempPersonMaximum> allExercisesMaximums = _dapperRepository.GetAllMaximums(userId, DateTime.Now);
                List<CrossfitterWorkout> personWods = wodsWithCalculationNeeded.Where(x => x.Crossfitter.Id == userId).ToList();
                IEnumerable<CrossfitterWorkout> wodsWithFindMaxPM =  personWods.Where(x => x.RoutineComplex.RoutineSimple.Any(y => y.WeightDisplayType ==WeightDisplayType.PercentMaxPM));
                foreach (CrossfitterWorkout workout in wodsWithFindMaxPM)
                {
                    IEnumerable<RoutineSimple> exercisesWithPercentMaxPM = workout.RoutineComplex.RoutineSimple.Where(x => x.WeightDisplayType == WeightDisplayType.PercentMaxPM);
                    foreach (RoutineSimple routineSimple in exercisesWithPercentMaxPM)
                    {

                        decimal? calculatedMaximumWeight = GetMaxValue(allExercisesMaximums, routineSimple.ExerciseId, workout.Date);
                        routineSimple.CalculateWeight(calculatedMaximumWeight);
                    }
                }

                IEnumerable<CrossfitterWorkout> wodsWithFindPrevPM = personWods.Where(x => x.RoutineComplex.RoutineSimple.Any(y => y.WeightDisplayType == WeightDisplayType.PercentPreviousPM));
                foreach (CrossfitterWorkout workout in wodsWithFindPrevPM)
                {
                    IEnumerable<RoutineSimple> exercisesWithPercentPreviousPM = workout.RoutineComplex.RoutineSimple.Where(x => x.WeightDisplayType == WeightDisplayType.PercentPreviousPM);

                    foreach (RoutineSimple routineSimple in exercisesWithPercentPreviousPM)
                    {
                        CrossfitterWorkout wod = _dapperRepository.GetCrossfiterWodWithFindMaxWeight(userId, workout.Date, routineSimple.ExerciseId);

                        if (wod == null)
                        {
                            decimal? calculatedMaximumWeight = GetMaxValue(allExercisesMaximums, routineSimple.ExerciseId, workout.Date);
                            routineSimple.CalculateWeight(calculatedMaximumWeight);
                        }
                        else
                        {
                            routineSimple.CalculateWeight(wod.Weight);
                        }

                    }
                }
            }

        }

        private decimal? GetMaxValue(IEnumerable<TempPersonMaximum> allExercisesMaximums, int exerciseId,
            DateTime workoutDate)
        {
            IEnumerable<TempPersonMaximum> foundAllExerciseMaximums = allExercisesMaximums
                .Where(x => x.ExerciseId == exerciseId && x.Date <= workoutDate)
                .OrderByDescending(x => x.CalculatedMaximumWeight)
                .ThenBy(x => x.Date);
            return foundAllExerciseMaximums.FirstOrDefault()?.CalculatedMaximumWeight;
        }
    }
}