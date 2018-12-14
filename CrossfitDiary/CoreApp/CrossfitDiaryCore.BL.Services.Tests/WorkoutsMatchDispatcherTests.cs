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
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = GetRoutineComplexWithChilds();
            RoutineComplex secondWorkout = GetRoutineComplexWithChilds();
            secondWorkout.Children.Add(GetRoutineComplex());

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveSameChildrensWorkouts_TheySame()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = GetRoutineComplexWithChilds();
            RoutineComplex secondWorkout = GetRoutineComplexWithChilds();

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);
            
            //  Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveSameParameters_TheySame()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = GetRoutineComplex();
            RoutineComplex secondWorkout = GetRoutineComplex();
            
            //  Arrange
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);
            
            //  Arrange
            Assert.IsTrue(actual);
        }

        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveDifferent_ComlexType_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex()
            {
                ComplexType = RoutineComplexType.AMRAP,
            };
            RoutineComplex secondWorkout = new RoutineComplex()
            {
                ComplexType = RoutineComplexType.E2MOM,
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveDifferent_RestBetweenExercises_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RestBetweenExercises = TimeSpan.Zero
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RestBetweenExercises = TimeSpan.Zero.Add(new TimeSpan(1))
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveDifferent_RestBetweenRounds_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RestBetweenRounds = TimeSpan.Zero
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RestBetweenRounds = TimeSpan.Zero.Add(new TimeSpan(1))
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveDifferent_RoundCount_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoundCount = 1
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoundCount = 2
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveDifferent_TimeCap_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                TimeCap = TimeSpan.Zero
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                TimeCap = TimeSpan.Zero.Add(new TimeSpan(1))
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveDifferent_TimeToWork_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                TimeToWork = TimeSpan.Zero
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                TimeToWork = TimeSpan.Zero.Add(new TimeSpan(1))
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveSame_ExercisesCount_TheySame()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){new RoutineSimple()},
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){ new RoutineSimple() },
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void WorkoutsMatchDispatcher_WorkoutsHaveDifferent_ExercisesCount_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){new RoutineSimple()},
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){ new RoutineSimple(), new RoutineSimple() },
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void WorkoutsMatchDispatcher_ExerciseInWorkoutHaveDifferent_TimeToWork_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){new RoutineSimple(){TimeToWork = TimeSpan.Zero}},
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){ new RoutineSimple(){ TimeToWork = TimeSpan.Zero.Add(new TimeSpan(1)) } },
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void WorkoutsMatchDispatcher_ExerciseInWorkoutHaveDifferent_Calories_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){new RoutineSimple(){Calories = 1}},
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){ new RoutineSimple(){ Calories = 2 } },
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void WorkoutsMatchDispatcher_ExerciseInWorkoutHaveDifferent_Centimeters_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){new RoutineSimple(){Centimeters = 1}},
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){ new RoutineSimple(){ Centimeters = 2 } },
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void WorkoutsMatchDispatcher_ExerciseInWorkoutHaveDifferent_Count_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){new RoutineSimple(){Count = 1}},
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){ new RoutineSimple(){ Count = 2 } },
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void WorkoutsMatchDispatcher_ExerciseInWorkoutHaveDifferent_Distance_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){new RoutineSimple(){Distance = 1}},
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){ new RoutineSimple(){ Distance = 2 } },
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void WorkoutsMatchDispatcher_ExerciseInWorkoutHaveDifferent_Exercise_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){new RoutineSimple(){ExerciseId = 1}},
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){ new RoutineSimple(){ ExerciseId = 2 } },
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void WorkoutsMatchDispatcher_ExerciseInWorkoutHaveDifferent_IsAlternative_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){new RoutineSimple(){IsAlternative = true}},
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){ new RoutineSimple(){ IsAlternative = false } },
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void WorkoutsMatchDispatcher_ExerciseInWorkoutHaveDifferent_IsDoUnbroken_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){new RoutineSimple(){ IsDoUnbroken = true}},
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){ new RoutineSimple(){ IsDoUnbroken = false } },
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void WorkoutsMatchDispatcher_ExerciseInWorkoutHaveDifferent_Weight_TheyDifferent()
        {
            //  Arrange
            WorkoutsMatchDispatcher workoutsMatchDispatcher = new WorkoutsMatchDispatcher();
            RoutineComplex firstWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){new RoutineSimple(){ Weight = 1}},
            };
            RoutineComplex secondWorkout = new RoutineComplex
            {
                RoutineSimple = new List<RoutineSimple>(){ new RoutineSimple(){ Weight = 2 } },
            };

            //  Act
            bool actual = workoutsMatchDispatcher.IsWorkoutsMatch(firstWorkout, secondWorkout);

            //  Assert
            Assert.IsFalse(actual);
        }



    }
}