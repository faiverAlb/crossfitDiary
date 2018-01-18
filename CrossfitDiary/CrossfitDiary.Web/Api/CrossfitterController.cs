using System;
using System.Collections.Generic;
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
        private readonly CrossfitterService _crossfitterService;

        public CrossfitterController(CrossfitterService crossfitterService)
        {
            _crossfitterService = crossfitterService;
        }

        [HttpGet]
        [Route("exercises/{exerciseId}/personMaximum")]
        public IHttpActionResult GetPersonMaximum(int exerciseId)
        {
            var result = new List<PersonExerciseMaximumViewModel>();
            PersonMaximum gotMaximum = _crossfitterService.GetPersonMaximumForExercise(User.Identity.GetUserId(), exerciseId);
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
            var result = new List<PersonExerciseMaximumViewModel>()
            {
                new PersonExerciseMaximumViewModel()
                {
                    Date = DateTime.Now.ToString("d"),
                    MaximumWeight = "124.5",
                    PersonName = User.Identity.Name
                },
                new PersonExerciseMaximumViewModel()
                {
                    Date = DateTime.Now.AddDays(-2).ToString("d"),
                    MaximumWeight = "100",
                    PersonName = User.Identity.Name
                }
            };
            return Ok(result);
        }
    }
}