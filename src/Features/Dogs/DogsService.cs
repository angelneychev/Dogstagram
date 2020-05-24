namespace Dogstagram.Features.Dogs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dogstagram.Data;
    using Dogstagram.Data.Model;
    using Dogstagram.Features.Dogs.Models;
    using Microsoft.EntityFrameworkCore;

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

      public async Task<bool> Update(int id, string description, string userId)
      {
        var dog = await this.GetByIdAndUserId(id, userId);

        if (dog == null)
        {
          return false;
        }

        dog.Description = description;

        await this.data.SaveChangesAsync();

        return true;
      }

      public async Task<bool> Delete(int id, string userId)
      {
        var dog = await this.GetByIdAndUserId(id, userId);

        if (dog == null)
        {
          return false;
        }

        this.data.Dogs.Remove(dog);

        await this.data.SaveChangesAsync();

        return true;
      }

      public async Task<IEnumerable<DogListingServiceModel>> ByUser(string userId)
      => await this.data
        .Dogs
        .Where(d => d.UserId == userId)
        .Select(
          d => new DogListingServiceModel
          {
            Id = d.Id,
            ImagesUrl = d.ImagesUrl,
          })
        .ToListAsync();

      public async Task<DogDetailsServiceModel> Details(int id)
      => await this.data
        .Dogs
        .Where(d => d.Id == id)
        .Select(d => new DogDetailsServiceModel
        {
          Id = d.Id,
          ImagesUrl = d.ImagesUrl,
          Description = d.Description,
          UserId = d.UserId,
          UserName = d.User.UserName,
        })
        .FirstOrDefaultAsync();

      private async Task<Dog> GetByIdAndUserId(int id, string userId)
      => await this.data
        .Dogs
        .Where(d => d.Id == id && d.UserId == userId)
        .FirstOrDefaultAsync();
  }
}
