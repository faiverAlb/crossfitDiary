using System.Security.Claims;
using AutoMapper;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;

namespace CrossfitDiaryCore.Web.AutomapperConfiguration.Resolvers
{
    internal class CanBeRemovedResolver : IValueResolver<CrossfitterWorkout, ToLogWorkoutViewModel, bool>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CanBeRemovedResolver(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public bool Resolve(CrossfitterWorkout source, ToLogWorkoutViewModel destination, bool destMember, ResolutionContext context)
        {
            string currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string workoutUserId = source.Crossfitter.Id;
            return currentUserId == workoutUserId;
        }
    }
}