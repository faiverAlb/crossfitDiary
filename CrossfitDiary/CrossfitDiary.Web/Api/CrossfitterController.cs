using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Service;
using CrossfitDiary.Web.ViewModels.Pride;
using Microsoft.AspNet.Identity;

namespace CrossfitDiary.Web.Api
{
    [Authorize]
    [RoutePrefix("api")]
    public class CrossfitterController : BaseApiController
    {
        private readonly ReadWorkoutsService _readWorkoutsService;

        public CrossfitterController(ReadWorkoutsService readWorkoutsService)
        {
            _readWorkoutsService = readWorkoutsService;
        }

        [HttpGet]
        [Route("exercises/{exerciseId}/personMaximum")]
        public IHttpActionResult GetPersonMaximum(int exerciseId)
        {
            var result = new List<PersonExerciseMaximumViewModel>();
            PersonMaximum gotMaximum = _readWorkoutsService.GetPersonMaximumForExercise(User.Identity.GetUserId(), exerciseId);
            if (gotMaximum != null)
            {
                result.Add(Mapper.Map<PersonExerciseMaximumViewModel>(gotMaximum));
            }
            return Ok(result);
        }


        [HttpGet]
        [Route("exercises/{exerciseId}/allPersonsMaximums")]
        public IHttpActionResult GetAllPersonsMaximums(int exerciseId)
        {
            List<PersonMaximum> maximums = _readWorkoutsService.GetAllPersonMaximumForExercise(exerciseId, User.Identity.GetUserId());
            return Ok(maximums.Select(x => Mapper.Map<PersonExerciseMaximumViewModel>(x)));
        }

        [HttpGet]
        [Route("getPersonMaximums")]
        public IHttpActionResult GetPersonMaximums()
        {
            List<PersonMaximum> maximums = _readWorkoutsService.GetPersonMaximumsWithAllExercises(User.Identity.GetUserId());
            return Ok(maximums.Select(x => Mapper.Map<PersonExerciseMaximumViewModel>(x)));
        }
    }
}