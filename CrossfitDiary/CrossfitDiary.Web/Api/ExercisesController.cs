using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.ViewModels;

namespace CrossfitDiary.Web.Api
{
    [Authorize]
    [RoutePrefix("api")]
    public class ExercisesController : ApiController
    {
        private readonly IExerciseService _exerciseService;
        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        //TODO: Add pagination
        /// <summary>
        /// Get exercises
        /// </summary>
        /// <returns>All available exercises</returns>
        [HttpGet]
        [Route("getExercises")]
        public IHttpActionResult GetExercises()
        {
            try
            {
                var exercises = _exerciseService.GetExercises();
                return Ok(Mapper.Map<IEnumerable<ExerciseViewModel>>(exercises));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

}
