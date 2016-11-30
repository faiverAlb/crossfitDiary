using System.Collections.Generic;

namespace CrossfitDiary.Model
{
    public class Crossfitter: ApplicationUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public virtual ICollection<CrossfitterWorkout> CrossfitterWorkout { get; set; }
    }
}