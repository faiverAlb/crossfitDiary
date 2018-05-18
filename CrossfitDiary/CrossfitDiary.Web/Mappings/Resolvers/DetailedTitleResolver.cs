using System.Linq;
using AutoMapper;
using CrossfitDiary.Model;
using CrossfitDiary.Web.ViewModels;

namespace CrossfitDiary.Web.Mappings.Resolvers
{
    internal class DetailedTitleResolver : IValueResolver<RoutineComplex, WorkoutViewModel, string>
    {
        public string Resolve(RoutineComplex source, WorkoutViewModel destination, string destMember, ResolutionContext context)
        {
            bool containDefaultName = ContainDefaultName(source.Title);
            string mainTitle = containDefaultName ? source.ComplexType.ToString() : source.Title;
            string lastTitlePart = string.Empty;
            if (source.RoutineSimple.Count > 3)
            {
                lastTitlePart = $"{BuildRoutineTitle(source.RoutineSimple.ElementAt(0))}, {BuildRoutineTitle(source.RoutineSimple.ElementAt(1))}, {BuildRoutineTitle(source.RoutineSimple.ElementAt(2))} and {source.RoutineSimple.Count - 3} more...";
            }
            else
            {
                for (int i = 0; i < source.RoutineSimple.Count; i++)
                {
                    lastTitlePart += $"{BuildRoutineTitle(source.RoutineSimple.ElementAt(i))}";
                    if (i + 1  < source.RoutineSimple.Count)
                    {
                        lastTitlePart += ", ";
                    }
                }
            }

            return $"{mainTitle}: {lastTitlePart}";
        }


        /// <summary>
        /// Build routine simple detailed title
        /// </summary>
        /// <param name="routineSimple">
        /// The routine simple.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string BuildRoutineTitle(RoutineSimple routineSimple)
        {
            if (routineSimple.Weight.HasValue && routineSimple.Weight.Value > 0)
            {
                if (routineSimple.Count.HasValue)
                {
                    return $"{routineSimple.Exercise.Title}({routineSimple.Count.ToCustomString()} * {routineSimple.Weight.ToCustomString()} kg)";
                }

                return $"{routineSimple.Exercise.Title}({routineSimple.Weight.ToCustomString()} kg)";
            }

            if (routineSimple.Count.HasValue)
            {
                return $"{routineSimple.Exercise.Title}({routineSimple.Count.ToCustomString()})";
            }

            if (routineSimple.Calories.HasValue)
            {
                return $"{routineSimple.Exercise.Title}({routineSimple.Calories.ToCustomString()} cal)";
            }

            if (routineSimple.Distance.HasValue)
            {
                return $"{routineSimple.Exercise.Title}({routineSimple.Distance.ToCustomString()} meters)";
            }

            if (routineSimple.Centimeters.HasValue)
            {
                return $"{routineSimple.Exercise.Title}({routineSimple.Centimeters.ToCustomString()} cm)";
            }

            return "-";
        }


        /// <summary>
        /// Returns does workout title contain default type name
        /// </summary>
        /// <param name="workoutTitle">
        /// The source title.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ContainDefaultName(string workoutTitle)
        {
            return workoutTitle.Contains(RoutineComplexType.AMRAP.ToString())
                   || workoutTitle.Contains(RoutineComplexType.E2MOM.ToString())
                   || workoutTitle.Contains(RoutineComplexType.EMOM.ToString())
                   || workoutTitle.Contains(RoutineComplexType.ForTime.ToString())
                   || workoutTitle.Contains(RoutineComplexType.NotForTime.ToString());
        }
    }
}