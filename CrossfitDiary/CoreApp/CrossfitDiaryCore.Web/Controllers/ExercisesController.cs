using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CrossfitDiaryCore.BL.Services;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitDiaryCore.Web.Controllers
{
    [Authorize]
    public class ExercisesController : Controller
    {
        private readonly ReadExercisesService _readExercisesService;
        private readonly IMapper _mapper;

        #region members


        #endregion

        #region constructors

        public ExercisesController(ReadExercisesService readExercisesService, IMapper mapper)
        {
            _readExercisesService = readExercisesService;
            _mapper = mapper;
        }

        #endregion

        #region methods

        /// <summary>
        ///     The get exercises.
        /// </summary>
        [HttpGet]
        [Route("api/getExercises")]
        public List<ExerciseViewModel> GetExercises()
        {
            List<Exercise> exercises = _readExercisesService.GetExercises();
            List<ExerciseViewModel> exerciseViewModels = _mapper.Map<List<ExerciseViewModel>>(exercises.OrderBy(x => x.Title));
            return exerciseViewModels;
        }
  
        #endregion
    }
}