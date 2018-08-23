using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CrossfitDiaryCore.BL.Services;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels.Pride;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CrossfitDiary.Web.Api
{
    [Authorize]
    [Route("api")]
    public class CrossfitterController : Controller
    {
        private readonly ReadWorkoutsService _readWorkoutsService;

        public CrossfitterController(ReadWorkoutsService readWorkoutsService)
        {
            _readWorkoutsService = readWorkoutsService;
        }

//        [HttpGet]
//        [Route("exercises/{exerciseId}/personMaximum")]
//        public List<PersonExerciseMaximumViewModel> GetPersonMaximum(int exerciseId)
//        {
//            var result = new List<PersonExerciseMaximumViewModel>();
//            PersonMaximum gotMaximum = _readWorkoutsService.GetPersonMaximumForExercise(User.Identity.GetUserId(), exerciseId);
//            if (gotMaximum != null)
//            {
//                result.Add(Mapper.Map<PersonExerciseMaximumViewModel>(gotMaximum));
//            }
//
//            return result;
//        }
//
//
//        [HttpGet]
//        [Route("exercises/{exerciseId}/allPersonsMaximums")]
//        public IEnumerable<PersonExerciseMaximumViewModel> GetAllPersonsMaximums(int exerciseId)
//        {
//            List<PersonMaximum> maximums = _readWorkoutsService.GetAllPersonMaximumForExercise(exerciseId, User.Identity.GetUserId());
//            IEnumerable<PersonExerciseMaximumViewModel> personExerciseMaximumViewModels = maximums.Select(x => Mapper.Map<PersonExerciseMaximumViewModel>(x));
//            return personExerciseMaximumViewModels;
////            return Ok(personExerciseMaximumViewModels);
//        }
//
//        [HttpGet]
//        [Route("getPersonMaximums")]
//        public IEnumerable<PersonExerciseMaximumViewModel> GetPersonMaximums()
//        {
//            List<PersonMaximum> maximums = _readWorkoutsService.GetPersonMaximumsWithAllExercises(User.Identity.GetUserId());
//            IEnumerable<PersonExerciseMaximumViewModel> personExerciseMaximumViewModels = maximums.Select(x => Mapper.Map<PersonExerciseMaximumViewModel>(x));
//            return personExerciseMaximumViewModels;
//        }
    }
}