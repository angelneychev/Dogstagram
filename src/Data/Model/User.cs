using System.Collections.Generic;

namespace Dogstagram.Data.Model
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
      public IEnumerable<Dog> Dogs { get;} = new HashSet<Dog>();
    }
}
