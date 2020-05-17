using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dogstagram.Data;
using Dogstagram.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Dogstagram.Features.Dogs
{
  public class DogsService : IDogsService
  {
    private readonly DogstagramDbContext data;

    public DogsService(DogstagramDbContext data) => this.data = data;

    public async Task<int> Create(string imagesUrl, string description, string userId)
    {
      var dog = new Dog()
      {
        ImagesUrl = imagesUrl,
        Description = description,
        UserId = userId,
      };

      this.data.Add(dog);

      return dog.Id;
    }

    public async Task<IEnumerable<DogListingResponseModel>> ByUser(string userId)
      => await this.data
        .Dogs
        .Where(d => d.UserId == userId)
        .Select(
          d => new DogListingResponseModel
          {
            Id = d.Id,
            ImagesUrl = d.ImagesUrl
          })
        .ToListAsync();
  }
}
