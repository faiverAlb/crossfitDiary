using System;
using System.Collections.Generic;
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
                RoutineSimple = new List<RoutineSimple>(),
                Id = 123
            };
        }

        [Test]
        public void FindDefaultOrExistingWorkout_NoneWorkoutToCheck_MethodReturns_0()
        {
            // Arrange
            IRoutineComplexRepository stubRoutineComplexRepository = A.Fake<IRoutineComplexRepository>();
            CrossfitterService crossfitterService =
                new CrossfitterService(stubRoutineComplexRepository, null, null, null);
            A.CallTo(() => stubRoutineComplexRepository.GetMany(A<Expression<Func<RoutineComplex, bool>>>._))
                .Returns(new RoutineComplex[0]);

            // Act
            int actual = crossfitterService.FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }


        [Test]
        public void FindDefaultOrExistingWorkout_WorkoutInRepositoryHas_Different_ComplexType_MethodReturns_0()
        {
            // Arrange
            IRoutineComplexRepository stubRoutineComplexRepository = A.Fake<IRoutineComplexRepository>();

            RoutineComplex routineComplexToSave = GetRoutineComplex();
            routineComplexToSave.ComplexType++;
            A.CallTo(() => stubRoutineComplexRepository.GetMany(A<Expression<Func<RoutineComplex, bool>>>._)).Returns(new[] { routineComplexToSave });
            
            // Act
            CrossfitterService crossfitterService = new CrossfitterService(stubRoutineComplexRepository, null, null, null);
            int actual = crossfitterService.FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void FindDefaultOrExistingWorkout_RepositoryContainsSameRoutine_MethodReturnsNonZeroId()
        {
            // Arrange
            IRoutineComplexRepository stubRoutineComplexRepository = A.Fake<IRoutineComplexRepository>();
            A.CallTo(() => stubRoutineComplexRepository.GetMany(A<Expression<Func<RoutineComplex, bool>>>._)).Returns(new[] { GetRoutineComplex() });

            // Act
            CrossfitterService crossfitterService = new CrossfitterService(stubRoutineComplexRepository, null, null, null);
            int actual = crossfitterService.FindDefaultOrExistingWorkout(GetRoutineComplex());

            // Assert
            Assert.NotZero(actual);
        }
    }
}