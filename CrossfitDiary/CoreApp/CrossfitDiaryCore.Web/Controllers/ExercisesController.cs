using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CrossfitDiaryCore.BL.Services;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CrossfitDiaryCore.Web.Controllers
{
    [Authorize]
    public class ExercisesController : Controller
    {
        private readonly ReadExercisesService _readExercisesService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private string _allExercisesCacheConst = "all-exercises-cache";

        #region members


        #endregion

        #region constructors

        public ExercisesController(ReadExercisesService readExercisesService, IMapper mapper, IMemoryCache memoryCache)
        {
            _readExercisesService = readExercisesService;
            _mapper = mapper;
            _memoryCache = memoryCache;
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
            List<ExerciseViewModel> cachedExerciseViewModels = _memoryCache.GetOrCreate(_allExercisesCacheConst, entry =>
            {
                List<Exercise> exercises = _readExercisesService.GetExercises();
                List<ExerciseViewModel> exerciseViewModels = _mapper.Map<List<ExerciseViewModel>>(exercises.OrderBy(x => x.Title));
                return exerciseViewModels;
            });
            return cachedExerciseViewModels;
        }
  
        #endregion
    }
}