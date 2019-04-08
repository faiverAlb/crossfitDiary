using System;
using AutoMapper;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels;

namespace CrossfitDiaryCore.Web.AutomapperConfiguration.Resolvers
{
    public class ResolveCanSeeLeaderboard : IValueResolver<RoutineComplex, WorkoutViewModel, bool>
    {
        public bool Resolve(RoutineComplex source, WorkoutViewModel destination, bool destMember, ResolutionContext context)
        {
            if (source.ComplexType ==  RoutineComplexType.E2MOM || source.ComplexType == RoutineComplexType.EMOM || source.ComplexType == RoutineComplexType.NotForTime)
            {
                return false;
            }

            if (source.ResultsCount < 2)
            {
                return false;
            }

            return true;
        }
    }
}