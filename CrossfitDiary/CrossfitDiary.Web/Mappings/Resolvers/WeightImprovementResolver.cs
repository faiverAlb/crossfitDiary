using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Web.ViewModels.Pride;

namespace CrossfitDiary.Web.Mappings.Resolvers
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