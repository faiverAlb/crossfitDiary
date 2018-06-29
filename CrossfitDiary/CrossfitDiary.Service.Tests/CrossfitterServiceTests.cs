using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CrossfitDiary.DAL.EF.Repositories;
using CrossfitDiary.Model;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Service.WorkoutMatchers;
using FakeItEasy;
using NUnit.Framework;

namespace CrossfitDiary.Service.Tests
{
    [TestFixture]
    public class CrossfitterServiceTests
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
                        new RoutineSimple() {Weight = 444, Count = 555, Id = 4, ExerciseId = 1}
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
                RoutineSimple =new List<RoutineSimple>(){},
                Children = new List<RoutineComplex>() { GetRoutineComplex()},
                Id = 123
            };
        }

        private WorkoutsMatchDispatcher GetWorkoutsMatchDispatcher()
        {
            return new WorkoutsMatchDispatcher(new List<IWorkoutMatcher>
            {
                new ComplexParametersMatcher(),
                new ChildrenWorkoutsMatcher(),
                new ExerciseMatcher()
            });
        }



        private IRoutineComplexRepository GetConfiguredRoutineComplexRepository(ICollection<RoutineComplex> toBeReturned)
        {
            IRoutineComplexRepository stubRoutineComplexRepository = A.Fake<IRoutineComplexRepository>();
            A.CallTo(() => stubRoutineComplexRepository.GetMany(A<Expression<Func<RoutineComplex, bool>>>._)).Returns(toBeReturned);
            A.CallTo(() => stubRoutineComplexRepository.GetAll()).Returns(toBeReturned);
            return stubRoutineComplexRepository;
        }


        [Test]
        public void FindDefaultOrExistingWorkout_NoneWorkoutToCheck_MethodReturns_0()
        {
            // Arrange
            
            // Act
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new RoutineComplex[0]), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }



        [Test]
        public void FindDefaultOrExistingWorkout_WorkoutInRepositoryHas_Different_ComplexType_MethodReturns_0()
        {
            // Arrange
            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.ComplexType++;
            
            // Act
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_WorkoutInRepositoryHas_Different_RoundsCount_MethodReturns_0()
        {
            // Arrange
            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.RoundCount++;
            
            // Act
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_WorkoutInRepositoryHas_Different_TimeToWork_MethodReturns_0()
        {
            // Arrange
            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.TimeToWork = routineComplexToSave.TimeToWork.Value.Add(TimeSpan.FromSeconds(1));
            
            // Act
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_ExercisesHasDifferentCount_MethodReturns_0()
        {
            // Arrange
            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.RoutineSimple.Clear();
            
            // Act
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_ExercisesHasDifferentOrder_MethodReturns_0()
        {
            // Arrange
            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.RoutineSimple = GetRoutineComplex().RoutineSimple.OrderByDescending(x => x.Id).ToList();
            
            // Act
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }


        [Test]
        public void FindDefaultOrExistingWorkout_ExercisesHasDifferentCaloriesValue_MethodReturns_0()
        {
            // Arrange
            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.RoutineSimple.ElementAt(0).Calories++;
            
            // Act
            CrossfitterService crossfitterService = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null, GetWorkoutsMatchDispatcher());
            int actual = crossfitterService.FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }


        [Test]
        public void FindDefaultOrExistingWorkout_ExercisesHasDifferentCountValue_MethodReturns_0()
        {
            // Arrange
            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.RoutineSimple.ElementAt(1).Count++;
            
            // Act
            CrossfitterService crossfitterService = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null, GetWorkoutsMatchDispatcher());
            int actual = crossfitterService.FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_ExercisesHasDifferentDistanceValue_MethodReturns_0()
        {
            // Arrange
            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.RoutineSimple.ElementAt(2).Distance++;
            
            // Act
            CrossfitterService crossfitterService = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null, GetWorkoutsMatchDispatcher());
            int actual = crossfitterService.FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }


        [Test]
        public void FindDefaultOrExistingWorkout_ExercisesHasDifferentWeightValueSameCount_MethodReturns_0()
        {
            // Arrange
            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.RoutineSimple.ElementAt(3).Weight++;
            
            // Act
            CrossfitterService crossfitterService = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null, GetWorkoutsMatchDispatcher());
            int actual = crossfitterService.FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_ExercisesHasDifferentCountValueSameWeight_MethodReturns_0()
        {
            // Arrange
            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.RoutineSimple.ElementAt(3).Count++;
            
            // Act
            CrossfitterService crossfitterService = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null, GetWorkoutsMatchDispatcher());
            int actual = crossfitterService.FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_ExercisesDifferentWithSameMeasures_MethodReturns_0()
        {
            // Arrange
            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.RoutineSimple.ElementAt(0).ExerciseId++;
            
            // Act
            CrossfitterService crossfitterService = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null, GetWorkoutsMatchDispatcher());
            int actual = crossfitterService.FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_RepositoryContainsSameRoutine_MethodReturnsNonZeroId()
        {
            // Arrange

            // Act
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { GetRoutineComplex() }), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.NotZero(actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_RepositoryContainsRoutineWithSameChildCount_MethodReturnsNonZeroId()
        {
            // Arrange

            // Act
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { GetRoutineComplexWithChilds() }), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplexWithChilds());

            // Assert
            Assert.NotZero(actual);
        }
        [Test]
        public void FindDefaultOrExistingWorkout_RepositoryContainsRoutineWithDifferentChildCount_MethodReturnsZeroId()
        {
            // Arrange
            RoutineComplex routineComplexWithChilds = GetRoutineComplexWithChilds();
            routineComplexWithChilds.Children.Add(GetRoutineComplex());
            // Act

            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexWithChilds }), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplexWithChilds());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_RepositoryContainsChildrentWithDifferentExercisesCount_MethodReturnsZeroId()
        {
            // Arrange
            RoutineComplex routineComplexWithChilds = GetRoutineComplexWithChilds();
            routineComplexWithChilds.Children.ElementAt(0).RoutineSimple.Add(new RoutineSimple() { Weight = 0, Count = 1, Id = 5, ExerciseId = 1 });
            // Act

            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexWithChilds }), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplexWithChilds());

            // Assert
            Assert.AreEqual(0, actual);
        }
        [Test]
        public void FindDefaultOrExistingWorkout_RepositoryContainsChildrentWithDifferentExercisesCalories_MethodReturnsZeroId()
        {
            // Arrange
            RoutineComplex routineComplexWithChilds = GetRoutineComplexWithChilds();
            routineComplexWithChilds.Children.ElementAt(0).RoutineSimple.ElementAt(0).Calories++;
            // Act

            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexWithChilds }), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplexWithChilds());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_RepositoryContainsChildrentWithDifferentType_MethodReturnsZeroId()
        {
            // Arrange
            RoutineComplex routineComplexWithChilds = GetRoutineComplexWithChilds();
            routineComplexWithChilds.Children.ElementAt(0).ComplexType = RoutineComplexType.ForTime;
            // Act

            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexWithChilds }), null, null, null, GetWorkoutsMatchDispatcher()).FindDefaultOrExistingWorkout(GetRoutineComplexWithChilds());

            // Assert
            Assert.AreEqual(0, actual);
        }


        [Test]
        public void MarkWorkoutWithWeightRecord_PersonMaximumIsNull_NRE_NOT_Called()
        {
            //  Arrange
            var crossfitterService = new CrossfitterService(null, null, null, null, GetWorkoutsMatchDispatcher());
            PersonMaximum nullPersonMaximum = null;
            
            //  Act
            //  Assert
            Assert.DoesNotThrow(() =>
            {
                crossfitterService.MarkWorkoutWithWeightRecord(nullPersonMaximum, new List<CrossfitterWorkout>() { new CrossfitterWorkout() });
            });

        }


        [Test]
        public void MarkWorkoutWithWeightRecord_HasPersonMaximumWitnNullWeight_NoWorkoutExercisesMarked()
        {
            //  Arrange
            PersonMaximum personMaximumWithNullWeight = new PersonMaximum()
            {
                MaximumWeight = null,
                CrossfitWorkoutId = 1,
                ExerciseId = GetRoutineComplex().RoutineSimple.First().ExerciseId
            };
            var crossfitterService = new CrossfitterService(null, null, null, null, GetWorkoutsMatchDispatcher());

            //  Act
            var crossfitterWorkouts = new List<CrossfitterWorkout>() {new CrossfitterWorkout() {Id = 1, RoutineComplex = GetRoutineComplex()} };
            crossfitterService.MarkWorkoutWithWeightRecord(personMaximumWithNullWeight,crossfitterWorkouts);

            //  Assert
            Assert.That(crossfitterWorkouts, Has.Exactly(0).Matches<CrossfitterWorkout>(x => x.RoutineComplex.RoutineSimple.Count(y => y.IsNewWeightMaximum) != 0));
        }


        [Test]
        public void MarkWorkoutWithWeightRecord_HasPersonMaximumWithZeroMaximum_NoWorkoutExercisesMarked()
        {
            //  Arrange
            PersonMaximum personMaximumWithNullWeight = new PersonMaximum()
            {
                MaximumWeight = 0,
                CrossfitWorkoutId = 1,
                ExerciseId = GetRoutineComplex().RoutineSimple.First().ExerciseId
            };
            var crossfitterService = new CrossfitterService(null, null, null, null, GetWorkoutsMatchDispatcher());

            //  Act
            var crossfitterWorkouts = new List<CrossfitterWorkout>() {new CrossfitterWorkout() {Id = 1, RoutineComplex = GetRoutineComplex()} };
            crossfitterService.MarkWorkoutWithWeightRecord(personMaximumWithNullWeight,crossfitterWorkouts);

            //  Assert
            Assert.That(crossfitterWorkouts, Has.Exactly(0).Matches<CrossfitterWorkout>(x => x.RoutineComplex.RoutineSimple.Count(y => y.IsNewWeightMaximum) != 0));
        }


        [Test]
        public void MarkWorkoutWithWeightRecord_PersonMaximumWorkoutNotInList_NRE_NotRaised()
        {
            //  Arrange
            PersonMaximum personMaximumNotInList = new PersonMaximum()
            {
                MaximumWeight = 100,
                CrossfitWorkoutId = 0
            };
            var crossfitterService = new CrossfitterService(null, null, null, null, GetWorkoutsMatchDispatcher());

            //  Act

            //  Assert
            Assert.DoesNotThrow(() =>
            {
                crossfitterService.MarkWorkoutWithWeightRecord(personMaximumNotInList, new List<CrossfitterWorkout>() { new CrossfitterWorkout() { Id = 1 } });
            });

        }


        [Test]
        public void MarkWorkoutWithWeightRecord_HasExistingPersonMaximum_WorkoutInnerExerciseMarked()
        {
            //  Arrange
            RoutineComplex routineComplex = GetRoutineComplex();
            routineComplex.RoutineSimple.First().Weight = 100;

            PersonMaximum personMaximumWithNullWeight = new PersonMaximum()
            {
                MaximumWeight = 100,
                CrossfitWorkoutId = 1,
                ExerciseId = routineComplex.RoutineSimple.First().ExerciseId
            };
            var crossfitterService = new CrossfitterService(null, null, null, null, GetWorkoutsMatchDispatcher());

            //  Act
            var crossfitterWorkouts = new List<CrossfitterWorkout>() { new CrossfitterWorkout() { Id = 1, RoutineComplex = routineComplex } };
            crossfitterService.MarkWorkoutWithWeightRecord(personMaximumWithNullWeight, crossfitterWorkouts);

            //  Assert
            Assert.That(crossfitterWorkouts, Has.One.Matches<CrossfitterWorkout>(x => x.RoutineComplex.RoutineSimple.Count(y => y.IsNewWeightMaximum) != 0));
        }



    }
}