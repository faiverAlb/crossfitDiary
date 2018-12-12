using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CrossfitDiaryCore.BL.Services.WorkoutMatchers;
using CrossfitDiaryCore.Model;
using FakeItEasy;
using NUnit.Framework;

namespace CrossfitDiaryCore.BL.Services.Tests
{
    [TestFixture]
    public class WorkoutsMatchDispatcherTests
    {

        private RoutineComplex GetRoutineComplex()
        {
            return new RoutineComplex()
            {
                CreatedBy = new ApplicationUser(),
                ComplexType = RoutineComplexType.AMRAP,
                RoundCount = 11,
                TimeToWork = TimeSpan.FromMinutes(60),
                RoutineSimple =
                    new List<RoutineSimple>()
                    {
                        new RoutineSimple() {Calories = 111, Id = 1, ExerciseId = 1},
                        new RoutineSimple() {Count = 222, Id = 2, ExerciseId = 1},
                        new RoutineSimple() {Distance = 333, Id = 3, ExerciseId = 1},
                        new RoutineSimple() {Weight = 444, Count = 555, Id = 4, ExerciseId = 1},
                        new RoutineSimple() {Weight = 444, Count = 555, Id = 5, ExerciseId = 1, IsDoUnbroken = false},
                    },
                Id = 123
            };
        }
        private RoutineComplex GetRoutineComplexWithChilds()
        {
            return new RoutineComplex()
            {
                CreatedBy = new ApplicationUser(),
                ComplexType = RoutineComplexType.ForTimeManyInners,
                RoutineSimple = new List<RoutineSimple>() { },
                Children = new List<RoutineComplex>() { GetRoutineComplex() },
                Id = 123
            };
        }

        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveDifferentChildrensWorkouts_TheyDifferent()
        {
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = GetRoutineComplexWithChilds();
            RoutineComplex secondWorkout = GetRoutineComplexWithChilds();
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);
            Assert.IsFalse(actual);
        }

        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveSameChildrensWorkouts_TheySame()
        {
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = GetRoutineComplexWithChilds();
            RoutineComplex secondWorkout = GetRoutineComplexWithChilds();
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);
            Assert.IsTrue(actual);
        }
    }
}