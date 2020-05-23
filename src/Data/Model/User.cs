namespace Dogstagram.Data.Model
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
      public IEnumerable<Dog> Dogs { get; } = new HashSet<Dog>();
    }
}
