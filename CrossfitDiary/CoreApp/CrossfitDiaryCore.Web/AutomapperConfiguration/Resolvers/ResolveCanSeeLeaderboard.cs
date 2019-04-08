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
            return false;
        }
    }
}