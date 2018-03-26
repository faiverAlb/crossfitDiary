using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CrossfitDiary.DAL.EF.Repositories;
using CrossfitDiary.Model;
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
                        new RoutineSimple() {Calories = 111, Id = 1},
                        new RoutineSimple() {Count = 222, Id = 2},
                        new RoutineSimple() {Distance = 333, Id = 3},
                        new RoutineSimple() {Weight = 444, Count = 555, Id = 4},
                    },
                Id = 123
            };
        }

        private IRoutineComplexRepository GetConfiguredRoutineComplexRepository(ICollection<RoutineComplex> toBeReturned)
        {
            IRoutineComplexRepository stubRoutineComplexRepository = A.Fake<IRoutineComplexRepository>();
            A.CallTo(() => stubRoutineComplexRepository.GetMany(A<Expression<Func<RoutineComplex, bool>>>._)).Returns(toBeReturned);
            return stubRoutineComplexRepository;
        }


        [Test]
        public void FindDefaultOrExistingWorkout_NoneWorkoutToCheck_MethodReturns_0()
        {
            // Arrange
            
            // Act
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new RoutineComplex[0]), null, null, null).FindDefaultOrExistingWorkout(GetRoutineComplex());

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
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null).FindDefaultOrExistingWorkout(GetRoutineComplex());

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
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null).FindDefaultOrExistingWorkout(GetRoutineComplex());

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
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null).FindDefaultOrExistingWorkout(GetRoutineComplex());

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
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null).FindDefaultOrExistingWorkout(GetRoutineComplex());

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
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null).FindDefaultOrExistingWorkout(GetRoutineComplex());

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
            CrossfitterService crossfitterService = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null);
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
            CrossfitterService crossfitterService = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null);
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
            CrossfitterService crossfitterService = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null);
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
            CrossfitterService crossfitterService = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null);
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
            CrossfitterService crossfitterService = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { routineComplexToSave }), null, null, null);
            int actual = crossfitterService.FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_RepositoryContainsSameRoutine_MethodReturnsNonZeroId()
        {
            // Arrange

            // Act
            int actual = new CrossfitterService(GetConfiguredRoutineComplexRepository(new[] { GetRoutineComplex() }), null, null, null).FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.NotZero(actual);
        }
    }
}