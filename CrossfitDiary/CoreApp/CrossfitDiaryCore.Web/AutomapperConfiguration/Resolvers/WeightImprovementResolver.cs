using AutoMapper;
using CrossfitDiaryCore.Model;
using CrossfitDiaryCore.Web.ViewModels.Pride;

namespace CrossfitDiaryCore.Web.AutomapperConfiguration.Resolvers
{
    internal class WeightImprovementResolver : IValueResolver<PersonMaximum, PersonExerciseMaximumViewModel, string>
    {
        public string Resolve(PersonMaximum source, PersonExerciseMaximumViewModel destination, string destMember,ResolutionContext context)
        {
            string improved = null;

            if (source.MaximumWeight.HasValue && source.PreviousMaximumWeight.HasValue)
            {
                string substraction = (source.MaximumWeight - source.PreviousMaximumWeight).ToCustomString();
                improved = $"+{substraction}kg";
            }

            return improved;
        }
    }
}