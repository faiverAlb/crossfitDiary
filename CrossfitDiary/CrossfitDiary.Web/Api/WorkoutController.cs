using System;
using System.Collections.Generic;
using System.Web.Http;
using CrossfitDiary.Web.ViewModels;
using Newtonsoft.Json;

namespace CrossfitDiary.Web.Api
{

    [Authorize]
    [RoutePrefix("api")]
    public class WorkoutController : ApiController
    {

        /// <summary>
        /// Create workout by viewmodel
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("createWorkout")]
        public void CreateWorkout(WorkoutViewModel model)
        {
            var test = 123;
        }
    }

    
}
