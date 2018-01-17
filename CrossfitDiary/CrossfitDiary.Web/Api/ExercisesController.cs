using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.ViewModels;

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

        #endregion
    }
}