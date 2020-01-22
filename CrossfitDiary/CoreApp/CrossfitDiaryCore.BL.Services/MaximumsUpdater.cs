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

        // private void UpdateRoutinesWithMaximums(List<CrossfitterWorkout> crossfitterWorkouts, List<TempPersonMaximum> personMainMaxumumsOnly, List<TempPersonMaximum> previousMaximumsList)
        // {
        //     foreach (TempPersonMaximum personMaximum in personMainMaxumumsOnly)
        //     {
        //         CrossfitterWorkout crossfitterWorkout = crossfitterWorkouts.SingleOrDefault(x => x.Id == personMaximum.CrossfitterWorkoutId);
        //         if (crossfitterWorkout == null)
        //         {
        //             continue;
        //         }
        //         TempPersonMaximum previousMaximum = previousMaximumsList.FirstOrDefault(x => x.ExerciseId == personMaximum.ExerciseId);
        //
        //         if (previousMaximum == null)
        //         {
        //             personMaximum.AddedToMaxWeight = personMaximum.MaximumWeight;
        //         }
        //         else
        //         {
        //             personMaximum.AddedToMaxWeight = personMaximum.MaximumWeight - previousMaximum.MaximumWeight;
        //         }
        //         crossfitterWorkout.PersonalRecords.Add(personMaximum);
        //     }
        // }


        // private void UpdateWorkoutsWithRecords1(List<CrossfitterWorkout> crossfitterWorkouts)
        // {
        //     List<string> distinctUsers = crossfitterWorkouts
        //         .GroupBy(x => x.Crossfitter.Id).Select(x => x.Key).ToList();
        //     foreach (string userId in distinctUsers)
        //     {
        //         // List<TempPersonMaximum> personMainMaxumumsOnly =  _dapperRepository.GetPersonMainMaxumumsOnly(userId).ToList();
        //         // List<TempPersonMaximum> previousMaximumsList =  _dapperRepository.GetPersonPreviousMainMaxumumsOnly(userId).ToList();
        //
        //         _dapperRepository.GetAllMaximums(userId, DateTime.Now);
        //
        //         UpdateRoutinesWithMaximums(crossfitterWorkouts, personMainMaxumumsOnly, previousMaximumsList);
        //
        //         // List<CrossfitterWorkout> personWods = crossfitterWorkouts.Where(x => x.Crossfitter.Id == userId).ToList();
        //         // UpdateRoutinesWithMaximumsForPercentWeights(personWods, personMainMaxumumsOnly);
        //     }
        // }

        // private void UpdateRoutinesWithMaximumsForPercentWeights(List<CrossfitterWorkout> crossfitterWorkouts, List<TempPersonMaximum> personMainMaxumumsOnly)
        // {
        //     if (crossfitterWorkouts.Count == 0)
        //     {
        //         return;
        //     }
        //
        //     UpdateRoutinesByTotalPreviousMaximum(crossfitterWorkouts);
        //     UpdateRoutinesByLastMaximum(crossfitterWorkouts);
        //
        //
        //
        //     IEnumerable<RoutineSimple> previousPm = crossfitterWorkouts
        //             .SelectMany(x => x.RoutineComplex.RoutineSimple
        //             .Where(y => y.WeightDisplayType == WeightDisplayType.PercentPreviousPM));
        //
        //     foreach (RoutineSimple routineSimple in previousPm)
        //     {
        //         RoutineComplex lastFindMaxWithExercideWod = _context.ComplexRoutines.LastOrDefault(x => x.FindMaxWeight && x.RoutineSimple.Any(y => y.ExerciseId == routineSimple.ExerciseId));
        //         if (lastFindMaxWithExercideWod == null)
        //         {
        //             decimal? foundInMaximums = personMainMaxumumsOnly.SingleOrDefault(x => x.ExerciseId == routineSimple.ExerciseId)?.MaximumWeight;
        //             routineSimple.CalculateWeight(foundInMaximums);
        //             break;
        //         }
        //         decimal? maxWeight = lastFindMaxWithExercideWod.RoutineSimple
        //             .Where(x => x.ExerciseId == routineSimple.ExerciseId)
        //             .Max(x => x.Weight);
        //
        //         routineSimple.CalculateWeight(maxWeight);
        //     }
        //
        // }

        // private void UpdateRoutinesByLastMaximum(List<CrossfitterWorkout> crossfitterWorkouts)
        // {
        //     // throw new NotImplementedException();
        // }

        // private void UpdateRoutinesByTotalPreviousMaximum(List<CrossfitterWorkout> crossfitterWorkouts)
        // {
        //     string userId = crossfitterWorkouts.First().Crossfitter.Id;
        //     IEnumerable<RoutineSimple> percentMaxPm = crossfitterWorkouts
        //         .SelectMany(x => x.RoutineComplex.RoutineSimple
        //             .Where(y => y.WeightDisplayType == WeightDisplayType.PercentMaxPM));
        //
        //     IEnumerable<CrossfitterWorkout> wodsWithFindMaxExercises = crossfitterWorkouts
        //         .Where(x => x.RoutineComplex.RoutineSimple.Any(y => y.WeightDisplayType == WeightDisplayType.PercentMaxPM));
        //
        //     foreach (CrossfitterWorkout wod in wodsWithFindMaxExercises)
        //     {
        //         IEnumerable<TempPersonMaximum> maximumsBeforeDate = _dapperRepository.GetPersonMainMaxumumsOnly(userId, wod.Date);
        //         IEnumerable<RoutineSimple> wodExercises = wod.RoutineComplex.RoutineSimple.Where(y => y.WeightDisplayType == WeightDisplayType.PercentMaxPM);
        //         foreach (RoutineSimple routineSimple in wodExercises)
        //         {
        //             routineSimple.CalculateWeight(maximumsBeforeDate.SingleOrDefault(x => x.ExerciseId == routineSimple.ExerciseId)?.MaximumWeight);
        //         }
        //     }

        //    

        //

        // }
    }
}