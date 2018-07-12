using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CrossfitDiaryCore.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{LastName} {FirstName}";


        public virtual ICollection<CrossfitterWorkout> CrossfitterWorkout { get; set; }

        public virtual ICollection<RoutineComplex> RoutineComplexCollection { get; set; }

    }
}