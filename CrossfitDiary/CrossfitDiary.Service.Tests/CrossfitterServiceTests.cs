using System;
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
        [Test]
        public void FindDefaultOrExistingWorkout_NoneWorkoutToCheck_MethodReturns_0()
        {
            // Arrange
            IRoutineComplexRepository stubRoutineComplexRepository = A.Fake<IRoutineComplexRepository>();
            CrossfitterService crossfitterService = new CrossfitterService(stubRoutineComplexRepository, null, null, null);
            var routineComplexToSave = new RoutineComplex()
            {
                CreatedBy = new ApplicationUser()
            };
            
            // Act
            int actual = crossfitterService.FindDefaultOrExistingWorkout(routineComplexToSave);

            // Assert
            Assert.AreEqual(0, actual);
        }


        [Test]
        public void FindDefaultOrExistingWorkout_WorkoutInRepositoryHas_Different_ComplexType_MethodReturns_0()
        {
            // Arrange
            IRoutineComplexRepository stubRoutineComplexRepository = A.Fake<IRoutineComplexRepository>();

            var routineComplexs = new RoutineComplex[1]{new RoutineComplex(){ ComplexType = RoutineComplexType.E2MOM}};
            A.CallTo(() => stubRoutineComplexRepository.GetMany(A<Expression<Func<RoutineComplex, bool>>>._)).Returns(routineComplexs);
            CrossfitterService crossfitterService = new CrossfitterService(stubRoutineComplexRepository, null, null, null);
            var routineComplexToSave = new RoutineComplex()
            {
                CreatedBy = new ApplicationUser(),
                ComplexType = RoutineComplexType.AMRAP
            };
            
            // Act
            int actual = crossfitterService.FindDefaultOrExistingWorkout(routineComplexToSave);

            // Assert
            Assert.AreEqual(0, actual);
        }
    }
}