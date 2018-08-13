//using AutoMapper;
//using CrossfitDiaryCore.Model;
//using CrossfitDiaryCore.Web.ViewModels;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNetCore.Http;
//
//
//namespace CrossfitDiary.Web.Mappings
//{
//    internal class CanBeRemovedResolver : IValueResolver<CrossfitterWorkout, ToLogWorkoutViewModel, bool>
//    {
//        public bool Resolve(CrossfitterWorkout source, ToLogWorkoutViewModel destination, bool destMember, ResolutionContext context)
//        {
//            string currentUserId = HttpContext.User.Identity.GetUserId();
//            string actualId = source.Crossfitter.Id;
//
//            return currentUserId == actualId;
//        }
//    }
//}