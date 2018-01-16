using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Web.ViewModels;
using Microsoft.AspNet.Identity;

namespace CrossfitDiary.Web.Mappings
{
    internal class CanBeRemovedResolver : IValueResolver<CrossfitterWorkout, ToLogWorkoutViewModel, bool>
    {
        public bool Resolve(CrossfitterWorkout source, ToLogWorkoutViewModel destination, bool destMember, ResolutionContext context)
        {
            string currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            string actualId = source.Crossfitter.Id;

            return currentUserId == actualId;
        }
    }
}