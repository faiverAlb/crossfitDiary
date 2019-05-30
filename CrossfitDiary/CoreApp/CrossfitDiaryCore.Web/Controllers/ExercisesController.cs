using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using CrossfitDiaryCore.BL.Services;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Model.TempModels;
using CrossfitDiaryCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ReadWorkoutsService _readWorkoutsService;

        public ExercisesController(ReadExercisesService readExercisesService
            , IMapper mapper
            , IMemoryCache memoryCache
            , IHttpContextAccessor httpContextAccessor
            , ReadWorkoutsService readWorkoutsService)
        {
            _readExercisesService = readExercisesService;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
            _readWorkoutsService = readWorkoutsService;
        }

        /// <summary>
        ///     The get exercises.
        /// </summary>
        [HttpGet]
        [Route("api/getExercises")]
        public List<ExerciseViewModel> GetExercises()
        {
            List<Exercise> cachedExercises = _memoryCache.GetOrCreate(_allExercisesCacheConst, entry =>
            {
                List<Exercise> exercises = _readExercisesService.GetExercises();
                return exercises;
            });

            List<ExerciseViewModel> exerciseViewModels = _mapper.Map<List<ExerciseViewModel>>(cachedExercises.OrderBy(x => x.Title));
            return exerciseViewModels;
        }
        
        /// <summary>
        ///     The get exercises maximums.
        /// </summary>
        [HttpGet]
        [Route("api/getExerciseMaximums")]
        public List<PersonMaximumViewModel> GetExerciseMaximums()
        {

            string currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<TempPersonMaximum> personMaxumumsByExercise = _readWorkoutsService.GetPersonMaxumums(currentUserId);
            return _mapper.Map<List<PersonMaximumViewModel>>(personMaxumumsByExercise);

        }
        /// <summary>
        ///     The get exercises maximums.
        /// </summary>
        [HttpGet]
        [Route("api/getWeightsMaximums")]
        public List<PersonMaximumViewModel> GetWeightsMaximums()
        {
            string currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<TempPersonMaximum> personMaxumumsByExercise = _readWorkoutsService.GetPersonMaxumumsOneWeight(currentUserId);
            return _mapper.Map<List<PersonMaximumViewModel>>(personMaxumumsByExercise);

        }
    }
}