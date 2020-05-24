namespace Dogstagram.Features.Dogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dogstagram.Features.Dogs.Models;

    public interface IDogsService
  {
    public Task<int> Create(string imagesUrl, string description, string userId);

    public Task<bool> Update(int id, string description, string userId);

    public Task<bool> Delete(int id, string userId);

    public Task<IEnumerable<DogListingServiceModel>> ByUser(string userId);

    public Task<DogDetailsServiceModel> Details(int id);
  }
}
