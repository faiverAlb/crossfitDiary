using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.ViewModels;
using CrossfitDiary.Web.ViewModels.Pride;

namespace CrossfitDiary.Web.Api
{
    [Authorize]
    [RoutePrefix("api")]
    public class ExercisesController : BaseApiController
    {
        #region members

        private readonly IExerciseService _exerciseService;

        #endregion

        #region constructors

        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        #endregion

        #region methods

        /// <summary>
        /// The get exercises.
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpGet]
        [Route("getExercises")]
        public IHttpActionResult GetExercises()
        {
            var exercises = _exerciseService.GetExercises();
            return Ok(Mapper.Map<IEnumerable<ExerciseViewModel>>(exercises));
        }


        [HttpGet]
        [Route("getStatisticalExercises")]
        public IHttpActionResult GetStatisticalExercises()
        {
            IEnumerable<Exercise> exercises = _exerciseService.GetStatisticalExercises();
            return Ok(Mapper.Map<IEnumerable<ExerciseViewModel>>(exercises));
        }


        [HttpGet]
        [Route("exercises/{exerciseId}/personMaximum")]
        public IHttpActionResult GetPersonMaximum(int exerciseId)
        {
            var result = new List<PersonExerciseMaximumViewModel>(){new PersonExerciseMaximumViewModel()
            {
                Date = DateTime.Now.ToString("d"),
                MaximumWeight = "124.5",
                PersonName = User.Identity.Name
            }};
            return Ok(result);
//            IEnumerable<Exercise> exercises = _exerciseService.GetStatisticalExercises();
//            return Ok(Mapper.Map<IEnumerable<ExerciseViewModel>>(exercises));
        }

        #endregion
    }
}